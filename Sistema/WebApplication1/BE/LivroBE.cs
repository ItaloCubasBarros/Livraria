using app.DAO; // Certifique-se de que LivroDAO esteja corretamente referenciado
using app.Data;
using app.DTO;
using System.Threading.Tasks;

namespace app.BE
{
    public class LivroBE
    {
        private readonly AppDbContext _context;

        public LivroBE(AppDbContext context)
        {
            _context = context;
        }

        public async Task<LivroDTO> GetById(long id)
        {
            var dao = new LivroDAO(_context);
            return await dao.GetById(id);
        }

        public async Task<List<LivroDTO>> GetAll(LivroDTO dto)
        {
            var dao = new LivroDAO(_context);
            return await dao.GetAll(dto);
        }

        public async Task<int> Insert(LivroDTO dto)
        {
            var dao = new LivroDAO(_context);
            return await dao.Insert(dto);
        }

        public async Task<int> Update(LivroDTO dto)
        {
            var dao = new LivroDAO(_context);
            return await dao.Update(dto);
        }

        public async Task Delete(long id)
        {
            var dao = new LivroDAO(_context);
            await dao.Delete(id);
        }
    }
}
