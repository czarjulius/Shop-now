using shopNowDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using shopNowApp.Models;

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
        [Route("api/cart/{cartId}/{productId}")]
        public HttpResponseMessage addToCart(int cartId, int productId)
        {
            try
            {

                PRODUCT product = db.PRODUCT.Where(u => u.productId == productId).FirstOrDefault();
                //USER queryUser = db.USER.Where(u => u.cartId == cart.cartId).FirstOrDefault();
                CART cartFound = db.CART.Where(u => u.productId == productId && u.cartId == cartId).FirstOrDefault();


                if (product == null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "There is no Product with Id " + productId);
                }
                else
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
                            cartId = cartId,
                            productId = productId,
                            //quantity = cart.quantity == 0? 1:cart.quantity,
                            quantity = 1,
                            isClosed = false,
                            subTotal = product.productPrice
                        };

                        db.CART.Add(newCart);
                        db.SaveChanges();

                        return Request.CreateResponse(HttpStatusCode.Created, newCart);


                    }


                }

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);

            }
        }


        [HttpGet]
        [Route("api/cart/{cartId}")]
        public HttpResponseMessage fetchUserCart(int cartId)
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


        [HttpDelete]
        [Route("api/cart/{id}")]
        public HttpResponseMessage removeCartItemById(int id)
        {
            try
            {
                CART cart = db.CART.Where(x => x.Id == id).FirstOrDefault();
                if (cart == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Cart Product with id " + id.ToString() + " not found");
                }
                else
                {
                    db.CART.Remove(cart);
                    db.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK);

                }

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);

            }

        }


        [HttpPut]
        [Route("api/cart/{id}")]
        public HttpResponseMessage editEmployeeById(int id, [FromBody] CART cart)
        {
            try
            {


                var cartToEdit = db.CART.Where(x => x.Id == id).FirstOrDefault();
                var product = db.PRODUCT.Where(u => u.productId == cartToEdit.productId).FirstOrDefault();

                if (cartToEdit == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Cart Product with id " + id.ToString() + " not found");
                }
                else
                {
                    cartToEdit.quantity = cart.quantity;
                    cartToEdit.subTotal = cart.quantity * product.productPrice;

                    db.SaveChanges();

                    return Request.CreateResponse(HttpStatusCode.OK, cartToEdit);

                }


            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        // The endpoint for checkout details
        [HttpGet]
        [Route("api/checkout/{cartId}")]
        public HttpResponseMessage checkoutReceipt(int cartId)
        {
            try
            {

                //UserCart queryUserCart = UserCart.u.Where(u => u.cartId == cartId).FirstOrDefault();
                UserCart queryUserCart = new UserCart();

                queryUserCart.user =  db.USER.Where(u => u.cartId == cartId).FirstOrDefault();
            



                //USER queryUser = db.USER.Where(u => u.cartId == cartId).FirstOrDefault();

            if (queryUserCart.user == null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, "User with cart Id " + cartId + " not found");
            }
            else
            {

                    queryUserCart.cart = db.CART.Where(u => u.cartId == cartId && u.isClosed == false).ToList();



                return Request.CreateResponse(HttpStatusCode.OK, queryUserCart);


            }


        }
        catch (Exception ex)
        {
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
        }


}


    }
}
