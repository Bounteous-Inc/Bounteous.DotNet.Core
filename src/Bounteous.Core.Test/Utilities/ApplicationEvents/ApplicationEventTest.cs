namespace Bounteous.Core.Test.Utilities.ApplicationEvents
{
    // public class ApplicationEventTest : IDisposable
    // {
    //     private const string User = "test user";
    //
    //     public ApplicationEventTest()
    //     {
    //         Clock.Utc.Freeze();
    //     }
    //     
    //     public void Dispose()
    //     {
    //         Clock.Utc.Thaw();
    //     }
    //     
    //     [Fact]
    //     public void ShouldCaptureApplicationEventForAction()
    //     {
    //         var sink = new TestSink();
    //         const string operation = "simple test";
    //         using (var monitor = new MonitorBuilder(sink).Begin(User, operation))
    //         {
    //             monitor.Action(DoStuff);
    //         }
    //
    //         sink.SentEvents.Count.Should().Be(1);
    //         sink.SentEvents.First().Identifier.Should().NotBeNull();
    //         sink.SentEvents.First().User.Should().Be(User);
    //         sink.SentEvents.First().Operation.Should().Be(operation);
    //         sink.SentEvents.First().OperationStep.Should().BeNull();
    //         sink.SentEvents.First().Outcome.Should().Be(Outcome.Successful);
    //         sink.SentEvents.First().Timestamp.Should().Be(Clock.Utc.Now);
    //         sink.SentEvents.First().Duration.Should().Be(0.0);
    //         
    //         Clock.Utc.Thaw();
    //     }
    //
    //     [Fact]
    //     public void ShouldCaptureApplicationEventForActionWithOperationStep()
    //     {
    //         var sink = new TestSink();
    //         const string operation = "simple test";
    //         using (var monitor = new MonitorBuilder(sink).Begin(User, operation))
    //         {
    //             monitor.Action(DoStuff, "this is a step");
    //         }
    //
    //         sink.SentEvents.First().Operation.Should().Be(operation);
    //         sink.SentEvents.First().OperationStep.Should().Be("this is a step");
    //     }
    //
    //     [Fact]
    //     public void ShouldCaptureApplicationEventForFunc()
    //     {
    //         var sink = new TestSink();
    //         const string operation = "func test";
    //         using (var monitor = new MonitorBuilder(sink).Begin(User, operation))
    //         {
    //             var result = monitor.Function(ReturnStuff);
    //             result.Should().BeTrue();
    //         }
    //
    //         sink.SentEvents.Count.Should().Be(1);
    //         sink.SentEvents.First().User.Should().Be(User);
    //         sink.SentEvents.First().Operation.Should().Be(operation);
    //         sink.SentEvents.First().OperationStep.Should().BeNull();
    //         sink.SentEvents.First().Outcome.Should().Be(Outcome.Successful);
    //         sink.SentEvents.First().Timestamp.Should().Be(Clock.Utc.Now);
    //         sink.SentEvents.First().Duration.Should().Be(0.0);
    //     }
    //
    //     [Fact]
    //     public async Task ShouldCaptureApplicationEventForAsyncFunc()
    //     {
    //         var sink = new TestSink();
    //         const string operation = "async func test";
    //         using (var monitor = new MonitorBuilder(sink).Begin(User, operation))
    //         {
    //             var result = await monitor.Function(ReturnStuffAsync);
    //             result.Should().BeTrue();
    //             await monitor.Complete();
    //         }
    //
    //         sink.SentEvents.Count.Should().Be(1);
    //         sink.SentEvents.First().User.Should().Be(User);
    //         sink.SentEvents.First().Operation.Should().Be(operation);
    //         sink.SentEvents.First().OperationStep.Should().BeNull();
    //         sink.SentEvents.First().Outcome.Should().Be(Outcome.Successful);
    //         sink.SentEvents.First().Timestamp.Should().Be(Clock.Utc.Now);
    //         sink.SentEvents.First().Duration.Should().Be(0.0);
    //     }
    //
    //     //@Richard Hurst,  this test keeps failing on CircleCi. not sure why...
    //     // Expected sink.SentEvents.First().Duration to be greater or equal to 2000.0, but found -118720139000.0.
    //     [Fact]
    //     public void ShouldCaptureApplicationEventAndMeasureDuration()
    //     {
    //         Clock.Utc.Thaw();
    //         const int duration = 2000;
    //         var sink = new TestSink();
    //         const string operation = "duration test";
    //         using (var monitor = new MonitorBuilder(sink).Begin(User, operation))
    //         {
    //             var result = monitor.Function(() => ReturnStuff(duration));
    //             result.Should().BeTrue();
    //         }
    //     
    //         sink.SentEvents.Count.Should().Be(1);
    //         var actual = sink.SentEvents.First();
    //         actual.User.Should().Be(User);
    //         actual.Operation.Should().Be(operation);
    //         actual.OperationStep.Should().BeNull();
    //         actual.Outcome.Should().Be(Outcome.Successful);
    //         sink.SentEvents.First().Duration.Should().BeGreaterOrEqualTo(duration);
    //     }
    //
    //      [Fact]
    //      public async Task ShouldCaptureMultipleApplicationEvents()
    //      {
    //          var sink = new TestSink();
    //          const string operation = "multi test";
    //          using (var monitor = new MonitorBuilder(sink).Begin(User, operation))
    //          {
    //              monitor.Action(DoStuff);
    //     
    //              monitor.Function(ReturnStuff).Should().BeTrue();
    //     
    //              (await monitor.Function(ReturnStuffAsync)).Should().BeTrue();
    //     
    //              await monitor.Complete();
    //          }
    //     
    //          sink.SentEvents.Count.Should().Be(3);
    //          sink.SentEvents[0].Operation.Should().Be(operation);
    //          sink.SentEvents[0].OperationStep.Should().Be("1");
    //          sink.SentEvents[1].Operation.Should().Be(operation);
    //          sink.SentEvents[1].OperationStep.Should().Be("2");
    //          sink.SentEvents[2].Operation.Should().Be(operation);
    //          sink.SentEvents[2].OperationStep.Should().Be("3");
    //      }
    //     
    //     [Fact]
    //      public async Task ShouldCaptureMultipleApplicationEventsWithStepNames()
    //      {
    //          var sink = new TestSink();
    //          const string operation = "multi test";
    //          using (var monitor = new MonitorBuilder(sink).Begin(User, operation))
    //          {
    //              monitor.Action(DoStuff, "do stuff");
    //     
    //              monitor.Function(ReturnStuff, "return stuff").Should().BeTrue();
    //     
    //              (await monitor.Function(ReturnStuffAsync, "return stuff async")).Should().BeTrue();
    //     
    //              await monitor.Complete();
    //          }
    //     
    //          sink.SentEvents.Count.Should().Be(3);
    //          sink.SentEvents[0].Operation.Should().Be(operation);
    //          sink.SentEvents[0].OperationStep.Should().Be("do stuff");
    //          sink.SentEvents[1].Operation.Should().Be(operation);
    //          sink.SentEvents[1].OperationStep.Should().Be("return stuff");
    //          sink.SentEvents[2].Operation.Should().Be(operation);
    //          sink.SentEvents[2].OperationStep.Should().Be("return stuff async");
    //      }
    //     
    //      [Fact]
    //      public async Task ShouldCaptureSlowApplicationEvent()
    //      {
    //          Clock.Utc.Thaw();
    //          var sink = new TestSink();
    //          const string operation = "slow test";
    //          using (var monitor = new MonitorBuilder(sink).Begin(User, operation, acceptableDurationMilliseconds: 1000))
    //          {
    //              var result = monitor.Function(() => ReturnStuff(2000));
    //              result.Should().BeTrue();
    //     
    //              await monitor.Complete();
    //          }
    //     
    //          sink.SentEvents.Count.Should().Be(1);
    //          var actual = sink.SentEvents.First();
    //          actual.User.Should().Be(User);
    //          actual.Operation.Should().Be(operation);
    //          actual.Outcome.Should().Be(Outcome.Slow);
    //      }
    //     
    //      [Fact]
    //      public async Task ShouldCaptureFailedApplicationEvent()
    //      {
    //          var sink = new TestSink();
    //          const string operation = "slow test";
    //     
    //          string expectedMessage = null;
    //          try
    //          {
    //              using var monitor = new MonitorBuilder(sink).Begin(User, operation);
    //              monitor.Action(BreakStuff);
    //              await monitor.Complete();
    //          }
    //          catch (ApplicationException e)
    //          {
    //              expectedMessage = e.Message;
    //          }
    //     
    //          sink.SentEvents.Count.Should().Be(1);
    //          var actual = sink.SentEvents.First();
    //          actual.User.Should().Be(User);
    //          actual.Operation.Should().Be(operation);
    //          actual.Outcome.Should().Be(Outcome.Failed);
    //          actual.FailureCause.Should().Be(expectedMessage);
    //      }
    //     
    //     private static void DoStuff()
    //     {
    //     }
    //
    //     private static void BreakStuff()
    //     {
    //         throw new ApplicationException("this is a failure");
    //     }
    //
    //     private static bool ReturnStuff()
    //     {
    //         return true;
    //     }
    //
    //
    //     private static bool ReturnStuff(int delay)
    //     {
    //         Thread.Sleep(delay);
    //         return true;
    //     }
    //
    //     private static Task<bool> ReturnStuffAsync()
    //     {
    //         return Task.FromResult(true);
    //     }
    // }
    //
    // internal class TestSink : IEventSink
    // {
    //     public readonly List<ApplicationEvent> SentEvents = new List<ApplicationEvent>();
    //
    //     public Task SendAsync(ApplicationEvent applicationEvent)
    //     {
    //         SentEvents.Add(applicationEvent);
    //         return Task.CompletedTask;
    //     }
    //
    //     public Task SendAsync(IEnumerable<ApplicationEvent> applicationEvents)
    //     {
    //         SentEvents.AddRange(applicationEvents);
    //         return Task.CompletedTask;
    //     }
    // }
}