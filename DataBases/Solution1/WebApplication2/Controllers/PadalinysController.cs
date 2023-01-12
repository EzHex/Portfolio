using Microsoft.AspNetCore.Mvc;
using WebApplication2.Repositories;
using WebApplication2.Models;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace WebApplication2.Controllers
{
    public class PadalinysController : Controller
    {
		public ActionResult Index()
		{
			return View(PadalinysRepo.List());
		}


		public ActionResult Create()
		{
			var pad = new Padalinys();
			PopulateSelections(pad);
			return View(pad);
		}


		[HttpPost]
		public ActionResult Create(Padalinys pad)
		{
			var match = PadalinysRepo.Find(pad.Id);
			//PopulateSelections(pad);

			if (match != null)
				ModelState.AddModelError("id_Padalinys", "Field value already exists in database.");

			if (ModelState.IsValid)
			{
				PadalinysRepo.Insert(pad);

				return RedirectToAction("Index");
			}
			
			return View(pad);
		}

		public ActionResult Edit(int id)
		{
			var pad = PadalinysRepo.Find(id);
			PopulateSelections(pad);
			return View(pad);
		}


		[HttpPost]
		public ActionResult Edit(string id, Padalinys pad)
		{
			if (ModelState.IsValid)
			{
				PadalinysRepo.Update(pad);


				return RedirectToAction("Index");
			}

			PopulateSelections(pad);
			return View(pad);
		}


		public ActionResult Delete(int id)
		{
			var pad = PadalinysRepo.Find(id);
			return View(pad);
		}


		[HttpPost]
		public ActionResult DeleteConfirm(int id)
		{
			try
			{
				PadalinysRepo.Delete(id);

				return RedirectToAction("Index");
			}
			catch (MySql.Data.MySqlClient.MySqlException)
			{
				ViewData["deletionNotPermitted"] = true;

				var pad = PadalinysRepo.Find(id);
				return View("Delete", pad);
			}
		}

		public void PopulateSelections(Padalinys pad)
        {
			var companies = ImoneRepo.List();

			pad.Lists.Companies =
				companies.Select(c =>
				{
					return
						new SelectListItem()
						{
							Value = Convert.ToString(c.Id),
							Text = c.Title
						};
				}).ToList();

		}
	}
}
