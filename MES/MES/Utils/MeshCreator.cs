using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES.Utils
{
    class MeshCreator
    {

        private List<Node> _nodes;
        private List<Element> _elements;
        double dx;
        double dy;
        int numberOfNodes;
        int seedPartX;
        int seedPartY;

        public Mesh createMesh(double width, double height, int seedPartX, int seedPartY)
        {
            this.seedPartX = seedPartX;
            this.seedPartY = seedPartY;
            numberOfNodes = seedPartX * seedPartY;
            dx = width / (seedPartX - 1);
            dy = height / (seedPartY - 1);
            // NumberOfNodes = (NodesInHeightDim - 1) * (NodesInLengthDim - 1);
            _nodes = generateNodes(seedPartX, seedPartX);
           
             generateElements();

            return new Mesh(_nodes, _elements);
        }

        private List<Node> generateNodes(int seedPartX, int seedPartY)
        {
            List<Node> nodes = new List<Node>(numberOfNodes);
            for (int i = 0; i < seedPartX; i++)
                for (int j = 0; j < seedPartY; j++)
                    nodes.Add(new Node(i * dx, j * dy));

            return nodes;
        }

        private void generateElements()
        {

            _elements = new List<Element>();
           int breaker = seedPartY * (seedPartX - 1);
            int i = 0;
            while (i < breaker)
            {

                for (int j = i; j < seedPartY + i - 1; j++)
                    _elements.Add(new Element(j, j + seedPartY, j + seedPartY + 1, j + 1));
                i += seedPartY;
            }

            //foreach(Element el in elements)
            //{
            //   // Element el = elements[k];

            //    el.Pow.Add(hasNeighbour(el.IDS[3], el.IDS[0], elements.IndexOf(el)));
            //    el.Pow.Add(hasNeighbour(el.IDS[0], el.IDS[1], elements.IndexOf(el)));
            //    el.Pow.Add(hasNeighbour(el.IDS[1], el.IDS[2], elements.IndexOf(el)));
            //    el.Pow.Add(hasNeighbour(el.IDS[2], el.IDS[3], elements.IndexOf(el)));

            //}
            for (int k = 0; k < _elements.Count; k++)
            {
                Element el = _elements[k];
                el.Pow.Add(hasNeighbour(el.IDS[3], el.IDS[0], k));
                el.Pow.Add(hasNeighbour(el.IDS[0], el.IDS[1], k));
                el.Pow.Add(hasNeighbour(el.IDS[1], el.IDS[2], k));
                el.Pow.Add(hasNeighbour(el.IDS[2], el.IDS[3], k));
            }

            
        }

        private bool hasNeighbour(int id1, int id2, int elementid)
        {
            for (int i = 0; i < _elements.Count; i++)
            {
                if (i == elementid)
                    continue;
                if (_elements[i].HasNodes(id1, id2))
                    return true;

            }
            return false;
        }
    }
}
