using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using Application.DTOs.Chapter;
using Application.DTOs.OptionQuestion;
using Application.DTOs.OptionResponse;
using Application.DTOs.QuestionDto;
using Application.DTOs.SubQuestion;
using Application.DTOs.SummaryOption;
using Application.DTOs.Survey;

namespace APIForms.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<CategoryCatalog, CategoryCatalogDto>().ReverseMap();
            CreateMap<CategoryOption, CategoryOptionDto>().ReverseMap();
            CreateMap<Chapter, ChapterDto>().ReverseMap();
            CreateMap<OptionQuestion, OptionQuestionDto>().ReverseMap();
            CreateMap<OptionResponse, OptionResponseDto>().ReverseMap();
            CreateMap<Question, QuestionDto>().ReverseMap();
            CreateMap<SubQuestion, SubQuestionDto>().ReverseMap();
            CreateMap<SummaryOption, SummaryOptionDto>().ReverseMap();
            CreateMap<Survey, SurveyDto>().ReverseMap();
        }
    }
}