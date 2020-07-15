using System.Threading.Tasks;
using WebMotors.Domain;

namespace WebMotors.Repository.Interface
{
    public interface IAnuncioRepository
    {
        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;

        Task<bool> SaveChangesAsync();

        Task<Anuncio[]> GetAllAnunciosAsync();
        Task<Anuncio> GetAnuncioAsyncById(int AnuncioId);
    }
}