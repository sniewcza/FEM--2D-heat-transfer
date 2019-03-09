using MathNet.Numerics.LinearAlgebra;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace MES.Utils
{
    class SimulationDataCreator
    {
        private double _maxTemperature;
        private List<Color[]> _snapShoots = new List<Color[]>();
        private Mesh _mesh;
        private List<Vector<double>> _solutions;
        private readonly ValuePainter _painter = new ValuePainter();

        public SimulationDataCreator(Mesh mesh, List<Vector<double>> solutions)
        {
            _mesh = mesh;
            _solutions = solutions;
            _maxTemperature = solutions.Max(vec => vec.Max());
        }
        public Color[] getSnapshootFromSpecificSolution(int solutionNumber)
        {
            return _snapShoots[solutionNumber];
        }

        public Task<List<Color[]>> createSolutionsSnapshootsAsync()
        {
            return Task.Run(() =>
            {
                foreach (Vector<double> solution in _solutions)
                {
                   var snapShoot = createSnapshootForSolution(solution);
                    _snapShoots.Add(snapShoot);
                }
                return _snapShoots;
            });
        }

        private Color[] createSnapshootForSolution(Vector<double> solution)
        {
            List<Color> colors = new List<Color>();
            foreach (Element el in _mesh.Elements)
            {
                for (int i = 1; i <= 4; i++)
                {
                    double d = interpolateTemperature(solution[el.IDS[0]], solution[el.IDS[1]], solution[el.IDS[2]], solution[el.IDS[3]], i);
                    colors.Add(_painter.GetColorForValue(d, _maxTemperature));
                }
            }
            return colors.ToArray();
        }

        private double interpolateTemperature(double T1, double T2, double T3, double T4, int pointID)
        {
            // wartosci funkcji kształtu w punktach interpolacji {-0,5;-0,5} {0,5;-0,5} {0,5;0,5} {-0,5;0,5}
            double factor1 = 0.5625;
            double factor2 = 0.1875;
            double factor3 = 0.0625;
            double factor4 = 0.1875;
            switch (pointID)
            {
                case 1:
                    return factor1 * T1 + factor2 * T2 + factor3 * T3 + factor4 * T4;
                case 2:
                    return factor2 * T1 + factor1 * T2 + factor2 * T3 + factor3 * T4;
                case 3:
                    return factor3 * T1 + factor2 * T2 + factor1 * T3 + factor2 * T4;
                case 4:
                    return factor2 * T1 + factor3 * T2 + factor2 * T3 + factor1 * T4;
                default:
                    return -1;
            }

        }
    }
}
