using System;
using System.Threading.Tasks;
using Bounteous.Core.Utilities;
using FluentAssertions;
using Xunit;

namespace Bounteous.Core.Test.Utilities
{
    public class EndeavorTests
    {
        private int timesFailed;

        public EndeavorTests()
        {
            timesFailed = 0;
        }

        [Fact]
        public void Success()
        {
            Endeavor.Go<PlayItAgainException>(() => Go(),
                e => throw new Exception("should not be here"));
            timesFailed.Should().Be(0);
        }

        [Fact]
        public async Task FailsOnce()
        {
            await Endeavor.Go<PlayItAgainException>(async () => await Go(timesFailed == 0),
                e => CountFails(), 3, 50);
            timesFailed.Should().Be(1);
        }

        [Fact]
        public async Task FailsAFewTimes()
        {
            await Endeavor.Go<PlayItAgainException>(async () => await Go(timesFailed <= 3),
                async e => await CountFails(), 5, 50);
            timesFailed.Should().Be(4);
        }

        [Fact]
        public async Task AlwaysFails()
        {
            try
            {
                await Endeavor.Go<PlayItAgainException>(async () => await Bork(),
                    async e => await CountFails(), 3, 50);
                throw new Exception($"didn't catch an {nameof(PlayItAgainException)}");
            }
            catch (PlayItAgainException ex)
            {
                ex.Should().NotBeNull();
            }

            timesFailed.Should().Be(3);
        }

        private async Task<bool> CountFails()
        {
            timesFailed += 1;
            return await Task.FromResult(true);
        }

        private static Task Bork()
        {
            throw new PlayItAgainException("ka-ka");
        }

        private static Task Go(bool forceFail = false)
        {
            if (forceFail) Bork();
            return Task.CompletedTask;
        }
    }

    internal class PlayItAgainException : Exception
    {
        public PlayItAgainException(string message) : base(message)
        {
        }
    }
}