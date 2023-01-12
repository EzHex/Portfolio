using Microsoft.AspNetCore.Mvc;
using WebApplication2.Repositories;
using WebApplication2.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApplication2.Controllers
{
    public class UzsakymasController : Controller
    {
		public ActionResult Index()
		{
			return View(UzsakymasRepo.List());
		}


		public ActionResult Create()
		{
			var uzs = new Uzsakymas();
			PopulateSelections(uzs);
			return View(uzs);
		}


		[HttpPost]
		public ActionResult Create(int? save, int? add, int? remove, Uzsakymas uzs)
		{
			if (add != null)
			{
				var newOrder = new UzsakymoPreke
				{
					Id = 0,
					ItemId = 0,
					OrderId = 0,
					Count = 0
				};

				uzs.Items.Add(newOrder);

				ModelState.Clear();

				PopulateSelections(uzs);
				return View(uzs);
			}


			if (remove != null)
			{
				uzs.Items = uzs.Items.Where(it => it.Id != remove.Value).ToList();

				ModelState.Clear();

				PopulateSelections(uzs);
				return View(uzs);
			}

			if (save != null)
			{
				var match = UzsakymasRepo.Find(uzs.Number);
				PopulateSelections(uzs);

				if (match != null)
					ModelState.AddModelError("uzsakymo Id", "Field value already exists in database.");

				if (ModelState.IsValid)
				{
					PirkejasRepo.Insert(uzs.Buyer);

					int lastId = UzsakymasRepo.LastId();

					uzs.BuyerId = lastId;
					uzs.Buyer = PirkejasRepo.Find(lastId);

					UzsakymasRepo.Insert(uzs);

					uzs.Check.FkOrderNumber = lastId;
					SaskaitaRepo.Insert(uzs.Check);

					lastId = UzsakymasRepo.LastId();

                    for (int i = 0; i < uzs.Items.Count; i++)
                    {
                        if (lastId != -1)
                        {
                            uzs.Items[i].OrderId = lastId;
                            UzsakymoPrekeRepo.Insert(uzs.Items[i]);
                        }
                        else throw new Exception("Wrong item index");
                    }

                    return RedirectToAction("Index");
				}
			}

			PopulateSelections(uzs);
			return View(uzs);
		}


		public ActionResult Edit(int id)
		{
			var uzs = UzsakymasRepo.Find(id);
			uzs.Buyer = PirkejasRepo.Find(uzs.BuyerId);
			PopulateSelections(uzs);
			return View(uzs);
		}


		[HttpPost]
		public ActionResult Edit(int? save, int? add, int? remove, Uzsakymas uzs)
		{

			if (add != null)
			{
				var newOrder = new UzsakymoPreke
				{
					Id = 0,
					ItemId = 0,
					OrderId = 0,
					Count = 0
				};

				uzs.Items.Add(newOrder);

				ModelState.Clear();

				PopulateSelections(uzs);
				return View(uzs);
			}


			if (remove != null)
			{
				uzs.Items = uzs.Items.Where(it => it.Id != remove.Value).ToList();

				ModelState.Clear();

				PopulateSelections(uzs);
				return View(uzs);
			}


			if (save != null)
			{
				if (ModelState.IsValid)
				{
					PirkejasRepo.Update(uzs.Buyer);

					int Id = uzs.Number;

					uzs.BuyerId = uzs.Buyer.Code;

					UzsakymasRepo.Update(uzs);
					
					UzsakymasRepo.DeleteForUzsakymas(Id);

					uzs.Check.FkOrderNumber = Id;
					SaskaitaRepo.Insert(uzs.Check);

					for (int i = 0; i < uzs.Items.Count; i++)
					{
						if (Id != -1)
						{
							uzs.Items[i].OrderId = Id;
							UzsakymoPrekeRepo.Insert(uzs.Items[i]);
						}
						else throw new Exception("Wrong item index");
					}

					return RedirectToAction("Index");
				}
			}


			PopulateSelections(uzs);
			return View(uzs);
		}


		public ActionResult Delete(int id)
		{
			var uzs = UzsakymasRepo.Find(id);
			return View(uzs);
		}


		[HttpPost]
		public ActionResult DeleteConfirm(int id)
		{
			try
			{
				UzsakymasRepo.Delete(id);

				return RedirectToAction("Index");
			}
			catch (MySql.Data.MySqlClient.MySqlException)
			{
				ViewData["deletionNotPermitted"] = true;

				var uzs = UzsakymasRepo.Find(id);
				return View("Delete", uzs);
			}
		}

		public void PopulateSelections(Uzsakymas uzs)
		{
			var workers = DarbuotojasRepo.List();

			var statuses = new Dictionary<string, string>();
			statuses.Add("Laukiama_apmokejimo", "Laukiama apmokėjimo");
			statuses.Add("Ruosiamas", "Ruošiamas");
			statuses.Add("Perduotas_kurjeriui", "Perduotas kurjeriui");
			statuses.Add("Paruostas", "Paruoštas");
			statuses.Add("Atliktas", "Atliktas");

			var items = PrekeRepo.List();
			
			var payments = new Dictionary<string, string>();
			payments.Add("Swedbank", "Swedbank");
			payments.Add("Seb", "Seb");
			payments.Add("Luminor", "Luminor");
			payments.Add("Siauliu_bankas", "Šiaulių bankas");
			payments.Add("Paypal", "Paypal");
			payments.Add("Kortele", "Kortele");
			payments.Add("Grynais_atsiimant_prekes", "Grynais atsiimant prekę");


			var deliveryMethods = new Dictionary<string, string>();
			deliveryMethods.Add("atsiemimas_parduotuveje", "Atsiėmimas parduotuvėje");
			deliveryMethods.Add("pastomatas", "Paštomatas");
			deliveryMethods.Add("kurjeris", "Kurjeris");


			uzs.Lists.Workers =
				workers.Where(w => w.Duties == "Sales_darbuotojas").Select(w =>
				{
					return
						new SelectListItem()
						{
							Value = Convert.ToString(w.Id),
							Text = w.Name
						};
				}).ToList();

			uzs.Lists.Statuses =
				statuses.Select(s =>
				{
					return new SelectListItem()
					{
						Value = s.Key,
						Text = s.Value
					};
				}).ToList();

			uzs.Lists.Items =
				items.Select(i =>
				{
					return new SelectListItem()
					{
						Value = Convert.ToString(i.Code),
						Text = i.Title
					};
				}).ToList();

			uzs.Buyer.Lists.Payments =
				payments.Select(p =>
                {
					return new SelectListItem()
					{
						Value = p.Key,
						Text = p.Value
					};
                }).ToList();

			uzs.Buyer.Lists.DeliveryMethods =
				deliveryMethods.Select(d =>
                {
					return new SelectListItem()
					{
						Value = d.Key,
						Text= d.Value
					};
                }).ToList();

		}
	}
}
