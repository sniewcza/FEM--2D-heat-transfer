using System.Collections.Generic;

namespace MES.Model
{
    public class Element
    {
        public double[,] C { get; set; }
        public double[] P { get; set; }
        public double[,] H { get; set; }
        public List<bool> Pow { get; set; } = new List<bool>(4);
        public List<int> IDS { get; set; } = new List<int>(4);

        public bool HasNodes(int id1, int id2)
        {
            return (this.IDS.Contains(id1) && this.IDS.Contains(id2));
        }

        public Element(int NodeID1, int NodeID2, int NodeID3, int NodeID4)
        {
            IDS.Add(NodeID1);
            IDS.Add(NodeID2);
            IDS.Add(NodeID3);
            IDS.Add(NodeID4);
        }
    }
}

