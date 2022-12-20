using System;

namespace Main
{
    class Program
    {
        static void Main(string[] args)
        {
            Ukol1.NewQueue<int>.Test();
            Ukol2.Identity<int>.Test();
            Ukol3.ReaderExtensions.Test();
            Ukol4.ImmutableArray<int>.Test();
        }
    }
}
