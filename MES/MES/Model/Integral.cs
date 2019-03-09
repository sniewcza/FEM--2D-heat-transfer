using System;
using System.Data;
namespace MES
{
    class Integral
    {
        private double[] cordinates_2pt = new double[] { -0.5773, 0.5773 };
        private double[] cordinates_3pt = new double[] { -0.7745, 0.0, 0.7745 };
        private double[] wages_2pt = new double[] { 1.0, 1.0 };
        private double[] wages_3pt = new double[] { 5 / 9.0, 8 / 9.0, 5 / 9.0 };
       // private string expression = "(x*x)*y+6*(x+y)";


        public double Compute(String expression, int type, int dim)
        {
            double[] cordinates = null;
            double[] wages = null;

            switch(type)
            {
                case 2:
                    cordinates = cordinates_2pt;
                    wages = wages_2pt;
                    break;
                case 3:
                    cordinates = cordinates_3pt;
                    wages = wages_3pt;
                    break;
                default:
                    break;
            }

            if (cordinates != null && wages != null)
            {
                DataTable table = new DataTable();
                double result = 0;
                string s;
                switch (dim)
                {
                    case 1:
                        for (int i = 0; i < cordinates.Length; i++)
                        {
                            s = expression.Replace("x", cordinates[i].ToString()).Replace(",", ".");
                            result += Convert.ToDouble(table.Compute(s, null)) * wages[i];
                        }
                        break;

                    case 2:
                        for (int i = 0; i < cordinates.Length; i++)
                        {
                            s = expression.Replace("x", cordinates[i].ToString()).Replace(",", ".");
                            for (int j = 0; j < cordinates.Length; j++)
                            {
                                String s2 = s.Replace("y", cordinates[j].ToString()).Replace(",", ".");
                                result += Convert.ToDouble(table.Compute(s2, null)) * wages[i] * wages[j];
                            }
                        }
                        break;
                        // popraw dla 3d
                    case 3:
                        for (int i = 0; i < cordinates.Length; i++)
                        {
                            s = expression.Replace("x", cordinates[i].ToString()).Replace(",", ".");
                            for (int j = 0; j < cordinates.Length; j++)
                            {
                                String s2 = s.Replace("y", cordinates[j].ToString()).Replace(",", ".");
                                for (int k = 0; k < cordinates.Length; k++)
                                {
                                    String s3 = s2.Replace("z", cordinates[k].ToString()).Replace(",", ".");
                                    result += Convert.ToDouble(table.Compute(s3, null)) * wages[i] * wages[j] * wages[k];
                                }

                            }
                        }
                        break;
                }
                return result;
            }
            return -1;
                        
           
            //table.Columns.Add("expression", typeof(string),s );
            //DataRow row = table.NewRow();
            //table.Rows.Add(row);
            //return double.Parse((string)row["expression"]);
            
        }

     

    }
}
