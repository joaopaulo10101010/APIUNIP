using APIUNIP.Models;
using APIUNIP.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace APIUNIP.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SensorController : Controller
    {
        [HttpGet]
        public IActionResult GetModel()
        {
            try
            {
                return Ok(new
                {
                    sensor = "LDR (LDR, MQ135, BMP280, DHT22)",
                    valorColetado = 750.5,
                    valorProcessado = 72.35,
                    status = "GELADO"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    erro = ex.Message
                });
            }
        }

        [HttpGet("lista")]
        public IActionResult GetList(string sensor)
        {
            List<SensorDB> list = new List<SensorDB>();

            try
            {
                SensorRepositorio rep = new SensorRepositorio();

                list = rep.Select(sensor);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    erro = ex.Message
                });
            }

            return Ok(list);
        }
        [HttpGet("listaMedia")]
        public IActionResult GetListMedia(string sensor)
        {
            List<SensorDB> list = new List<SensorDB>();

            try
            {
                SensorRepositorio rep = new SensorRepositorio();

                list = rep.SelectMedia(sensor);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    erro = ex.Message
                });
            }

            return Ok(list);
        }

        [HttpPost("Insert")]
        public IActionResult Gravar([FromBody] SensorDB sensors)
        {
            try
            {
                SensorDB sensor = new SensorDB();

                sensor.Sensor = sensors.Sensor.ToUpper();
                sensor.VALORCOLETADO = sensors.VALORCOLETADO;

                sensor.STATUS = "OK";

                SensorRepositorio rep = new SensorRepositorio();

                rep.Insert(sensor);

                return Ok(sensor);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    erro = ex.Message
                });
            }
        }
    }
}