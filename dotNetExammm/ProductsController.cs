using DotNetExamm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DotNetExamm.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products
        public ActionResult Index()
        {
            List<Product> products = Product.GetAllProducts();
            return View(products);
        }
        [ChildActionOnly]
        public ActionResult MyPartialView()
        {
            return View();
        }


        // GET: Products/Edit/5
        public ActionResult Edit(int id)
        {
            Product product = Product.GetProductById(id);
            return View(product);
        }

        // POST: Products/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Product product)
        {
            try
            {
                // TODO: Add update logic here
                Product.UpdateProduct(product);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
