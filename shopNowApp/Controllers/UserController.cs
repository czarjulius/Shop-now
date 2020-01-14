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
    public class UserController : ApiController
    {
        private shop_now_DBEntities db;

        public UserController()
        {
            db = new shop_now_DBEntities();
        }


        [HttpGet]
        public HttpResponseMessage fetchAllUsers()
        {
            try
            {
                var allCustomers = from u in db.USER
                                   select new UserDTO()
                                   {
                                        userId = u.userId,
                                        firstName = u.firstName,
                                        lastName = u.lastName,
                                        email = u.email,
                                        createdOn = u.createdOn,
                                        phone = u.phone,
                                        address = u.address,
                                        isAdmin = u.isAdmin,
                                        cartId = u.cartId
                                    };

                if (allCustomers != null) {
                    return Request.CreateResponse(HttpStatusCode.OK, allCustomers);

                } else {
                    return Request.CreateResponse(HttpStatusCode.OK, "No Customer Created yet");
                }


            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }


        }


        [HttpPost]
        public HttpResponseMessage userSignup([FromBody] USER user)
        {
            try {

                Random generator = new Random();
                String r = generator.Next(0, 999999).ToString("D6");

                var newUser = new USER()
                    {
                        firstName = user.firstName,
                        lastName = user.lastName,
                        createdOn = DateTime.Now,
                        email = user.email,
                        password = user.password,
                        phone = user.phone,
                        address = user.address,
                        isAdmin = false,
                        cartId = int.Parse(r)
            };

                    db.USER.Add(newUser);
                    db.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, newUser);

                    return message;
            }
            catch (Exception ex) {

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
           
        }


        [Route("api/login")]
        [HttpPost]
        public HttpResponseMessage userLogin([FromBody] USER user)
        {
            try {

                    var checkUser = db.USER.Where(e => e.email == user.email && e.password == user.password ).FirstOrDefault();

                    if (checkUser != null)
                    {

                        return Request.CreateResponse(HttpStatusCode.OK, "Login Successful");
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Email Or Password Incorrect");
                    }



            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
