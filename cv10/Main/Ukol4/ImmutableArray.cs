using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ukol1;

namespace Ukol4
{
    public class ImmutableArray<T> : IEnumerable<T>
    {
        private interface ITriplet
        {
        }
        private abstract class Triplet<A> : ITriplet
        {
            private A[] data = new A[3];
            public Triplet(A left, A middle, A right)
            {
                data[0] = left;
                data[1] = middle;
                data[2] = right;
            }
            public A this[int index]
            {
                get => index switch
                {
                    >= 0 and <= 2 => this.data[index],
                    _ => throw new IndexOutOfRangeException($"Index {index} out of range for a triplet.")
                };
            }
        }
        private class ValueTriplet<A> : Triplet<A>
        {
            public ValueTriplet(A left, A middle, A right) : base(left, middle, right)
            {
            }
        }

        private class ReferenceTriplet : Triplet<ITriplet>
        {
            public ReferenceTriplet(ITriplet left, ITriplet middle, ITriplet right) : base(left, middle, right)
            {

            }
        }
        
        private readonly int length;
        private readonly int realSize;
        private readonly ITriplet root;
        public T this[int index]
        {
            get
            {
                if (index >= length) throw new IndexOutOfRangeException($"Index {index} out of range (length:{this.length}).");
                var currentNode = root;
                var currentIndex = index;
                var currentSize = realSize;
                while (currentNode is not ValueTriplet<T>)
                {
                    int tripletIndex;
                    (tripletIndex, currentIndex ) = GetRealIndex(currentSize, currentIndex);
                    currentNode = ((ReferenceTriplet)currentNode)[tripletIndex];
                    currentSize /= 3;
                }
                return ((ValueTriplet<T>)currentNode)[currentIndex];
            }
        }

        private static ITriplet InnerSet<A>(int size, int index, ITriplet currentNode, A value)
        {
            if (size == 3)
            {
                var valueTriplet = (currentNode as ValueTriplet<A>);
                var values = new A[] { valueTriplet[0], valueTriplet[1], valueTriplet[2] };
                values[index] = value;
                return new ValueTriplet<A>(values[0], values[1], values[2]);
            }else
            {
                int tripletIndex;
                (tripletIndex, index) = GetRealIndex(size, index);
                var triplet = (currentNode as ReferenceTriplet);
                var values = new ITriplet[] { triplet[0], triplet[1], triplet[2] };
                values[tripletIndex] = InnerSet(size/3, index, values[tripletIndex], value);
                return new ReferenceTriplet(values[0], values[1], values[2]);
            }
        }

        public ImmutableArray<T> Set(int index, T value)
        {
            if (index >= length) throw new IndexOutOfRangeException($"Index {index} out of range (length:{this.length}).");
            return new ImmutableArray<T>(this.length, this.realSize, InnerSet(this.realSize, index, this.root, value));
        }

        private static int Pow(int bas, int exp)
        {
            return Enumerable
                  .Repeat(bas, exp)
                  .Aggregate(1, (a, b) => a * b);
        }
        public ImmutableArray(int length)
        {
            this.length = length;
            int depth = 1;
            while (Pow(3,depth) < length)
            {
                depth++;
            }

            (root, realSize) = Create<T>(depth);
        }
 
        private ImmutableArray(int length, int realSize, ITriplet root)
        {
            this.length = length;
            this.realSize = realSize;
            this.root = root;
        }
        private static (int IndexForTriplet, int NewIndex) GetRealIndex(int size, int index)
        {
            var chunk = size / 3;
            if (index  < chunk) return (0, index);
            if (index >= chunk && index < chunk * 2) return (1, index - chunk);
            return (2, index- 2 * chunk);
        }
        private static (ITriplet Element, int Size) Create<A>(int depth)
        {
            if (depth <= 0) throw new Exception("Something went wrong...");
            if (depth == 1) return (new ValueTriplet<A>(default, default, default), 3);
            var chunks = new (ITriplet Element, int Size)[3];
            for(int i =0;i<3; i++)
            {
                chunks[i] = Create<A>(depth - 1);
            }
            return (new ReferenceTriplet(chunks[0].Element, chunks[1].Element, chunks[2].Element), chunks.Select(x=>x.Size).Sum());
        }

        public IEnumerator<T> GetEnumerator()
        {
            int counter = 0;
            var stack = NewStack<ITriplet>.Empty();
            stack = NewStack<ITriplet>.Push(stack, this.root);
            while (!NewStack<ITriplet>.IsEmpty(stack))
            {
                ITriplet current;
                (current, stack) = NewStack<ITriplet>.Pop(stack);
                switch (current)
                {
                    case ReferenceTriplet r:
                        for (int i = 2; i >= 0; i--)
                        {
                            stack = NewStack<ITriplet>.Push(stack, r[i]);
                        }
                        break;
                    case ValueTriplet<T> v:
                        for(int i=0;i<3;i++)
                        {
                            counter++;
                            yield return v[i];
                            if (counter == this.length)
                            {
                                stack = NewStack<ITriplet>.Empty();
                                break;
                            }
                        }
                        break;
                    default: throw new Exception("Unexpected data type");
                }
            }

        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public static void Test()
        {
            ImmutableArray<int> test = new ImmutableArray<int>(30);
            for (int i = 0; i < 30; i++)
            {
                Console.Write(test[i] + ", ");
            }
            Console.WriteLine();
            
            for (int i = 0; i < 30; i++)
            {
                test = test.Set(i, i);
            }
            foreach(var item in test)
            {
                Console.Write(item + "; ");
            }
            Console.WriteLine();
            Console.WriteLine(string.Join(": ", test));
        }
    }
}
