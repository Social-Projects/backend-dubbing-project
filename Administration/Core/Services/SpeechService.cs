using System.Threading.Tasks;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Entities;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Interfaces;

namespace SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Services
{
    public class SpeechService : ISpeechService
    {
        private readonly IRepository<Speech> _speechRepository;

        public SpeechService(IRepository<Speech> speechRepository)
        {
            _speechRepository = speechRepository;
        }

        public async Task ReorderSpeech(Speech currentSpeech, int newOrder)
        {

            await _speechRepository.UpdateAsync(new Speech
            {
                Id = currentSpeech.Id,
                Audio = currentSpeech.Audio,
                Duration = currentSpeech.Duration,
                PerformanceId = currentSpeech.PerformanceId,
                Text = currentSpeech.Text,
                Order = newOrder
            });

            if (newOrder < currentSpeech.Order)
            {
                var listSpeech = _speechRepository.List(x => x.Order >= newOrder && x.Order < currentSpeech.Order).Result;
                foreach (var speech in listSpeech)
                {
                    await _speechRepository.UpdateAsync(new Speech
                    {
                        Id = speech.Id,
                        Audio = speech.Audio,
                        Duration = speech.Duration,
                        PerformanceId = speech.PerformanceId,
                        Text = speech.Text,
                        Order = speech.Order + 1
                    });
                }
            }
            else if (newOrder > currentSpeech.Order)
            {
                var listSpeech = _speechRepository.List(x => x.Order > currentSpeech.Order && x.Order <= newOrder).Result;
                foreach (var speech in listSpeech)
                {
                    await _speechRepository.UpdateAsync(new Speech
                    {
                        Id = speech.Id,
                        Audio = speech.Audio,
                        Duration = speech.Duration,
                        PerformanceId = speech.PerformanceId,
                        Text = speech.Text,
                        Order = speech.Order - 1
                    });
                }
            }
        }
    }
}