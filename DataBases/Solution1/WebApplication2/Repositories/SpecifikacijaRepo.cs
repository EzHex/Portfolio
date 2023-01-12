using System.Data;
using MySql.Data.MySqlClient;
using WebApplication2.Models;

namespace WebApplication2.Repositories
{
    public class SpecifikacijaRepo
    {
		public static List<Specifikacija> List()
		{
			var spec = new List<Specifikacija>();

			string query = $@"SELECT * FROM `{Config.TblPrefix}specifikacijos` ORDER BY id_Specifikacija ASC";
			var dt = Sql.Query(query);

			foreach (DataRow item in dt)
			{
				spec.Add(new Specifikacija
				{
					Id = Convert.ToInt32(item["id_Specifikacija"]),
					ItemId = Convert.ToInt32(item["fk_Prekekodas"]),
					Feature = Convert.ToString(item["specifikacijos_pavadinimas"]),
					Value = Convert.ToString(item["reiksme"])
				});
			}

			return spec;
		}

		public static Specifikacija Find(int id)
		{
			var query = $@"SELECT * FROM `{Config.TblPrefix}specifikacijos` WHERE id_Specifikacija=?id";

			var dt =
				Sql.Query(query, args => {
					args.Add("?id", MySqlDbType.Int32).Value = id;
				});

			if (dt.Count > 0)
			{
				var spec = new Specifikacija();

				foreach (DataRow item in dt)
				{
					spec.Id = Convert.ToInt32(item["id_Specifikacija"]);
					spec.ItemId = Convert.ToInt32(item["fk_Prekekodas"]);
					spec.Feature = Convert.ToString(item["specifikacijos_pavadinimas"]);
					spec.Value = Convert.ToString(item["reiksme"]);
				}

				return spec;
			}

			return null;
		}

		public static void Update(Specifikacija spec)
		{
			var query =
				$@"UPDATE `{Config.TblPrefix}specifikacijos`
				SET 
					specifikacijos_pavadinimas=?pav,
					reiksme=?reiksme,
					fk_Prekekodas=?fkPreke
				WHERE 
					id_Specifikacija=?id";

			Sql.Update(query, args => {
				args.Add("?pav", MySqlDbType.VarChar).Value = spec.Feature;
				args.Add("?reiksme", MySqlDbType.VarChar).Value = spec.Value;
				args.Add("?id", MySqlDbType.Int32).Value = spec.Id;
				args.Add("?fkPreke", MySqlDbType.Int32).Value = spec.ItemId;
			});
		}

		public static void Insert(Specifikacija spec)
		{
			var query =
				$@"INSERT INTO `{Config.TblPrefix}specifikacijos`
				(
					specifikacijos_pavadinimas,
					reiksme,
					fk_Prekekodas,
					id_Specifikacija
				)
				VALUES(
					?pav,
					?reiksme,
					?fkPreke,
					?id
				)";

			Sql.Insert(query, args => {
				args.Add("?pav", MySqlDbType.VarChar).Value = spec.Feature;
				args.Add("?reiksme", MySqlDbType.VarChar).Value = spec.Value;
				args.Add("?id", MySqlDbType.Int32).Value = spec.Id;
				args.Add("?fkPreke", MySqlDbType.Int32).Value = spec.ItemId;
			});
		}

		public static void Delete(int id)
		{
			var query = $@"DELETE FROM `{Config.TblPrefix}specifikacijos` WHERE id_Specifikacija=?id";
			Sql.Delete(query, args => {
				args.Add("?id", MySqlDbType.Int32).Value = id;
			});
		}
	}
}
