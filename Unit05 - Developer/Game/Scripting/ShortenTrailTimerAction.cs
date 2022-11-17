using System.Collections.Generic;
using Unit05.Game.Casting;

namespace Unit05.Game.Scripting
{
    /// <summary>
    /// An Action that runs a timer and periodically tells the Cycles to shorten
    /// themselves.
    /// </summary>
    public class ShortenTrailTimerAction : Action
    {
        private int _defaultTimer;
        private int _timer;
        /// <summary>
        /// Cosntructs a new instance of ShortenTrailTimerAction
        /// </summary>
        public ShortenTrailTimerAction(int defaultTimer)
        {
            this._defaultTimer = defaultTimer;
            _timer = this._defaultTimer;
        }

        /// <inheritdoc/>
        public void Execute(Cast cast, Script script)
        {
            if (!_TimerIsZero())
            {
                _DecrementTimer();
            }
            else
            {
                foreach (Cycle cycle in cast.GetActors("cycle"))
                {
                    cycle.ShortenTrail();
                }
                _ResetTimer();
            }
        }

        /// <summary>
        /// Decrements the timer by 1.
        /// </summary>
        private void _DecrementTimer()
        {
            _timer -= 1;
        }

        /// <summary>
        /// Tells whether the timer is at 0. Call this before decrementing.
        /// </summary>
        /// <returns>True if the timer is at 0. False if otherwise.</returns>
        private bool _TimerIsZero()
        {
            return _timer == 0;
        }

        /// <summary>
        /// Sets the timer back to the default timer.
        /// </summary>
        private void _ResetTimer()
        {
            _timer = _defaultTimer;
        }
    }
}