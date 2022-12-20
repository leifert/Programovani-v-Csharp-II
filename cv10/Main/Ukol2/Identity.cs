using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ukol2
{
    public class Identity<A> : IMonad<A>
    {
        public A Value { get; init; }

        public IApplicative<B> fmap<B>(Func<A, B> f) => Bind(x => Return(f(x)));
        public IApplicative<B> Apply<B>(IApplicative<Func<A, B>> f) =>
            ((IMonad<Func<A, B>>)f).Bind(f => this.Bind(x => Return(f(x))));
        public IApplicative<A> Wrap(A value) => Return(value);
        public IMonad<B> Return<B>(B value) => new Identity<B>() { Value = value };
        public IMonad<B> Bind<B>(Func<A, IMonad<B>> f) => f(Value);

        public Identity<C> SelectMany<B, C>(Func<A, IMonad<B>> f, Func<A, B, C> resultSelector) =>
           (Identity<C>)Bind(x => f(x).Bind(y => Return(resultSelector(x, y))));

        public Identity<B> Select<B>(Func<A, B> selector) =>
              SelectMany(x => Return(selector(x)), (_, result) => result);

        public static void Test()
        {
            Console.WriteLine("Identity test");
            var result = new Identity<int>() { Value = 1 }
               .Bind(x => new Identity<int> { Value = x + 1 })
               .Bind(x => new Identity<string>() { Value = "Value: " + x });
            Console.WriteLine(((Identity<string>)result).Value);

            var test =
                from x in new Identity<int> { Value = 1 }
                from y in new Identity<int> { Value = 1 }
                from z in new Identity<int> { Value = 1 }
                select x + y + z;

            Console.WriteLine($"Result type: {test.GetType().Name}, Value: {test.Value}");
        }
    }
}
