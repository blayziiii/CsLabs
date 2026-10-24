
using NUnit.Framework;

namespace sem_1_lab_06_tests
{
    public class MonitorTests
    {
        [Test]
        public void CreateBar_HasTwentyColumns()
        {
            string graph = sem_1_lab_06_realtime_monitor.Program.CreateBar(50);
            Assert.That(graph.Length, Is.EqualTo(20));
        }
    }
}

