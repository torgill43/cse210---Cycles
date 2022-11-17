using System.Collections.Generic;
using Unit05.Game.Casting;
using Unit05.Game.Services;


namespace Unit05.Game.Scripting
{
    /// <summary>
    /// <para>An output action that draws all the actors.</para>
    /// <para>The responsibility of DrawActorsAction is to draw each of the actors.</para>
    /// </summary>
    public class DrawActorsAction : Action
    {
        private VideoService _videoService;

        /// <summary>
        /// Constructs a new instance of ControlActorsAction using the given KeyboardService.
        /// </summary>
        public DrawActorsAction(VideoService videoService)
        {
            this._videoService = videoService;
        }

        /// <inheritdoc/>
        public void Execute(Cast cast, Script script)
        {
            List<Actor> segments = new List<Actor>();
            foreach (Cycle cycle in cast.GetActors("cycle"))
            {
                segments.AddRange(cycle.GetSegments());
            }
            Actor fuel = cast.GetFirstActor("fuel");
            List<Actor> messages = cast.GetActors("messages");
            
            _videoService.ClearBuffer();
            _videoService.DrawActors(segments);
            _videoService.DrawActor(fuel);
            _videoService.DrawActors(messages);
            _videoService.FlushBuffer();
        }
    }
}