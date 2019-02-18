using System.Collections.Generic;

namespace SoftServe.ITAcademy.BackendDubbingProject.ApplicationCore.Entities
{
    public class Speech : BaseEntity
    {
        public string Text { get; set; }

        public int Duration { get; set; }

        public Performance Performance { get; set; }

        public ICollection<Audio> Audio { get; set; }

        public Speech(string text, int performanceId, Performance performance, int duration, ICollection<Audio> audio)
        {
            Text = text;
            Duration = duration;
            Performance = performance;
            Audio = audio;
        }
    }
}