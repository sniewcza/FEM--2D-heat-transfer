namespace MES.Model
{
    public class Node
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double T { get; set; }
        public bool Status { get; set; }

        public Node(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}
