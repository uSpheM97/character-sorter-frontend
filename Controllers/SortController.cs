using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CharacterSorterApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SortController : ControllerBase
    {
        // POST api/sort
        [HttpPost]
        public IActionResult SortCharacters([FromBody] InputModel input)
        {
            if (input == null || string.IsNullOrEmpty(input.Data))
            {
                return BadRequest(new { error = "Invalid input" });
            }

            var sortedArray = input.Data
                                   .ToCharArray()
                                   .OrderBy(c => c)
                                   .ToArray();

            return Ok(new
            {
                word = sortedArray
            });
        }
    }

    // Input model
    public class InputModel
    {
        public string Data { get; set; }
    }
}