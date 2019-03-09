using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using MES.Model;
using System.Drawing;

namespace MES.Utils
{
    class MatrixBuilder
    {
        private readonly IntegrationPoints _points;
        private readonly Deviratives _deviratives;
        private readonly Mesh _mesh;
        private Matrix<double> _jacobianMatrix;
        private readonly SimulationData _simulationData;

        public MatrixBuilder(Mesh mesh, SimulationData simulationData)
        {
            _points = new IntegrationPoints();
            _deviratives = new Deviratives(_points);
            _mesh = mesh;
            _simulationData = simulationData;
            buildJacobianMatrix();
            initializeLocalMatrixes();
        }

        private void buildJacobianMatrix()
        {
            Element el = _mesh.Elements[0];
            double[] X = new double[4]
            {       _mesh.Nodes[el.IDS[0]].X,
                    _mesh.Nodes[el.IDS[1]].X,
                    _mesh.Nodes[el.IDS[2]].X,
                    _mesh.Nodes[el.IDS[3]].X
            };
            double[] Y = new double[4]
            {       _mesh.Nodes[el.IDS[0]].Y,
                    _mesh.Nodes[el.IDS[1]].Y,
                    _mesh.Nodes[el.IDS[2]].Y,
                    _mesh.Nodes[el.IDS[3]].Y
            };
            double[] matrix = new double[4]; // macierz jakobianu

            for (int nr_funkcji_ksztaltu = 0; nr_funkcji_ksztaltu < 4; nr_funkcji_ksztaltu++)
            {
                matrix[0] += _deviratives.Npoksi[nr_funkcji_ksztaltu, 0] * X[nr_funkcji_ksztaltu]; // x po ksi
                matrix[1] += _deviratives.Npoeta[nr_funkcji_ksztaltu, 0] * X[nr_funkcji_ksztaltu]; // x po eta
                matrix[2] += _deviratives.Npoksi[nr_funkcji_ksztaltu, 0] * Y[nr_funkcji_ksztaltu]; // y po ksi
                matrix[3] += _deviratives.Npoeta[nr_funkcji_ksztaltu, 0] * Y[nr_funkcji_ksztaltu]; // y po eta
            }



            Matrix<double> c = DenseMatrix.OfArray(new double[,]
            {
                    {matrix[0], matrix[2] },
                    {matrix[1], matrix[3] }
            });


            _jacobianMatrix = c;
        }

        private double[,] createPlainHglobal()
        {
            int nodesCount = _mesh.Nodes.Count;
            double[,] h = new double[nodesCount, nodesCount];
            for (int i = 0; i < nodesCount; i++)
                for (int j = 0; j < nodesCount; j++)
                    h[i, j] = 0.0;

            return h;
        }

        public Matrix<double> buildHglobalMatrix()
        {
            //foreach (Element el in _mesh.Elements)
            //{
            //    createHlocalForElement(el);
            //}

            double[,] HGlobalMatrix = createPlainHglobal();

            for (int k = 0; k < _mesh.Elements.Count; k++)
                for (int i = 0; i < 4; i++)
                    for (int j = 0; j < 4; j++)
                        HGlobalMatrix[_mesh.Elements[k].IDS[i], _mesh.Elements[k].IDS[j]] += _mesh.Elements[k].H[i, j];

            return DenseMatrix.OfArray(HGlobalMatrix);
        }

        public Vector<double> buildPglobalVector()
        {
            //foreach (Element el in _mesh.Elements)
            //{
            //    createPlocalForElement(el);
            //}

            double[] PglobalVector = ZeroesPglobal();

            for (int k = 0; k < _mesh.Elements.Count; k++)
                for (int i = 0; i < 4; i++)
                    PglobalVector[_mesh.Elements[k].IDS[i]] += _mesh.Elements[k].P[i];

            return DenseVector.OfArray(PglobalVector);
        }

        public Matrix<double> buildCglobalMatrix()
        {
            //foreach (Element el in _mesh.Elements)
            //{
            //    createClocalForElement(el);
            //}

            double[,] CglobalMatrix = new double[_mesh.Nodes.Count, _mesh.Nodes.Count];
            for (int k = 0; k < _mesh.Elements.Count; k++)
                for (int i = 0; i < 4; i++)
                    for (int j = 0; j < 4; j++)
                        CglobalMatrix[_mesh.Elements[k].IDS[i], _mesh.Elements[k].IDS[j]] += _mesh.Elements[k].C[i, j];

            return DenseMatrix.OfArray(CglobalMatrix);
        }

        private double [] ZeroesPglobal()
        {
            double[] p = new double[_mesh.Nodes.Count];

            for (int i = 0; i < p.Length; i++)
            {
                p[i] = 0;
            }

            return p;
        }

        private void createHlocalForElement(Element el)
        {

            double[] X = new double[4]
            {       _mesh.Nodes[el.IDS[0]].X,
                     _mesh.Nodes[el.IDS[1]].X,
                     _mesh.Nodes[el.IDS[2]].X,
                     _mesh.Nodes[el.IDS[3]].X
            };
            double[] Y = new double[4]
            {        _mesh.Nodes[el.IDS[0]].Y,
                     _mesh.Nodes[el.IDS[1]].Y,
                     _mesh.Nodes[el.IDS[2]].Y,
                     _mesh.Nodes[el.IDS[3]].Y
            };

            double[,] Npox = new double[4, 4]; // pochodne funkcji krztałtu po x
            double[,] Npoy = new double[4, 4]; // pochodne funkcji krztałtu po y

            for (int i = 0; i < 4; i++) // po punktach całkowania
            {
                for (int j = 0; j < 4; j++) // petla przez funkcje krztałtu j w punkcie całkowania i
                {
                    Vector<double> v = DenseVector.OfArray(new double[]
                    {
                       _deviratives.Npoksi[j,i] ,_deviratives.Npoeta[j,i]
                    }); // wektor pochodnej funkcji krztałtu j  w punkcie całkowania i po ksi i eta
                    double[] tmp = _jacobianMatrix.Inverse().Multiply(v).ToArray();

                    Npox[j, i] = tmp[0];
                    Npoy[j, i] = tmp[1];
                }
            }

            Matrix<double> Hlocal = DenseMatrix.Create(4, 4, 0);
            Matrix<double> NpoX;
            Matrix<double> NpoY;
            Matrix<double> VxT;
            Matrix<double> VyT;

            for (int i = 0; i < 4; i++) // wybieramy wektory NpoX i NpoY w kazdym punkcie całkowania
            {
                NpoX = DenseMatrix.OfArray(Npox).Column(i).ToRowMatrix();
                NpoY = DenseMatrix.OfArray(Npoy).Column(i).ToRowMatrix();

                VxT = NpoX.Transpose();
                VyT = NpoY.Transpose();

                Matrix<double> m = (VxT.Multiply(NpoX).Add(VyT.Multiply(NpoY))); // sumujemy macierze Npox i Npoy
                Hlocal.Add(m, Hlocal); // sumujemy macierze wynikowe w kazdym punkcie całkowania

            }

            el.H = Hlocal.Multiply(_simulationData.Conductivity).Multiply(_jacobianMatrix.Determinant()).ToArray(); // mnozymy przez wyznacznik jakobianu i przewodniość
        }

        private void createPlocalForElement(Element el)
        {
            double[,] punktycalkowania = new double[2, 2];
            double[,] funkcja_ksztaltu_w_punkcie = new double[4, 2];
            double jakobian1D = 0;
            Vector<double> P = DenseVector.Create(4, 0.0);
            Matrix<double> druga_calka_do_Hloc = DenseMatrix.Create(4, 4, 0);
            for (int i = 0; i < el.Pow.Count; i++) // petla po krawedziach elementu
            {

                if (el.Pow[i] == false)
                {

                    switch (i) // zaleznie od krawedzi to pakuje do tablicy takie punkty i taki jakobian
                    {
                        case 0:
                            punktycalkowania[0, 0] = -1;
                            punktycalkowania[0, 1] = -0.5773;
                            punktycalkowania[1, 0] = -1;
                            punktycalkowania[1, 1] = 0.5773;
                            jakobian1D = (_mesh.Nodes[el.IDS[3]].Y - _mesh.Nodes[el.IDS[0]].Y) * 0.5;
                            break;
                        case 1:
                            punktycalkowania[0, 0] = -0.5773;
                            punktycalkowania[0, 1] = -1;
                            punktycalkowania[1, 0] = 0.5773;
                            punktycalkowania[1, 1] = -1;
                            jakobian1D = (_mesh.Nodes[el.IDS[1]].X - _mesh.Nodes[el.IDS[0]].X) * 0.5;
                            break;
                        case 2:
                            punktycalkowania[0, 0] = 1;
                            punktycalkowania[0, 1] = -0.5773;
                            punktycalkowania[1, 0] = 1;
                            punktycalkowania[1, 1] = 0.5773;
                            jakobian1D = (_mesh.Nodes[el.IDS[2]].Y - _mesh.Nodes[el.IDS[1]].Y) * 0.5;
                            break;
                        case 3:
                            punktycalkowania[0, 0] = 0.5773;
                            punktycalkowania[0, 1] = 1;
                            punktycalkowania[1, 0] = -0.5773;
                            punktycalkowania[1, 1] = 1;
                            jakobian1D = (_mesh.Nodes[el.IDS[2]].X - _mesh.Nodes[el.IDS[3]].X) * 0.5;
                            break;
                    }


                    funkcja_ksztaltu_w_punkcie[0, 0] = 0.25 * ((1 - punktycalkowania[0, 0]) * (1 - punktycalkowania[0, 1])); // N1 w 1 punkcie całkowania
                    funkcja_ksztaltu_w_punkcie[0, 1] = 0.25 * ((1 - punktycalkowania[1, 0]) * (1 - punktycalkowania[1, 1])); // N1 w 2 -/-

                    funkcja_ksztaltu_w_punkcie[1, 0] = 0.25 * ((1 + punktycalkowania[0, 0]) * (1 - punktycalkowania[0, 1])); // N2 w 1 punkcie całkowania
                    funkcja_ksztaltu_w_punkcie[1, 1] = 0.25 * ((1 + punktycalkowania[1, 0]) * (1 - punktycalkowania[1, 1])); // N2 w 2 -/-

                    funkcja_ksztaltu_w_punkcie[2, 0] = 0.25 * ((1 + punktycalkowania[0, 0]) * (1 + punktycalkowania[0, 1])); // N3 w 1 punkcie całkowania
                    funkcja_ksztaltu_w_punkcie[2, 1] = 0.25 * ((1 + punktycalkowania[1, 0]) * (1 + punktycalkowania[1, 1])); // N3 w 2 -/-

                    funkcja_ksztaltu_w_punkcie[3, 0] = 0.25 * ((1 - punktycalkowania[0, 0]) * (1 + punktycalkowania[0, 1])); // N4 w 1 punkcie całkowania
                    funkcja_ksztaltu_w_punkcie[3, 1] = 0.25 * ((1 - punktycalkowania[1, 0]) * (1 + punktycalkowania[1, 1])); // N4 w 2 -/-


                    Vector<double> suma = DenseVector.OfArray(new double[] // tu sumuje funkcje kształtu w 2 punktach całkowania
                    {
                        funkcja_ksztaltu_w_punkcie[0,0] + funkcja_ksztaltu_w_punkcie[0,1],
                        funkcja_ksztaltu_w_punkcie[1,0] + funkcja_ksztaltu_w_punkcie[1,1],
                        funkcja_ksztaltu_w_punkcie[2,0] + funkcja_ksztaltu_w_punkcie[2,1],
                        funkcja_ksztaltu_w_punkcie[3,0] + funkcja_ksztaltu_w_punkcie[3,1]
                    });


                    P.Add(suma.Multiply(jakobian1D), P); // tu sie dodaje każde P z krawędzi pomnożone przez jakobian do Plocal elementu

                    // druga calka do Hloc
                    Matrix<double> m = DenseMatrix.Create(4, 4, 0);
                    for (int j = 0; j < 2; j++) // calka na krawedzi
                    {
                        Matrix<double> N = DenseVector.OfArray(new double[] {
                        funkcja_ksztaltu_w_punkcie[0,j],
                        funkcja_ksztaltu_w_punkcie[1,j],
                        funkcja_ksztaltu_w_punkcie[2,j],
                        funkcja_ksztaltu_w_punkcie[3,j]
                    }).ToRowMatrix();

                        m = m.Add(N.Transpose().Multiply(N));
                    }

                    druga_calka_do_Hloc = druga_calka_do_Hloc.Add(m.Multiply(jakobian1D));
                }

            } // koniec petli

            el.P = P.Multiply(_simulationData.Convection_Factor).Multiply(_simulationData.Ambient_Temperature).ToArray();
            el.H = DenseMatrix.OfArray(el.H).Add(druga_calka_do_Hloc.Multiply(_simulationData.Convection_Factor)).ToArray();


        }

        private void createClocalForElement(Element el)
        {
            Vector<double> funkcje_ksztaltu = DenseVector.Create(4, 0.0);
            Matrix<double> tmp;
            double[] matrix = new double[4];
            double[] X = new double[4]
            {       _mesh.Nodes[el.IDS[0]].X,
                    _mesh.Nodes[el.IDS[1]].X,
                     _mesh.Nodes[el.IDS[2]].X,
                     _mesh.Nodes[el.IDS[3]].X
            };
            double[] Y = new double[4]
            {       _mesh.Nodes[el.IDS[0]].Y,
                    _mesh.Nodes[el.IDS[1]].Y,
                     _mesh.Nodes[el.IDS[2]].Y,
                    _mesh.Nodes[el.IDS[3]].Y
            };

            Matrix<double> c = DenseMatrix.Create(4, 4, 0);
            foreach (PointF p in _points.Points)
            {

                tmp = DenseVector.OfArray(new double[]
                {

                    0.25 * ((1 - p.X) * (1 - p.Y)), // N1 w punkcie p 
                    0.25 * ((1 + p.X) * (1 - p.Y)), // N2 w punkcie p
                    0.25 * ((1 + p.X) * (1 + p.Y)), // N3 w punkcie P
                    0.25 * ((1 - p.X) * (1 + p.Y)) // N4 w punkcie p
                }).ToRowMatrix();

                // funkcje_ksztaltu.Add(tmp,funkcje_ksztaltu); 
                // sumujemy poszczególne funkcje kształtu we wszystkich punktach

                Matrix<double> m = tmp.Transpose();
                c = c.Add(m.Multiply(tmp));
            }

            //Matrix<double> m = funkcje_ksztaltu.ToRowMatrix();
            c = c.Multiply(_jacobianMatrix.Determinant());
            //Matrix<double> m1 = m.Transpose().Multiply(m);
            el.C = c.Multiply(_simulationData.Density * _simulationData.Specific_Heat).ToArray();
            //el.C = c.ToArray(); ;
        }

        private void initializeLocalMatrixes()
        {
            foreach(Element el in _mesh.Elements)
            {
                createHlocalForElement(el);
                createPlocalForElement(el);
                createClocalForElement(el);
            }
        }
    }
}
