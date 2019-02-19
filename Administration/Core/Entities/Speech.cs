using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Entities
{
    public class Speech : BaseEntity
    {
        public string Text { get; set; }

        [NotMapped]
        public int Duration
        {
            get
            {
                if (Audio != null && Audio.Count != 0)
                {
                    return Audio.Max(a => a.Duration);
                }
                else
                {
                    return 0;
                }
            }
        }

        public int PerformanceId { get; set; }

        public Performance Performance { get; set; }

        public ICollection<Audio> Audio { get; set; }

        public Speech(string text, Performance performance, ICollection<Audio> audio)
        {
            Text = text;
            Performance = performance;
            Audio = audio;
        }
    }
}