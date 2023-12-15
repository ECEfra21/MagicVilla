using MagicVilla_API.Datos;
using MagicVilla_API.Repositorio.IRepositorio;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MagicVilla_API.Repositorio
{
    public class Repositorio<T> : IRepositorio<T> where T : class
    {
        private readonly AplicationDBContext Contexto;
        internal DbSet<T> DbSet;
        public Repositorio(AplicationDBContext contexto)
        {
            Contexto = contexto;
            this.DbSet = contexto.Set<T>();
        }
        public async Task Create(T entidad)
        {
            DbSet.Add(entidad);
            await Grabar();
        }
        public async Task Grabar()
        {
            await Contexto.SaveChangesAsync();
        }
        public async Task<T> GetById(Expression<Func<T, bool>>? filtro = null, bool tracked = true)
        {
            IQueryable<T> query = DbSet;
            if (!tracked)
                query = query.AsNoTracking();
            if (filtro != null)
                query = query.Where(filtro);
            return await query.FirstOrDefaultAsync();
        }
        public async Task<List<T>> GetAll(Expression<Func<T, bool>>? filtro = null)
        {
            IQueryable<T> query = DbSet;
            if (!(filtro is null))
                query = query.Where(filtro);
            return await query.ToListAsync();
        }
        public async Task Delete(T endtidad)
        {
            DbSet.Remove(endtidad);
            await Grabar();
        }
    }
}
