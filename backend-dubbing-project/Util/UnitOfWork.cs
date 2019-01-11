using System;
using System.Threading.Tasks;
using Dubbing.Util;
using Dubbing.Models;
namespace Dubbing.Util
{
    public class UnitOfWork : IDisposable
    {
        private Repository<Performance> performanceRepository;
        private DubbingContext db = new DubbingContext();
        public Repository<Performance>  Performances
        {
            get
            {
                if (performanceRepository == null)
                    performanceRepository = new Repository<Performance>(db);
                return performanceRepository;
            }
        }

        public async Task CommitAsync()
        {
            await db.SaveChangesAsync();
        }
       
        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    db.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
