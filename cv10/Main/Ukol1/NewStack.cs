using System;
using System.Collections.Generic;
using System.Text;

namespace Ukol1
{
    public class NewStack<T>
    {
        private T Data { get; init; }
        private NewStack<T> Next { get; init; }

        private NewStack()
        {

        }

        static public NewStack<T> Empty() => null;
        static public bool IsEmpty(NewStack<T> stack) => stack == null;
        static public NewStack<T> Push(NewStack<T> stack, T item) =>
             new NewStack<T> { Data = item, Next = stack };

        static public (T Item, NewStack<T> Stack) Pop(NewStack<T> stack) =>
         (IsEmpty(stack)) ? throw new Exception("Empty stack.") : (stack.Data, stack.Next);

    }
}
