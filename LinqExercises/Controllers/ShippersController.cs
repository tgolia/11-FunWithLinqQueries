﻿using LinqExercises.Infrastructure;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace LinqExercises.Controllers
{
    public class ShippersController : ApiController
    {
        private NORTHWNDEntities _db;

        public ShippersController()
        {
            _db = new NORTHWNDEntities();
        }

        //GET: api/shippers/reports/freight
        [HttpGet, Route("api/shippers/reports/freight"), ResponseType(typeof(IQueryable<object>))]
        public IHttpActionResult GetFreightReport()
        {
            // See this blog post for more information about projecting to anonymous objects. https://blogs.msdn.microsoft.com/swiss_dpe_team/2008/01/25/using-your-own-defined-type-in-a-linq-query-expression/
            /*throw new NotImplementedException(@"
                Write a query to return an array of anonymous objects that have two properties. 

                1. A Shipper property containing that particular shipper.
                2. A FreightTotals property containing the freight totals for that shipper

                Return the rows ordered by FreightTotals
            ");*/
            var resultSet = _db.Shippers
                   .Select(s => new
                   {
                       Shipper = new
                       {
                           s.ShipperID,
                           s.CompanyName,
                           s.Phone
                       },
                       FreightTotals = s.Orders.Sum(o => o.Freight)//.GroupBy(o => o.ShipVia)
                   }
                   ).OrderBy(s => s.FreightTotals);

            return Ok(resultSet);
        }

        //GET: api/shippers/reports/ordertime
        [HttpGet, Route("api/shippers/reports/ordertime"), ResponseType(typeof(IQueryable<object>))]
        public IHttpActionResult OrderToShippedReport()
        {
            /*
                Write a query to return an array of anonymous objects that have two properties. 

                1. A Shipper property containing that particular shipper.
                2. A OrderToShipped property containing the average amount of days between the OrderDate and ShippedDate by Shipper.

                Return the rows ordered by OrderToShipped
            ");*/
            var resultSet = _db.Shippers
                   .Select(s => new
                   {
                       Shipper = new
                       {
                           s.ShipperID,
                           s.CompanyName,
                           s.Phone
                       },
                       OrderToShipped = s.Orders.Average(o => DbFunctions.DiffDays(o.OrderDate,o.ShippedDate))
                   })
                   .OrderBy(s => s.OrderToShipped);
            Console.Write(resultSet);
            return Ok(resultSet);
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
        }
    }
}
