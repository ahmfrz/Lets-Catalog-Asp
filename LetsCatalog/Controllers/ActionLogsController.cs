using Catalog.Entities;
using Catalog.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LetsCatalog.Controllers
{
    public class ActionLogsController : Controller
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
        public ActionLogsController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        #endregion

        #region Controller Methods
        // GET: ActionLogs
        public ActionResult Index()
        {
            var logs = unitOfWork.ActionLogsRepository.Get(orderBy:log => log.OrderByDescending(l => l.Time));
            return View(logs);
        }
        #endregion
    }
}