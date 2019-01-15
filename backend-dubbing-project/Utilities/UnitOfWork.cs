using System;
using System.Threading.Tasks;
using SoftServe.ITAcademy.BackendDubbingProject.Models;

namespace SoftServe.ITAcademy.BackendDubbingProject.Utilities
{
    public class UnitOfWork : IDisposable
    {
        private readonly DubbingContext _db = new DubbingContext();
        private bool _disposedValue = false; // To detect redundant calls
        private Repository<Performance> _performanceRepository;

        public Repository<Performance> Performances => _performanceRepository ?? (_performanceRepository = new Repository<Performance>(_db));

        public async Task CommitAsync()
        {
            await _db.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _db.Dispose();
                }

                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
