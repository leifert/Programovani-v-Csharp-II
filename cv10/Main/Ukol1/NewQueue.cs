using System;
using System.Collections;
using System.Collections.Generic;

namespace Ukol1
{
    public class NewQueue<T> 
    {
        private T Data { get; init; }
        private NewQueue<T> Next { get; init; }


        private NewQueue()
        {

        }

        static public NewQueue<T> Empty() => null;
        static public bool IsEmpty(NewQueue<T> queue) => queue == null;
        static public NewQueue<T> Add(NewQueue<T> queue, T item) =>
             new NewQueue<T> { Data = item, Next = queue };


        static public (T Item, NewQueue<T> Queue) Remove(NewQueue<T> queue) {
            switch(queue)
            {
                case null: throw new Exception("Empty queue.");
                case NewQueue<T> x when x.Next == null: return (x.Data, null);
                case NewQueue<T> y:
                    var newQueue = Remove(y.Next);
                    return (newQueue.Item, Add(newQueue.Queue, queue.Data));
            };
        }
         
        static public IList<T> ToList(NewQueue<T> queue)
        {
            List<T> result = new List<T>();
            while(!IsEmpty(queue))
            {
                result.Add(queue.Data);
                queue = queue.Next;
            }
            result.Reverse();
            return result;
        }

        public static void Test()
        {
            Console.WriteLine("New Queue test");

            NewQueue<int> newQueue = NewQueue<int>.Empty();
            newQueue = NewQueue<int>.Add(newQueue, 1);
            newQueue = NewQueue<int>.Add(newQueue, 2);
            newQueue = NewQueue<int>.Add(newQueue, 3);

            int x;
            (x, newQueue) = NewQueue<int>.Remove(newQueue);

            Console.WriteLine($"Value: {x}, remaining elements: {string.Join(", ", NewQueue<int>.ToList(newQueue))}");
        }

       
    }
}
