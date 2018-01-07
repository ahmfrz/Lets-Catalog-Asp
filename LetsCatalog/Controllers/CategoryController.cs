using Catalog.Entities;
using Catalog.Models.Entities;
using System;
using System.Data;
using System.Web.Mvc;

namespace LetsCatalog.Controllers
{
    public class CategoryController : Controller
    {
        /// <summary>
        /// Private member to hold injected instance
        /// </summary>
        private IUnitOfWork unitOfWork;

        /// <summary>
        /// Creates a new instance of CategoryController
        /// </summary>
        /// <param name="db">The injected db instance</param>
        public CategoryController(IUnitOfWork unitOfWork)
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
        public ActionResult CreateCategory()
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
        public RedirectToRouteResult CreateCategory(Category category)
        {
            try
            {
                category.Created_Date = DateTime.Now;
                unitOfWork.CategoryRepository.Insert(category);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }

            return RedirectToAction("Create", category);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult EditCategory(int id)
        {
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
        public RedirectToRouteResult EditCategory(Category category)
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

            return RedirectToAction("EditCategory", new { category.ID });
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DeleteCategory(int id)
        {
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
        public RedirectToRouteResult DeleteCategoryConfirmed(Category category)
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
        public RedirectToRouteResult CategoryDetails(int id)
        {
            return RedirectToAction("Index", "SubCategory");
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