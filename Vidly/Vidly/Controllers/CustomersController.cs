using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        public ActionResult New()
        {
            List<MembershipType> membershipTypes;
            using (var context = new ApplicationDbContext())
            {
                membershipTypes = context.MembershipTypes.ToList();
            }

            var viewModel = new NewCustomerViewModel
            {
                MembershipTypes = membershipTypes
            };
            return View(viewModel);
        }
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

        [HttpPost]
        public ActionResult Create(Customer customer)
        {

        }
    }
}