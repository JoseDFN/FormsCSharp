using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace APIForms.Controllers
{
    public class SummaryOptionController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork; //<- Se inyecta la unidad de trabajo
        public SummaryOptionController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<SummaryOption>>> Get()
        {
            var SummaryOption = await _unitOfWork.SummaryOptions.GetAllAsync();
            return Ok(SummaryOption);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get(int id)
        {
            var SummaryOption = await _unitOfWork.SummaryOptions.GetByIdAsync(id);
            if (SummaryOption == null)
            {
                return NotFound($"SummaryOption with id {id} was not found.");
            }
            return Ok(SummaryOption);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<SummaryOption>> Post(SummaryOption SummaryOption)
        {
            _unitOfWork.SummaryOptions.Add(SummaryOption);
            await _unitOfWork.SaveAsync();
            if (SummaryOption == null)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(Post), new { id = SummaryOption.Id }, SummaryOption);
        }

        // PUT: api/Productos/4
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put(int id, [FromBody] SummaryOption SummaryOption)
        {
            // Validación: objeto nulo
            if (SummaryOption == null)
                return BadRequest("El cuerpo de la solicitud está vacío.");

            // Validación: el ID de la URL debe coincidir con el del objeto (si viene con ID)
            if (id != SummaryOption.Id)
                return BadRequest("El ID de la URL no coincide con el ID del objeto enviado.");

            // Verificación: el recurso debe existir antes de actualizar
            var existingSummaryOption = await _unitOfWork.SummaryOptions.GetByIdAsync(id);
            if (existingSummaryOption == null)
                return NotFound($"No se encontró el SummaryOption con ID {id}.");

            // Actualización controlada de campos específicos
            existingSummaryOption.SurveyId = SummaryOption.SurveyId;
            existingSummaryOption.CodeNumber = SummaryOption.CodeNumber;
            existingSummaryOption.IdQuestion = SummaryOption.IdQuestion;
            existingSummaryOption.ValorT = SummaryOption.ValorT;

            // Puedes agregar más propiedades aquí según el modelo

            _unitOfWork.SummaryOptions.Update(existingSummaryOption);
            await _unitOfWork.SaveAsync();

            return Ok(existingSummaryOption);
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