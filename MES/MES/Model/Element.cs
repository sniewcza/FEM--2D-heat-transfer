using System.Collections.Generic;

namespace MES
{
    public class Element
    {
         List<int> IDs = new List<int>(4);
        List<bool> pow = new List<bool>(4);
        double[,] Hlocal;
        double[] Plocal;
        double[,] Clocal;

        public bool HasNodes(int id1, int id2)
        {
            return (this.IDs.Contains(id1) && this.IDs.Contains(id2));
        }

        public Element (int NodeID1, int NodeID2, int NodeID3, int NodeID4)
        {
            IDs.Add(NodeID1);
            IDs.Add(NodeID2);
            IDs.Add(NodeID3);
            IDs.Add(NodeID4);
        }

        public double[,] C
        {
            get
            {
                return Clocal;
            }
            set
            {
                Clocal = value;
            }
        }
        public double[] P
        {
            get
            {
                return Plocal;
            }
            set
            {
                Plocal = value;
            }
        }
        public double[,] H
        {
            get
            {
                return Hlocal;
            }
            set
            {
                Hlocal = value;
            }
        }
        public List<bool> Pow
        {
            get
            {
                return pow;
            }

            set
            {
                pow = value;
            }
        }

        internal List<int> IDS
        {
            get
            {
                return IDs;
            }

            set
            {
                IDs = value;
            }
        }

        
    }
    }

