using MySql.Data.MySqlClient;
using System.Data;
using WebApplication2.Models;

namespace WebApplication2.Repositories
{
    public class SaskaitaRepo
    {
		public static List<Saskaita> List()
		{
			var sask = new List<Saskaita>();

			string query = $@"SELECT * FROM `{Config.TblPrefix}saskaitos` ORDER BY id_Saskaita ASC";
			var dt = Sql.Query(query);

			foreach (DataRow item in dt)
			{
				sask.Add(new Saskaita
				{
					Date = Convert.ToDateTime(item["data"]),
					Sum = Convert.ToDecimal(item["suma"]),
					Id = Convert.ToInt32(item["id_Saskaita"]),
					FkOrderNumber = Convert.ToInt32(item["fk_Uzsakymasnr"])
				});
			}

			return sask;
		}

		public static Saskaita Find(int id)
		{
			var query = $@"SELECT * FROM `{Config.TblPrefix}saskaitos` WHERE id_Saskaita=?id";

			var dt =
				Sql.Query(query, args => {
					args.Add("?id", MySqlDbType.Int32).Value = id;
				});

			if (dt.Count > 0)
			{
				var sask = new Saskaita();

				foreach (DataRow item in dt)
				{
					sask.Date = Convert.ToDateTime(item["data"]);
					sask.Sum = Convert.ToDecimal(item["suma"]);
					sask.Id = Convert.ToInt32(item["id_Saskaita"]);
					sask.FkOrderNumber = Convert.ToInt32(item["fk_Uzsakymasnr"]);
				}

				return sask;
			}

			return null;
		}

		public static void Update(Saskaita sask)
		{
			var query =
				$@"UPDATE `{Config.TblPrefix}saskaitos`
				SET 
					data=?date,
					suma=?sum,
					fk_Uzsakymasnr=?uzs
				WHERE 
					id_Saskaita=?id";

			Sql.Update(query, args => {
				args.Add("?date", MySqlDbType.Date).Value = sask.Date;
				args.Add("?sum", MySqlDbType.Decimal).Value = sask.Sum;
				args.Add("?uzs", MySqlDbType.Int32).Value = sask.FkOrderNumber;
				args.Add("?id", MySqlDbType.Int32).Value = sask.Id;
			});
		}

		public static void Insert(Saskaita sask)
		{
			var query =
				$@"INSERT INTO `{Config.TblPrefix}saskaitos`
				(
					data,
					suma,
					fk_Uzsakymasnr,
					id_Saskaita
				)
				VALUES(
					?date,
					?sum,
					?uzs,
					?id
				)";

			Sql.Insert(query, args => {
				args.Add("?date", MySqlDbType.Date).Value = sask.Date;
				args.Add("?sum", MySqlDbType.Decimal).Value = sask.Sum;
				args.Add("?uzs", MySqlDbType.Int32).Value = sask.FkOrderNumber;
				args.Add("?id", MySqlDbType.Int32).Value = sask.Id;
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

			var query = $@"DELETE FROM `{Config.TblPrefix}saskaitos` WHERE id_Saskaita=?id";
			Sql.Delete(query, args => {
				args.Add("?id", MySqlDbType.Int32).Value = id;
			});
		}
	}
}
