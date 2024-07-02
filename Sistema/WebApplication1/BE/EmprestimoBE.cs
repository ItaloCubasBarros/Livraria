using app.DAO; // Certifique-se de que LivroDAO esteja corretamente referenciado
using app.Data;
using app.DTO;
using System.Threading.Tasks;

namespace app.BE
{
    public class EmprestimoBE
    {
        private readonly AppDbContext _context;

        public EmprestimoBE(AppDbContext context)
        {
            _context = context;
        }

        public async Task<EmprestimoDTO> GetById(long id)
        {
            var dao = new EmprestimoDAO(_context);
            return await dao.GetById(id);
        }

        public async Task<List<EmprestimoDTO>> GetAll(EmprestimoDTO dto)
        {
            var dao = new EmprestimoDAO(_context);
            return await dao.GetAll(dto);
        }

        public async Task<int> Insert(EmprestimoDTO dto)
        {
            var dao = new EmprestimoDAO(_context);
            return await dao.Insert(dto);
        }

        public async Task<int> Update(EmprestimoDTO dto)
        {
            var dao = new EmprestimoDAO(_context);
            return await dao.Update(dto);
        }

        public async Task Delete(long id)
        {
            var dao = new EmprestimoDAO(_context);
            await dao.Delete(id);
        }
    }
}
