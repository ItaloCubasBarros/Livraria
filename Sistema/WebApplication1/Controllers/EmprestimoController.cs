using app.Data;
using app.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmprestimoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EmprestimoController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Emprestimo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmprestimoDTO>>> GetEmprestimos()
        {
            return await _context.Emprestimos
                .Select(e => new EmprestimoDTO
                {
                    Id = e.Id,

                    Aluno = e.Aluno,
                    Livro = e.Livro
                })
                .ToListAsync();
        }

        // GET: api/Emprestimo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmprestimoDTO>> GetEmprestimoById(long id)
        {
            var emprestimo = await _context.Emprestimos.FindAsync(id);

            if (emprestimo == null)
            {
                return NotFound();
            }

            return new EmprestimoDTO
            {
                Id = emprestimo.Id,
                Aluno = emprestimo.Aluno,
                Livro = emprestimo.Livro
            };
        }

        [HttpPost]
        public async Task<ActionResult<EmprestimoDTO>> PostEmprestimo(EmprestimoDTO emprestimoDTO)
        {
            try
            {
                var aluno = await _context.Alunos.FindAsync(emprestimoDTO.Aluno.Id);
                var livro = await _context.Livros.FindAsync(emprestimoDTO.Livro.Id);



                if (emprestimoDTO.Aluno == null || emprestimoDTO.Livro == null)
                {
                    return BadRequest("Aluno ou livro não selecionado.");
                }

                var novoEmprestimo = new EmprestimoDTO
                {
                    Aluno = aluno,
                    Livro = livro
                };

                _context.Emprestimos.Add(novoEmprestimo);
                await _context.SaveChangesAsync();

                // Criar um EmprestimoDTO para retornar com os dados corretos
                var emprestimoDtoToReturn = new EmprestimoDTO
                {
                    Id = novoEmprestimo.Id,
                    Aluno = new AlunoDTO
                    {
                        Id = novoEmprestimo.Aluno.Id,
                        Nome = novoEmprestimo.Aluno.Nome,
                        CPF = novoEmprestimo.Aluno.CPF,
                        Email = novoEmprestimo.Aluno.Email,
                        Telefone = novoEmprestimo.Aluno.Telefone,
                        Endereco = novoEmprestimo.Aluno.Endereco,
                        Sexo = novoEmprestimo.Aluno.Sexo
                        // Adicione outras propriedades de AlunoDTO conforme necessário
                    },
                    Livro = new LivroDTO
                    {
                        Id = novoEmprestimo.Livro.Id,
                        Titulo = novoEmprestimo.Livro.Titulo,
                        Autor = novoEmprestimo.Livro.Autor,
                        Genero = novoEmprestimo.Livro.Genero
                        // Adicione outras propriedades de LivroDTO conforme necessário
                    }
                };

                return CreatedAtAction(nameof(GetEmprestimoById), new { id = novoEmprestimo.Id }, emprestimoDtoToReturn);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }




        // PUT: api/Emprestimo/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmprestimo(long id, EmprestimoDTO emprestimoDTO)
        {
            if (id != emprestimoDTO.Id)
            {
                return BadRequest();
            }

            var emprestimo = await _context.Emprestimos.FindAsync(id);
            if (emprestimo == null)
            {
                return NotFound();
            }

            emprestimo.Aluno = emprestimoDTO.Aluno ?? emprestimo.Aluno;
            emprestimo.Livro = emprestimoDTO.Livro ?? emprestimo.Livro;

            try
            {
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Emprestimo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmprestimo(long id)
        {
            var emprestimo = await _context.Emprestimos.FindAsync(id);
            if (emprestimo == null)
            {
                return NotFound();
            }

            _context.Emprestimos.Remove(emprestimo);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }

}