using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryCatalogRepository CategoryCatalogs { get; }
        ICategoryOptionRepository CategoryOptions { get; }
        IChapterRepository Chapters { get; }
        IOptionQuestionRepository OptionQuestions { get; }
        IOptionResponseRepository OptionResponses { get; }
        IQuestionRepository Questions { get; }
        ISubQuestionRepository SubQuestions { get; }
        ISummaryOptionRepository SummaryOptions { get; }
        ISurveyRepository Surveys { get; }
        Task<int> SaveAsync();
    }
}