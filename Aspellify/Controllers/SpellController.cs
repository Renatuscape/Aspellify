using Aspellify.Integrations;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Aspellify.Controllers
{

    [ApiController]
    [Route("api/spells")]
    public class SpellController : ControllerBase //ControllerBase for API, Controller for MVC
    {
        private readonly ILogger<SpellController> _logger;
        private readonly DnD5eAPI _dnD5eAPI;

        public SpellController(ILogger<SpellController> logger, DnD5eAPI dnD5eAPI)
        {
            _logger = logger;
            _dnD5eAPI = dnD5eAPI;
        }


        [HttpGet("/")]
        public IActionResult Index()
        {
            return Ok();//View(nameof(Index));
        }
        [HttpGet("{index}")]
        public async Task<IActionResult> Get(string index)
        {
            var spell = await _dnD5eAPI.GetSpellAsync(index);
            return Ok(spell);
        }

        [HttpGet()]
        public async Task<IActionResult> Get()
        {
            var spells = await _dnD5eAPI.GetSpellsAsync();
            return Ok(spells);
        }
    }
}
