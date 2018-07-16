using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EmployeeWeb.ViewModel;
using System.Net.Http;
using System.Net.Http.Headers;


namespace EmployeeWeb.Controllers
{
    public class EmployeeController : Controller
    {
        HttpClient client = new HttpClient();
        public EmployeeController()
        {
            client.BaseAddress = new Uri("http://localhost:60395/api/");
            client.DefaultRequestHeaders.Accept.Add(
                     new MediaTypeWithQualityHeaderValue("application/json"));
        }

        // GET: Employee
        public ActionResult Index()
        {
            List<EmployeeVM> emp = new List<EmployeeVM>();
            HttpResponseMessage response = client.GetAsync("employees").Result;
            if (response.IsSuccessStatusCode)
            {
                emp = response.Content.ReadAsAsync<List<EmployeeVM>>().Result;
            }
            return View(emp);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(EmployeeVM emp)
        {
            client.PostAsJsonAsync<EmployeeVM>("employees", emp).ContinueWith((e => e.Result.EnsureSuccessStatusCode()));
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var EmployeeDetails = client.GetAsync("employees/"+id.ToString()).Result;
            return View(EmployeeDetails.Content.ReadAsAsync<EmployeeVM>().Result);
        }

        [HttpPost]
        public ActionResult Edit(EmployeeVM emp)
        {
            var editedEmployee = client.PutAsJsonAsync<EmployeeVM>("employees/"+emp.ID, emp).Result;
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int ID)
        {
            var EmployeeDetails = client.DeleteAsync("employees/" + ID.ToString()).Result;
            return RedirectToAction("Index");
        }
    }
}