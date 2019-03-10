
using System.Collections.Generic;

namespace MES.Model
{
    public class Mesh
    {
        public double Dx { get; private set; }
        public double Dy { get; private set; }
        public List<Node> Nodes { get; private set; }
        public List<Element> Elements { get; private set; }

        public Mesh(List<Node> nodes, List<Element> elements, double dx, double dy)
        {
            Nodes = nodes;
            Elements = elements;
            Dx = dx;
            Dy = dy;
        }
    }
}
