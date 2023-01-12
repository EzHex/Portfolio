using Microsoft.AspNetCore.Mvc;
using WebApplication2.Repositories;
using WebApplication2.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApplication2.Controllers
{
    public class PrekeController : Controller
    {
		public ActionResult Index()
		{
			return View(PrekeRepo.List());
		}


		public ActionResult Create()
		{
			var preke = new Preke();
			PopulateSelections(preke);
			return View(preke);
		}


		[HttpPost]
		public ActionResult Create(int? save, int? add, int? remove, Preke preke)
		{
			if (add != null)
            {
				var newSpec = new Specifikacija
				{
					Id = 0,
					Feature = null,
					Value = null,
					ItemId = preke.Code
				};

				preke.Specifikacijos.Add(newSpec);

				ModelState.Clear();

				PopulateSelections(preke);
				return View(preke);
            }


			if (remove != null)
            {
				preke.Specifikacijos = preke.Specifikacijos.Where(it => it.Id != remove.Value).ToList();

				ModelState.Clear();

				PopulateSelections(preke);
				return View(preke);
            }

			if (save != null)
            {
				var match = PrekeRepo.Find(preke.Code);
				PopulateSelections(preke);

				if (match != null)
					ModelState.AddModelError("prekesId", "Field value already exists in database.");

				if (ModelState.IsValid)
				{
					PrekeRepo.Insert(preke);

					int lastId = PrekeRepo.LastId();

					for (int i = 0; i < preke.Specifikacijos.Count; i++)
                    {
						if (lastId != -1)
						{
							preke.Specifikacijos[i].ItemId = lastId;
							SpecifikacijaRepo.Insert(preke.Specifikacijos[i]);
						}
						else throw new Exception("Wrong item index");
                    }

					return RedirectToAction("Index");
				}
			}

			PopulateSelections(preke);
			return View(preke);
		}


		public ActionResult Edit(int id)
		{
			var preke = PrekeRepo.Find(id);
			PopulateSelections(preke);
			return View(preke);
		}


		[HttpPost]
		public ActionResult Edit(int? save, int? add, int? remove, Preke preke)
		{

			if (add != null)
            {
				var newSpec = new Specifikacija
				{
					Id = 0,
					Feature = null,
					Value = null,
					ItemId = preke.Code
				};

				preke.Specifikacijos.Add(newSpec);

				ModelState.Clear();

				PopulateSelections(preke);
				return View(preke);
			}

			if (remove != null)
			{
				preke.Specifikacijos = preke.Specifikacijos.Where(it => it.Id != remove.Value).ToList();

				ModelState.Clear();

				PopulateSelections(preke);
				return View(preke);
			}

			if (save != null)
            {
				if (ModelState.IsValid)
				{
					PrekeRepo.Update(preke);

					int lastId = preke.Code;

					PrekeRepo.DeleteForPreke(preke.Code);

					for (int i = 0; i < preke.Specifikacijos.Count; i++)
					{
						if (lastId != -1)
						{
							preke.Specifikacijos[i].ItemId = lastId;
							SpecifikacijaRepo.Insert(preke.Specifikacijos[i]);
						}
						else throw new Exception("Wrong item index");
					}

					return RedirectToAction("Index");
				}
			}


			PopulateSelections(preke);
			return View(preke);
		}


		public ActionResult Delete(int id)
		{
			var spec = PrekeRepo.Find(id);
			return View(spec);
		}


		[HttpPost]
		public ActionResult DeleteConfirm(int id)
		{
			try
			{
				PrekeRepo.Delete(id);

				return RedirectToAction("Index");
			}
			catch (MySql.Data.MySqlClient.MySqlException)
			{
				ViewData["deletionNotPermitted"] = true;

				var spec = PrekeRepo.Find(id);
				return View("Delete", spec);
			}
		}

		public void PopulateSelections(Preke preke)
		{
			var types = new Dictionary<string, string>();
			types.Add("Kompiuteriai_ir_komponentai", "Kompiuteriai ir komponentai");
			types.Add("Periferija_ir_biuro_iranga", "Periferija ir biuro įranga");
			types.Add("Namu_elektronika", "Namų elektronika");
			types.Add("Komunikacine_ir_rysio_iranga", "Komunikacinė ir ryšio įranga");
			types.Add("Zaidimai_ir_zaidimu_iranga", "Žaidimai ir žaidimų įranga");

			preke.Lists.Types =
				types.Select(t =>
				{
					return
						new SelectListItem()
						{
							Value = t.Key,
							Text = t.Value
						};
				}).ToList();

		}
	}
}
