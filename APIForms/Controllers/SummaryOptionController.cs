using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Application.DTOs.SummaryOption;

namespace APIForms.Controllers
{
    public class SummaryOptionController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork; //<- Se inyecta la unidad de trabajo
        private readonly IMapper _mapper;
        public SummaryOptionController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<SummaryOptionDto>>> Get()
        {
            var SummaryOption = await _unitOfWork.SummaryOptions.GetAllAsync();
            return _mapper.Map<List<SummaryOptionDto>>(SummaryOption);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<SummaryOptionDto>> Get(int id)
        {
            var SummaryOption = await _unitOfWork.SummaryOptions.GetByIdAsync(id);
            if (SummaryOption == null)
            {
                return NotFound($"SummaryOption with id {id} was not found.");
            }
            return _mapper.Map<SummaryOptionDto>(SummaryOption);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<SummaryOption>> Post(SummaryOptionDto SummaryOptionDto)
        {
            var summaryOption = _mapper.Map<SummaryOption>(SummaryOptionDto);
            _unitOfWork.SummaryOptions.Add(summaryOption);
            await _unitOfWork.SaveAsync();
            if (SummaryOptionDto == null)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(Post), new { id = SummaryOptionDto.Id }, SummaryOptionDto);
        }

        // PUT: api/Productos/4
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put(int id, [FromBody] SummaryOptionDto SummaryOptionDto)
        {
            if (SummaryOptionDto == null)
                return NotFound();
            var summaryOption = _mapper.Map<SummaryOption>(SummaryOptionDto);
            _unitOfWork.SummaryOptions.Update(summaryOption);
            await _unitOfWork.SaveAsync();
            return Ok(SummaryOptionDto);
        }
        //DELETE: api/Productos
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var SummaryOption = await _unitOfWork.SummaryOptions.GetByIdAsync(id);
            if (SummaryOption == null)
                return NotFound();

            _unitOfWork.SummaryOptions.Remove(SummaryOption);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }
    }
}