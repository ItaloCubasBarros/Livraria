﻿using Microsoft.AspNetCore.Mvc;

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
    public class LivroController : ControllerBase
    {
        private readonly AppDbContext _dbcontext;

        public LivroController(AppDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        // GET: api/Livro
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LivroDTO>>> GetLivros()
        {
            return await _dbcontext.Livros.ToListAsync();
        }

        // GET: api/Livro/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LivroDTO>> GetLivroById(int id)
        {
            var livro = await _dbcontext.Livros.FindAsync(id);

            if (livro == null)
            {
                return NotFound();
            }

            return livro;
        }

        // POST: api/Livro
        [HttpPost]
        public async Task<ActionResult<LivroDTO>> PostLivro(LivroDTO livro)
        {
            _dbcontext.Livros.Add(livro);
            await _dbcontext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetLivroById), new { id = livro.Id }, livro);
        }

        // PUT: api/Livro/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLivro(int id, LivroDTO livro)
        {
            if (id != livro.Id)
            {
                return BadRequest();
            }

            _dbcontext.Entry(livro).State = EntityState.Modified;

            try
            {
                await _dbcontext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LivroExists(id))
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

        // DELETE: api/Livro/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLivro(long id)
        {
            var livro = await _dbcontext.Livros.FindAsync(id);
            if (livro == null)
            {
                return NotFound();
            }

            _dbcontext.Livros.Remove(livro);
            await _dbcontext.SaveChangesAsync();

            return NoContent();
        }


        private bool LivroExists(int id)
        {
            return _dbcontext.Livros.Any(e => e.Id == id);
        }
    }
}
