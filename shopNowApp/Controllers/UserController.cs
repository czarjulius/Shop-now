using shopNowDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace shopNowApp.Controllers
{
    public class UserController : ApiController
    {

        [HttpGet]
        public HttpResponseMessage fetchAllUsers()
        {
            try
            {
                using (shop_now_DBEntities entities = new shop_now_DBEntities())
                {
                     return Request.CreateResponse(HttpStatusCode.OK, entities.USER.ToList());
                    
                }

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }


        }

        

        [HttpPost]
        public HttpResponseMessage addUser([FromBody] USER user)
        {
            try {

                using (shop_now_DBEntities entities = new shop_now_DBEntities())
                {
                    var newUser = new USER()
                    {
                        firstName = user.firstName,
                        lastName = user.lastName,
                        createdOn = DateTime.Now,
                        email = user.email,
                        password = user.password,
                        phone = user.phone,
                        address = user.address,
                        isAdmin = false
                    };

                    entities.USER.Add(newUser);
                    entities.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, newUser);

                    return message;
                }
            }
            catch (Exception ex) {

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
           
        }


    }
}
