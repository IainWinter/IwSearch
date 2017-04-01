using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IwSearch {
    /// <summary>
    ///     Simple solution for making timers.
    ///     Used to get time in milliseconds since a start time.
    /// </summary>
    public class ElapsedTime {
        /// <summary>
        ///     Start time in milliseconds.
        /// </summary>
        private long start;

        /// <summary>
        ///     Milliseconds since last time MillisSienceUpdate() gets called.
        /// </summary>
        private long currentStart;

        /// <summary>
        ///     Defines start as the current system time in milliseconds.
        /// </summary>
        public ElapsedTime() {
            start = GetCurrentTime();
            currentStart = start;
        }

        /// <summary>
        ///     Total time in milliseconds since start and the current system time.
        /// </summary>
        public long MillisSince() {
            return (long)(DateTime.Now - DateTime.MinValue).TotalMilliseconds - start;
        }

        /// <summary>
        ///     Total time in milliseconds since the last time Update got called.
        /// </summary>
        public long MillisSinceUpdate() {
            return (long)(DateTime.Now - DateTime.MinValue).TotalMilliseconds - currentStart;
        }

        /// <summary>
        ///     Updates current time to the time on the system in milliseconds
        /// </summary>
        public void Update() {
            currentStart = GetCurrentTime();
        }

        /// <summary>
        ///     Get current system time in milliseconds.
        /// </summary>
        private long GetCurrentTime() {
            return (long)(DateTime.Now - DateTime.MinValue).TotalMilliseconds;
        }
    }
}
