using Catalog.Entities;
using Catalog.Models.Entities;
using System;
using System.Data;
using System.Net;
using System.Web.Mvc;

namespace LetsCatalog.Controllers
{
    public class CategoriesController : Controller
    {
        /// <summary>
        /// Private member to hold injected instance
        /// </summary>
        private IUnitOfWork unitOfWork;

        /// <summary>
        /// Creates a new instance of CategoriesController
        /// </summary>
        /// <param name="db">The injected db instance</param>
        public CategoriesController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Gets the default category view
        /// </summary>
        /// <returns>The category index view</returns>
        public ActionResult Index()
        {
            return View(unitOfWork.CategoryRepository.Get());
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Created_Date")] Category category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    category.Created_Date = DateTime.Now;
                    unitOfWork.CategoryRepository.Insert(category);
                    unitOfWork.Save();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }

            return View(category);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var category = unitOfWork.CategoryRepository.GetByID(id);
            if (category == null)
            {
                return HttpNotFound();
            }

            return View(category);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Created_Date")] Category category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    unitOfWork.CategoryRepository.Update(category);
                    unitOfWork.Save();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }

            return View(category);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var category = unitOfWork.CategoryRepository.GetByID(id);

            if (category == null)
            {
                return HttpNotFound();
            }

            return View(category);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public RedirectToRouteResult DeleteConfirmed(Category category)
        {
            unitOfWork.CategoryRepository.Delete(category);
            unitOfWork.Save();
            return RedirectToAction("Index");
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public RedirectToRouteResult Details(int id)
        {
            return RedirectToAction("Index", "SubCategories", new { categoryId = id });
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}