using System.IO;
using System.Reflection;
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
            TrackEngine engine = new TrackEngine(Assembly.GetExecutingAssembly().GetManifestResourceStream("rx.coreTests.track.route.gpx"));
            DirectoryInfo info = new DirectoryInfo(".");
            var a=info.FullName;
            engine.Tracks.Should().HaveCountGreaterThan(0);
        }

    }
}