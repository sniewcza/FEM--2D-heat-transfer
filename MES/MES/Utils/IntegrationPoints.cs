using System.Drawing;

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
