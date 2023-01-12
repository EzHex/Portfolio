using MySql.Data.MySqlClient;
using System.Data;
using WebApplication2.Models;

namespace WebApplication2.Repositories
{
    public class PrekeRepo
    {
		public static List<Preke> List()
		{
			var spec = new List<Preke>();

			string query = $@"SELECT * FROM `{Config.TblPrefix}prekes` ORDER BY kodas ASC";
			var dt = Sql.Query(query);

			foreach (DataRow item in dt)
			{
				spec.Add(new Preke
				{
					Code = Convert.ToInt32(item["kodas"]),
					Title = Convert.ToString(item["pavadinimas"]),
					Price = Convert.ToDecimal(item["kaina"]),
					Count = Convert.ToInt32(item["kiekis"]),
					Manufacturer = Convert.ToString(item["gamintojas"]),
					Distributor = Convert.ToString(item["platintojas"]),
					Type = Convert.ToString(item["rusis"])
				});
			}

			return spec;
		}

		public static Preke Find(int id)
		{
			var query = $@"SELECT * FROM `{Config.TblPrefix}prekes` WHERE kodas=?id";

			var dt =
				Sql.Query(query, args => {
					args.Add("?id", MySqlDbType.Int32).Value = id;
				});

			if (dt.Count > 0)
			{
				var preke = new Preke();

				foreach (DataRow item in dt)
				{
					preke.Code = Convert.ToInt32(item["kodas"]);
					preke.Title = Convert.ToString(item["pavadinimas"]);
					preke.Price = Convert.ToDecimal(item["kaina"]);
					preke.Count = Convert.ToInt32(item["kiekis"]);
					preke.Manufacturer = Convert.ToString(item["gamintojas"]);
					preke.Distributor = Convert.ToString(item["platintojas"]);
					preke.Type = Convert.ToString(item["rusis"]);
				}

				return preke;
			}

			return null;
		}

		public static void Update(Preke preke)
		{
			var query =
				$@"UPDATE `{Config.TblPrefix}prekes`
				SET 
					pavadinimas=?pav,
					kaina=?kaina,
					kiekis=?kiekis,
					gamintojas=?gamintojas,
					platintojas=?platintojas,
					rusis=?rusis
				WHERE 
					kodas=?id";

			Sql.Update(query, args => {
				args.Add("?pav", MySqlDbType.VarChar).Value = preke.Title;
				args.Add("?kaina", MySqlDbType.Float).Value = preke.Price;
				args.Add("?kiekis", MySqlDbType.Int32).Value = preke.Count;
				args.Add("?gamintojas", MySqlDbType.VarChar).Value = preke.Manufacturer;
				args.Add("?platintojas", MySqlDbType.VarChar).Value = preke.Distributor;
				args.Add("?rusis", MySqlDbType.VarChar).Value = preke.Type;
				args.Add("?id", MySqlDbType.Int32).Value = preke.Code;
			});
		}

		public static void Insert(Preke preke)
		{
			var query =
				$@"INSERT INTO `{Config.TblPrefix}prekes`
				(
					pavadinimas,
					kaina,
					kiekis,
					gamintojas,
					platintojas,
					rusis,
					kodas
				)
				VALUES(
					?pav,
					?kaina,
					?kiekis,
					?gamintojas,
					?platintojas,
					?rusis,
					?id
				)";

			Sql.Insert(query, args => {
				args.Add("?pav", MySqlDbType.VarChar).Value = preke.Title;
				args.Add("?kaina", MySqlDbType.Float).Value = preke.Price;
				args.Add("?kiekis", MySqlDbType.Int32).Value = preke.Count;
				args.Add("?gamintojas", MySqlDbType.VarChar).Value = preke.Manufacturer;
				args.Add("?platintojas", MySqlDbType.VarChar).Value = preke.Distributor;
				args.Add("?rusis", MySqlDbType.VarChar).Value = preke.Type;
				args.Add("?id", MySqlDbType.Int32).Value = preke.Code;
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

			var query = $@"DELETE FROM `{Config.TblPrefix}prekes` WHERE kodas=?id";
			Sql.Delete(query, args => {
				args.Add("?id", MySqlDbType.Int32).Value = id;
			});
		}

		public static void DeleteForPreke(int prekesID)
		{
			var query =
				$@"DELETE FROM a
				USING `{Config.TblPrefix}specifikacijos` as a
				WHERE a.fk_Prekekodas=?fkid";

			Sql.Delete(query, args => {
				args.Add("?fkid", MySqlDbType.Int32).Value = prekesID;
			});
		}
	}
}
