using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Application.DTOs.OptionResponse;

namespace APIForms.Controllers
{
    public class OptionResponseController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork; //<- Se inyecta la unidad de trabajo
        private readonly IMapper _mapper;
        public OptionResponseController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<OptionResponseDto>>> Get()
        {
            var OptionResponse = await _unitOfWork.OptionResponses.GetAllAsync();
            return _mapper.Map<List<OptionResponseDto>>(OptionResponse);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<OptionResponseDto>> Get(int id)
        {
            var OptionResponse = await _unitOfWork.OptionResponses.GetByIdAsync(id);
            if (OptionResponse == null)
            {
                return NotFound($"OptionResponse with id {id} was not found.");
            }
            return _mapper.Map<OptionResponseDto>(OptionResponse);
        }
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<OptionResponse>> Post(OptionResponseDto OptionResponseDto)
        {
            var optionResponse = _mapper.Map<OptionResponse>(OptionResponseDto);
            _unitOfWork.OptionResponses.Add(optionResponse);
            await _unitOfWork.SaveAsync();
            if (OptionResponseDto == null)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(Post), new { id = OptionResponseDto.Id }, OptionResponseDto);
        }

        // PUT: api/Productos/4
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put(int id, [FromBody] OptionResponseDto OptionResponseDto)
        {
            if (OptionResponseDto == null)
                return NotFound();
            var optionResponse = _mapper.Map<OptionResponse>(OptionResponseDto);
            _unitOfWork.OptionResponses.Update(optionResponse);
            await _unitOfWork.SaveAsync();
            return Ok(OptionResponseDto);
        }
        //DELETE: api/Productos
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var OptionResponse = await _unitOfWork.OptionResponses.GetByIdAsync(id);
            if (OptionResponse == null)
                return NotFound();

            _unitOfWork.OptionResponses.Remove(OptionResponse);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }
    }
}