using MySql.Data.MySqlClient;
using System.Data;
using WebApplication2.Models;

namespace WebApplication2.Repositories
{
    public class UzsakymoPrekeRepo
    {
		public static List<UzsakymoPreke> List()
		{
			var spec = new List<UzsakymoPreke>();

			string query = $@"SELECT * FROM `{Config.TblPrefix}uzsakymo_prekes` ORDER BY id_Uzsakymo_preke ASC";
			var dt = Sql.Query(query);

			foreach (DataRow item in dt)
			{
				spec.Add(new UzsakymoPreke
				{
					Id = Convert.ToInt32(item["id_Uzsakymo_preke"]),
					Count = Convert.ToInt32(item["kiekis"]),
					ItemId = Convert.ToInt32(item["fk_Prekekodas"]),
					OrderId = Convert.ToInt32(item["fk_Uzsakymasnr"])
				});
			}

			return spec;
		}

		public static UzsakymoPreke Find(int id)
		{
			var query = $@"SELECT * FROM `{Config.TblPrefix}uzsakymo_prekes` WHERE id_Uzsakymo_preke=?id";

			var dt =
				Sql.Query(query, args => {
					args.Add("?id", MySqlDbType.Int32).Value = id;
				});

			if (dt.Count > 0)
			{
				var uzs = new UzsakymoPreke();

				foreach (DataRow item in dt)
				{
					uzs.Id = Convert.ToInt32(item["id_Uzsakymo_preke"]);
					uzs.Count = Convert.ToInt32(item["kiekis"]);
					uzs.ItemId = Convert.ToInt32(item["fk_Prekekodas"]);
					uzs.OrderId = Convert.ToInt32(item["fk_Uzsakymasnr"]);
				}

				return uzs;
			}

			return null;
		}

		public static void Update(UzsakymoPreke uzs)
		{
			var query =
				$@"UPDATE `{Config.TblPrefix}uzsakymo_prekes`
				SET 
					kiekis=?kiekis,
					fk_Prekekodas=?preke,
					fk_Uzsakymasnr=?uzsakymas
				WHERE 
					id_Uzsakymo_preke=?id";

			Sql.Update(query, args => {
				args.Add("?kiekis", MySqlDbType.Int32).Value = uzs.Count;
				args.Add("?preke", MySqlDbType.Int32).Value = uzs.ItemId;
				args.Add("?uzsakymas", MySqlDbType.Int32).Value = uzs.OrderId;
				args.Add("?id", MySqlDbType.Int32).Value = uzs.Id;
			});
		}

		public static void Insert(UzsakymoPreke uzs)
		{
			var query =
				$@"INSERT INTO `{Config.TblPrefix}uzsakymo_prekes`
				(
					kiekis,
					fk_Prekekodas,
					fk_Uzsakymasnr,
					id_Uzsakymo_preke
				)
				VALUES(
					?kiekis,
					?preke,
					?uzsakymas,
					?id
				)";

			Sql.Insert(query, args => {
				args.Add("?kiekis", MySqlDbType.Int32).Value = uzs.Count;
				args.Add("?preke", MySqlDbType.Int32).Value = uzs.ItemId;
				args.Add("?uzsakymas", MySqlDbType.Int32).Value = uzs.OrderId;
				args.Add("?id", MySqlDbType.Int32).Value = uzs.Id;
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
			var query = $@"DELETE FROM `{Config.TblPrefix}uzsakymo_prekes` WHERE id_Uzsakymo_preke=?id";
			Sql.Delete(query, args => {
				args.Add("?id", MySqlDbType.Int32).Value = id;
			});
		}
	}
}
