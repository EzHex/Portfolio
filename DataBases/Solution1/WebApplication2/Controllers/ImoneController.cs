using Microsoft.AspNetCore.Mvc;
using WebApplication2.Repositories;
using WebApplication2.Models;



namespace WebApplication2.Controllers
{
    public class ImoneController : Controller
    {
        public ActionResult Index()
        {
            return View(ImoneRepo.List());
        }

        public ActionResult Create()
        {
            var imone = new Imone();
            return View(imone);
        }

		[HttpPost]
		public ActionResult Create(Imone imone)
		{
			var match = ImoneRepo.Find(imone.Id);

			if (match != null)
				ModelState.AddModelError("id_Imone", "Field value already exists in database.");

			//form field validation passed?
			if (ModelState.IsValid)
			{
				ImoneRepo.Insert(imone);

				//save success, go back to the entity list
				return RedirectToAction("Index");
			}

			//form field validation failed, go back to the form
			return View(imone);
		}

		public ActionResult Edit(int id)
		{
			return View(ImoneRepo.Find(id));
		}

		[HttpPost]
		public ActionResult Edit(string id, Imone imone)
		{
			
			if (ModelState.IsValid)
			{
				ImoneRepo.Update(imone);

				return RedirectToAction("Index");
			}

			return View(imone);
		}

		public ActionResult Delete(int id)
		{
			var imone = ImoneRepo.Find(id);
			return View(imone);
		}

		[HttpPost]
		public ActionResult DeleteConfirm(int id)
		{
			//try deleting, this will fail if foreign key constraint fails
			try
			{
				ImoneRepo.Delete(id);

				//deletion success, redired to list form
				return RedirectToAction("Index");
			}
			//entity in use, deletion not permitted
			catch (MySql.Data.MySqlClient.MySqlException)
			{
				//enable explanatory message and show delete form
				ViewData["deletionNotPermitted"] = true;

				var imone = ImoneRepo.Find(id);
				return View("Delete", imone);
			}
		}
	}
}
