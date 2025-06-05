using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace APIForms.Controllers
{
    public class CategoryOptionController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork; //<- Se inyecta la unidad de trabajo
        public CategoryOptionController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<CategoryOption>>> Get()
        {
            var CategoryOption = await _unitOfWork.CategoryOptions.GetAllAsync();
            return Ok(CategoryOption);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get(int id)
        {
            var CategoryOption = await _unitOfWork.CategoryOptions.GetByIdAsync(id);
            if (CategoryOption == null)
            {
                return NotFound($"CategoryOption with id {id} was not found.");
            }
            return Ok(CategoryOption);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CategoryOption>> Post(CategoryOption CategoryOption)
        {
            _unitOfWork.CategoryOptions.Add(CategoryOption);
            await _unitOfWork.SaveAsync();
            if (CategoryOption == null)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(Post), new { id = CategoryOption.Id }, CategoryOption);
        }

        // PUT: api/Productos/4
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put(int id, [FromBody] CategoryOption CategoryOption)
        {
            // Validación: objeto nulo
            if (CategoryOption == null)
                return BadRequest("El cuerpo de la solicitud está vacío.");

            // Validación: el ID de la URL debe coincidir con el del objeto (si viene con ID)
            if (id != CategoryOption.Id)
                return BadRequest("El ID de la URL no coincide con el ID del objeto enviado.");

            // Verificación: el recurso debe existir antes de actualizar
            var existingCategoryOption = await _unitOfWork.CategoryOptions.GetByIdAsync(id);
            if (existingCategoryOption == null)
                return NotFound($"No se encontró el CategoryOption con ID {id}.");

            // Actualización controlada de campos específicos
            existingCategoryOption.CatalogOptionsId = CategoryOption.CatalogOptionsId;
            existingCategoryOption.CategoriesOptionsId = CategoryOption.CategoriesOptionsId;

            // Puedes agregar más propiedades aquí según el modelo

            _unitOfWork.CategoryOptions.Update(existingCategoryOption);
            await _unitOfWork.SaveAsync();

            return Ok(existingCategoryOption);
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