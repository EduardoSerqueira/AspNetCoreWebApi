using SmartSchool.API.Models;

namespace SmartSchool.API.Data
{
    public interface IRepository
    {
        void Add<T>(T Entity) where T : class;
        void Update<T>(T Entity) where T : class;
        void Remove<T>(T Entity) where T : class;
        bool Save();

        Aluno[] GetAllAlunos();
        Aluno[] GetAllAunosByDisciplinaId();
        Aluno GetAlunoById();

        Professor[] GetAllProfessores();
        Professor[] GetAllProfessoresByDisciplinaId();
        Professor GetProfessorById();
    }
}
