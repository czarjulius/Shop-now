using shopNowDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace shopNowApp.Controllers
{
    public class CategoryController : ApiController
    {
        private shop_now_DBEntities db;

        public CategoryController()
        {
            db = new shop_now_DBEntities();
        }

        [HttpGet]
        public HttpResponseMessage fetchAllCategories()
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, db.CATEGORY.ToList());
                
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }


        }

        [HttpPost]
        public HttpResponseMessage addCategory([FromBody] CATEGORY category)
        {
            try
            {
                var newCategory = new CATEGORY()
                {
                    catName = category.catName
                };

                db.CATEGORY.Add(newCategory);
                db.SaveChanges();

                var message = Request.CreateResponse(HttpStatusCode.Created, newCategory );

                return message;
            }
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }
    }
}
