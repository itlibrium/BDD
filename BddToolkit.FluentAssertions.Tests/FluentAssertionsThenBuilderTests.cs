using System;
using FluentAssertions;
using ITLIBRIUM.BddToolkit.Execution;
using Xunit;

namespace ITLIBRIUM.BddToolkit.FluentAssertions.Tests
{
    public class FluentAssertionsThenBuilderTests
    {
        [Fact]
        public void AssertPassWhenExceptionTypeIsEqualToExpected()
        {
            Action test = () => Bdd.Scenario<Context>()
                .When(f => f.BusinessRuleWasBroken())
                .Then().Throws<BusinessException>()
                .Test();
            test.Should().NotThrow();
            
            Action test2 = () => Bdd.Scenario<Context>()
                .When(f => f.BusinessRuleWasBroken())
                .Then().ThrowsExactly<BusinessException>()
                .Test();
            test2.Should().NotThrow();
        }

        [Fact]
        public void AssertPassWhenExceptionTypeIsAssignableToExpected()
        {
            Action test = () => Bdd.Scenario<Context>()
                .When(f => f.BusinessRuleWasBroken())
                .Then().Throws<Exception>()
                .Test();
            test.Should().NotThrow();
        }

        [Fact]
        public void AssertFailWhenExceptionTypeIsNotAssignableToExpected()
        {
            Action test = () => Bdd.Scenario<Context>()
                .When(f => f.BusinessRuleWasBroken())
                .Then().Throws<InvalidOperationException>()
                .Test();
            test.Should().Throw<AggregateAssertException>();
        }

        [Fact]
        public void AssertFailWhenExceptionTypeIsNotEqualToExpected()
        {
            Action test = () => Bdd.Scenario<Context>()
                .When(f => f.BusinessRuleWasBroken())
                .Then().ThrowsExactly<Exception>()
                .Test();
            test.Should().Throw<AggregateAssertException>();
        }

        private class Context
        {
            public void BusinessRuleWasBroken() => throw new BusinessException();
        }

        private class BusinessException : Exception
        {
        }
    }
}