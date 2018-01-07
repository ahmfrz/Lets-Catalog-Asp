using Catalog.Entities;
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

        public ActionResult Create()
        {
            return View();
        }
    }
}