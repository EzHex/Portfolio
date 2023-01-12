using Microsoft.AspNetCore.Mvc;
using WebApplication2.Repositories;
using WebApplication2.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApplication2.Controllers
{
    public class SpecifikacijaController : Controller
    {
		public ActionResult Index()
		{
			return View(SpecifikacijaRepo.List());
		}


		public ActionResult Create()
		{
			var spec = new Specifikacija();
			PopulateSelections(spec);
			return View(spec);
		}


		[HttpPost]
		public ActionResult Create(Specifikacija spec)
		{
			var match = SpecifikacijaRepo.Find(spec.Id);
			PopulateSelections(spec);

			if (match != null)
				ModelState.AddModelError("darbuotojoId", "Field value already exists in database.");

			if (ModelState.IsValid)
			{
				SpecifikacijaRepo.Insert(spec);

				return RedirectToAction("Index");
			}

			return View(spec);
		}


		public ActionResult Edit(int id)
		{
			return View(SpecifikacijaRepo.Find(id));
		}


		[HttpPost]
		public ActionResult Edit(string id, Specifikacija spec)
		{
			if (ModelState.IsValid)
			{
				SpecifikacijaRepo.Update(spec);


				return RedirectToAction("Index");
			}

			return View(spec);
		}


		public ActionResult Delete(int id)
		{
			var spec = SpecifikacijaRepo.Find(id);
			return View(spec);
		}


		[HttpPost]
		public ActionResult DeleteConfirm(int id)
		{
			try
			{
				SpecifikacijaRepo.Delete(id);

				return RedirectToAction("Index");
			}
			catch (MySql.Data.MySqlClient.MySqlException)
			{
				ViewData["deletionNotPermitted"] = true;

				var spec = SpecifikacijaRepo.Find(id);
				return View("Delete", spec);
			}
		}

		public void PopulateSelections(Specifikacija spec)
		{
			var items = PrekeRepo.List();

			spec.Lists.Items =
				items.Select(i =>
				{
					return
						new SelectListItem()
						{
							Value = Convert.ToString(i.Code),
							Text = i.Title
						};
				}).ToList();

		}
	}
}
