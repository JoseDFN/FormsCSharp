using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIForms.Controllers
{
    public class CategoryCatalogController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork; //<- Se inyecta la unidad de trabajo
        public CategoryCatalogController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<CategoryCatalog>>> Get()
        {
            var CategoryCatalog = await _unitOfWork.CategoryCatalogs.GetAllAsync();
            return Ok(CategoryCatalog);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get(int id)
        {
            var CategoryCatalog = await _unitOfWork.CategoryCatalogs.GetByIdAsync(id);
            if (CategoryCatalog == null)
            {
                return NotFound($"CategoryCatalog with id {id} was not found.");
            }
            return Ok(CategoryCatalog);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CategoryCatalog>> Post(CategoryCatalog CategoryCatalog)
        {
            _unitOfWork.CategoryCatalogs.Add(CategoryCatalog);
            await _unitOfWork.SaveAsync();
            if (CategoryCatalog == null)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(Post), new { id = CategoryCatalog.Id }, CategoryCatalog);
        }

        // PUT: api/Productos/4
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put(int id, [FromBody] CategoryCatalog CategoryCatalog)
        {
            // Validación: objeto nulo
            if (CategoryCatalog == null)
                return BadRequest("El cuerpo de la solicitud está vacío.");

            // Validación: el ID de la URL debe coincidir con el del objeto (si viene con ID)
            if (id != CategoryCatalog.Id)
                return BadRequest("El ID de la URL no coincide con el ID del objeto enviado.");

            // Verificación: el recurso debe existir antes de actualizar
            var existingCategoryCatalog = await _unitOfWork.CategoryCatalogs.GetByIdAsync(id);
            if (existingCategoryCatalog == null)
                return NotFound($"No se encontró el CategoryCatalog con ID {id}.");

            // Actualización controlada de campos específicos
            existingCategoryCatalog.Name = CategoryCatalog.Name;
            // Puedes agregar más propiedades aquí según el modelo

            _unitOfWork.CategoryCatalogs.Update(existingCategoryCatalog);
            await _unitOfWork.SaveAsync();

            return Ok(existingCategoryCatalog);
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