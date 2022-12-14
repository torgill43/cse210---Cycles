using System;
using System.Collections.Generic;
using System.Linq;


namespace Unit05.Game.Casting
{
    /// <summary>
    /// <para>A cycle with its trail following behind.</para>
    /// <para>The responsibility of Cycle is to move itself.</para>
    /// </summary>
    public class Cycle : Actor
    {
        private List<Actor> _segments = new List<Actor>();
        private int _player;

        /// <summary>
        /// Constructs a new instance of a Cycle.
        /// </summary>
        public Cycle(int player)
        {
            this._player = player;
            PrepareCycle();
        }

        /// <summary>
        /// Gets the cycle's segments.
        /// </summary>
        /// <returns>The cycle segments in a List.</returns>
        public List<Actor> GetBody()
        {
            return new List<Actor>(_segments.Skip(1).ToArray());
        }

        /// <summary>
        /// Gets the cycle's first segment (Essentially the head).
        /// </summary>
        /// <returns>The first segment as an instance of Actor.</returns>
        public Actor GetHead()
        {
            return _segments[0];
        }

        /// <summary>
        /// Gets the cycle's segments (including the head).
        /// </summary>
        /// <returns>A list of cycle segments as instances of Actors.</returns>
        public List<Actor> GetSegments()
        {
            return _segments;
        }

        /// <summary>
        /// Grows the cycle's trail by the given number of segments.
        /// </summary>
        /// <param name="numberOfSegments">The number of segments to grow.</param>
        public void GrowTail(int numberOfSegments)
        {
            for (int i = 0; i < numberOfSegments; i++)
            {
                Actor trail = _segments.Last<Actor>();
                Point velocity = trail.GetVelocity();
                Point offset = velocity.Reverse();
                Point position = trail.GetPosition().Add(offset);

                Actor segment = new Actor();
                segment.SetPosition(position);
                segment.SetVelocity(velocity);
                segment.SetText("#");
                segment.SetColor(_player == 1 ? Constants.CYAN : Constants.RED);
                _segments.Add(segment);
            }
        }

        /// <inheritdoc/>
        public override void MoveNext()
        {
            foreach (Actor segment in _segments)
            {
                segment.MoveNext();
            }

            for (int i = _segments.Count - 1; i > 0; i--)
            {
                Actor trailing = _segments[i];
                Actor previous = _segments[i - 1];
                Point velocity = previous.GetVelocity();
                trailing.SetVelocity(velocity);
            }
        }

        /// <summary>
        /// Turns the head of the cycle in the given direction.
        /// </summary>
        /// <param name="direction">The given direction.</param>
        public void TurnHead(Point direction)
        {
            _segments[0].SetVelocity(direction);
        }

        /// <summary>
        /// Prepares the cycle's trail for moving.
        /// </summary>
        private void PrepareCycle()
        {
            Random random = new Random();
            int rand_x = random.Next(1, Constants.MAX_X);
            rand_x -= rand_x % Constants.CELL_SIZE;
            int x = rand_x;
            int y = Constants.MAX_Y / 2;

            for (int i = 0; i < Constants.CYCLE_LENGTH; i++)
            {
                Point position = new Point(x, y - i * Constants.CELL_SIZE);
                Point velocity = new Point(0, 1 * Constants.CELL_SIZE);
                string text = (i == 0 ? "8" : "#");
                Color color = (_player == 1 ? Constants.CYAN : Constants.RED);

                Actor segment = new Actor();
                segment.SetPosition(position);
                segment.SetVelocity(velocity);
                segment.SetText(text);
                segment.SetColor(color);
                _segments.Add(segment);
            }
        }

        /// <summary>
        /// Shortens the trail by 1.
        /// </summary>
        public void ShortenTrail()
        {
            if (GetBody().Count > 3)
                _segments.RemoveAt(_segments.Count - 1);
        }

        /// <summary>
        /// Gets the player number
        /// </summary>
        /// <returns>The player's number</returns>
        public int GetPlayer()
        {
            return _player;
        }
    }
}