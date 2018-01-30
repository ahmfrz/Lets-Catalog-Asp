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
        public ActionResult Create(int? categoryId, int? subcategoryId)
        {
            if (subcategoryId == null || categoryId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Category ID and Subcategory ID are required");
            }

            TempData["CurrentSubCategory"] = unitOfWork.SubCategoryRepository.Get(sub => sub.ID == subcategoryId && sub.Category.ID == categoryId).FirstOrDefault();
            if (TempData["CurrentSubCategory"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Subcategory not found");
            }

            TempData["SubCategories"] = unitOfWork.SubCategoryRepository.Get();
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
        public ActionResult Create([Bind(Include = "ID,Name,Description,Created_Date,Brand,ProductSpecs,ProductPics")] Product product, string subcategoriesList)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var subcategory = unitOfWork.SubCategoryRepository.Get((sub) => sub.Name == subcategoriesList).FirstOrDefault();
                    if (subcategory != null)
                    {
                        product.SubCategory = subcategory;
                        product.ProductPics.Product = product;
                        product.ProductSpecs.Product = product;
                        UpdateBrand(product, subcategory);
                        unitOfWork.ProductRepository.Insert(product);
                        unitOfWork.Save();
                        return RedirectToAction("ShowProducts", new { categoryId = subcategory.Category.ID, subcategoryId = subcategory.ID });
                    }
                    else
                    {
                        ModelState.AddModelError("", "Subcategory not found");
                    }
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }

            TempData["CurrentSubCategory"] = unitOfWork.SubCategoryRepository.Get(sub => sub.Name == subcategoriesList).FirstOrDefault();
            TempData["SubCategories"] = unitOfWork.SubCategoryRepository.Get();
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
            var sub = unitOfWork.SubCategoryRepository.Get(includeProperties: "Brands");
            var selected = sub?.Where(s => s.ID == subcategoryId && s.Category.ID == categoryId).FirstOrDefault();
            ViewBag.SubCategories = new SelectList(sub, "ID", "Name", selected?.Name);
            return GetValidatedView(categoryId, subcategoryId, productId);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "ProductID,Name,Description,Created_Date,Brand,ProductSpecs,ProductPics,SubCategory")] Product product)
        public ActionResult EditPost(int? categoryId, int? subcategoryId, int? productId)
        {
            //int categoryId = 0, subcategoryId = 0, productId = 0;
            try
            {
                if (ModelState.IsValid)
                {
                    var productToUpdate = unitOfWork.ProductRepository.GetByID(productId);
                    if (TryUpdateModel(productToUpdate, "",
                            new string[] { "Name", "Description", "ProductSpecs", "ProductPics", "SubCategory" }))
                    {
                        unitOfWork.Save();
                    }
                    //var subcategory = unitOfWork.SubCategoryRepository.Get((s) => s.Name == product.SubCategory.Name).FirstOrDefault();
                    //if (subcategory != null)
                    //{
                    //    categoryId = subcategory.Category.ID;
                    //    subcategoryId = subcategory.ID;
                    //    productId = product.ProductID;
                    //    product.SubCategory = subcategory;
                    //    UpdateBrand(product, subcategory);
                    //    unitOfWork.BrandRepository.Update(product.Brand);
                    //    unitOfWork.ProductRepository.Update(product);
                    //    unitOfWork.ProductPicsRepository.Update(product.ProductPics);
                    //    unitOfWork.ProductSpecsRepository.Update(product.ProductSpecs);
                    //    unitOfWork.Save();
                    //    return RedirectToAction("ShowProducts", new { categoryId = categoryId, subcategoryId = subcategoryId });
                    //}
                    //else
                    //{
                    //    return new HttpStatusCodeResult(HttpStatusCode.NotFound, "Subcategory not found");
                    //}
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }

            var sub = unitOfWork.SubCategoryRepository.Get(includeProperties: "Brands");
            var selected = sub?.Where(s => s.ID == subcategoryId && s.Category.ID == categoryId).FirstOrDefault();
            ViewBag.SubCategories = new SelectList(sub, "ID", "Name", selected?.Name);
            return RedirectToAction("Edit", new { categoryId = categoryId, subcategoryId = subcategoryId, productId = productId });
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="product"></param>
        /// <param name="subcategory"></param>
        private void UpdateBrand(Product product, SubCategory subcategory)
        {
            var brand = subcategory?.Brands?.FirstOrDefault(b => b.Name == product.Brand.Name);
            if (brand == null)
            {
                product.Brand.SubCategory = subcategory;
            }
            else
            {
                brand.SubCategory = subcategory;
                unitOfWork.BrandRepository.Update(brand);
            }
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
            if (product != null || product.SubCategory.ID != subcategoryId || product.SubCategory.Category.ID != categoryId)
            {
                var pics = unitOfWork.ProductPicsRepository.GetByID(product.ProductID);
                var specs = unitOfWork.ProductSpecsRepository.GetByID(product.ProductID);
                if (pics != null)
                {
                    unitOfWork.ProductPicsRepository.Delete(pics);
                }

                if (specs != null)
                {
                    unitOfWork.ProductSpecsRepository.Delete(specs);
                }

                unitOfWork.ProductRepository.Delete(product);
                unitOfWork.Save();
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound, "Product not found");
            }

            return RedirectToAction("ShowProducts", new { categoryId = categoryId, subcategoryId = subcategoryId });
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
