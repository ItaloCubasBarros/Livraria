using app.DAO; // Certifique-se de que LivroDAO esteja corretamente referenciado
using app.Data;
using app.DTO;
using System.Threading.Tasks;

namespace app.BE
{
    public class AlunoBE
    {
        private readonly AppDbContext _context;

        public AlunoBE(AppDbContext context)
        {
            _context = context;
        }

        public async Task<AlunoDTO> GetById(long id)
        {
            var dao = new AlunoDAO(_context);
            return await dao.GetById(id);
        }

        public async Task<List<AlunoDTO>> GetAll(AlunoDTO dto)
        {
            var dao = new AlunoDAO(_context);
            return await dao.GetAll(dto);
        }

        public async Task<int> Insert(AlunoDTO dto)
        {
            var dao = new AlunoDAO(_context);
            return await dao.Insert(dto);
        }

        public async Task<int> Update(AlunoDTO dto)
        {
            var dao = new AlunoDAO(_context);
            return await dao.Update(dto);
        }

        public async Task Delete(long id)
        {
            var dao = new AlunoDAO(_context);
            await dao.Delete(id);
        }
    }
}
