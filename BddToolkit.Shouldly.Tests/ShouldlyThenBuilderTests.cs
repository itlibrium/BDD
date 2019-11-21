using System;
using ITLIBRIUM.BddToolkit.Execution;
using Shouldly;
using Xunit;

namespace ITLIBRIUM.BddToolkit.Shouldly.Tests
{
    public class ShouldlyThenBuilderTests
    {
        [Fact]
        public void AssertPassWhenExceptionTypeIsEqualToExpected()
        {
            Should.NotThrow(() => Bdd.Scenario<Context>()
                .When(f => f.BusinessRuleWasBroken())
                .Then().Throws<BusinessException>()
                .Create()
                .RunTest()
                .ThrowOnErrors());
            
            Should.NotThrow(() => Bdd.Scenario<Context>()
                .When(f => f.BusinessRuleWasBroken())
                .Then().ThrowsExactly<BusinessException>()
                .Create()
                .RunTest()
                .ThrowOnErrors());
        }

        [Fact]
        public void AssertPassWhenExceptionTypeIsAssignableToExpected()
        {
            Should.NotThrow(() => Bdd.Scenario<Context>()
                .When(f => f.BusinessRuleWasBroken())
                .Then().Throws<Exception>()
                .Create()
                .RunTest()
                .ThrowOnErrors());
        }

        [Fact]
        public void AssertFailWhenExceptionTypeIsNotAssignableToExpected()
        {
            Should.Throw<AggregateAssertException>(() => Bdd.Scenario<Context>()
                .When(f => f.BusinessRuleWasBroken())
                .Then().Throws<InvalidOperationException>()
                .Create()
                .RunTest()
                .ThrowOnErrors());
        }

        [Fact]
        public void AssertFailWhenExceptionTypeIsNotEqualToExpected()
        {
            Should.Throw<AggregateAssertException>(() => Bdd.Scenario<Context>()
                .When(f => f.BusinessRuleWasBroken())
                .Then().ThrowsExactly<Exception>()
                .Create()
                .RunTest()
                .ThrowOnErrors());
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
