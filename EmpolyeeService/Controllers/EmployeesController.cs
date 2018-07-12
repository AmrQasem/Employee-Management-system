using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EmployeeDataAccess;

namespace EmpolyeeService.Controllers
{
    public class EmployeesController : ApiController
    {
        EmployeeDBEntities entities = new EmployeeDBEntities();

        //[HttpGet]
        /// <summary>
        /// if i need send an parametar to the api and get data using specific filter
        /// </summary>
        /// <param name="gender"></param>
        /// <returns></returns>
        //public HttpResponseMessage Get(string gender = "All")
        //{
        //    switch (gender.ToLower())
        //    {
        //        case "all":
        //            return Request.CreateResponse(HttpStatusCode.OK, entities.Employees.ToList());
        //        case "male":
        //            return Request.CreateResponse(HttpStatusCode.OK, entities.Employees.Where(e => e.Gender.ToLower() == "male").ToList());
        //        case "female":
        //            return Request.CreateResponse(HttpStatusCode.OK, entities.Employees.Where(e => e.Gender.ToLower() == "female").ToList());
        //        default:
        //            return Request.CreateResponse(HttpStatusCode.BadRequest, "in valid gender");
        //    }
        //}



        //[HttpGet]

        public IEnumerable<Employee> Get()
        {
            return entities.Employees.ToList();
        }

        public HttpResponseMessage Get(int Id)
        {
            var empData = entities.Employees.FirstOrDefault(e => e.ID == Id);
            if (empData != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, empData);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with ID = " + Id.ToString() + " is not found ");
            }
        }

        public HttpResponseMessage Post([FromBody]Employee emp)
        {
            try
            {
                entities.Employees.Add(emp);
                entities.SaveChanges();

                var msg = Request.CreateResponse(HttpStatusCode.Created, emp);
                msg.Headers.Location = new Uri(Request.RequestUri + "/" + emp.ID.ToString());
                return msg;
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage Delete(int ID)
        {
            try
            {

                var empData = entities.Employees.FirstOrDefault(e => e.ID == ID);
                if (empData != null)
                {
                    entities.Employees.Remove(empData);
                    entities.SaveChanges();
                    var msg = Request.CreateResponse(HttpStatusCode.OK, ID);
                    return msg;
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with ID = " + ID.ToString() + " is not found ");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage Put(int id, [FromBody] Employee emp)
        {
            try
            {

                var entity = entities.Employees.FirstOrDefault(e => e.ID == id);
                if (entity != null)
                {
                    entity.FirstName = emp.FirstName;
                    entity.LastName = emp.LastName;
                    entity.Salary = emp.Salary;
                    entity.Gender = emp.Gender;

                    entities.SaveChanges();

                    var msg = Request.CreateResponse(HttpStatusCode.OK, entity);
                    return msg;
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Employee with ID " + id.ToString() + " is not found");
                }
            }
            catch(Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex);
            }

        }
    }
}