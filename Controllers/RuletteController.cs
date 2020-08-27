using Microsoft.AspNetCore.Mvc;
using rulete.Models;
using rulete.Persistence.IRepository;

namespace rulete.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RuletteController : ControllerBase
    {
        private readonly IRuletteRepository RuletteRepository;
        public RuletteController(IRuletteRepository ruletteRepository)
        {
            RuletteRepository = ruletteRepository;
        }
        [HttpPost]
        [Route("GetObjRulette")]
        public IActionResult GetObjRulette([FromBody] RuletteModel entidad)
        {
            return Ok(RuletteRepository.GetObjRulettes(entidad));
        }
        [HttpPost]
        [Route("GetRulette")]
        public IActionResult GetRulette()
        {
            return Ok(RuletteRepository.GetRulettes());
        }
        [HttpPost]
        [Route("CreateRulette")]
        public IActionResult CreateRulette([FromBody] RuletteModel entidad)
        {
            return Ok(RuletteRepository.CreateRulette(entidad));
        }
        [HttpPut]
        [Route("OpenRulette")]
        public IActionResult OpenRulette([FromBody] RuletteModel entidad)
        {
            return Ok(RuletteRepository.OpenRulette(entidad));
        }
        [HttpPost]
        [Route("OpenBet")]
        public IActionResult OpenBet([FromBody] RuletteModel entidadRulette, [FromBody] BetModel entidadBet, [FromBody] GamblerModel entidadGambler)
        {
            return Ok(RuletteRepository.OpenBet(entidadRulette, entidadBet, entidadGambler));
        }
        [HttpPut]
        [Route("ClosedRulette")]
        public IActionResult ClosedRulette([FromBody] RuletteModel entidad)
        {
            return Ok(RuletteRepository.ClosedRulette(entidad));
        }
    }
}
