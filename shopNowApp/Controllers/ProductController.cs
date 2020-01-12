using shopNowDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace shopNowApp.Controllers
{
    public class ProductController : ApiController
    {
        private shop_now_DBEntities db;

        public ProductController()
        {
            db = new shop_now_DBEntities();
        }

        [HttpGet]
        public HttpResponseMessage fetchAllProducts()
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, db.PRODUCT.ToList());


            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }


        }

        [HttpGet]
        [Route("api/product/available")]
        public HttpResponseMessage fetchAllAvailableProducts()
        {
            try
            {
                var avalaibleProducts = db.PRODUCT.Where(e => e.isAvailable == true).ToList();
                if (avalaibleProducts.Count() > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, avalaibleProducts);
                }
                return Request.CreateResponse(HttpStatusCode.OK, "There is no available Product");


            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }


        }
    }
}
