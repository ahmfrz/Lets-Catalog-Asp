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

namespace LetsCatalog.Controllers
{
    public class SubCategoriesController : Controller
    {
        /// <summary>
        /// Private member to hold injected instance
        /// </summary>
        private IUnitOfWork unitOfWork;

        /// <summary>
        /// Creates a new instance of SubCategoriesController
        /// </summary>
        /// <param name="db">The injected db instance</param>
        public SubCategoriesController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(int? categoryId)
        {
            var subcategories = unitOfWork.SubCategoryRepository.Get((sub) => sub.Category.ID == categoryId);

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
                        subCategory.Created_Date = DateTime.Now;
                        unitOfWork.SubCategoryRepository.Insert(subCategory);
                        unitOfWork.Save();
                        return RedirectToAction("Index");
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
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            SubCategory subCategory = unitOfWork.SubCategoryRepository.GetByID(id);

            if (subCategory == null)
            {
                return HttpNotFound();
            }

            return View(subCategory);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="subCategory"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Description,Created_Date")] SubCategory subCategory)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    unitOfWork.SubCategoryRepository.Update(subCategory);
                    unitOfWork.Save();
                    return RedirectToAction("Index");
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
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            SubCategory subCategory = unitOfWork.SubCategoryRepository.GetByID(id);
            if (subCategory == null)
            {
                return HttpNotFound();
            }

            return View(subCategory);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            unitOfWork.SubCategoryRepository.Delete(id);
            unitOfWork.Save();
            return RedirectToAction("Index");
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

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private Category GetCategory(int id)
        {
            return unitOfWork.CategoryRepository.GetByID(id);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private SubCategory GetSubCategory(int id)
        {
            return unitOfWork.SubCategoryRepository.GetByID(id);
        }
    }
}
