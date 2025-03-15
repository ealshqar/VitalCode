using System;
using System.Linq.Expressions;

namespace Vital.Business.Shared.Shared
{
    public class SortKey<TSource>
    {
        public Expression<Func<TSource, object>> Key { get; set; }

        public SortByTypesEnum Type;
    }
}
