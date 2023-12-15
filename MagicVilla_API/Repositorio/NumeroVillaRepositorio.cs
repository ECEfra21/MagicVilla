using MagicVilla_API.Datos;
using MagicVilla_API.Modelos;
using MagicVilla_API.Repositorio.IRepositorio;

namespace MagicVilla_API.Repositorio
{
    public class NumeroVillaRepositorio : Repositorio<NumeroVilla>, INumeroVillaRepositorio
    {
        private readonly AplicationDBContext Context;
        public NumeroVillaRepositorio(AplicationDBContext contexto) : base(contexto)
        {
            Context = contexto;
        }
        public async Task<NumeroVilla> Update(NumeroVilla numeroVilla)
        {
            numeroVilla.FUM = DateTime.Now;
            Context.Update(numeroVilla);
            await Context.SaveChangesAsync();
            return numeroVilla;
        }
    }
}
