using Catalog.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LetsCatalog.Filters
{
    public class AutoTimeFilter : ActionFilterAttribute, IActionFilter
    {
        #region IActionFilter
        /// <summary>
        ///
        /// </summary>
        /// <param name="filterContext"></param>
        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {
            object temp;
            if (filterContext.ActionParameters.TryGetValue("category", out temp))
            {
                (temp as Category).Created_Date = DateTime.Now;
            }
            else if (filterContext.ActionParameters.TryGetValue("subCategory", out temp))
            {
                (temp as SubCategory).Created_Date = DateTime.Now;
            }
            else if (filterContext.ActionParameters.TryGetValue("product", out temp))
            {
                var product = temp as Product;
                product.Created_Date = DateTime.Now;
                if (product.Brand != null)
                {
                    product.Brand.Created_Date = DateTime.Now;
                }
            }
        }
        #endregion
    }
}