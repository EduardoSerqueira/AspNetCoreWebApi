using SmartSchool.API.Models;

namespace SmartSchool.API.Data
{
    public interface IRepository
    {
        void Add<T>(T Entity) where T : class;
        void Update<T>(T Entity) where T : class;
        void Remove<T>(T Entity) where T : class;
        bool Save();

        Aluno[] GetAllAlunos(bool includeProfessor = false);
        Aluno[] GetAllAunosByDisciplinaId(int disciplinaId, bool includeProfessor = false);
        Aluno GetAlunoById(int alunoId, bool includeProfessor = false);

        Professor[] GetAllProfessores(bool includeAlunos = false);
        Professor[] GetAllProfessoresByDisciplinaId(int disciplinaId, bool includeAlunos = false);
        Professor GetProfessorById(int professorId, bool includeProfessor = false);
    }
}
