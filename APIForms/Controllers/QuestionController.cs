using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace APIForms.Controllers
{
    public class QuestionController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork; //<- Se inyecta la unidad de trabajo
        public QuestionController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<Question>>> Get()
        {
            var Question = await _unitOfWork.Questions.GetAllAsync();
            return Ok(Question);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get(int id)
        {
            var Question = await _unitOfWork.Questions.GetByIdAsync(id);
            if (Question == null)
            {
                return NotFound($"Question with id {id} was not found.");
            }
            return Ok(Question);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Question>> Post(Question Question)
        {
            _unitOfWork.Questions.Add(Question);
            await _unitOfWork.SaveAsync();
            if (Question == null)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(Post), new { id = Question.Id }, Question);
        }

        // PUT: api/Productos/4
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put(int id, [FromBody] Question Question)
        {
            // Validación: objeto nulo
            if (Question == null)
                return BadRequest("El cuerpo de la solicitud está vacío.");

            // Validación: el ID de la URL debe coincidir con el del objeto (si viene con ID)
            if (id != Question.Id)
                return BadRequest("El ID de la URL no coincide con el ID del objeto enviado.");

            // Verificación: el recurso debe existir antes de actualizar
            var existingQuestion = await _unitOfWork.Questions.GetByIdAsync(id);
            if (existingQuestion == null)
                return NotFound($"No se encontró el Question con ID {id}.");

            // Actualización controlada de campos específicos
            existingQuestion.ChapterId = Question.ChapterId;
            existingQuestion.QuestionNumber = Question.QuestionNumber;
            existingQuestion.ResponseType = Question.ResponseType;
            existingQuestion.QuestionText = Question.QuestionText;
            existingQuestion.CommentQuestion = Question.CommentQuestion;

            // Puedes agregar más propiedades aquí según el modelo

            _unitOfWork.Questions.Update(existingQuestion);
            await _unitOfWork.SaveAsync();

            return Ok(existingQuestion);
        }
        //DELETE: api/Productos
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var Question = await _unitOfWork.Questions.GetByIdAsync(id);
            if (Question == null)
                return NotFound();

            _unitOfWork.Questions.Remove(Question);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }
    }
}