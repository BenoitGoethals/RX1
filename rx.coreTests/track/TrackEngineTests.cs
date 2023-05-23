using FluentAssertions;
using rx.core.track;
using Xunit;

namespace rx.coreTests.track
{
    public class TrackEngineTests
    {
        [Fact()]
        public void TrackEngineTest()
        {
            TrackEngine engine = new TrackEngine(".\\track\\route.gpx");
            engine.Tracks.Should().HaveCountGreaterThan(0);
        }

    }
}