using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebMotors.Domain;
using WebMotors.Repository.Interface;
using WebMotors.WebAPI.Dtos;

namespace WebMotors.WebAPI.Controllers
{
    [Route("api/WebMotors/[controller]")]
    [ApiController]
    public class AnuncioController : ControllerBase
    {
        private readonly IAnuncioRepository _repository;
        private readonly IMapper _mapper;

        public AnuncioController(IAnuncioRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        [Produces(typeof(Anuncio))]
        public async Task<IActionResult> Get()
        {
            try
            {
                var anuncios = await _repository.GetAllAnunciosAsync();
                var results = _mapper.Map<IEnumerable<AnuncioDto>>(anuncios);
                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Banco de dados falhou {ex.Message}");
            }
        }

        [HttpGet("{AnuncioId}")]
        [Produces(typeof(Anuncio))]
        public async Task<IActionResult> Get(int AnuncioId)
        {
            try
            {
                var anuncio = await _repository.GetAnuncioAsyncById(AnuncioId);
                var results = _mapper.Map<AnuncioDto>(anuncio);
                return Ok(results);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
            }

        }

        [HttpPost]
        public async Task<IActionResult> Post(Anuncio model)
        {
            try
            {
                var anuncio = _mapper.Map<Anuncio>(model);
                _repository.Add(anuncio);

                if (await _repository.SaveChangesAsync())
                {
                    return Created($"/api/webmotors/{model.Id}", _mapper.Map<AnuncioDto>(anuncio));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Banco de dados falhou {ex.Message}");
            }

            return BadRequest();
        }

        [HttpPut("{AnuncioId}")]
        public async Task<IActionResult> Put(int AnuncioId, AnuncioDto model)
        {
            try
            {
                var anuncio = await _repository.GetAnuncioAsyncById(AnuncioId);
                if (anuncio == null) return NotFound();

                _mapper.Map(model, anuncio);

                _repository.Update(anuncio);

                if (await _repository.SaveChangesAsync())
                {
                    return Created($"/api/webmotors/{model.Id}", model);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Banco de dados falhou {ex.Message}");
            }

            return BadRequest();
        }

        [HttpDelete("{AnuncioId}")]
        public async Task<IActionResult> Delete(int AnuncioId)
        {
            try
            {
                var anuncio = await _repository.GetAnuncioAsyncById(AnuncioId);
                if (anuncio == null) return NotFound();

                _repository.Delete(anuncio);

                if (await _repository.SaveChangesAsync())
                {
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Banco de dados falhou {ex.Message}");
            }

            return BadRequest();
        }
    }
}