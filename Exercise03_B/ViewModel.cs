namespace Exercise03_B
{
    public class ViewModel
    {
        private LinkedList<Outline> _outlineList;

        public LinkedList<Outline> OutlineList
        {
            get { return _outlineList; }
            set { _outlineList = value; }
        }

        public ViewModel()
        {
            _outlineList = new LinkedList<Outline>();
        }           
        public void Evolution(int n)
        {
            var temp = OutlineList.Head;
            for (int i = 0; i < n; i++)
            {
                OutlineList.AddToTail(temp.Data.Evolve());
                temp = temp.Next;
            }
        }
        

    }
}