using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ukol2
{

    public interface IFunctor<A>
    {
        IApplicative<B> fmap<B>(Func<A, B> f);
    }

    public interface IApplicative<A> : IFunctor<A>
    {
        IApplicative<A> Wrap(A t);
        IApplicative<B> Apply<B>(IApplicative<Func<A, B>> f);
    }
    public interface IMonad<A> : IApplicative<A>
    {
        IMonad<B> Return<B>(B value);
        IMonad<B> Bind<B>(Func<A, IMonad<B>> f);
    }
}
