using System.Collections.Generic;
using System.Linq;
using SoftServe.ITAcademy.BackendDubbingProject.Models;

namespace SoftServe.ITAcademy.BackendDubbingProject.Services
{
    public class StreamService : IStreamService
    {
        private int _current;

        public StreamService()
        {
            Speeches = new List<Speech>();
            _current = 0;
        }

        public List<Speech> Speeches { get; set; }

        public Speech CurrentSpeech { get => Speeches[_current]; }

        public bool IsPaused { get; set; }

        public void Load(IEnumerable<Speech> speeches)
        {
            _current = 0;
            Speeches.Clear();
            foreach (var speech in speeches)
            {
                Speeches.Add(speech);
            }
        }

        public void Pause()
        {
            IsPaused = true;
        }

        public void Play()
        {
            IsPaused = false;
        }

        public bool Play(int id)
        {
            var item = Speeches.FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                _current = Speeches.IndexOf(item);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool PlayNext()
        {
            if (Speeches.Count > (_current + 1))
            {
                _current++;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool PlayPrevious()
        {
            if (_current > 0)
            {
                _current--;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}