using System;
using FluentAssertions;
using ITLIBRIUM.BddToolkit.Builders.Scenarios;
using JetBrains.Annotations;

namespace ITLIBRIUM.BddToolkit.FluentAssertions
{
    public class FluentAssertionsThenBuilder<TContext>
    {
        private readonly IThenContinuationBuilder<TContext> _thenContinuationBuilder;

        protected internal FluentAssertionsThenBuilder(IThenBuilder<TContext> thenBuilder) =>
            _thenContinuationBuilder = thenBuilder.GetContinuationBuilder();

        protected internal FluentAssertionsThenBuilder(IThenContinuationBuilder<TContext> thenContinuationBuilder) =>
            _thenContinuationBuilder = thenContinuationBuilder;

        [PublicAPI]
        public IThenContinuationBuilder<TContext> Throws<TException>() =>
            _thenContinuationBuilder.And((context, result) =>
                {
                    result.IsSuccessful.Should().BeFalse();
                    result.Exception.Should().BeAssignableTo<TException>();
                },
                $"Exception type is assignable to {typeof(TException).Name}");

        [PublicAPI]
        public IThenContinuationBuilder<TContext> ThrowsExactly<TException>() =>
            _thenContinuationBuilder.And((context, result) =>
                {
                    result.IsSuccessful.Should().BeFalse();
                    result.Exception.Should().BeOfType<TException>();
                },
                $"Exception type is exactly {typeof(TException).Name}");
    }
}