﻿using AutoMapper;
using MagicVilla_API.Modelos;
using MagicVilla_API.Modelos.Dto;
using MagicVilla_API.Repositorio;
using MagicVilla_API.Repositorio.IRepositorio;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Linq.Expressions;
using System.Net;

namespace MagicVilla_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaController : ControllerBase
    {
        public ILogger<VillaController> Logger;
        private readonly IVillaRepositorio Repositorio;
        private readonly IMapper Mapper;
        private ApiResponse Resultado;
        public VillaController(ILogger<VillaController> logger, IVillaRepositorio contexto, IMapper mapper)
        {
            Logger = logger;
            Repositorio = contexto;
            Mapper = mapper;
            Resultado = new ApiResponse();
        }
        [Route("api/GetList")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Villa>>> GetLista()
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
        [HttpGet]//("/GetById/{id:int}")
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Villa>> GetById(int id)
        {
            try
            {
                bool valido = id > 0;
                Resultado.Estado = valido ? HttpStatusCode.OK : HttpStatusCode.BadRequest;
                Resultado.Objeto = valido ? await Repositorio.GetById(e => e.Id == id) : new Villa();
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
        public async Task<ActionResult<Villa>> Add([FromBody] PVilla villa)
        {
            try
            {
                if (!(villa is null))
                {
                    bool valido = villa.Id == 0;
                    Villa newvilla = Mapper.Map<Villa>(villa);
                    newvilla.FA = DateTime.Now;
                    await Repositorio.Create(newvilla);
                    Resultado.Estado = HttpStatusCode.OK;
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
                Villa res = await Repositorio.GetById(x => x.Id == id);
                if (!(res is null))
                {
                    await Repositorio.Delete(res);
                    Resultado.Objeto = new Villa();
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
        public async Task<ActionResult<Villa>> Edit([FromBody] PVilla villa)
        {
            try
            {
                Villa res = await Repositorio.GetById(x => x.Id.Equals(villa.Id), false);
                if (!(res is null) || res.Id.Equals(0))
                {
                    res = Mapper.Map<Villa>(villa);
                    await Repositorio.Update(res);
                    Resultado.Objeto = await Repositorio.GetById(x => x.Id.Equals(res.Id));
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