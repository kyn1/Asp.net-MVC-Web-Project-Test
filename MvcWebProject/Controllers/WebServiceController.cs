using MvcWebProject.Models;
using MvcWebProject.PersonService;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcWebProject.Controllers
{
    public class WebServiceController : Controller
    {
        
        // GET: WebService
        /// <summary>
        /// Displays a view for web service operations.
        /// </summary>
        public ActionResult WebService()
        {
            return View();
        }

        /// <summary>
        /// Displays a view for deleting an entity.
        /// </summary>
        public ActionResult Delete(int id)
         {
            try
            {
                var model = new Entity
                {
                    Id = id
                };
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred while processing your request.";
                return View("Error");
            }
        }

        // POST: Person/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int Id)
        {
            try
            {
                // Validate that the 'id' parameter is not null
                if (Id <= 0)
                {
                    ViewBag.ErrorMessage = "Invalid person ID.";
                    return View("Error");
                }

                // Create an instance of the service client
                using (PersonServices.CRUDServiceSoapClient obj = new PersonServices.CRUDServiceSoapClient())
                {
                    // Call the DeletePerson method of the service with the correct parameter name
                    obj.DeletePerson(personId: Id);
                }

                TempData["SuccessMessage"] = "Successsfully deleted!";
                // Redirect to the desired action after successful deletion
                return RedirectToAction("List", "WebService");
            }
            catch (Exception ex)
            {
                // Handle exceptions, log error, or return an error view
                ViewBag.ErrorMessage = "An error occurred while deleting the person.";
                return View("Error");
            }
        }




        /// <summary>
        /// Displays a list of entities.
        /// </summary>
        [HttpGet]
        public ActionResult List()
        {
            PersonServices.CRUDServiceSoapClient obj = new PersonServices.CRUDServiceSoapClient();
            DataTable dataTable = obj.GetPersons();

            // Check if the DataTable contains the "Id" column
            if (!dataTable.Columns.Contains("Id"))
            {
                ViewBag.ErrorMessage = "The 'Id' column is missing in the data.";
                return View("Error");
            }

            List<MvcWebProject.Models.Entity> entities = ConvertDataTableToEntities(dataTable);
            return View(entities);
        }

        private List<MvcWebProject.Models.Entity> ConvertDataTableToEntities(DataTable dataTable)
        {
            List<MvcWebProject.Models.Entity> entities = new List<MvcWebProject.Models.Entity>();

            foreach (DataRow row in dataTable.Rows)
            {
                // Assuming "Id" is the name of the column containing IDs in the DataTable
                int id = Convert.ToInt32(row["Id"]); // Map the ID column from the DataTable to the Id property

                MvcWebProject.Models.Entity entity = new MvcWebProject.Models.Entity
                {
                    Id = id,
                    Name = row["Name"].ToString(),    // Map other columns similarly
                    Surname = row["Surname"].ToString(),
                    Age = Convert.ToInt32(row["Age"])
                };

                entities.Add(entity);
            }

            return entities;
        }


        /// <summary>
        /// Searches for entities based on a given name.
        /// </summary>
        public ActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Search(string name)
        {
            PersonServices.CRUDServiceSoapClient obj = new PersonServices.CRUDServiceSoapClient();
            DataTable dataTable = obj.GetPersons(); // Assuming GetPersons() returns a DataTable
            List<MvcWebProject.Models.Entity> entities = ConvertDataTableToEntities(dataTable);

            // Filter entities by name
            List<MvcWebProject.Models.Entity> filteredEntities = entities.Where(e => e.Name.ToLower().Contains(name.ToLower())).ToList();
            if (filteredEntities.Count > 0)
            {
                TempData["SuccessMessage"] = "Successsfull fetch!";
            }
            else
            {
                TempData["SuccessError"] = "Person not found";
            }

            return View(filteredEntities);
        }

        /// <summary>
        /// Displays a form for adding a new entity.
        /// </summary>
        public ActionResult Add()
        {
            var model = new EntityViewModel();
            return View(model);
        }


        /// <summary>
        /// Displays a form for adding a new entity.
        /// </summary>
        [HttpPost]
        public ActionResult Add(EntityViewModel model)
        {
            using (PersonServices.CRUDServiceSoapClient obj = new PersonServices.CRUDServiceSoapClient())
            {
                // Fully qualify the namespace for the Person class
                MvcWebProject.PersonServices.Person pers = new MvcWebProject.PersonServices.Person
                {
                    Name = model.Name,
                    Age = model.Age,
                    Surname = model.Surname
                };

                string result = obj.SavePerson(pers);

                ViewBag.Message = result;
                return View();
            }
        }
    }
}