using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.API.Data;
using SmartSchool.API.Models;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SmartSchool.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessorController : ControllerBase
    {
        private readonly SmartContext _context;
        private readonly IRepository _repository;

        public ProfessorController(SmartContext smartSchoolContext,
                                   IRepository repository)
        {
            _context = smartSchoolContext;
            _repository = repository;
        }

        [HttpGet]
        public ActionResult Get()
        {
            return Ok(_context.Professores);
        }

        [HttpGet("{id:int}")]
        public ActionResult GetById(int id)
        {
            var professor = _context.Professores.FirstOrDefault(a => a.Id == id);
            if (professor == null) return BadRequest("Professor não encontrado!");

            return Ok(professor);
        }

        [HttpGet("nome")]
        public ActionResult GetByName(string nome)
        {
            var aluno = _context.Alunos.FirstOrDefault(a => a.Nome == nome);
            if (aluno == null) return BadRequest("Aluno não encontrado!");

            return Ok(aluno);
        }

        [HttpPost]
        public ActionResult Post(Professor professor)
        {
            _repository.Add(professor);
            if (_repository.Save())
            {
                return Ok(professor);

            }

            return BadRequest("Aluno não cadastrado.");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Professor professor)
        {
            var prof = _context.Professores.AsNoTracking().FirstOrDefault(p => p.Id == id);
            if (prof == null) return BadRequest("Professor não encontrado");

            _repository.Update(professor);

            if (_repository.Save())
            {
                return Ok(professor);

            }

            return BadRequest("Professor não atualizado.");
        }

        [HttpPatch("{id}")]
        public ActionResult Patch(int id, Professor professor)
        {
            var prof = _context.Professores.AsNoTracking().AsNoTracking().FirstOrDefault(p => p.Id == id);
            if (prof == null) return BadRequest("Professor não encontrado");

            _repository.Update(professor);

            if (_repository.Save())
            {
                return Ok(professor);

            }

            return BadRequest("Professor não atualizado.");
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var professor = _context.Professores.AsNoTracking().AsNoTracking().FirstOrDefault(p => p.Id == id);
            if (professor == null) return BadRequest("Professor não encontrado");

            _repository.Remove(professor);

            if (_repository.Save())
            {
                return Ok("Professor deletado");

            }

            return BadRequest("Professor não deletado.");
        }
    }
}
