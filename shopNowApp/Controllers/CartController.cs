using shopNowDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace shopNowApp.Controllers
{
    public class CartController : ApiController
    {
        private shop_now_DBEntities db;

        public CartController()
        {
            db = new shop_now_DBEntities();
        }


        [HttpPost]
        public async Task<HttpResponseMessage> addToCart([FromBody] CART cart)
        {
            try {

                PRODUCT product = db.PRODUCT.Where(u => u.productId == cart.productId).FirstOrDefault();
                USER queryUser = db.USER.Where(u => u.cartId == cart.cartId).FirstOrDefault();
                CART cartFound = db.CART.Where(u => u.productId == cart.productId).FirstOrDefault();


                if (product == null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "There is no Product with Id " + cart.productId);
                }else
                {
                    

                    if (cartFound != null)
                    {
                        cartFound.quantity += 1;
                        cartFound.subTotal = cartFound.quantity * product.productPrice;

                        db.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.Created, cartFound);
                    }
                    else
                    {
                       
                        var newCart = new CART()
                        {
                            cartId = cart.cartId,
                            productId = cart.productId,
                            quantity = cart.quantity | 1,
                            subTotal = cart.quantity * product.productPrice
                        };

                        db.CART.Add(newCart);
                        db.SaveChanges();

                        return Request.CreateResponse(HttpStatusCode.Created, newCart);


                    }

                  
                }
  
            }
            catch (Exception ex) {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);

            }
        }


        [HttpGet]
        [Route("api/cart/{cartId}")]
        public async Task<HttpResponseMessage> fetchUserCart(int cartId)
        {
            try
            {
                USER queryUser = db.USER.Where(u => u.cartId == cartId).FirstOrDefault();

                if (queryUser == null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "User with cart Id " + cartId + " not found");
                }
                else
                {

                    var userCart = db.CART.Where(u => u.cartId == cartId).ToList();

                    if (userCart.Count() > 0)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, userCart);

                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "Cart Is Empty");

                    }
                }
                

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }


        }


        
    }
}
