using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SoftServe.ITAcademy.BackendDubbingProject.Models;
using SoftServe.ITAcademy.BackendDubbingProject.Utilities;

namespace SoftServe.ITAcademy.BackendDubbingProject.Services
{
    public class PerformanceService : IPerformanceService
    {
        private readonly IRepository<Performance> _performances;

        public PerformanceService(IRepository<Performance> performances)
        {
            _performances = performances;
        }

        public async Task<ICollection<Speech>> GetSpeeches(int id)
        {
            var listOfAllPerformances = await _performances.GetAllItemsAsync();

            var doesNotExist = listOfAllPerformances.All(x => x.Id != id);

            if (doesNotExist)
                return null;

            var performance = await _performances.GetItemAsync(
                id,
                source => source.Include(x => x.Speeches).ThenInclude(y => y.Audios));

            var speeches = performance.Speeches;

            return speeches;
        }

        public async Task<List<Performance>> GetAllPerformances()
        {
            var listOfAllPerformances = await _performances.GetAllItemsAsync();

            return listOfAllPerformances;
        }

        public async Task<Performance> GetPerformanceById(int id)
        {
            var listOfAllPerformances = await _performances.GetAllItemsAsync();

            var doesNotExist = listOfAllPerformances.All(x => x.Id != id);

            if (doesNotExist)
                return null;

            var performance = await _performances.GetItemAsync(id);

            return performance;
        }

        public async Task<Performance> CreatePerformance(Performance performance)
        {
            await _performances.CreateAsync(performance);

            return performance;
        }

        public async Task<Performance> UpdatePerformance(Performance performance)
        {
            var listOfAllPerformances = await _performances.GetAllItemsAsync();

            var doesNotExist = listOfAllPerformances.All(x => x.Id != performance.Id);

            if (doesNotExist)
                return null;

            await _performances.UpdateAsync(performance);

            return performance;
        }

        public async Task<Performance> DeletePerformance(int id)
        {
            var list = await _performances
                .GetAllItemsAsync(source => source.Include(x => x.Speeches)
                    .ThenInclude(y => y.Audios));

            var performance = list.FirstOrDefault(x => x.Id == id);

            if (performance == null)
                return null;

            foreach (var speech in performance.Speeches)
            {
                foreach (var audio in speech.Audios)
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory() + @"\AudioFiles\", audio.FileName);
                    try
                    {
                        if (System.IO.File.Exists(path))
                        {
                            System.IO.File.Delete(path);
                        }
                        else
                        {
                            Console.WriteLine($"File '{path}' not found!");
                        }
                    }
                    catch (IOException ioExc)
                    {
                        Console.WriteLine(ioExc);
                    }
                }
            }

            await _performances.DeleteAsync(performance);

            return performance;
        }
    }
}