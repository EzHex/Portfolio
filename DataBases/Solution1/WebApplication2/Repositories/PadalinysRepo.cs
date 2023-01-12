using MySql.Data.MySqlClient;
using System.Data;
using WebApplication2.Models;

namespace WebApplication2.Repositories
{
    public class PadalinysRepo
    {
		public static List<PadalinysList> List()
		{
			var padaliniai = new List<PadalinysList>();

			string query = $@"SELECT * FROM `{Config.TblPrefix}padaliniai` ORDER BY id_Padalinys ASC";
			var dt = Sql.Query(query);

			foreach (DataRow item in dt)
			{
				padaliniai.Add(new PadalinysList
				{
					City = Convert.ToString(item["miestas"]),
					Address = Convert.ToString(item["adresas"]),
					ContactNumber = Convert.ToString(item["kontaktinis_nr"]),
					Id = Convert.ToString(item["id_Padalinys"]),
					FkCompany = Convert.ToString(item["fk_Imoneid_Imone"])
				});
			}

			return padaliniai;
		}

		public static Padalinys Find(int id)
		{
			var query = $@"SELECT * FROM `{Config.TblPrefix}padaliniai` WHERE id_Padalinys=?id";

			var dt =
				Sql.Query(query, args => {
					args.Add("?id", MySqlDbType.Int32).Value = id;
				});

			if (dt.Count > 0)
			{
				var pad = new Padalinys();

				foreach (DataRow item in dt)
				{
					pad.City = Convert.ToString(item["miestas"]);
					pad.Address = Convert.ToString(item["adresas"]);
					pad.ContactNumber = Convert.ToString(item["kontaktinis_nr"]);
					pad.Id = Convert.ToInt32(item["id_Padalinys"]);
					pad.FkCompany = Convert.ToInt32(item["fk_Imoneid_Imone"]);
				}

				return pad;
			}

			return null;
		}

		public static void Update(Padalinys pad)
		{
			var query =
				$@"UPDATE `{Config.TblPrefix}padaliniai`
				SET 
					miestas=?miestas,
					adresas=?adresas,
					kontaktinis_nr=?num,
					fk_Imoneid_Imone=?fkImone
				WHERE 
					id_Padalinys=?id";

			Sql.Update(query, args => {
				args.Add("?miestas", MySqlDbType.VarChar).Value = pad.City;
				args.Add("?adresas", MySqlDbType.VarChar).Value = pad.Address;
				args.Add("?num", MySqlDbType.VarChar).Value = pad.ContactNumber;
				args.Add("?fkImone", MySqlDbType.Int32).Value = pad.FkCompany;
				args.Add("?id", MySqlDbType.Int32).Value = pad.Id;
			});
		}

		public static void Insert(Padalinys pad)
		{
			var query =
				$@"INSERT INTO `{Config.TblPrefix}padaliniai`
				(
					miestas,
					adresas,
					kontaktinis_nr,
					fk_Imoneid_Imone,
					id_Padalinys
				)
				VALUES(
					?miestas,
					?adresas,
					?num,
					?fkImone,
					?id
				)";

			Sql.Insert(query, args => {
				args.Add("?miestas", MySqlDbType.VarChar).Value = pad.City;
				args.Add("?adresas", MySqlDbType.VarChar).Value = pad.Address;
				args.Add("?num", MySqlDbType.VarChar).Value = pad.ContactNumber;
				args.Add("?fkImone", MySqlDbType.Int32).Value = pad.FkCompany;
				args.Add("?id", MySqlDbType.Int32).Value = pad.Id;
			});
		}

		public static void Delete(int id)
		{
			var query = $@"DELETE FROM `{Config.TblPrefix}padaliniai` WHERE id_Padalinys=?id";
			Sql.Delete(query, args => {
				args.Add("?id", MySqlDbType.Int32).Value = id;
			});
		}
	}
}
