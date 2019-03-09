using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES.Utils
{
    class Deviratives
    {
        public double[,] Npoksi = new double[4, 4]; // pochodne funkcji krztałtu po ksi w 4 punktach całkowania
        public double[,] Npoeta = new double[4, 4]; // pochodne funkcji krztałtu po eta w 4 punktach całkowania

        public Deviratives(IntegrationPoints IntegrationPoints)
        {
            for (int i = 0; i < 4; i++)
            {
                Npoksi[0, i] = (-0.25 * (1 - IntegrationPoints.Points[i].Y));
                Npoksi[1, i] = (0.25 * (1 - IntegrationPoints.Points[i].Y));
                Npoksi[2, i] = (0.25 * (1 + IntegrationPoints.Points[i].Y));
                Npoksi[3, i] = (-0.25 * (1 + IntegrationPoints.Points[i].Y));
            }



            for (int i = 0; i < 4; i++)
            {
                Npoeta[0, i] = (-0.25 * (1 - IntegrationPoints.Points[i].X));
                Npoeta[1, i] = (-0.25 * (1 + IntegrationPoints.Points[i].X));
                Npoeta[2, i] = (0.25 * (1 + IntegrationPoints.Points[i].X));
                Npoeta[3, i] = (0.25 * (1 - IntegrationPoints.Points[i].X));
            }
        }
    }
}
