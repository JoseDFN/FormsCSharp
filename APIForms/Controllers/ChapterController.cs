using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs.Chapter;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace APIForms.Controllers
{
    public class ChapterController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork; //<- Se inyecta la unidad de trabajo
        private readonly IMapper _mapper;

        public ChapterController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ChapterDto>>> Get()
        {
            var Chapter = await _unitOfWork.Chapters.GetAllAsync();
            return _mapper.Map<List<ChapterDto>>(Chapter);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ChapterDto>> Get(int id)
        {
            var Chapter = await _unitOfWork.Chapters.GetByIdAsync(id);
            if (Chapter == null)
            {
                return NotFound($"Chapter with id {id} was not found.");
            }
            return _mapper.Map<ChapterDto>(Chapter);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Chapter>> Post(ChapterDto ChapterDto)
        {
            var chapter = _mapper.Map<Chapter>(ChapterDto);
            _unitOfWork.Chapters.Add(chapter);
            await _unitOfWork.SaveAsync();
            if (ChapterDto == null)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(Post), new { id = ChapterDto.Id }, ChapterDto);
        }

        // PUT: api/Productos/4
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put(int id, [FromBody] ChapterDto ChapterDto)
        {
            // Validación: objeto nulo
            if (ChapterDto == null)
                return NotFound();
            var chapter = _mapper.Map<Chapter>(ChapterDto);
            _unitOfWork.Chapters.Update(chapter);
            await _unitOfWork.SaveAsync();
            return Ok(ChapterDto);
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