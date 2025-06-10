using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace APIForms.Controllers
{
    public class CategoryOptionController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork; //<- Se inyecta la unidad de trabajo

        private readonly IMapper _mapper;
        public CategoryOptionController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<CategoryOptionDto>>> Get()
        {
            var CategoryOption = await _unitOfWork.CategoryOptions.GetAllAsync();
            return _mapper.Map<List<CategoryOptionDto>>(CategoryOption);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CategoryOptionDto>> Get(int id)
        {
            var CategoryOption = await _unitOfWork.CategoryOptions.GetByIdAsync(id);
            if (CategoryOption == null)
            {
                return NotFound($"CategoryOption with id {id} was not found.");
            }
            return _mapper.Map<CategoryOptionDto>(CategoryOption);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CategoryOption>> Post(CategoryOptionDto CategoryOptionDto)
        {
            var categoryOption = _mapper.Map<CategoryOption>(CategoryOptionDto);
            _unitOfWork.CategoryOptions.Add(categoryOption);
            await _unitOfWork.SaveAsync();
            if (CategoryOptionDto == null)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(Post), new { id = CategoryOptionDto.Id }, CategoryOptionDto);
        }

        // PUT: api/Productos/4
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put(int id, [FromBody] CategoryOptionDto CategoryOptionDto)
        {
            // Validación: objeto nulo
            if (CategoryOptionDto == null)
                return NotFound();
            var categoryOption = _mapper.Map<CategoryOption>(CategoryOptionDto);
            _unitOfWork.CategoryOptions.Update(categoryOption);
            await _unitOfWork.SaveAsync();
            return Ok(CategoryOptionDto);
        }
        //DELETE: api/Productos
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var CategoryOption = await _unitOfWork.CategoryOptions.GetByIdAsync(id);
            if (CategoryOption == null)
                return NotFound();
            _unitOfWork.CategoryOptions.Remove(CategoryOption);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}