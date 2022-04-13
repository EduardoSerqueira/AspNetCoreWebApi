using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.API.Data;
using SmartSchool.API.Dto;
using SmartSchool.API.Models;
using System.Collections.Generic;
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
        private readonly IMapper _mapper;

        public AlunoController(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var alunos = _repository.GetAllAlunos(true);

            //Retornando com autoMapper
            return Ok(_mapper.Map<IEnumerable<AlunoDto>>(alunos));

            //Retornando com mesmo conceito de autoMapper porém na mão
            //var alunosRetorno = new List<AlunoDto>();

            //foreach (var aluno in alunos)
            //{
            //    alunosRetorno.Add(new AlunoDto()
            //    {
            //        Id = aluno.Id,
            //        Matricula = aluno.Matricula,
            //        Nome = $"{aluno.Nome} {aluno.Sobrenome}",
            //        Telefone = aluno.Telefone,
            //        DataNascimento = aluno.DataNascimento,
            //        DataInicio = aluno.DataInicio,
            //        Ativo = aluno.Ativo
            //    });
            //}
            //return Ok(alunosRetorno);
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
