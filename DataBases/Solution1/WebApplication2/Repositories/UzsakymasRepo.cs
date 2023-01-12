using MySql.Data.MySqlClient;
using System.Data;
using WebApplication2.Models;

namespace WebApplication2.Repositories
{
    public class UzsakymasRepo
    {

		public static List<Uzsakymas> List()
		{
			var spec = new List<Uzsakymas>();

			string query = $@"SELECT * FROM `{Config.TblPrefix}uzsakymai` ORDER BY nr ASC";
			var dt = Sql.Query(query);

			foreach (DataRow item in dt)
			{
				spec.Add(new Uzsakymas
				{
					Number = Convert.ToInt32(item["nr"]),
					Date = Convert.ToDateTime(item["data"]),
					Status = Convert.ToString(item["busena"]),
					WorkerId = Convert.ToInt32(item["fk_Darbuotojasid_Darbuotojas"]),
					BuyerId = Convert.ToInt32(item["fk_Pirkejaskodas"])
				});
			}

			return spec;
		}

		public static Uzsakymas Find(int id)
		{
			var query = $@"SELECT * FROM `{Config.TblPrefix}uzsakymai` WHERE nr=?id";

			var dt =
				Sql.Query(query, args => {
					args.Add("?id", MySqlDbType.Int32).Value = id;
				});

			if (dt.Count > 0)
			{
				var uzs = new Uzsakymas();

				foreach (DataRow item in dt)
				{
					uzs.Number = Convert.ToInt32(item["nr"]);
					uzs.Date = Convert.ToDateTime(item["data"]);
					uzs.Status = Convert.ToString(item["busena"]);
					uzs.WorkerId = Convert.ToInt32(item["fk_Darbuotojasid_Darbuotojas"]);
					uzs.BuyerId = Convert.ToInt32(item["fk_Pirkejaskodas"]);
				}

				return uzs;
			}

			return null;
		}

		public static void Update(Uzsakymas uzs)
		{
			var query =
				$@"UPDATE `{Config.TblPrefix}uzsakymai`
				SET 
					data=?date,
					busena=?status,
					fk_Darbuotojasid_Darbuotojas=?worker,
					fk_Pirkejaskodas=?buyer
				WHERE 
					nr=?id";

			Sql.Update(query, args => {
				args.Add("?date", MySqlDbType.Date).Value = uzs.Date;
				args.Add("?status", MySqlDbType.VarChar).Value = uzs.Status;
				args.Add("?worker", MySqlDbType.Int32).Value = uzs.WorkerId;
				args.Add("?buyer", MySqlDbType.Int32).Value = uzs.BuyerId;
				args.Add("?id", MySqlDbType.Int32).Value = uzs.Number;
			});
		}

		public static void Insert(Uzsakymas uzs)
		{
			var query =
				$@"INSERT INTO `{Config.TblPrefix}uzsakymai`
				(
					data,
					busena,
					fk_Darbuotojasid_Darbuotojas,
					fk_Pirkejaskodas,
					nr
				)
				VALUES(
					?date,
					?status,
					?worker,
					?buyer,
					?id
				)";

			Sql.Insert(query, args => {
				args.Add("?date", MySqlDbType.Date).Value = uzs.Date;
				args.Add("?status", MySqlDbType.VarChar).Value = uzs.Status;
				args.Add("?worker", MySqlDbType.Int32).Value = uzs.WorkerId;
				args.Add("?buyer", MySqlDbType.Int32).Value = uzs.BuyerId;
				args.Add("?id", MySqlDbType.Int32).Value = uzs.Number;
			});
		}

		public static int LastId()
		{
			var query = $@"SELECT LAST_INSERT_ID()";
			var dt = Sql.Query(query);
			foreach (DataRow dr in dt)
			{
				return Convert.ToInt32(dr["last_insert_id()"]);
			}

			return -1;
		}

		public static void Delete(int id)
		{

			var query = $@"DELETE FROM `{Config.TblPrefix}uzsakymai` WHERE nr=?id";
			Sql.Delete(query, args => {
				args.Add("?id", MySqlDbType.Int32).Value = id;
			});
		}

		public static void DeleteForUzsakymas(int uzsID)
		{
			var query =
				$@"DELETE FROM a
				USING `{Config.TblPrefix}uzsakymo_prekes` as a
				WHERE a.fk_Uzsakymasnr=?fkid";

			Sql.Delete(query, args => {
				args.Add("?fkid", MySqlDbType.Int32).Value = uzsID;
			});

			query =
				$@"DELETE FROM a
				USING `{Config.TblPrefix}saskaitos` as a
				WHERE a.fk_Uzsakymasnr=?fkid";

			Sql.Delete(query, args => {
				args.Add("?fkid", MySqlDbType.Int32).Value = uzsID;
			});
		}
	}
}
