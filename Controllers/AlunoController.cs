using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.API.Data;
using SmartSchool.API.Models;
using System.Linq;
using System.Net.Http;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SmartSchool.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunoController : ControllerBase
    {
        private readonly SmartContext _context;
        private readonly IRepository _repository;

        public AlunoController(SmartContext smartSchoolContext, 
                               IRepository repository)
        {
            _context = smartSchoolContext;
            _repository = repository;
        }

        [HttpGet]
        public ActionResult Get()
        {
            var query = _repository.GetAllAlunos();
            return Ok(query);
        }

        //parametro tipado
        [HttpGet("{id:int}")]
        //queryString -> api/aluno/ById? id = 2
        //[HttpGet("ById/{id}")]
        public ActionResult GetById(int id)
        {
            var aluno = _context.Alunos.FirstOrDefault(a => a.Id == id);
            if (aluno == null) return BadRequest("O aluno com o Id: " + id + " não foi encontrado!");

            return Ok(aluno);
        }

        //parametro normal
        //[HttpGet("{name}")]
        //queryString http://localhost:5000/api/aluno/ByName?nome=Marta&sobrenome=Kent
        [HttpGet("ByName")]
        public ActionResult GetByName(string nome, string sobrenome)
        {
            var aluno = _context.Alunos.FirstOrDefault(a => a.Nome.Contains(nome) && a.Sobrenome.Contains(sobrenome));
            if (aluno == null) return BadRequest("O aluno com o nome: " + nome + " não foi encontrado!");

            return Ok(aluno);
        }

        [HttpPost]
        public ActionResult Post(Aluno aluno)
        {
            _repository.Add(aluno);

            if (_repository.Save())
            {
                return Created("teste", aluno);

            }

            return BadRequest("Aluno não cadastrado.");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Aluno aluno)
        {
            var alu = _context.Alunos.AsNoTracking().FirstOrDefault(a => a.Id == id);
            if (alu == null) return BadRequest("Aluno não foi encontrado!");

            _repository.Update(aluno);

            if (_repository.Save())
            {
                return Ok(aluno);

            }

            return BadRequest("Aluno não atualizado.");
        }

        [HttpPatch("{id}")]
        public ActionResult Patch(int id, Aluno aluno)
        {
            var alu = _context.Alunos.AsNoTracking().FirstOrDefault(a => a.Id == id);
            if (alu == null) return BadRequest("Professor não encontrado");

            _repository.Update(aluno);

            if (_repository.Save())
            {
                return Ok(aluno);

            }

            return BadRequest("Professor não atualizado.");
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var aluno = _context.Alunos.AsNoTracking().FirstOrDefault(a => a.Id == id);
            if (aluno == null) return BadRequest("Aluno não encontrado");

            _repository.Remove(aluno);

            if (_repository.Save())
            {
                return Ok("Aluno deletado");

            }

            return BadRequest("Aluno não deletado.");
        }
    }
}
