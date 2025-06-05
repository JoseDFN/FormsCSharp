using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace APIForms.Controllers
{
    public class OptionQuestionController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork; //<- Se inyecta la unidad de trabajo
        public OptionQuestionController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<OptionQuestion>>> Get()
        {
            var OptionQuestion = await _unitOfWork.OptionQuestions.GetAllAsync();
            return Ok(OptionQuestion);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get(int id)
        {
            var OptionQuestion = await _unitOfWork.OptionQuestions.GetByIdAsync(id);
            if (OptionQuestion == null)
            {
                return NotFound($"OptionQuestion with id {id} was not found.");
            }
            return Ok(OptionQuestion);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<OptionQuestion>> Post(OptionQuestion OptionQuestion)
        {
            _unitOfWork.OptionQuestions.Add(OptionQuestion);
            await _unitOfWork.SaveAsync();
            if (OptionQuestion == null)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(Post), new { id = OptionQuestion.Id }, OptionQuestion);
        }

        // PUT: api/Productos/4
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put(int id, [FromBody] OptionQuestion OptionQuestion)
        {
            // Validación: objeto nulo
            if (OptionQuestion == null)
                return BadRequest("El cuerpo de la solicitud está vacío.");

            // Validación: el ID de la URL debe coincidir con el del objeto (si viene con ID)
            if (id != OptionQuestion.Id)
                return BadRequest("El ID de la URL no coincide con el ID del objeto enviado.");

            // Verificación: el recurso debe existir antes de actualizar
            var existingOptionQuestion = await _unitOfWork.OptionQuestions.GetByIdAsync(id);
            if (existingOptionQuestion == null)
                return NotFound($"No se encontró el OptionQuestion con ID {id}.");

            // Actualización controlada de campos específicos
            existingOptionQuestion.OptionId = OptionQuestion.OptionId;
            existingOptionQuestion.OptionCatalogId = OptionQuestion.OptionCatalogId;
            existingOptionQuestion.QuestionId = OptionQuestion.QuestionId;
            existingOptionQuestion.SubQuestionId = OptionQuestion.SubQuestionId;
            existingOptionQuestion.NumberOption = OptionQuestion.NumberOption;
            existingOptionQuestion.CommentOptionRes = OptionQuestion.CommentOptionRes;

            // Puedes agregar más propiedades aquí según el modelo

            _unitOfWork.OptionQuestions.Update(existingOptionQuestion);
            await _unitOfWork.SaveAsync();

            return Ok(existingOptionQuestion);
        }
        //DELETE: api/Productos
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var OptionQuestion = await _unitOfWork.OptionQuestions.GetByIdAsync(id);
            if (OptionQuestion == null)
                return NotFound();

            _unitOfWork.OptionQuestions.Remove(OptionQuestion);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }
    }
}