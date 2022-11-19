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
    /// The responsibility of HandleCollisionsAction is to handle the situation when the cycle 
    /// collides with the food, or the cycle collides with the trail of another cycle, or the game is over.
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
        /// Growls the trail of a cycle if the a cycle collides with a fuel.
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
                    int value = fuel.GetValue();
                    cycle.GrowTail(value);
                    fuel.Reset();
                }
            }
        }

        /// <summary>
        /// Sets the game over flag if a cycle collides with the trail of another cycle.
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
        }

        private void HandleGameOver(Cast cast)
        {
            if (_isGameOver == true)
            {
                Fuel fuel = (Fuel)cast.GetFirstActor("fuel");

                // create a "winner" message
                int x = Constants.MAX_X / 2;
                int y = Constants.MAX_Y / 2;
                Point position = new Point(x, y);

                Actor message = new Actor();
                message.SetText($"Player {_winner} wins!");
                message.SetPosition(position);
                cast.AddActor("messages", message);

                // Later we will make this color the same as winner

                // make loser white
                foreach (Cycle cycle in cast.GetActors("cycle"))
                {
                    if (cycle.GetPlayer() != _winner)
                    {
                        List<Actor> segments = cycle.GetSegments();
                        foreach (Actor segment in segments)
                        {
                            segment.SetColor(Constants.WHITE);
                        }
                    }
                }
                fuel.SetColor(Constants.WHITE);
            }
        }
    }
}