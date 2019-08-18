using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using Vidly.Models;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        public ActionResult Index()
        {
            List<Customer> customers;
            using (var context = new ApplicationDbContext())
            {
                customers = context.Customers
                                   .Include(c=>c.MembershipType)
                                   .ToList();
            }

            return View(customers);
        }

        public ActionResult Details(int id)
        {
            Customer customer;
            using (var context = new ApplicationDbContext())
            {
                customer = context.Customers
                                  .Include(c=>c.MembershipType)
                                  .SingleOrDefault(c => c.Id == id);
            }

            if (customer == null)
                return HttpNotFound();

            return View(customer);
        }
    }
}