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
        public VillaController(ILogger<VillaController> logger, AplicationDBContext contexto)
        {
            Logger = logger;
            Contexto = contexto;
        }
        [Route("/GetList")]
        [HttpGet]
        public ActionResult<IEnumerable<Villa>> GetLista()
        {
            Logger.LogInformation("Obteniendo listado de villas");
            return Ok(Contexto.Villas.ToList()); // new List<Villa> { new Villa() { Id = 1, Nombre = "yo" } }; 
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
            Villa res = Contexto.Villas.ToList().FirstOrDefault(x=> x.Id.Equals(id));//  VillaStore.VillaList.FirstOrDefault(x => x.Id.Equals(id));

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
        public ActionResult<Villa> Add([FromBody] PVilla villa)
        {
            if (villa is null)
                return BadRequest();
            if (villa.Id > 0)
                return StatusCode(StatusCodes.Status500InternalServerError);
            Villa newvilla = new Villa()
            {
                Nombre = villa.Nombre,
                Detalle = villa.Detalle,
                Tarifa = villa.Tarifa,
                Ocupantes = villa.Ocupantes,
                MetrosCuadrados = villa.MetrosCuadrados,
                Amenidad = villa.Amenidad,
                ImageUrl = villa.ImageUrl,
                FA = DateTime.Now
            };

            Contexto.Villas.Add(newvilla);
            Contexto.SaveChanges();
            newvilla.Id = Contexto.Villas.ToList().OrderByDescending(x => x.Id).FirstOrDefault().Id;
            return Ok(newvilla);
        }
        [Route("/Delete/{id:int}")]
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Villa> Delete(int id)
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
        public ActionResult<Villa> Edit([FromBody] PVilla villa)
        {
            Villa res = Contexto.Villas.FirstOrDefault(x => x.Id.Equals(villa.Id));
            if ( res is null || res.Id.Equals(0))
                return NotFound();
            res.Nombre = villa.Nombre;
            res.Detalle = villa.Detalle;
            res.Tarifa = villa.Tarifa;
            res.Ocupantes = villa.Ocupantes;
            res.MetrosCuadrados = villa.MetrosCuadrados;
            res.ImageUrl = villa.ImageUrl;
            res.FUM = DateTime.Now;
            Contexto.Villas.Update(res);
            Contexto.SaveChanges();
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
