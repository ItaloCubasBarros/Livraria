using Microsoft.AspNetCore.Mvc;

using app.Data;
using app.DTO;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunoController : ControllerBase
    {
        private readonly AppDbContext _dbcontext;

        public AlunoController(AppDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        // GET: api/Aluno
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AlunoDTO>>> GetAlunos()
        {
            return await _dbcontext.Alunos.ToListAsync();
        }

        // GET: api/Aluno/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AlunoDTO>> GetAlunoById(int id)
        {
            var Aluno = await _dbcontext.Alunos.FindAsync(id);

            if (Aluno == null)
            {
                return NotFound();
            }

            return Aluno;
        }

        // POST: api/Aluno
        [HttpPost]
        public async Task<ActionResult<AlunoDTO>> PostAluno(AlunoDTO Aluno)
        {
            _dbcontext.Alunos.Add(Aluno);
            await _dbcontext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAlunoById), new { id = Aluno.Id }, Aluno);
        }

        // PUT: api/Aluno/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAluno(int id, AlunoDTO Aluno)
        {
            if (id != Aluno.Id)
            {
                return BadRequest();
            }

            _dbcontext.Entry(Aluno).State = EntityState.Modified;

            try
            {
                await _dbcontext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlunoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Aluno/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAluno(int id)
        {
            var Aluno = await _dbcontext.Alunos.FindAsync(id);
            if (Aluno == null)
            {
                return NotFound();
            }

            _dbcontext.Alunos.Remove(Aluno);
            await _dbcontext.SaveChangesAsync();

            return NoContent();
        }

        private bool AlunoExists(int id)
        {
            return _dbcontext.Alunos.Any(e => e.Id == id);
        }
    }
}
