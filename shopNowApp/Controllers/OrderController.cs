﻿using shopNowDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace shopNowApp.Controllers
{
    public class OrderController : ApiController
    {
        private shop_now_DBEntities db;

        public OrderController()
        {
            db = new shop_now_DBEntities();
        }

        [HttpGet]
        public HttpResponseMessage fetchAllOrder(int id)
        {
            try
            {
                var AllOrderByUserId = db.ORDER.Where(u => u.userId == id).ToList();

                return Request.CreateResponse(HttpStatusCode.OK, AllOrderByUserId);


            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }


        }
    }
}
