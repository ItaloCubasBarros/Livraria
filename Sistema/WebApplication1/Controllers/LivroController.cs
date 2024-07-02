using Microsoft.AspNetCore.Mvc;
using app.BE;
using app.Data;
using app.DTO;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Threading.Tasks;

namespace app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivroController : ControllerBase
    {
        private readonly LivroBE _be;
        private readonly AppDbContext _context;
        private readonly AuthBE _auth;

        public LivroController(LivroBE be, AppDbContext context, AuthBE auth)
        {
            _be = be ?? throw new ArgumentNullException(nameof(be), "LivroBE não pode ser nulo.");
            _context = context ?? throw new ArgumentNullException(nameof(context), "AppDbContext não pode ser nulo.");
            _auth = auth ?? throw new ArgumentNullException(nameof(auth), "AuthBE não pode ser nulo.");
        }

        

       


        private string ExtractAuthToken()
        {
            if (HttpContext.Request.Headers.TryGetValue("Authorization", out var authHeader))
            {
                var tokenParts = authHeader.ToString().Split(' ');
                if (tokenParts.Length == 2 && tokenParts[0].Equals("Bearer", StringComparison.OrdinalIgnoreCase))
                {
                    return tokenParts[1].Trim('"');
                }
            }
            return null;
        }

        // GET: api/Livro
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] LivroDTO dto)
        {
            try
            {
                var token = ExtractAuthToken();

                _context.BeginTransaction();
                var response = await _be.GetAll(dto);
                _context.Commit();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Livro/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            try
            {
                var token = ExtractAuthToken();

                _context.BeginTransaction();
                var response = await _be.GetById(id);
                _context.Commit();

                if (response == null)
                {
                    return NotFound();
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/Livro
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] LivroDTO dto)
        {
            try
            {
                var token = ExtractAuthToken();

                _context.BeginTransaction();
                var id = await _be.Insert(dto);
                _context.Commit();
                return CreatedAtAction(nameof(GetById), new { id }, dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Livro/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, [FromBody] LivroDTO dto)
        {
            if (id != dto.Id)
            {
                return BadRequest("O Id do livro no corpo da requisição não corresponde ao Id informado na URL.");
            }

            try
            {
                var token = ExtractAuthToken();

                _context.BeginTransaction();
                var result = await _be.Update(dto);
                _context.Commit();

                if (result == 0)
                {
                    return NotFound();
                }

                return Ok(dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Livro/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                var token = ExtractAuthToken();

                _context.BeginTransaction();
                await _be.Delete(id);
                _context.Commit();

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
