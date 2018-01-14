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
    public class ProductsController : Controller
    {
        #region Private Members
        /// <summary>
        ///
        /// </summary>
        private IUnitOfWork unitOfWork;
        #endregion

        #region Constructors
        /// <summary>
        ///
        /// </summary>
        public ProductsController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        #endregion

        #region Controller Methods
        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View(unitOfWork.ProductRepository.Get());
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public ActionResult ShowProducts(int? categoryId, int? subcategoryId)
        {
            var products = unitOfWork.ProductRepository.Get(
                (prod) => prod.SubCategory.ID == subcategoryId && prod.SubCategory.Category.ID == categoryId,
                (prod) => prod.OrderByDescending(p => p.Created_Date)
                );

            if (products == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View("Index", products);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Details(int? categoryId, int? subcategoryId, int? productId)
        {
            return GetValidatedView(categoryId, subcategoryId, productId);
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
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AutoTimeFilter]
        public ActionResult Create([Bind(Include = "ID,Name,Description,Created_Date")] Product product, string subcategoriesList)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var subcategory = unitOfWork.SubCategoryRepository.Get((sub) => sub.Name == subcategoriesList).FirstOrDefault();
                    if (subcategory != null)
                    {
                        product.SubCategory = subcategory;
                        unitOfWork.ProductRepository.Insert(product);
                        unitOfWork.Save();
                        return RedirectToAction("Index", new { categoryId = subcategory.Category.ID, subcategoryId = subcategory.ID });
                    }
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }

            return View(product);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="subcategoryId"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        public ActionResult Edit(int? categoryId, int? subcategoryId, int? productId)
        {
            return GetValidatedView(categoryId, subcategoryId, productId);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Description,Created_Date")] Product product, string subcategoriesList)
        {
            int categoryId = 0, subcategoryId = 0, productId = 0;
            try
            {
                if (ModelState.IsValid)
                {
                    var subcategory = unitOfWork.SubCategoryRepository.Get((sub) => sub.Name == subcategoriesList).FirstOrDefault();
                    if (subcategory != null)
                    {
                        categoryId = subcategory.Category.ID;
                        subcategoryId = subcategory.ID;
                        productId = product.ID;
                        product.SubCategory = subcategory;
                        unitOfWork.ProductRepository.Update(product);
                        unitOfWork.Save();
                        return RedirectToAction("Index", new { categoryId = categoryId, subcategoryId = subcategoryId });
                    }
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }

            return RedirectToAction("Edit", new { categoryId = categoryId, subcategoryId = subcategoryId, productId = productId });
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(int? categoryId, int? subcategoryId, int? productId)
        {
            return GetValidatedView(categoryId, subcategoryId, productId);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? categoryId, int? subcategoryId, int? productId)
        {
            var product = unitOfWork.ProductRepository.GetByID(productId);
            if (product == null || product.SubCategory.ID != subcategoryId || product.SubCategory.Category.ID != categoryId)
            {
                unitOfWork.ProductRepository.Delete(product);
                unitOfWork.Save();
            }

            return RedirectToAction("Index", new { categoryId = categoryId, subcategoryId = subcategoryId });
        }
        #endregion

        #region IDisposable
        /// <summary>
        ///
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                unitOfWork.Dispose();
            }

            base.Dispose(disposing);
        }
        #endregion

        #region Private Methods
        /// <summary>
        ///
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="subcategoryId"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        private ActionResult GetValidatedView(int? categoryId, int? subcategoryId, int? productId)
        {
            if (categoryId == null || subcategoryId == null || productId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var product = unitOfWork.ProductRepository.GetByID(productId);

            if (product == null || product.SubCategory.ID != subcategoryId || product.SubCategory.Category.ID != categoryId)
            {
                return HttpNotFound();
            }

            return View(product);
        }
        #endregion
    }
}
