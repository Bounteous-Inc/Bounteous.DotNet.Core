using System;
using Bounteous.Core.Time;
using FluentAssertions;
using Xunit;

namespace Bounteous.Core.Test.Core.Time
{
    public class TimezoneOffsetTest
    {
        private readonly TimeZoneClock utc;

        public TimezoneOffsetTest() => utc = new TimeZoneClock(TimeZoneInfo.Utc);

        [Fact]
        public void TodayAtPacific() => TimezoneOffset.Pacific.TodayFrom(utc.Today);

        [Fact]
        public void PacificTodayFromUtc() => TimezoneOffset.Pacific.TodayFromUtc();

        [Fact]
        public void TodayAtMountain() => TimezoneOffset.Mountain.TodayFrom(utc.Today);

        [Fact]
        public void MountainTodayFromUtc() => TimezoneOffset.Mountain.TodayFromUtc();

        [Fact]
        public void TodayAtCentral() => TimezoneOffset.Central.TodayFrom(utc.Today);

        [Fact]
        public void CentralTodayFromUtc() => TimezoneOffset.Central.TodayFromUtc();

        [Fact]
        public void TodayAtEastern() => TimezoneOffset.Eastern.TodayFrom(utc.Today);

        [Fact]
        public void EasternFromUtc() => TimezoneOffset.Eastern.TodayFromUtc();

        [Fact]
        public void TodayOverMidnight()
        {
            var utc3Am = new DateTime(2020, 06, 02, 03, 00, 00, DateTimeKind.Utc);
            var mtnToday = TimezoneOffset.Mountain.TodayFrom(utc3Am);
            mtnToday.Should().Be(new DateTime(2020, 06, 01));
        }
    }
}