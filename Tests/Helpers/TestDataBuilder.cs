using System;
using System.Collections.Generic;
using System.Linq;

namespace Tests.Helpers
{
    public abstract class TestDataBuilder<TObject, TBuilder>
        where TBuilder : TestDataBuilder<TObject, TBuilder>, new()
    {
        private readonly IList<Action<TObject>> configurationActions = new List<Action<TObject>>();

        public TBuilder With(Action<TObject> configure)
        {
            configurationActions.Add(configure);
            return (TBuilder)this;
        }

        public TObject Build()
        {
            var instance = CreateInstanceWithDefaults();
            configurationActions.ToList().ForEach(applyConfiguration => applyConfiguration(instance));
            return instance;
        }

        public static implicit operator TObject(TestDataBuilder<TObject, TBuilder> builder)
        {
            return builder.Build();
        }

        public IEnumerable<TObject> BuildMultiple(int amount)
        {
            return Enumerable.Range(0, amount)
                .Select(_ => Build())
                .ToList();
        }

        public static TBuilder New()
        {
            return new TBuilder();
        }

        protected abstract TObject CreateInstanceWithDefaults();
    }
}