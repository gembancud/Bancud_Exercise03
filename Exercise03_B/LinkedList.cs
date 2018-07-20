using System;
using System.Collections;
using System.Collections.Generic;

namespace Exercise03_B
{
    public class LinkedList<T> : IEnumerable
    {
        public Node<T> _header { get; set; }
        public Node<T> _trailer { get; set; }
        public Node<T> Head { get; set; }
        public Node<T> Tail { get; set; }
        public int Size { get; private set; }

        public LinkedList()
        {
            _header = new Node<T>(default(T), null, null);
            _trailer = new Node<T>(default(T), _header, null);
            _header.Next = _trailer;

            Size = 0;
        }

        #region AddFunctions
        public void AddToTail(T data)
        {
            AddBetween(data, _trailer.Prev, _trailer);
        }
        public void AddToHead(T data)
        {
            AddBetween(data, _header, _header.Next);
        }
        private void AddBetween(T data, Node<T> prev, Node<T> next)
        {
            var newNode = new Node<T>(data, prev, next);
            newNode.Index = Size;
            prev.Next = newNode;
            next.Prev = newNode;

            Head = _header.Next;
            Tail = _trailer.Prev;

            Head.Prev = Tail;
            Tail.Next = Head;
            Size++;
        }
        #endregion

        public void InsertAt(int index, T data)
        {
            if (index > Size) throw new Exception("Out of Range");
            var temp = Head;
            for (int i = 0; i < index; i++)
            {
                temp = temp.Next;
            }
            AddBetween(data, temp.Prev, temp);
        }

        #region Remove
        private void Remove(Node<T> prev, Node<T> next)
        {
            prev.Next = next;
            next.Prev = prev;

            Head = Tail.Next;
            Tail = Head.Prev;

        }
        public Node<T> RemoveAt(int i)
        {
            if (i >= Size) throw new System.ArgumentOutOfRangeException();
            var temp = Head;
            for (int j = 0; j < i; j++)
            {
                temp = temp.Next;
            }

            Remove(temp.Prev, temp.Next);
            Size--;
            return temp;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public bool MoveNext()
        {
            throw new NotImplementedException();
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }



        #endregion

        public IEnumerator GetEnumerator()
        {
            var temp = Head;
            for (int i = 0; i < Size; i++)
            {
                yield return temp;
                temp = temp.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}