namespace Exercise03_B
{
    public class Coordinate
    {
        private double _y;
        private double _x;
        private double _importance;


        public double Y
        {
            get { return _y; }
            set { _y = value; }
        }

        public double X
        {
            get { return _x; }
            set { _x = value; }
        }

        public double Importance
        {
            get { return _importance; }
            set { _importance = value; }
        }

        public Coordinate()
        {
            
        }

        public Coordinate(double x, double y)
        {
            _x = x;
            _y = y;
        }
    }
}