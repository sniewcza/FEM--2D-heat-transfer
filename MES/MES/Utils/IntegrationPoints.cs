using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES.Utils
{
    class IntegrationPoints
    {
        public PointF[] Points { get; private set; }
     
        public IntegrationPoints()
        {
            Points = new PointF[] {
                        new PointF(-0.5773f,-0.5773f),
                        new PointF(0.5773f,-0.5773f),
                        new PointF(0.5773f,0.5773f),
                        new PointF (-0.5773f,0.5773f)
                };
        }

    }
}
