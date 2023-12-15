using MagicVilla_API.Datos;
using MagicVilla_API.Modelos;
using MagicVilla_API.Repositorio.IRepositorio;

namespace MagicVilla_API.Repositorio
{
    public class VillaRepositorio : Repositorio<Villa>, IVillaRepositorio
    {
        private readonly AplicationDBContext Context;
        public VillaRepositorio(AplicationDBContext contex) : base(contex)
        {
            Context = contex;
        }
        public async Task<bool> Update(Villa villa)
        {
            villa.FUM = DateTime.Now;
            Context.Update(villa);
            await Context.SaveChangesAsync();
            return true;
        }
    }
}
