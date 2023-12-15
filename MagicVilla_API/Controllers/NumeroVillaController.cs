using AutoMapper;
using MagicVilla_API.Modelos;
using MagicVilla_API.Modelos.Dto;
using MagicVilla_API.Repositorio.IRepositorio;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MagicVilla_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NumeroVillaController : ControllerBase
    {
        public ILogger<NumeroVillaController> Logger;
        private readonly INumeroVillaRepositorio Repositorio;
        private readonly IVillaRepositorio VillaRepositorio;
        private readonly IMapper Mapper;
        private ApiResponse Resultado;
        public NumeroVillaController(ILogger<NumeroVillaController> logger, INumeroVillaRepositorio contexto, IVillaRepositorio villaRepositorio, IMapper mapper)
        {
            Logger = logger;
            Repositorio = contexto;
            VillaRepositorio = villaRepositorio;
            Mapper = mapper;
            Resultado = new ApiResponse();
        }
        [Route("api/GetList")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NumeroVilla>>> GetLista()
        {
            Logger.LogInformation("Obteniendo listado de villas");
            try
            {
                Resultado.Estado = HttpStatusCode.OK;
                Resultado.Objeto = await Repositorio.GetAll();
                Resultado.Exitoso = true;
            }
            catch (Exception ex)
            {
                Resultado.Erores = new List<string>() { ex.ToString() };
            }
            return Ok(Resultado);
        }
        [Route("api/GetById")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<NumeroVilla>> GetById(int id)
        {
            try
            {
                bool valido = id > 0;
                Resultado.Estado = valido ? HttpStatusCode.OK : HttpStatusCode.BadRequest;
                Resultado.Objeto = valido ? await Repositorio.GetById(e => e.Numero == id) : new NumeroVilla();
                Resultado.Exitoso = valido;
            }
            catch (Exception ex)
            {
                Resultado.Estado = HttpStatusCode.NotFound;
                Resultado.Erores = new List<string>() { ex.ToString() };
                Resultado.Exitoso = false;
            }
            return Ok(Resultado);
        }
        [Route("api/Add")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<NumeroVilla>> Add([FromBody] PNumeroVilla numVilla)
        {
            try
            {
                Villa villa = await VillaRepositorio.GetById(x => x.Id.Equals(numVilla.VillaId));
                if (!(numVilla is null) && !(villa is null))
                {
                    NumeroVilla newVilla = Mapper.Map<NumeroVilla>(numVilla);
                    newVilla.FA = DateTime.Now;
                    await Repositorio.Create(newVilla);
                    Resultado.Estado = HttpStatusCode.OK;
                    Resultado.Objeto = newVilla;
                }
            }
            catch (Exception ex)
            {
                Resultado.Estado = HttpStatusCode.NotFound;
                Resultado.Erores = new List<string>() { ex.ToString() };
                Resultado.Exitoso = false;
            }
            return Ok(Resultado);
        }
        [Route("api/Delete/{id:int}")]
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                NumeroVilla res = await Repositorio.GetById(x => x.Numero.Equals(id));
                if (!(res is null))
                {
                    await Repositorio.Delete(res);
                    Resultado.Objeto = new NumeroVilla();
                    Resultado.Estado = HttpStatusCode.OK;
                }
            }
            catch (Exception ex)
            {
                Resultado.Estado = HttpStatusCode.BadRequest;
                Resultado.Erores = new List<string>() { ex.ToString() };
                Resultado.Exitoso = false;
            }
            return Ok(Resultado);
        }
        [Route("api/Edit")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Villa>> Edit([FromBody] PNumeroVilla numeroVilla)
        {
            try
            {
                NumeroVilla res = await Repositorio.GetById(x => x.Numero.Equals(numeroVilla.Numero), false);
                if (!(res is null) || res.Numero.Equals(0) && VillaRepositorio.GetById(x => x.Id.Equals(numeroVilla.VillaId)) is null)
                {
                    res = Mapper.Map<NumeroVilla>(numeroVilla);
                    await Repositorio.Update(res);
                    Resultado.Objeto = await Repositorio.GetById(x => x.Numero.Equals(res.Numero));
                    Resultado.Estado = HttpStatusCode.OK;
                }
            }
            catch (Exception ex)
            {
                Resultado.Estado = HttpStatusCode.BadRequest;
                Resultado.Erores = new List<string>() { ex.ToString() };
                Resultado.Exitoso = false;
            }
            return Ok(Resultado);
        }
    }
}