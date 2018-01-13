using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Catalog.Entities;
using Catalog.Models.Entities;
using LetsCatalog.Filters;

namespace LetsCatalog.Controllers
{
    public class SubCategoriesController : Controller
    {
        #region Private Members
        /// <summary>
        /// Private member to hold injected instance
        /// </summary>
        private IUnitOfWork unitOfWork;
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of SubCategoriesController
        /// </summary>
        /// <param name="db">The injected db instance</param>
        public SubCategoriesController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        #endregion

        #region Controller Methods
        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(int? categoryId)
        {
            var subcategories = unitOfWork.SubCategoryRepository.Get(
                (sub) => sub.Category.ID == categoryId,
                (sub) => sub.OrderByDescending(s => s.Created_Date));

            if (subcategories == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View(subcategories);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Details(int? categoryId, int? subcategoryId)
        {
            return GetValidatedView(categoryId, subcategoryId);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public ActionResult Create(int? categoryId)
        {
            TempData["CurrentCategory"] = unitOfWork.CategoryRepository.GetByID(categoryId);
            TempData["Categories"] = unitOfWork.CategoryRepository.Get();
            return View();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="subCategory"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AutoTimeFilter]
        public ActionResult Create([Bind(Include = "ID,Name,Description,Created_Date")] SubCategory subCategory, string categoriesList)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var category = unitOfWork.CategoryRepository.Get((cat) => cat.Name == categoriesList).FirstOrDefault();
                    if (category != null)
                    {
                        subCategory.Category = category;
                        unitOfWork.SubCategoryRepository.Insert(subCategory);
                        unitOfWork.Save();
                        return RedirectToAction("Index", new { categoryId = category.ID });
                    }
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }

            return View(subCategory);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int? categoryId, int? subcategoryId)
        {
            TempData["CurrentCategory"] = unitOfWork.CategoryRepository.GetByID(categoryId);
            TempData["Categories"] = unitOfWork.CategoryRepository.Get();
            return GetValidatedView(categoryId, subcategoryId);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="subCategory"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Description,Created_Date")] SubCategory subCategory, string categoriesList)
        {
            int categoryId = 0, subcategoryId = 0;
            try
            {
                if (ModelState.IsValid)
                {
                    var category = unitOfWork.CategoryRepository.Get((cat) => cat.Name == categoriesList).FirstOrDefault();
                    if (category != null)
                    {
                        categoryId = category.ID;
                        subcategoryId = subCategory.ID;
                        subCategory.Category = category;
                        unitOfWork.SubCategoryRepository.Update(subCategory);
                        unitOfWork.Save();
                        return RedirectToAction("Index", new { categoryId = categoryId });
                    }
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }

            return RedirectToAction("Edit", new { categoryId = categoryId, subcategoryId = subcategoryId });
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(int? categoryId, int? subcategoryId)
        {
            return GetValidatedView(categoryId, subcategoryId);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? categoryId, int? subcategoryId)
        {
            unitOfWork.SubCategoryRepository.Delete(subcategoryId);
            unitOfWork.Save();
            return RedirectToAction("Index", new { categoryId = categoryId });
        }
        #endregion

        #region IDisposable
        /// <summary>
        ///
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            unitOfWork.Dispose();
            base.Dispose(disposing);
        }
        #endregion

        #region Private Methods
        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private ActionResult GetValidatedView(int? categoryId, int? subcategoryId)
        {
            if (categoryId == null || subcategoryId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var subcategory = unitOfWork.SubCategoryRepository.GetByID(subcategoryId);

            if (subcategory == null || subcategory.Category.ID != categoryId)
            {
                return HttpNotFound();
            }

            return View(subcategory);
        }
        #endregion
    }
}
