using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Application.DTOs.SubQuestion;

namespace APIForms.Controllers
{
    public class SubQuestionController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork; //<- Se inyecta la unidad de trabajo
        private readonly IMapper _mapper;
        public SubQuestionController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<SubQuestionDto>>> Get()
        {
            var SubQuestion = await _unitOfWork.SubQuestions.GetAllAsync();
            return _mapper.Map<List<SubQuestionDto>>(SubQuestion);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<SubQuestionDto>> Get(int id)
        {
            var SubQuestion = await _unitOfWork.SubQuestions.GetByIdAsync(id);
            if (SubQuestion == null)
            {
                return NotFound($"SubQuestion with id {id} was not found.");
            }
            return _mapper.Map<SubQuestionDto>(SubQuestion);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<SubQuestion>> Post(SubQuestionDto SubQuestionDto)
        {
            var subQuestion = _mapper.Map<SubQuestion>(SubQuestionDto);
            _unitOfWork.SubQuestions.Add(subQuestion);
            await _unitOfWork.SaveAsync();
            if (SubQuestionDto == null)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(Post), new { id = SubQuestionDto.Id }, SubQuestionDto);
        }

        // PUT: api/Productos/4
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put(int id, [FromBody] SubQuestionDto SubQuestionDto)
        {
            if (SubQuestionDto == null)
                return NotFound();
            var subQuestion = _mapper.Map<SubQuestion>(SubQuestionDto);
            _unitOfWork.SubQuestions.Update(subQuestion);
            await _unitOfWork.SaveAsync();
            return Ok(SubQuestionDto);
        }
        //DELETE: api/Productos
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var SubQuestion = await _unitOfWork.SubQuestions.GetByIdAsync(id);
            if (SubQuestion == null)
                return NotFound();

            _unitOfWork.SubQuestions.Remove(SubQuestion);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }
    }
}