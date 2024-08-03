using Long.Database.Entities;
using Long.Kernel.Database;

namespace Long.Module.Competion.Repositories
{
    public static class QuizRepository
    {
        public static IList<DbQuiz> Get()
        {
            using var ctx = new ServerDbContext();
            return ctx.QuizQuestions.ToList();
        }
    }
}
