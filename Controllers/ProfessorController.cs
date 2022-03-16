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
        private readonly IRepository _repository;

        public ProfessorController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var query = _repository.GetAllProfessores(true);
            return Ok(query);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var professor = _repository.GetProfessorById(id, false);
            if (professor == null) return BadRequest("Professor não encontrado!");

            return Ok(professor);
        }

        [HttpPost]
        public IActionResult Post(Professor professor)
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
            var prof = _repository.GetProfessorById(id, false);
            if (prof == null) return BadRequest("Professor não encontrado");

            _repository.Update(professor);

            if (_repository.Save())
            {
                return Ok(professor);

            }

            return BadRequest("Professor não atualizado.");
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Professor professor)
        {
            var prof = _repository.GetProfessorById(id, false);
            if (prof == null) return BadRequest("Professor não encontrado");

            _repository.Update(professor);

            if (_repository.Save())
            {
                return Ok(professor);

            }

            return BadRequest("Professor não atualizado.");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var professor = _repository.GetProfessorById(id, false);
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
