using Microsoft.AspNetCore.Mvc;
using WoodPelletsLib;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RestWoodPellets.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WoodPelletsController : ControllerBase
    {
        private WoodPelletRepository _woodPelletRepository;
        public WoodPelletsController(WoodPelletRepository woodPelletRepository)
        {
            _woodPelletRepository = woodPelletRepository;
        }

        // GET: api/<WoodPelletsController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<WoodPellet>> Get()
        {
            return Ok(_woodPelletRepository.GetAll());
        }

        // GET api/<WoodPelletsController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<WoodPellet> Get(int id)
        {
            WoodPellet? woodPellet = _woodPelletRepository.GetById(id);
            if(woodPellet == null) 
            {
                return NotFound();
            }
            return Ok(woodPellet);
        }

        // POST api/<WoodPelletsController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Post([FromBody] WoodPellet newWoodPellet)
        {
            try
            {
                _woodPelletRepository.Add(newWoodPellet);
                return Ok();
            }
            catch(Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<WoodPelletsController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Put(int id, [FromBody] WoodPellet updateWoodPellet)
        {
            try
            {
                if(_woodPelletRepository.GetById(id) == null) 
                {
                    return NotFound();
                }
                _woodPelletRepository.Update(updateWoodPellet, id);
                return Ok();
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
