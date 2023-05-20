using rx.core.track;
using Xunit;

namespace rx.coreTests.track
{
    public class TrackEngineTests
    {
        [Fact()]
        public void TrackEngineTest()
        {
            TrackEngine engine = new TrackEngine("C:\\Users\\benoit\\source\\repos\\RX1\\rx.coreTests\\track\\route.gpx");

            Assert.NotNull(engine);
        }

    }
}