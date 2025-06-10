using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Application.DTOs.QuestionDto;

namespace APIForms.Controllers
{
    public class QuestionController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork; //<- Se inyecta la unidad de trabajo
        private readonly IMapper _mapper;
        public QuestionController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<QuestionDto>>> Get()
        {
            var Question = await _unitOfWork.Questions.GetAllAsync();
            return _mapper.Map<List<QuestionDto>>(Question);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<QuestionDto>> Get(int id)
        {
            var Question = await _unitOfWork.Questions.GetByIdAsync(id);
            if (Question == null)
            {
                return NotFound($"Question with id {id} was not found.");
            }
            return _mapper.Map<QuestionDto>(Question);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Question>> Post(QuestionDto QuestionDto)
        {
            var question = _mapper.Map<Question>(QuestionDto);
            _unitOfWork.Questions.Add(question);
            await _unitOfWork.SaveAsync();
            if (QuestionDto == null)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(Post), new { id = QuestionDto.Id }, QuestionDto);
        }

        // PUT: api/Productos/4
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put(int id, [FromBody] QuestionDto QuestionDto)
        {
            if (QuestionDto == null)
                return NotFound();
            var question = _mapper.Map<Question>(QuestionDto);
            _unitOfWork.Questions.Update(question);
            await _unitOfWork.SaveAsync();
            return Ok(QuestionDto);
        }
        //DELETE: api/Productos
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var Question = await _unitOfWork.Questions.GetByIdAsync(id);
            if (Question == null)
                return NotFound();

            _unitOfWork.Questions.Remove(Question);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }
    }
}