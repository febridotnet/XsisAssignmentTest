using Microsoft.AspNetCore.Mvc;
using Movies.Data.Models;
using Movies.Data.Repositories;
using System;

namespace MoviesApp.Api.Controllers
{
    [Route("api/movies")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieRepository _movieRepository;
        private readonly ILogger<MoviesController> _logger;

        public MoviesController(IMovieRepository movieRepository, ILogger<MoviesController> logger)
        {
            _movieRepository = movieRepository;
            _logger = logger;
        }

        [HttpPost]
        //[Route("AddMovie")]
        public async Task<IActionResult> AddMovie(Movie person)
        {
            try
            {
                var createdMovie = await _movieRepository.CreateMoviesAsync(person);
                return CreatedAtAction(nameof(AddMovie), createdMovie);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    statusCode = 500,
                    message = ex.Message
                });
            }
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateMovie(Movie movieToUpdate)
        {
            try
            {
                var existingPerson = await _movieRepository.GetMoviesByIdAsync(movieToUpdate.Id);
                if (existingPerson == null)
                {
                    return NotFound(new
                    {
                        statusCode = 404,
                        message = "record not found"
                    });
                }
                existingPerson.title = movieToUpdate.title;
                existingPerson.description = movieToUpdate.description;
                existingPerson.rating = movieToUpdate.rating;
                await _movieRepository.UpdateMoviesAsync(existingPerson);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    statusCode = 500,
                    message = ex.Message
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            try
            {
                var existingMovie = await _movieRepository.GetMoviesByIdAsync(id);
                if (existingMovie == null)
                {
                    return NotFound(new
                    {
                        statusCode = 404,
                        message = "record not found"
                    });
                }

                await _movieRepository.DeleteMoviesAsync(existingMovie);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    statusCode = 500,
                    message = ex.Message
                });
            }
        }

        [HttpGet]
        //[Route("GetMovies")]
        public async Task<IActionResult> GetMovies()
        {
            try
            {
                var movie = await _movieRepository.GetMoviesAsync();
                return Ok(movie);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    statusCode = 500,
                    message = ex.Message
                });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovies(int id)
        {
            try
            {
                var person = await _movieRepository.GetMoviesByIdAsync(id);
                if (person == null)
                {
                    return NotFound(new
                    {
                        statusCode = 404,
                        message = "record not found"
                    });
                }
                return Ok(person);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    statusCode = 500,
                    message = ex.Message
                });
            }
        }
    }
}
