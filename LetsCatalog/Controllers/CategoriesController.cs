using Catalog.Entities;
using Catalog.Models.Entities;
using LetsCatalog.Filters;
using System.Linq;
using System.Data;
using System.Net;
using System.Web.Mvc;

namespace LetsCatalog.Controllers
{
    [LoggingFilter]
    public class CategoriesController : Controller
    {
        #region Private Members
        /// <summary>
        /// Private member to hold injected instance
        /// </summary>
        private IUnitOfWork unitOfWork;
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of CategoriesController
        /// </summary>
        /// <param name="db">The injected db instance</param>
        public CategoriesController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        #endregion

        #region Controller Methods
        /// <summary>
        /// Gets the default category view
        /// </summary>
        /// <returns>The category index view</returns>
        public ActionResult Index()
        {
            var categories = unitOfWork.CategoryRepository.Get(orderBy: cat => cat.OrderByDescending(c => c.Created_Date));
            return View(categories);
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
        [AutoTimeFilter]
        public ActionResult Create([Bind(Include = "ID,Name,Created_Date")] Category category)
        {
            try
            {
                if (ModelState.IsValid)
                {
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
    }
}