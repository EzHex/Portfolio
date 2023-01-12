using MySql.Data.MySqlClient;
using System.Data;
using WebApplication2.Models;


namespace WebApplication2.Repositories
{
    public class ImoneRepo
    {
		public static List<Imone> List()
		{
			var imones = new List<Imone>();

			string query = $@"SELECT * FROM `{Config.TblPrefix}imones` ORDER BY pavadinimas ASC";
			var dt = Sql.Query(query);

			foreach (DataRow item in dt)
			{
				imones.Add(new Imone
				{
					Title = Convert.ToString(item["pavadinimas"]),
					City = Convert.ToString(item["miestas"]),
					Address = Convert.ToString(item["adresas"]),
					Phone = Convert.ToString(item["telefono_nr"]),
					NumberOfEmployees = Convert.ToInt32(item["darbuotoju_skaicius"]),
					Id = Convert.ToInt32(item["id_Imone"])
				});
			}

			return imones;
		}

		public static Imone Find(int id)
		{
			var query = $@"SELECT * FROM `{Config.TblPrefix}imones` WHERE id_Imone=?id";

			var dt = 
				Sql.Query(query, args => {
					args.Add("?id", MySqlDbType.VarChar).Value = id;
				});

			if (dt.Count > 0)
			{
				var imone = new Imone();

				foreach (DataRow item in dt)
				{
					imone.Title = Convert.ToString(item["pavadinimas"]);
					imone.City = Convert.ToString(item["miestas"]);
					imone.Address = Convert.ToString(item["adresas"]);
					imone.Phone = Convert.ToString(item["telefono_nr"]);
					imone.NumberOfEmployees = Convert.ToInt32(item["darbuotoju_skaicius"]);
					imone.Id = Convert.ToInt32(item["id_Imone"]);
				}

				return imone;
			}

			return null;
		}

		public static void Update(Imone imone)
		{
			var query =
				$@"UPDATE `{Config.TblPrefix}imones` 
				SET 
					pavadinimas=?pav,
					miestas=?miestas,
					adresas=?adresas,
					telefono_nr=?telNr,
					darbuotoju_skaicius=?dSkaicius
				WHERE 
					id_Imone=?id";

			Sql.Update(query, args => {
				args.Add("?pav", MySqlDbType.VarChar).Value = imone.Title;
				args.Add("?miestas", MySqlDbType.VarChar).Value = imone.City;
				args.Add("?adresas", MySqlDbType.VarChar).Value = imone.Address;
				args.Add("?telNr", MySqlDbType.VarChar).Value = imone.Phone;
				args.Add("?dSkaicius", MySqlDbType.Int32).Value = imone.NumberOfEmployees;
				args.Add("?id", MySqlDbType.Int32).Value = imone.Id;
			});
		}

		public static void Insert(Imone imone)
		{
			var query =
				$@"INSERT INTO `{Config.TblPrefix}imones`
				(
					pavadinimas,
					miestas,
					adresas,
					telefono_nr,
					darbuotoju_skaicius,
					id_Imone
				)
				VALUES(
					?pav,
					?miestas,
					?adresas,
					?telNr,
					?dSkaicius,
					?id
				)";

			Sql.Insert(query, args => {
				args.Add("?pav", MySqlDbType.VarChar).Value = imone.Title;
				args.Add("?miestas", MySqlDbType.VarChar).Value = imone.City;
				args.Add("?adresas", MySqlDbType.VarChar).Value = imone.Address;
				args.Add("?telNr", MySqlDbType.VarChar).Value = imone.Phone;
				args.Add("?dSkaicius", MySqlDbType.Int32).Value = imone.NumberOfEmployees;
				args.Add("?id", MySqlDbType.Int32).Value = imone.Id;
			});
		}

		public static void Delete(int id)
		{
			var query = $@"DELETE FROM `{Config.TblPrefix}imones` WHERE id_Imone=?id";
			Sql.Delete(query, args => {
				args.Add("?id", MySqlDbType.VarChar).Value = id;
			});
		}

	}
}
