
using System.Collections.Generic;
using System.Drawing;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using System.Collections.ObjectModel;

namespace MES
{
    class Mesh
    {
        private int NumberOfNodes;
        private int NodesInLengthDim;
        private int NodesInHeightDim;
        private double dx;
        private double dy;
        List<Node> nodes;
        List<Element> elements;
        public double[,] Hglobal;
        private double[] Pglobal;
        private double[,] Cglobal;
        private PointF[] punktycalkowania = new PointF[4]; // punkty calkowania dla H
        private double[,] Npoksi = new double[4, 4]; // pochodne funkcji krztałtu po ksi w 4 punktach całkowania
        private double[,] Npoeta = new double[4, 4]; // pochodne funkcji krztałtu po eta w 4 punktach całkowania
        private Matrix<double> JacobianMatrix;


        private double Initial_Temperature;
        private double Simulation_Time;
        private double Simulation_Step_Time;
        private double Ambient_Temperature;
        private double Convection_Factor;
        private double Specific_Heat;
        private double Conductivity;
        private double Density;




        public Mesh(double length, double height, int NodesInLengthDim, int NodesInHeightDim)
        {
            punktycalkowania[0].X = (float)-0.5773;
            punktycalkowania[0].Y = (float)-0.5773;
            punktycalkowania[1].X = (float)0.5773;
            punktycalkowania[1].Y = (float)-0.5773;
            punktycalkowania[2].X = (float)0.5773;
            punktycalkowania[2].Y = (float)0.5773;
            punktycalkowania[3].X = (float)-0.5773;
            punktycalkowania[3].Y = (float)0.5773;

            

            for (int i = 0; i < 4; i++)
            {
                Npoksi[0, i] = (-0.25 * (1 - punktycalkowania[i].Y));
                Npoksi[1, i] = (0.25 * (1 - punktycalkowania[i].Y));
                Npoksi[2, i] = (0.25 * (1 + punktycalkowania[i].Y));
                Npoksi[3, i] = (-0.25 * (1 + punktycalkowania[i].Y));
            }

            

            for (int i = 0; i < 4; i++)
            {
                Npoeta[0, i] = (-0.25 * (1 - punktycalkowania[i].X));
                Npoeta[1, i] = (-0.25 * (1 + punktycalkowania[i].X));
                Npoeta[2, i] = (0.25 * (1 + punktycalkowania[i].X));
                Npoeta[3, i] = (0.25 * (1 - punktycalkowania[i].X));
            }

            
           
            NumberOfNodes = NodesInHeightDim * NodesInLengthDim;
            // NumberOfNodes = (NodesInHeightDim - 1) * (NodesInLengthDim - 1);
            nodes = new List<Node>(NumberOfNodes);
            elements = new List<Element>();
            dx = length / (NodesInLengthDim - 1);
            dy = height / (NodesInHeightDim - 1);
            this.NodesInHeightDim = NodesInHeightDim;
            this.NodesInLengthDim = NodesInLengthDim;
            this.Hglobal = new double[NumberOfNodes, NumberOfNodes];
            this.Pglobal = new double[NumberOfNodes];
            this.Cglobal = new double[NumberOfNodes, NumberOfNodes];
            GenerateNodes();
            GenerateElements();
            GenerateJacobianMatrix();

            


        }

        public void GenerateMatrices()
        {
            GenerateHlocals();
            GeneratePvectors();
            GenerateHglobal();
            GeneratePglobal();
            GenerateClocals();
            GenerateCglobal();
        }

        public void SetSimulationData(double alfa, double conductivity, double initial_temp, double ambient_temp, double density, double specific_heat, double simulation_time, double step_time)
        {
            this.Initial_Temperature = initial_temp;
            this.Simulation_Time = simulation_time;
            this.Simulation_Step_Time = step_time;
            this.Ambient_Temperature = ambient_temp;
            this.Convection_Factor = alfa;
            this.Specific_Heat = specific_heat;
            this.Conductivity = conductivity;
            this.Density = density;
        }

        public void SolveUnstationary(ObservableCollection<Vector<double>> solves)
        {
            double currentTime = 0;
            //List<Vector<double>> solves = new List<Vector<double>>();
            Matrix<double> H = DenseMatrix.OfArray(Hglobal);
            Matrix<double> C = DenseMatrix.OfArray(Cglobal).Divide(Simulation_Step_Time);
            Vector<double> P = DenseVector.OfArray(Pglobal);
            foreach (Node n in nodes)
                n.T = Initial_Temperature;

            double[] t0 = new double[NumberOfNodes];

            for (int i = 0; i < t0.Length; i++)
                t0[i] = nodes[i].T;

            Vector<double> T0 = DenseVector.OfArray(t0);

            H = H.Add(C);
            P = P.Add(C.Multiply(T0));

            for(currentTime=0; currentTime < Simulation_Time;currentTime+=Simulation_Step_Time)
           // while ( currentTime < Simulation_Time)
            {
                
                Vector<double> T1 = H.Solve(P);
                solves.Add(T1);
                
                T0 = T1;
                P = DenseVector.OfArray(Pglobal).Add(C.Multiply(T0));
               // currentTime += Simulation_Step_Time;
            }

           // return solves;
        }

        public double[] SolveStationary()
        {
            Matrix<double> H = DenseMatrix.OfArray(Hglobal);
            Vector<double> P = DenseVector.OfArray(Pglobal);

            Vector<double> t = H.Solve(P);

            return t.ToArray();
        }

        private void GenerateCglobal()
        {
            for (int k = 0; k < elements.Count; k++)
                for (int i = 0; i < 4; i++)
                    for (int j = 0; j < 4; j++)
                        Cglobal[elements[k].IDS[i], elements[k].IDS[j]] += elements[k].C[i,j];
        }

        private void GenerateClocals()
        {
            foreach (Element el in elements)
                Clocal(el);
        }

        private void ZeroesPglobal()
        {
            for (int i = 0; i < Pglobal.Length; i++)
                Pglobal[i] = 0;
        }

        private void ZeroesHglobal()
        {
            for (int i = 0; i < NumberOfNodes; i++)
                for (int j = 0; j < NumberOfNodes; j++)
                    Hglobal[i, j] = 0.0;
        }

        private void GeneratePglobal()
        {
            for (int k = 0; k < Elements.Count; k++)
                for (int i = 0; i < 4; i++)
                    Pglobal[elements[k].IDS[i]] += elements[k].P[i];

        }

        private void GenerateHglobal()
        {
            ZeroesHglobal();
            for (int k = 0; k < elements.Count; k++)
                for (int i = 0; i < 4; i++)
                    for (int j = 0; j < 4; j++)
                        Hglobal[elements[k].IDS[i], elements[k].IDS[j]] += elements[k].H[i,j];
        }

        private void GenerateNodes()
        {

            for (int i = 0; i < NodesInLengthDim; i++)
                for (int j = 0; j < NodesInHeightDim; j++)
                    nodes.Add(new Node(i * dx, j * dy));

        }

        private void GenerateElements()
        {
            int breaker = NodesInHeightDim * (NodesInLengthDim - 1);
            int i = 0;
            while (i < breaker)
            {

                for (int j = i; j < NodesInHeightDim + i - 1; j++)
                    elements.Add(new Element(j, j + NodesInHeightDim, j + NodesInHeightDim + 1, j + 1));
                i += NodesInHeightDim;
            }

            //foreach(Element el in elements)
            //{
            //   // Element el = elements[k];

            //    el.Pow.Add(hasNeighbour(el.IDS[3], el.IDS[0], elements.IndexOf(el)));
            //    el.Pow.Add(hasNeighbour(el.IDS[0], el.IDS[1], elements.IndexOf(el)));
            //    el.Pow.Add(hasNeighbour(el.IDS[1], el.IDS[2], elements.IndexOf(el)));
            //    el.Pow.Add(hasNeighbour(el.IDS[2], el.IDS[3], elements.IndexOf(el)));

            //}
            for (int k = 0; k < elements.Count; k++) 
            {
                Element el = elements[k];
                el.Pow.Add(hasNeighbour(el.IDS[3], el.IDS[0], k));
                el.Pow.Add(hasNeighbour(el.IDS[0], el.IDS[1], k));
                el.Pow.Add(hasNeighbour(el.IDS[1], el.IDS[2], k));
                el.Pow.Add(hasNeighbour(el.IDS[2], el.IDS[3], k));
            }


        }

        private bool hasNeighbour(int id1, int id2, int elementid)
        {
            for (int i = 0; i < elements.Count; i++)
            {
                if (i == elementid)
                    continue;
                if (Elements[i].HasNodes(id1, id2))
                    return true;

            }
            return false;
        }

        private void GenerateHlocals()
        {
            foreach (Element el in elements)
                Hlocal(el);
        }

        private void GeneratePvectors()
        {
            foreach (Element el in this.elements)
                Plocal(el);
        }

        private void Clocal(Element el)
        {
            Vector<double> funkcje_ksztaltu = DenseVector.Create(4, 0.0);
            Matrix<double> tmp;
            double[] matrix = new double[4];         
            double[] X = new double[4]
            {       nodes[el.IDS[0]].X,
                    nodes[el.IDS[1]].X,
                    nodes[el.IDS[2]].X,
                    nodes[el.IDS[3]].X
            };
            double[] Y = new double[4]
            {       nodes[el.IDS[0]].Y,
                    nodes[el.IDS[1]].Y,
                    nodes[el.IDS[2]].Y,
                    nodes[el.IDS[3]].Y
            };

            Matrix<double> c = DenseMatrix.Create(4, 4, 0);
            foreach (PointF p in punktycalkowania)
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
            c = c.Multiply(JacobianMatrix.Determinant());
            //Matrix<double> m1 = m.Transpose().Multiply(m);
            el.C = c.Multiply(Density * Specific_Heat).ToArray();
            //el.C = c.ToArray(); ;
        }

        private void Plocal(Element el)
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
                            jakobian1D = (nodes[el.IDS[3]].Y - nodes[el.IDS[0]].Y) * 0.5;
                            break;
                        case 1:
                            punktycalkowania[0, 0] = -0.5773;
                            punktycalkowania[0, 1] = -1;
                            punktycalkowania[1, 0] = 0.5773;
                            punktycalkowania[1, 1] = -1;
                            jakobian1D = (nodes[el.IDS[1]].X - nodes[el.IDS[0]].X) * 0.5;
                            break;
                        case 2:
                            punktycalkowania[0, 0] = 1;
                            punktycalkowania[0, 1] = -0.5773;
                            punktycalkowania[1, 0] = 1;
                            punktycalkowania[1, 1] = 0.5773;
                            jakobian1D = (nodes[el.IDS[2]].Y - nodes[el.IDS[1]].Y) * 0.5;
                            break;
                        case 3:
                            punktycalkowania[0, 0] = 0.5773;
                            punktycalkowania[0, 1] = 1;
                            punktycalkowania[1, 0] = -0.5773;
                            punktycalkowania[1, 1] = 1;
                            jakobian1D = (nodes[el.IDS[2]].X - nodes[el.IDS[3]].X) * 0.5;
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

            el.P = P.Multiply(Convection_Factor).Multiply(Ambient_Temperature).ToArray();
            el.H = DenseMatrix.OfArray(el.H).Add(druga_calka_do_Hloc.Multiply(Convection_Factor)).ToArray();

           
        }

        private void Hlocal(Element el)
        {
            

            double[] X = new double[4]
            {       nodes[el.IDS[0]].X,
                    nodes[el.IDS[1]].X,
                    nodes[el.IDS[2]].X,
                    nodes[el.IDS[3]].X
            };
            double[] Y = new double[4]
            {       nodes[el.IDS[0]].Y,
                    nodes[el.IDS[1]].Y,
                    nodes[el.IDS[2]].Y,
                    nodes[el.IDS[3]].Y
            };

            

            double[,] Npox = new double[4, 4]; // pochodne funkcji krztałtu po x
            double[,] Npoy = new double[4, 4]; // pochodne funkcji krztałtu po y


           


            for (int i = 0; i < 4; i++) // po punktach całkowania
            {
               
                for (int j = 0; j < 4; j++) // petla przez funkcje krztałtu j w punkcie całkowania i
                {
                    Vector<double> v = DenseVector.OfArray(new double[]
                    {
                        Npoksi[j,i] , Npoeta[j,i]
                    }); // wektor pochodnej funkcji krztałtu j  w punkcie całkowania i po ksi i eta
                    double[] tmp =JacobianMatrix.Inverse().Multiply(v).ToArray();

                    Npox[j, i] = tmp[0];
                    Npoy[j, i] = tmp[1];
                }
            }

            Matrix<double> Hlocal = DenseMatrix.Create(4, 4, 0);
            Matrix<double> NpoX;
            Matrix<double> NpoY;
            Matrix<double> VxT;
            Matrix<double> VyT;
            for (int i = 0; i < 4; i++ ) // wybieramy wektory NpoX i NpoY w kazdym punkcie całkowania
            {
                NpoX = DenseMatrix.OfArray(Npox).Column(i).ToRowMatrix();
                NpoY = DenseMatrix.OfArray(Npoy).Column(i).ToRowMatrix();

                VxT = NpoX.Transpose();
                VyT = NpoY.Transpose();

                Matrix<double> m = (VxT.Multiply(NpoX).Add(VyT.Multiply(NpoY))); // sumujemy macierze Npox i Npoy
               Hlocal.Add(m,Hlocal); // sumujemy macierze wynikowe w kazdym punkcie całkowania
                
            }

            el.H = Hlocal.Multiply(Conductivity).Multiply(JacobianMatrix.Determinant()).ToArray(); // mnozymy przez wyznacznik jakobianu i przewodniość
        }

        private void GenerateJacobianMatrix()
        {
            Element el = elements[0];
            double[] X = new double[4]
            {       nodes[el.IDS[0]].X,
                    nodes[el.IDS[1]].X,
                    nodes[el.IDS[2]].X,
                    nodes[el.IDS[3]].X
            };
            double[] Y = new double[4]
            {       nodes[el.IDS[0]].Y,
                    nodes[el.IDS[1]].Y,
                    nodes[el.IDS[2]].Y,
                    nodes[el.IDS[3]].Y
            };
            double[] matrix = new double[4]; // macierz jakobianu
            
                for (int nr_funkcji_ksztaltu = 0; nr_funkcji_ksztaltu < 4; nr_funkcji_ksztaltu++)
                {
                    matrix[0] += Npoksi[nr_funkcji_ksztaltu, 0] * X[nr_funkcji_ksztaltu]; // x po ksi
                    matrix[1] += Npoeta[nr_funkcji_ksztaltu, 0] * X[nr_funkcji_ksztaltu]; // x po eta
                    matrix[2] += Npoksi[nr_funkcji_ksztaltu, 0] * Y[nr_funkcji_ksztaltu]; // y po ksi
                    matrix[3] += Npoeta[nr_funkcji_ksztaltu, 0] * Y[nr_funkcji_ksztaltu]; // y po eta
                }
            


            Matrix<double> c = DenseMatrix.OfArray(new double[,]
            {
                    {matrix[0], matrix[2] },
                    {matrix[1], matrix[3] }

            });


            this.JacobianMatrix = c;
        }

        internal List<Node> Nodes
        {
            get
            {
                return nodes;
            }

            set
            {
                nodes = value;
            }
        }

        internal List<Element> Elements
        {
            get
            {
                return elements;
            }

            set
            {
                elements = value;
            }
        }

        public double Dx
        {
            get
            {
                return dx;
            }

            set
            {
                dx = value;
            }
        }

        public double Dy
        {
            get
            {
                return dy;
            }

            set
            {
                dy = value;
            }
        }
    }
}
