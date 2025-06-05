using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces;
using Infrastructure.Data;
using Infrastructure;
using Infrastructure.Repositories;

namespace Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly FormsContext _context;
        private ICategoryCatalogRepository? _categoryCatalog;
        private ICategoryOptionRepository? _categoryOption;
        private IChapterRepository? _chapter;
        private IOptionQuestionRepository? _optionQuestion;
        private IOptionResponseRepository? _optionResponse;
        private IQuestionRepository? _question;
        private ISubQuestionRepository? _subQuestion;
        private ISummaryOptionRepository? _summaryOption;
        private ISurveyRepository? _survey;

        public UnitOfWork(FormsContext context)
        {
            _context = context;
        }

        public ICategoryCatalogRepository CategoryCatalogs
        {
            get
            {
                if (_categoryCatalog == null)
                {
                    _categoryCatalog = new CategoryCatalogRepository(_context);
                }
                return _categoryCatalog;
            }
        }
        public ICategoryOptionRepository CategoryOptions
        {
            get
            {
                if (_categoryOption == null)
                {
                    _categoryOption = new CategoryOptionRepository(_context);
                }
                return _categoryOption;
            }
        }
        public IChapterRepository Chapters
        {
            get
            {
                if (_chapter == null)
                {
                    _chapter = new ChapterRepository(_context);
                }
                return _chapter;
            }
        }
        public IOptionQuestionRepository OptionQuestions
        {
            get
            {
                if (_optionQuestion == null)
                {
                    _optionQuestion = new OptionQuestionRepository(_context);
                }
                return _optionQuestion;
            }
        }
        public IOptionResponseRepository OptionResponses
        {
            get
            {
                if (_optionResponse == null)
                {
                    _optionResponse = new OptionResponseRepository(_context);
                }
                return _optionResponse;
            }
        }
        public IQuestionRepository Questions
        {
            get
            {
                if (_question == null)
                {
                    _question = new QuestionRepository(_context);
                }
                return _question;
            }
        }
        public ISubQuestionRepository SubQuestions
        {
            get
            {
                if (_subQuestion == null)
                {
                    _subQuestion = new SubQuestionRepository(_context);
                }
                return _subQuestion;
            }
        }
        public ISummaryOptionRepository SummaryOptions
        {
            get
            {
                if (_summaryOption == null)
                {
                    _summaryOption = new SummaryOptionRepository(_context);
                }
                return _summaryOption;
            }
        }
        public ISurveyRepository Surveys
        {
            get
            {
                if (_survey == null)
                {
                    _survey = new SurveyRepository(_context);
                }
                return _survey;
            }
        }
        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
        }
}