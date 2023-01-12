
using System.Data;
using WebApplication2.Models;

namespace WebApplication2.Repositories
{
	public class AtaskaitaRepo
	{
		public static List<Tuple<int, DateTime, string, string, string, decimal, decimal, Tuple<int>>>
			GetOrders(DateTime? DateFrom, DateTime? DateTo, string? Status, decimal SumFrom, decimal SumTo)
		{
			List<Tuple<int, DateTime, string, string, string, decimal, decimal, Tuple<int>>> tuples = 
				new List<Tuple<int, DateTime, string, string, string, decimal, decimal, Tuple<int>>>();

			string query = "";

			if (DateFrom != null || DateTo != null || Status != null || SumFrom > 0 || SumTo > 0)
			{
				bool previous = false;
				string where = "\nWHERE ";

				if (DateFrom != null && DateTo != null)
				{
					where += $"uz.data > '{DateFrom}' AND uz.data < '{DateTo}' ";
					previous = true;
				}


				if (Status != null)
				{
					if (previous) where += "AND ";
					where += $"busena = '{Status}' ";
					previous = true;
				}


				if (SumFrom > 0 && SumTo > 0)
				{
					if (previous) where += "AND ";
					where += $"s.suma >= '{SumFrom}' AND s.suma <= '{SumTo}'";
				}

				

				query = $@"		SELECT 
									nr,
									uz.data,
									busena,
									d.vardas as darbuotojas,
									CONCAT(p.vardas, ' ', p.pavarde) as pirkejas,
									s.suma as suma,
									sumGrupes.suma as bendraSuma,
									kiekisGrupe.kiekis
								FROM `uzsakymai` uz
									LEFT JOIN `darbuotojai` d ON fk_Darbuotojasid_Darbuotojas = d.id_Darbuotojas
									LEFT JOIN `pirkejai` p ON fk_Pirkejaskodas = p.kodas
									LEFT JOIN `saskaitos` s ON nr = s.fk_Uzsakymasnr
									LEFT JOIN
									(
    									SELECT
											uzs.fk_Darbuotojasid_Darbuotojas as darb,
    										SUM(s.suma) as suma
										FROM `uzsakymai` as uzs
											LEFT JOIN `saskaitos` s ON nr = s.fk_Uzsakymasnr
										{where}
										GROUP BY darb
									) as sumGrupes ON darb = d.id_Darbuotojas
									LEFT JOIN
									(
    									SELECT
											uzs.fk_Darbuotojasid_Darbuotojas as darb1,
											COUNT(s.suma) as kiekis
										FROM `uzsakymai` as uzs
											LEFT JOIN `saskaitos` s ON nr = s.fk_Uzsakymasnr
										{where}
										GROUP BY darb1
									) as kiekisGrupe ON darb1 = d.id_Darbuotojas
								{where}";

				query += "\nORDER BY darbuotojas ASC";
			}
			else
			{
				query = $@"	SELECT 
								nr,
								uz.data,
								busena,
								d.vardas as darbuotojas,
								CONCAT(p.vardas, ' ', p.pavarde) as pirkejas,
								s.suma as suma,
								sumGrupes.suma as bendraSuma,
								kiekisGrupe.kiekis
							FROM `uzsakymai` as uz
								LEFT JOIN `darbuotojai` d ON fk_Darbuotojasid_Darbuotojas = d.id_Darbuotojas
								LEFT JOIN `pirkejai` p ON fk_Pirkejaskodas = p.kodas
								LEFT JOIN `saskaitos` s ON nr = s.fk_Uzsakymasnr
								LEFT JOIN
								(
    								SELECT
										uzs.fk_Darbuotojasid_Darbuotojas as darb,
    									SUM(s.suma) as suma
									FROM `uzsakymai` as uzs
										LEFT JOIN `saskaitos` s ON nr = s.fk_Uzsakymasnr
									GROUP BY darb
								) as sumGrupes ON darb = d.id_Darbuotojas
								LEFT JOIN
								(
    								SELECT
										uzs.fk_Darbuotojasid_Darbuotojas as darb1,
										COUNT(s.suma) as kiekis
									FROM `uzsakymai` as uzs
										LEFT JOIN `saskaitos` s ON nr = s.fk_Uzsakymasnr
									GROUP BY darb1
								) as kiekisGrupe ON darb1 = d.id_Darbuotojas
							ORDER BY darbuotojas ASC";
			}

			var dt = Sql.Query(query);

			foreach (DataRow item in dt)
			{
				var a = Tuple.Create(
					Convert.ToInt32(item["nr"]),
					Convert.ToDateTime(item["data"]),
					Convert.ToString(item["busena"]),
					Convert.ToString(item["darbuotojas"]),
					Convert.ToString(item["pirkejas"]),
					item["suma"] is DBNull ? 0 : Convert.ToDecimal(item["suma"]),
                    Convert.ToDecimal(item["bendraSuma"]),
                    Convert.ToInt32(item["kiekis"]));

				tuples.Add(a);
			}

			return tuples;
		}
	}
}
