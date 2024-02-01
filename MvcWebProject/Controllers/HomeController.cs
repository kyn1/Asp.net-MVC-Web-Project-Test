using MvcWebProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcWebProject.Controllers
{
    public class HomeController : Controller
    {
        private AppDbContext db = new AppDbContext();

        /// <summary>
        /// Displays the default view.
        /// </summary>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Displays a view for linking entities.
        /// </summary>
        public ActionResult LinkEntity()
        {
            return View();
        }

        /// <summary>
        /// Displays a view for web service operations.
        /// </summary>
        public ActionResult WebService()
        {
            return View();
        }

        /// <summary>
        /// Displays a form for adding a new entity.
        /// </summary>
        public ActionResult Add()
        {
            return View();
        }

        /// <summary>
        /// Handles the addition of a new entity.
        /// </summary>
        /// <param name="model">The view model containing data for the new entity.</param>
        [HttpPost]
        public ActionResult Add(EntityViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var newEntity = new Entity
                    {
                        Name = model.Name,
                        Surname = model.Surname,
                        Age = model.Age
                    };

                    db.Entities.Add(newEntity);
                    db.SaveChanges();
                    TempData["SussMessage"] = "Record created successfully.";
                    return RedirectToAction("Add");
                }
                catch (Exception ex)
                {
                    ViewBag.ErroMessage = "An error occurred while adding the record! " + ex.Message;
                    return View("Error");
                }
            }
            else
            {
                return View(model);
            }
        }

        /// <summary>
        /// Displays a view for deleting an entity.
        /// </summary>
        public ActionResult Delete()
        {
            return View();
        }

        /// <summary>
        /// Handles the deletion of an entity.
        /// </summary>
        /// <param name="model">The view model containing the name of the entity to be deleted.</param>
        [HttpPost]
        public ActionResult Delete(DeleteViewModel model)
        {
            var entityToDelete = db.Entities.FirstOrDefault(e => e.Name == model.Name);
            if (entityToDelete != null)
            {
                db.Entities.Remove(entityToDelete);
                db.SaveChanges();
                TempData["SuccMessage"] = "Record deleted successfully.";
            }
            else
            {
                TempData["ErroMessage"] = "Record not found.";
            }
            return RedirectToAction("Delete");
        }

        /// <summary>
        /// Displays a list of entities.
        /// </summary>
        [HttpGet]
        public ActionResult List()
        {
            List<Entity> people = db.Entities.ToList();
            return View(people);
        }

        

        /// <summary>
        /// Searches for entities based on a given name.
        /// </summary>
        /// <param name="name">The name to search for.</param>
        public ActionResult Search(string name)
        {
            List<Entity> people = db.Entities.Where(e => e.Name.Contains(name)).ToList();
            
            if (people.Any())
            {
                TempData["SuccesMessage"] = "Record retrieved successfully.";
                return View(people);
            }
            else
            {
                TempData["ErrorMessage"] = "Record not found.";
                return View();
            }
            
        }




    }
}