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
        private readonly IRepository _repository;

        public AlunoController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var query = _repository.GetAllAlunos(true);
            return Ok(query);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var aluno = _repository.GetAlunoById(id, false);
            return Ok(aluno);
        }

        [HttpPost]
        public IActionResult Post(Aluno aluno)
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
            var alu = _repository.GetAlunoById(id);
            if (alu == null) return BadRequest("Aluno não foi encontrado!");

            _repository.Update(aluno);

            if (_repository.Save())
            {
                return Ok(aluno);

            }

            return BadRequest("Aluno não atualizado.");
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Aluno aluno)
        {
            var alu = _repository.GetAlunoById(id);
            if (alu == null) return BadRequest("Professor não encontrado");

            _repository.Update(aluno);

            if (_repository.Save())
            {
                return Ok(aluno);

            }

            return BadRequest("Professor não atualizado.");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var aluno = _repository.GetAlunoById(id);
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
