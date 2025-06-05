using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace APIForms.Controllers
{
    public class SurveyController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork; //<- Se inyecta la unidad de trabajo
        public SurveyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<Survey>>> Get()
        {
            var Survey = await _unitOfWork.Surveys.GetAllAsync();
            return Ok(Survey);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get(int id)
        {
            var Survey = await _unitOfWork.Surveys.GetByIdAsync(id);
            if (Survey == null)
            {
                return NotFound($"Survey with id {id} was not found.");
            }
            return Ok(Survey);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Survey>> Post(Survey Survey)
        {
            _unitOfWork.Surveys.Add(Survey);
            await _unitOfWork.SaveAsync();
            if (Survey == null)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(Post), new { id = Survey.Id }, Survey);
        }

        // PUT: api/Productos/4
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put(int id, [FromBody] Survey Survey)
        {
            // Validación: objeto nulo
            if (Survey == null)
                return BadRequest("El cuerpo de la solicitud está vacío.");

            // Validación: el ID de la URL debe coincidir con el del objeto (si viene con ID)
            if (id != Survey.Id)
                return BadRequest("El ID de la URL no coincide con el ID del objeto enviado.");

            // Verificación: el recurso debe existir antes de actualizar
            var existingSurvey = await _unitOfWork.Surveys.GetByIdAsync(id);
            if (existingSurvey == null)
                return NotFound($"No se encontró el Survey con ID {id}.");

            // Actualización controlada de campos específicos
            existingSurvey.Name = Survey.Name;
            existingSurvey.Description = Survey.Description;
            existingSurvey.Instruction = Survey.Instruction;
            existingSurvey.ComponentReact = Survey.ComponentReact;
            existingSurvey.ComponentHtml = Survey.ComponentHtml;

            // Puedes agregar más propiedades aquí según el modelo

            _unitOfWork.Surveys.Update(existingSurvey);
            await _unitOfWork.SaveAsync();

            return Ok(existingSurvey);
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