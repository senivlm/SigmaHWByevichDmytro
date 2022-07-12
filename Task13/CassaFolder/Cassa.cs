using System;
using System.Collections;
using System.Collections.Generic;
using Task13.Persons;
namespace Task13.CassaFolder
{
    internal class Cassa : IEnumerable<IPerson>
    {
        private double _xCoord;
        private PriorityQueue<IPerson, string> _queue;
        public int Count => _queue.Count;
        public event Action<Cassa> OnCassaClosed;
        public double XCoord { get => _xCoord; set => _xCoord = value; }

        public Cassa(double xCoord):this()
        {
            _xCoord = xCoord;
        }

        public Cassa()
        {
            _queue = new();
        }
        public void Add(IPerson person)
        {
            person.Coordinate = _xCoord;
            _queue.Enqueue(person, person.Status);
        }
        public IPerson Dequeue()
        {
            return _queue.Dequeue();
        }
        public IPerson Peek()
        {
            return _queue.Peek();
        }
        public void Close()
        {
            OnCassaClosed.Invoke(this);
        }
        public IEnumerator<IPerson> GetEnumerator()
        {
            while (_queue.Count > 0)
            {
                yield return _queue.Dequeue();
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
