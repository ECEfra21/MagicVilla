using System.Linq.Expressions;

namespace MagicVilla_API.Repositorio.IRepositorio
{
    public interface IRepositorio<T> where T : class
    {
        Task Create(T entidad);
        Task Delete(T entidad);
        Task<List<T>> GetAll(Expression<Func<T, bool>>? filtro = null);
        Task<T> GetById(Expression<Func<T, bool>> filtro = null, bool track = true);
    }
}
