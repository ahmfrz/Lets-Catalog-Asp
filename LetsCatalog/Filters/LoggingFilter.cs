using Catalog.Entities;
using Catalog.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LetsCatalog.Filters
{
    public class LoggingFilter : ActionFilterAttribute, IActionFilter
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
        public LoggingFilter()
        {
        }
        #endregion

        #region IActionFilter
        /// <summary>
        ///
        /// </summary>
        /// <param name="filterContext"></param>
        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {
            // TODO: Need to implement Service Locater to inject instance of unit of work
            using (unitOfWork = new UnitOfWork())
            {
                ActionLog log = new ActionLog()
                {
                    Controller = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName,
                    Action = filterContext.ActionDescriptor.ActionName,
                    IP = filterContext.HttpContext.Request.UserHostAddress,
                    Time = filterContext.HttpContext.Timestamp
                };

                unitOfWork.ActionLogsRepository.Insert(log);
                unitOfWork.Save();
                OnActionExecuting(filterContext);
            }
        }
        #endregion
    }
}