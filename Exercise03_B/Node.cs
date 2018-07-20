namespace Exercise03_B
{
    public class Node<T>
    {
        public T Data { get; set; }
        public Node<T> Next { get; set; }
        public Node<T> Prev { get; set; }
        public int Index;

        public Node(T data, Node<T> prev, Node<T> next)
        {
            Data = data;
            Next = next;
            Prev = prev;
        }

        public Node(T data)
        {
            Data = data;
        }

        public Node()
        {
            
        }
    }
}