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

                string queryProduct = "SELECT * FROM Product WHERE productId =" + cart.productId;
                PRODUCT product = await db.PRODUCT.SqlQuery(queryProduct, cart.productId).SingleOrDefaultAsync();

                if (product == null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "There is no Product with Id " + cart.productId);
                }
                else
                {
                    string queryCart = "SELECT * FROM Cart WHERE productId = " + cart.productId;
                    CART cartFound = await db.CART.SqlQuery(queryCart, cart.productId).SingleOrDefaultAsync();

                    if (cartFound == null)
                    {
                        var newCart = new CART()
                        {
                            cartId = cart.cartId,
                            productId = cart.productId,
                            quantity =  cart.quantity | 1,
                            subTotal = cart.quantity * product.productPrice
                        };

                        db.CART.Add(newCart);
                        db.SaveChanges();

                        return Request.CreateResponse(HttpStatusCode.Created, newCart);
                    }
                    else
                    {
                        cartFound.quantity += 1;
                        cartFound.subTotal = cartFound.quantity * product.productPrice;

                        db.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.Created, cartFound);
                    }

                  
                }
  
            }
            catch (Exception ex) {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);

            }
        }


    }
}
