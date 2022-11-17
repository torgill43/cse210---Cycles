using System;
using System.Collections.Generic;
using System.Data;
using Unit05.Game.Casting;
using Unit05.Game.Services;


namespace Unit05.Game.Scripting
{
    /// <summary>
    /// <para>An update action that handles interactions between the actors.</para>
    /// <para>
    /// The responsibility of HandleCollisionsAction is to handle the situation when the snake 
    /// collides with the food, or the snake collides with its segments, or the game is over.
    /// </para>
    /// </summary>
    public class HandleCollisionsAction : Action
    {
        private bool _isGameOver = false;
        private int _winner;

        /// <summary>
        /// Constructs a new instance of HandleCollisionsAction.
        /// </summary>
        public HandleCollisionsAction()
        {
        }

        /// <inheritdoc/>
        public void Execute(Cast cast, Script script)
        {
            if (_isGameOver == false)
            {
                HandleFoodCollisions(cast);
                HandleSegmentCollisions(cast);
                HandleGameOver(cast);
            }
        }

        /// <summary>
        /// Updates the score nd moves the food if the snake collides with it.
        /// </summary>
        /// <param name="cast">The cast of actors.</param>
        private void HandleFoodCollisions(Cast cast)
        {
            List<Actor> cycles = cast.GetActors("cycle");
            Fuel fuel = (Fuel)cast.GetFirstActor("fuel");
            
            foreach (Cycle cycle in cycles)
            {
                if (cycle.GetHead().GetPosition().Equals(fuel.GetPosition()))
                {
                    int points = fuel.GetValue();
                    cycle.GrowTail(points);
                    fuel.Reset();
                }
            }
        }

        /// <summary>
        /// Sets the game over flag if the snake collides with one of its segments.
        /// </summary>
        /// <param name="cast">The cast of actors.</param>
        private void HandleSegmentCollisions(Cast cast)
        {
            List<Actor> cycles = cast.GetActors("cycle");
            foreach (Cycle cycle in cycles)
            {
                int player = cycle.GetPlayer();
                Actor head = cycle.GetHead();
                Cycle otherCycle = (Cycle)cycles[2 - player];
                List<Actor> otherBody = otherCycle.GetBody();
                foreach (Actor segment in otherBody)
                {
                    if (segment.GetPosition().Equals(head.GetPosition()))
                    {
                        _winner = 3 - player;
                        _isGameOver = true;
                    }
                }
            }



            // Cycle cycle = (Cycle)cast.GetFirstActor("cycle");
            // Actor head = cycle.GetHead();
            // List<Actor> body = cycle.GetBody();

            // foreach (Actor segment in body)
            // {
            //     if (segment.GetPosition().Equals(head.GetPosition()))
            //     {
            //         _isGameOver = true;
            //     }
            // }
        }

        private void HandleGameOver(Cast cast)
        {
            if (_isGameOver == true)
            {
                List<Actor> segments = new List<Actor>();
                foreach (Cycle cycle in cast.GetActors("cycle"))
                {
                    segments.AddRange(cycle.GetSegments());
                }
                Fuel fuel = (Fuel)cast.GetFirstActor("fuel");

                // create a "game over" message
                int x = Constants.MAX_X / 2;
                int y = Constants.MAX_Y / 2;
                Point position = new Point(x, y);

                Actor message = new Actor();
                message.SetText("Game Over!");
                message.SetPosition(position);
                cast.AddActor("messages", message);

                // make everything white
                foreach (Actor segment in segments)
                {
                    segment.SetColor(Constants.WHITE);
                }
                fuel.SetColor(Constants.WHITE);
            }
        }

    }
}