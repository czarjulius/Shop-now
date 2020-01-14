using shopNowApp.Models;
using shopNowDataAccess;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace shopNowApp.Controllers
{
    public class ProductController : ApiController
    {
        private shop_now_DBEntities db;

        public ProductController()
        {
            db = new shop_now_DBEntities();
        }

        [HttpPost]
        public HttpResponseMessage addProducts([FromBody]PRODUCT product)
        {
            try
            {
                //var ctx = HttpContext.Current;
                //var root = ctx.Server.MapPath("~/App_Data");
                //var provider = new MultipartFormDataStreamProvider(root);

                //  await Request.Content.ReadAsMultipartAsync(provider);

                //    foreach (var file in provider.FileData)
                //    {
                //        var name = file.Headers.ContentDisposition.FileName;

                //        // remove double quote from string
                //        name = name.Trim('"');

                //        var localFileName = file.LocalFileName;
                //        var filePath = Path.Combine(root, name);

                //        File.Move(localFileName, filePath);

                //    }
               
                var newProduct = new PRODUCT()
                                      {
                                          productName = product.productName,
                                          productImage = "image",
                                          catId = product.catId,
                                          productPrice = product.productPrice,
                                          isAvailable = true,
                                          createdOn = DateTime.Now,
                                          productDescription = product.productDescription,
                                      };
                db.PRODUCT.Add(newProduct);
                db.SaveChanges();


                var productJustCreated = db.PRODUCT.Where(p => p.productId == newProduct.productId).Select(p => new ProductDTO
                {
                    productId = p.productId,
                    productName = p.productName,
                    productImage = p.productImage,
                    catName = p.CATEGORY.catName,
                    productPrice = p.productPrice,
                    isAvailable = p.isAvailable,
                    createdOn = p.createdOn,
                    productDescription = p.productDescription,
                }).FirstOrDefault();

                var message = Request.CreateResponse(HttpStatusCode.Created, productJustCreated);
                return message;
            }
            catch (Exception ex )
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }


        }


        [HttpGet]
        [ResponseType(typeof(ProductDTO))]
        public HttpResponseMessage fetchAllProducts()
        {
            try
            {
                var productsFetched = from p in db.PRODUCT
                                      select new ProductDTO()
                                      {
                                          productId = p.productId,
                                          productName = p.productName,
                                          productImage = p.productImage,
                                          catName = p.CATEGORY.catName,
                                          productPrice = p.productPrice,
                                          isAvailable = p.isAvailable,
                                          createdOn = p.createdOn,
                                          productDescription = p.productDescription,
                                      };
                return Request.CreateResponse(HttpStatusCode.OK, productsFetched);
            }
            catch (Exception ex )
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }


        }


        [HttpGet]
        public HttpResponseMessage fetchProductById(int id)
        {
            try
            {
                var productFetched  = db.PRODUCT.Where(p => p.productId == id).Select(p => new ProductDTO
                                      {
                                          productId = p.productId,
                                          productName = p.productName,
                                          productImage = p.productImage,
                                          catName = p.CATEGORY.catName,
                                          productPrice = p.productPrice,
                                          isAvailable = p.isAvailable,
                                          createdOn = p.createdOn,
                                          productDescription = p.productDescription,
                                      }).FirstOrDefault();

                if(productFetched != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, productFetched);

                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Product with Id "+ id + " not found");
                }
            }
            catch (Exception ex )
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }


        }

        [HttpGet]
        [Route("api/product/available")]
        [ResponseType(typeof(ProductDTO))]
        public HttpResponseMessage fetchAllAvailableProducts()
        {
            try
            {
                var avalaibleProducts = db.PRODUCT.Where(p => p.isAvailable == true).Select(p => new ProductDTO
                {
                    productId = p.productId,
                    productName = p.productName,
                    productImage = p.productImage,
                    catName = p.CATEGORY.catName,
                    productPrice = p.productPrice,
                    isAvailable = p.isAvailable,
                    createdOn = p.createdOn,
                    productDescription = p.productDescription,
                }).ToList();
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
