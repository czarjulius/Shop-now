using shopNowDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using shopNowApp.Models;


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

                var categoriesFetched = from c in db.CATEGORY
                                      select new CategoryDTO()
                                      {
                                          catId = c.catId,
                                          catName = c.catName
                                      };
                return Request.CreateResponse(HttpStatusCode.OK, categoriesFetched);
                
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


                var categoryJustCreated = db.CATEGORY.Where(c => c.catId == newCategory.catId).Select(c => new CategoryDTO
                {
                    catId = c.catId,
                    catName = category.catName

                }).FirstOrDefault();

                var message = Request.CreateResponse(HttpStatusCode.Created, categoryJustCreated);
                return message;
            }
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }
    }
}
