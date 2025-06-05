using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace APIForms.Controllers
{
    public class ChapterController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork; //<- Se inyecta la unidad de trabajo
        public ChapterController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<Chapter>>> Get()
        {
            var Chapter = await _unitOfWork.Chapters.GetAllAsync();
            return Ok(Chapter);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get(int id)
        {
            var Chapter = await _unitOfWork.Chapters.GetByIdAsync(id);
            if (Chapter == null)
            {
                return NotFound($"Chapter with id {id} was not found.");
            }
            return Ok(Chapter);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Chapter>> Post(Chapter Chapter)
        {
            _unitOfWork.Chapters.Add(Chapter);
            await _unitOfWork.SaveAsync();
            if (Chapter == null)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(Post), new { id = Chapter.Id }, Chapter);
        }

        // PUT: api/Productos/4
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put(int id, [FromBody] Chapter Chapter)
        {
            // Validación: objeto nulo
            if (Chapter == null)
                return BadRequest("El cuerpo de la solicitud está vacío.");

            // Validación: el ID de la URL debe coincidir con el del objeto (si viene con ID)
            if (id != Chapter.Id)
                return BadRequest("El ID de la URL no coincide con el ID del objeto enviado.");

            // Verificación: el recurso debe existir antes de actualizar
            var existingChapter = await _unitOfWork.Chapters.GetByIdAsync(id);
            if (existingChapter == null)
                return NotFound($"No se encontró el Chapter con ID {id}.");

            // Actualización controlada de campos específicos
            existingChapter.SurveyId = Chapter.SurveyId;
            existingChapter.ChapterNumber = Chapter.ChapterNumber;
            existingChapter.ChapterTitle = Chapter.ChapterTitle;
            existingChapter.ComponentHtml = Chapter.ComponentHtml;
            existingChapter.ComponentReact = Chapter.ComponentReact;

            // Puedes agregar más propiedades aquí según el modelo

            _unitOfWork.Chapters.Update(existingChapter);
            await _unitOfWork.SaveAsync();

            return Ok(existingChapter);
        }
        //DELETE: api/Productos
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var Chapter = await _unitOfWork.Chapters.GetByIdAsync(id);
            if (Chapter == null)
                return NotFound();

            _unitOfWork.Chapters.Remove(Chapter);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }
    }
}