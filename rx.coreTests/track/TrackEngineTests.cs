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

            Assert.NotNull(engine);
        }

    }
}