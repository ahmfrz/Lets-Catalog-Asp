using LetsCatalog.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LetsCatalog.Controllers
{
    public class CategoryController : Controller
    {
        /// <summary>
        /// Private member to hold injected instance
        /// </summary>
        private CatalogDb _db;

        /// <summary>
        /// Creates a new instance of CategoryController
        /// </summary>
        /// <param name="db">The injected db instance</param>
        public CategoryController(CatalogDb db)
        {
            this._db = db;
        }

        /// <summary>
        /// Gets the default category view
        /// </summary>
        /// <returns>The category index view</returns>
        public ActionResult Index()
        {
            return View(_db.Categories.AsEnumerable());
        }

        public ActionResult Create()
        {
            return View();
        }
    }
}