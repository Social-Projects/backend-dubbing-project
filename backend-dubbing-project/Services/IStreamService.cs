using System.Collections.Generic;
using SoftServe.ITAcademy.BackendDubbingProject.Models;

namespace SoftServe.ITAcademy.BackendDubbingProject.Services
{
    public interface IStreamService
    {
        List<Speech> Speeches { get; set; }

        Speech CurrentSpeech { get; }

        void Load(IEnumerable<Speech> speeches);

        bool IsPaused { get; set; }

        bool PlayNext();

        bool PlayPrevious();

        void Play();

        bool Play(int id);

        void Pause();
    }
}