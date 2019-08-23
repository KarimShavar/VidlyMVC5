using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class MoviesController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: api/Movies
        public IHttpActionResult GetMovies()
        {
            var movieDtos =  _context.Movies
                                     .Include(m => m.Genre)
                                     .ToList()
                                     .Select(Mapper.Map<Movie, MovieDto>);

            return Ok(movieDtos);
        }

        // GET: api/Movies/5
        public IHttpActionResult GetMovie(int id)
        {
            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            var movieToReturn = Mapper.Map<Movie, MovieDto>(movie);

            return Ok(movieToReturn);
        }

        // POST: api/Movies
        [HttpPost]
        public IHttpActionResult CreateMovie([FromBody] MovieDto movieDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var movieToAdd = Mapper.Map<MovieDto, Movie>(movieDto);
            _context.Movies.Add(movieToAdd);
            _context.SaveChanges();

            movieDto.Id = movieToAdd.Id;

            return Created(new Uri($"{Request.RequestUri}/{movieToAdd.Id.ToString()}"), movieDto);
        }

        // PUT: api/Movies/5
        [HttpPut]
        public IHttpActionResult UpdateMovie(int id, MovieDto movieDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var movieInDb = _context.Movies.SingleOrDefault(m => m.Id == id);
            if (movieInDb == null)
            {
                return NotFound();
            }

            // Update this way as value is already loaded into context.
            Mapper.Map<MovieDto, Movie>(movieDto, destination: movieInDb);
            _context.SaveChanges();

            return Ok();
        }

        // DELETE: api/Movies/5
        public IHttpActionResult Delete(int id)
        {
            var movieInDb = _context.Movies.SingleOrDefault(m => m.Id == id);
            if (movieInDb == null)
            {
                return NotFound();
            }

            _context.Movies.Remove(movieInDb);
            _context.SaveChanges();

            return Ok();
        }
    }
}
