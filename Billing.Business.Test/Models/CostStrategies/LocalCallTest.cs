using Xunit;
using System;
using FluentDateTime;
using Billing.Business.Models.CostStrategies;
using Billing.Business.Test.Models;

namespace Billing.Business.Test.Models.CostStrategies
{
    public class LocalCallTest
    {
        private readonly LocalCall strategy;

        public LocalCallTest()
        {
            this.strategy = new LocalCall();
        }

        [Theory]
        [InlineData(DayOfWeek.Friday)]
        [InlineData(DayOfWeek.Monday)]
        [InlineData(DayOfWeek.Thursday)]
        [InlineData(DayOfWeek.Wednesday)]
        [InlineData(DayOfWeek.Tuesday)]
        public void WorkDaysStartHourReturns020(DayOfWeek dayOfWeek)
        {
            var call = ModelFakers.CallFaker.Generate(1)[0];
            var hour = 8;
            call.StartTime = call.StartTime.Next(dayOfWeek).SetHour(hour);
            Assert.Equal(0.20, this.strategy.GetTax(call));
        }

        [Theory]
        [InlineData(DayOfWeek.Friday)]
        [InlineData(DayOfWeek.Monday)]
        [InlineData(DayOfWeek.Thursday)]
        [InlineData(DayOfWeek.Wednesday)]
        [InlineData(DayOfWeek.Tuesday)]
        public void WorkDaysEndHourReturns020(DayOfWeek dayOfWeek)
        {
            var call = ModelFakers.CallFaker.Generate(1)[0];
            var hour = 20;
            call.StartTime = call.StartTime.Next(dayOfWeek).SetHour(hour);
            Assert.Equal(0.20, this.strategy.GetTax(call));
        }

        [Theory]
        [InlineData(DayOfWeek.Sunday)]
        [InlineData(DayOfWeek.Saturday)]
        public void WeekendDaysReturns010(DayOfWeek dayOfWeek)
        {
            var call = ModelFakers.CallFaker.Generate(1)[0];
            call.StartTime = call.StartTime.Next(dayOfWeek);
            Assert.Equal(0.10, this.strategy.GetTax(call));
        }
    }
}