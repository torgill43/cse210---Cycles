using System;


namespace Unit05.Game.Casting
{
    /// <summary>
    /// <para>A tasty item that snakes like to eat.</para>
    /// <para>
    /// The responsibility of Fuel is to select a random position and value that it's worth.
    /// </para>
    /// </summary>
    public class Fuel : Actor
    {
        private int _value = 0;

        /// <summary>
        /// Constructs a new instance of an Food.
        /// </summary>
        public Fuel()
        {
            SetText("@");
            SetColor(Constants.RED); 
            Reset();
        }

        /// <summary>
        /// Gets the value this fuel is worth.
        /// </summary>
        /// <returns>The value.</returns>
        public int GetValue()
        {
            return _value;
        }

        /// <summary>
        /// Selects a random position and value that the food is worth.
        /// </summary>
        public void Reset()
        {
            Random random = new Random();
            _value = random.Next(9);
            int x = random.Next(Constants.COLUMNS);
            int y = random.Next(Constants.ROWS);
            Point position = new Point(x, y);
            position = position.Scale(Constants.CELL_SIZE);
            SetPosition(position);
        }
    }
}