using Microsoft.AspNetCore.Mvc;
using WebApplication2.Repositories;
using WebApplication2.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApplication2.Controllers
{
    public class AtaskaitaController : Controller
    {

		public ActionResult UzsakymuAtaskaita(int? filter, DateTime? dateFrom, DateTime? dateTo,
			string? status, decimal sumFrom, decimal sumTo)
		{
			var report = new Ataskaita();
			PopulateSelections(report);
			report.DateFrom = dateFrom;
			report.DateTo = dateTo?.AddHours(23).AddMinutes(59).AddSeconds(59); //move time of end date to end of day

			report.OrdersTuple = AtaskaitaRepo.GetOrders(report.DateFrom, report.DateTo, status, sumFrom, sumTo);

			report.OrdersTuple.ForEach(i =>
			{
				if (!report.Workers.Contains(i.Item4))
                {
					report.Workers.Add(i.Item4);
                }
			});
			
			return View(report);
		}

		public void PopulateSelections(Ataskaita at)
        {
			var statuses = new Dictionary<string, string>();
			statuses.Add("Laukiama_apmokejimo", "Laukiama apmokėjimo");
			statuses.Add("Ruosiamas", "Ruošiamas");
			statuses.Add("Perduotas_kurjeriui", "Perduotas kurjeriui");
			statuses.Add("Paruostas", "Paruoštas");
			statuses.Add("Atliktas", "Atliktas");

			at.Lists.Statuses =
				statuses.Select(s =>
				{
					return new SelectListItem()
					{
						Value = s.Key,
						Text = s.Value
					};
				}).ToList();
		}
	}
}
