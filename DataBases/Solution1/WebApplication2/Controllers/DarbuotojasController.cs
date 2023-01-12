using Microsoft.AspNetCore.Mvc;
using WebApplication2.Repositories;
using WebApplication2.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApplication2.Controllers
{
	public class DarbuotojasController : Controller
	{
		
		public ActionResult Index()
		{
			return View(DarbuotojasRepo.List());
		}

		
		public ActionResult Create()
		{
			var darbuotojas = new Darbuotojas();
			PopulateSelections(darbuotojas);
			return View(darbuotojas);
		}

		
		[HttpPost]
		public ActionResult Create(Darbuotojas darbuotojas)
		{
			var match = DarbuotojasRepo.Find(darbuotojas.Id);
			PopulateSelections(darbuotojas);

			if (match != null)
				ModelState.AddModelError("darbuotojoId", "Field value already exists in database.");

			Console.WriteLine($"{darbuotojas.Name}, {darbuotojas.Duties}, {darbuotojas.Id}, {darbuotojas.FkDepartment}");
			Console.WriteLine(ModelState.IsValid);

			if (ModelState.IsValid)
			{
				DarbuotojasRepo.Insert(darbuotojas);

				return RedirectToAction("Index");
			}

			return View(darbuotojas);
		}

		
		public ActionResult Edit(int id)
		{
			var darb = DarbuotojasRepo.Find(id);
			PopulateSelections(darb);
			return View(darb);
		}

		
		[HttpPost]
		public ActionResult Edit(string id, Darbuotojas darb)
		{
			if (ModelState.IsValid)
			{
				DarbuotojasRepo.Update(darb);


				return RedirectToAction("Index");
			}

			PopulateSelections(darb);
			return View(darb);
		}

		
		public ActionResult Delete(int id)
		{
			var klientas = DarbuotojasRepo.Find(id);
			return View(klientas);
		}

		
		[HttpPost]
		public ActionResult DeleteConfirm(int id)
		{
			try
			{
				DarbuotojasRepo.Delete(id);

				return RedirectToAction("Index");
			}
			catch (MySql.Data.MySqlClient.MySqlException)
			{
				ViewData["deletionNotPermitted"] = true;

				var klientas = DarbuotojasRepo.Find(id);
				return View("Delete", klientas);
			}
		}

		public void PopulateSelections (Darbuotojas darb)
        {
			var Departaments = PadalinysRepo.List();

			var duties = new Dictionary<string, string>();
			duties.Add("Direktorius", "Direktorius");
			duties.Add("Vadybininkas", "Vadybininkas");
			duties.Add("Sandelio_darbuotojas", "Sandelio darbuotojas");
			duties.Add("Sales_darbuotojas", "Salės darbuotojas");
			duties.Add("Pavaduotojas", "Pavaduotojas");

			darb.Lists.Departaments =
				Departaments.Select(d =>
				{
					return
						new SelectListItem()
						{
							Value = Convert.ToString(d.Id),
							Text = d.Id.ToString()
						};
				}).ToList();

			darb.Lists.Duties =
				duties.Select(d =>
				{
					return
						new SelectListItem()
						{
							Value = d.Key,
							Text = d.Value
						};
				}).ToList();

		}
	}
}
