using Unit05.Game.Casting;
using Unit05.Game.Services;


namespace Unit05.Game.Scripting
{
    /// <summary>
    /// <para>An input action that controls the cycle.</para>
    /// <para>
    /// The responsibility of ControlActorsAction is to get the direction and move the cycle.
    /// </para>
    /// </summary>
    public class ControlActorsAction : Action
    {
        private KeyboardService _keyboardService;
        private Point _direction = new Point(Constants.CELL_SIZE, 0);

        /// <summary>
        /// Constructs a new instance of ControlActorsAction using the given KeyboardService.
        /// </summary>
        public ControlActorsAction(KeyboardService keyboardService)
        {
            this._keyboardService = keyboardService;
        }

        /// <inheritdoc/>
        public void Execute(Cast cast, Script script)
        {
            foreach (Cycle cycle in cast.GetActors("cycle"))
            {
                int player = cycle.GetPlayer();
                // left
                if (_keyboardService.IsKeyDown(player == 1 ? "a" : "j"))
                {
                    cycle.TurnHead(new Point(-Constants.CELL_SIZE, 0));
                }
                // right
                if (_keyboardService.IsKeyDown(player == 1 ? "d" : "l"))
                {
                    cycle.TurnHead(new Point(Constants.CELL_SIZE, 0));
                }
                // up
                if (_keyboardService.IsKeyDown(player == 1 ? "w" : "i"))
                {
                    cycle.TurnHead(new Point(0, -Constants.CELL_SIZE));
                }
                // down
                if (_keyboardService.IsKeyDown(player == 1 ? "s" : "k"))
                {
                    cycle.TurnHead(new Point(0, Constants.CELL_SIZE));
                }
            }
        }
    }
}