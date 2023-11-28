using AutoMapper;
using MagicVilla_API.Datos;
using MagicVilla_API.Modelos;
using MagicVilla_API.Modelos.Dto;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaController : ControllerBase
    {
        public ILogger<VillaController> Logger;
        private readonly AplicationDBContext Contexto;
        private readonly IMapper Mapper;
        public VillaController(ILogger<VillaController> logger, AplicationDBContext contexto, IMapper mapper)
        {
            Logger = logger;
            Contexto = contexto;
            Mapper = mapper;
        }
        [Route("/GetList")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Villa>>> GetLista()
        {
            Logger.LogInformation("Obteniendo listado de villas");
            IEnumerable<Villa> lista = Contexto.Villas.ToList();
            return Ok(Mapper.Map<IEnumerable<Villa>>(lista)); // new List<Villa> { new Villa() { Id = 1, Nombre = "yo" } }; 
        }
        //[Route("/GetById")]
        [HttpGet("/GetById/{id:int}")]//("id:int", Name = "GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Villa> GetById(int id)
        {
            if (id.Equals(0))
                return BadRequest();
            Villa res = Contexto.Villas.ToList().FirstOrDefault(x => x.Id.Equals(id));//  VillaStore.VillaList.FirstOrDefault(x => x.Id.Equals(id));
            if (res is null)
                return NotFound();
            else
                return Ok(res);
        }
        [Route("/Add")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Villa>> Add([FromBody] PVilla villa)
        {
            if (villa is null)
                return BadRequest();
            if (villa.Id > 0)
                return StatusCode(StatusCodes.Status500InternalServerError);
            Villa newvilla = Mapper.Map<Villa>(villa);
            newvilla.FA = DateTime.Now;
            Contexto.Villas.AddAsync(newvilla);
            await Contexto.SaveChangesAsync();
            newvilla.SetId(Contexto.Villas.ToList().OrderByDescending(x => x.Id).FirstOrDefault().Id);
            return Ok(newvilla);
        }
        [Route("/Delete/{id:int}")]
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Villa>> Delete(int id)
        {
            Villa res = Contexto.Villas.FirstOrDefault(x => x.Id.Equals(id));
            if (id.Equals(0) || res is null)
                return NotFound();
            Contexto.Villas.Remove(res);
            Contexto.SaveChanges();
            return NoContent();
        }
        [Route("/Edit")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Villa>> Edit([FromBody] PVilla villa)
        {
            Villa res = Contexto.Villas.FirstOrDefault(x => x.Id.Equals(villa.Id));
            if ( res is null || res.Id.Equals(0))
                return NotFound();
            Villa result = Mapper.Map<Villa>(villa);
            res.FUM = DateTime.Now;
            res.SetId(villa.Id);
            Contexto.Villas.Update(res);
            Contexto.SaveChangesAsync();
            return NoContent();
        }
        /*[Route("/Edit/{field}")]
        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<PVilla> EditPatch(int id, JsonPatchDocument<PVilla> villa)
        {
            if (villa is null || id.Equals(0))
                return BadRequest();
            PVilla res = VillaStore.VillaList.asNotracking().FirstOrDefault(x => x.Id.Equals(id)); / agregar referencia

            villa.ApplyTo(res, ModelState);
            if(!ModelState.IsValid)
                return BadRequest();
            return NoContent();
        }
*/
    }
}