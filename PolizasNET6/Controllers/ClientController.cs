using Amazon.Auth.AccessControlPolicy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using MongoDB.Bson;
using MongoDB.Driver;
using PolizasNET6.Models;
using PolizasNET6.Repositories;

namespace PolizasNET6.Controllers
{
    
    [Route("Api/[controller]")]
    [ApiController]
    [Authorize]
    public class ClientController : Controller
    {
        private IPolicyCollection db = new PolicyCollection();

        [HttpGet("{placa}")]
        public async Task<IActionResult> GetAllPolicy(string placa)
        {
            DatosPoliza datosPoliza = new DatosPoliza();
            var policy = await db.GetAllPolicy();
            for (int i = 0; i <= policy.Count; i++)
            {
                if(placa == policy[i].PlacaAutomotor)
                {
                    datosPoliza = policy[i];
                    return Ok(datosPoliza);
                }
                else
                {
                    return BadRequest("No se ha encontrado informacion ligada al numero de placa ingresado");
                }
                
            }
            return Ok(await db.GetAllPolicy());
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreatePolicy([FromBody] DatosPoliza datosPoliza)
        {
            if (datosPoliza == null) return BadRequest();
            if (datosPoliza.NombreCliente == string.Empty) { ModelState.AddModelError("NombreCliente", "The Name of client shouldn't be empty");}

            int añosCobertura = datosPoliza.añosDeCoberturaPagados;
            datosPoliza.FechaVigenciaPoliza = datosPoliza.FechaInicioPoliza.AddYears(añosCobertura);
            DateTime fechaActual = DateTime.Now;
            if (datosPoliza.FechaInicioPoliza > fechaActual || datosPoliza.FechaVigenciaPoliza < fechaActual)
            {
                return BadRequest("La póliza no está vigente");
            }

            await db.InsertPolicy(datosPoliza);
            return Created("Created", true);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePolicy([FromBody] DatosPoliza datosPoliza, string id)
        {
            if (datosPoliza == null) return BadRequest();
            if (datosPoliza.NombreCliente == string.Empty) { ModelState.AddModelError("NombreCliente", "The Name of client shouldn't be empty"); }

            datosPoliza.Id = new MongoDB.Bson.ObjectId(id);
            await db.UpdatePolicy(datosPoliza);
            return Created("Update", true);
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePolicy(string id)
        {
            await db.DeletePolicy(id);
            return NoContent(); //success
        }


    }
}
