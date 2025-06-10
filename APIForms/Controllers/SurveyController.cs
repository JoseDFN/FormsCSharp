using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Application.DTOs.Survey;

namespace APIForms.Controllers
{
    public class SurveyController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork; //<- Se inyecta la unidad de trabajo
        private readonly IMapper _mapper;
        public SurveyController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<SurveyDto>>> Get()
        {
            var Survey = await _unitOfWork.Surveys.GetAllAsync();
            return _mapper.Map<List<SurveyDto>>(Survey);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<SurveyDto>> Get(int id)
        {
            var Survey = await _unitOfWork.Surveys.GetByIdAsync(id);
            if (Survey == null)
            {
                return NotFound($"Survey with id {id} was not found.");
            }
            return _mapper.Map<SurveyDto>(Survey);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Survey>> Post(SurveyDto SurveyDto)
        {
            var survey = _mapper.Map<Survey>(SurveyDto);
            _unitOfWork.Surveys.Add(survey);
            await _unitOfWork.SaveAsync();
            if (SurveyDto == null)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(Post), new { id = SurveyDto.Id }, SurveyDto);
        }

        // PUT: api/Productos/4
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put(int id, [FromBody] SurveyDto SurveyDto)
        {
            if (SurveyDto == null)
                return NotFound();
            var survey = _mapper.Map<Survey>(SurveyDto);
            _unitOfWork.Surveys.Update(survey);
            await _unitOfWork.SaveAsync();
            return Ok(SurveyDto);
        }
        //DELETE: api/Productos
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var Survey = await _unitOfWork.Surveys.GetByIdAsync(id);
            if (Survey == null)
                return NotFound();

            _unitOfWork.Surveys.Remove(Survey);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }
    }
}