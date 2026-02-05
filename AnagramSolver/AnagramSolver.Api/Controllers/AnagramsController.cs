using AnagramSolver.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AnagramSolver.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnagramsController : ControllerBase
    {
        private readonly IAnagramSolver _anagramSolver;

        public AnagramsController(IAnagramSolver anagramSolver)
        {
            _anagramSolver = anagramSolver;
        }

        // GET api/<AnagramsController>/5
        [HttpGet("{word}")]
        public async Task<ActionResult<IEnumerable<string>>> Get(string word, CancellationToken ct = default)
        {
            var results = await _anagramSolver.GetAnagramsAsync(word, ct);

            return Ok(results);
        }
    }
}
