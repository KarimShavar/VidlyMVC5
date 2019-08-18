using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        public ActionResult Index()
        {
            List<Movie> movies;
            using (var context = new ApplicationDbContext())
            {
                movies = context.Movies.Include(m => m.Genre).ToList();
            }
            return View(movies);
        }

        public ActionResult Details(int id)
        {
            Movie movie;
            using (var context = new ApplicationDbContext())
            {
                movie = context.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == id);
            }
            return View(movie);
        }
    }
}
