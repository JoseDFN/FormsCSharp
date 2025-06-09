using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIForms.Controllers
{
    public class CategoryCatalogController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork; //<- Se inyecta la unidad de trabajo

        private readonly IMapper _mapper;
        public CategoryCatalogController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<CategoryCatalogDto>>> Get()
        {
            var CategoryCatalog = await _unitOfWork.CategoryCatalogs.GetAllAsync();
            return _mapper.Map<List<CategoryCatalogDto>>(CategoryCatalog);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CategoryCatalogDto>> Get(int id)
        {
            var CategoryCatalog = await _unitOfWork.CategoryCatalogs.GetByIdAsync(id);
            if (CategoryCatalog == null)
            {
                return NotFound($"CategoryCatalog with id {id} was not found.");
            }
            return _mapper.Map<CategoryCatalogDto>(CategoryCatalog);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CategoryCatalog>> Post(CategoryCatalogDto CategoryCatalogDto)
        {
            var categoryCatalog = _mapper.Map<CategoryCatalog>(CategoryCatalogDto);
            _unitOfWork.CategoryCatalogs.Add(categoryCatalog);
            await _unitOfWork.SaveAsync();
            if (categoryCatalog == null)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(Post), new { id = CategoryCatalogDto.Id }, CategoryCatalogDto);
        }

        // PUT: api/Productos/4
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put(int id, [FromBody] CategoryCatalogDto CategoryCatalogDto)
        {
            // Validación: objeto nulo
            if (CategoryCatalogDto == null)
                return NotFound();
            var categoryCatalog = _mapper.Map<CategoryCatalog>(CategoryCatalogDto);
            _unitOfWork.CategoryCatalogs.Add(categoryCatalog);
            await _unitOfWork.SaveAsync();
            return Ok(CategoryCatalogDto);
        }
        //DELETE: api/Productos
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var CategoryCatalog = await _unitOfWork.CategoryCatalogs.GetByIdAsync(id);
            if (CategoryCatalog == null)
                return NotFound();
            _unitOfWork.CategoryCatalogs.Remove(CategoryCatalog);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}