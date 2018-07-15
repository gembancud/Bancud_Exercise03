using System;

namespace Exercise03_B
{
    public class Outline
    {
        private LinkedList<Coordinate> _coordinateList;

        public LinkedList<Coordinate> CoordinateList
        {
            get { return _coordinateList; }
            set { _coordinateList = value; }
        }

        public Outline(LinkedList<Coordinate> coordinateList)
        {
            _coordinateList = coordinateList;
        }

        public Outline()
        {
        }

        public Outline Evolve()
        {
            ImprintImportance();
            int CoordinateIndex = 0;
            int indexofLeastImportantCoordinate = 0;
            var LeastImportantCoordinate = CoordinateList.Head;
            foreach (Node<Coordinate> coor in CoordinateList)
            {
                if (coor.Data.Importance <= LeastImportantCoordinate.Data.Importance)
                {
                    LeastImportantCoordinate = coor;
                    indexofLeastImportantCoordinate = CoordinateIndex;
                }
                CoordinateIndex++;
            }

            Outline newOutline= new Outline(CloneCoordinateLinkedList());
            newOutline.CoordinateList.RemoveAt(indexofLeastImportantCoordinate);
            return newOutline;
        }

        public LinkedList<Coordinate> CloneCoordinateLinkedList()
        {
            LinkedList<Coordinate> newCoordinateLinkedList= new LinkedList<Coordinate>();
            foreach (Node<Coordinate> node in _coordinateList)
            {
                newCoordinateLinkedList.AddToTail(node);
            }

            return newCoordinateLinkedList;
        }

        public void ImprintImportance()
        {
            foreach (Node<Coordinate> coor in _coordinateList)
            {
                coor.Data.Importance = GetImportance(coor);
            }
        }

        public double GetImportance(Node<Coordinate> coor)
        {
            double d1 = GetDistance(coor.Prev.Data, coor.Data);
            double d2 = GetDistance(coor.Data, coor.Next.Data);
            double d3 = GetDistance(coor.Prev.Data, coor.Next.Data);
            return d1 + d2 - d3;
        }

        public double GetDistance(Coordinate first, Coordinate second)
        {
            double sumOfSquares = (Convert.ToInt32(first.X - second.X)) ^ 2 + (Convert.ToInt32(first.Y - second.Y)) ^ 2;
            double distance = Math.Sqrt(sumOfSquares);
            return distance;
        }


    }
}