using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebMotors.Domain;
using WebMotors.Repository.Context;
using WebMotors.Repository.Interface;

namespace WebMotors.Repository.Repository
{
    public class AnuncioRepository : IAnuncioRepository
    {
        private readonly WebMotorsContext _context;

        #region CONSTRUTOR
        public AnuncioRepository(WebMotorsContext context)
        {
            this._context = context;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        #endregion

        #region CONSULTAS GERAIS
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
        #endregion       

        #region CONSULTAS ESPECIFICAS
        public async Task<Anuncio[]> GetAllAnunciosAsync()
        {
            IQueryable<Anuncio> query = _context.Anuncios;

            query = query.AsNoTracking()
                        .OrderBy(c => c.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Anuncio> GetAnuncioAsyncById(int AnuncioId)
        {
            IQueryable<Anuncio> query = _context.Anuncios;

            query = query.AsNoTracking()
                        .OrderBy(c => c.Id)
                        .Where(c => c.Id == AnuncioId);

            return await query.FirstOrDefaultAsync();
        }

        #endregion
    }
}