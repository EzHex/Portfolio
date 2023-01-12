using MySql.Data.MySqlClient;
using System.Data;
using WebApplication2.Models;

namespace WebApplication2.Repositories
{
    public class PirkejasRepo
    {
		public static List<Pirkejas> List()
		{
			var spec = new List<Pirkejas>();

			string query = $@"SELECT * FROM `{Config.TblPrefix}pirkejai` ORDER BY kodas ASC";
			var dt = Sql.Query(query);

			foreach (DataRow item in dt)
			{
				spec.Add(new Pirkejas
				{
					Code = Convert.ToInt32(item["kodas"]),
					Name = Convert.ToString(item["vardas"]),
					Surname = Convert.ToString(item["pavarde"]),
					Address = Convert.ToString(item["adresas"]),
					Phone = Convert.ToString(item["telefonas"]),
					MethodOfPayment = Convert.ToString(item["atsiskaitymo_budas"]),
					DeliveryMethod = Convert.ToString(item["pristatymo_budas"])
				});
			}

			return spec;
		}

		public static Pirkejas Find(int id)
		{
			var query = $@"SELECT * FROM `{Config.TblPrefix}pirkejai` WHERE kodas=?id";

			var dt =
				Sql.Query(query, args => {
					args.Add("?id", MySqlDbType.Int32).Value = id;
				});

			if (dt.Count > 0)
			{
				var p = new Pirkejas();

				foreach (DataRow item in dt)
				{
					p.Code = Convert.ToInt32(item["kodas"]);
					p.Name = Convert.ToString(item["vardas"]);
					p.Surname = Convert.ToString(item["pavarde"]);
					p.Address = Convert.ToString(item["adresas"]);
					p.Phone = Convert.ToString(item["telefonas"]);
					p.MethodOfPayment = Convert.ToString(item["atsiskaitymo_budas"]);
					p.DeliveryMethod = Convert.ToString(item["pristatymo_budas"]);
				}

				return p;
			}

			return null;
		}

		public static void Update(Pirkejas p)
		{
			var query =
				$@"UPDATE `{Config.TblPrefix}pirkejai`
				SET 
					vardas=?vardas,
					pavarde=?pavarde,
					adresas=?adresas,
					telefonas=?tel,
					atsiskaitymo_budas=?atsisk,
					pristatymo_budas=?pristBud
				WHERE 
					kodas=?id";

			Sql.Update(query, args => {
				args.Add("?vardas", MySqlDbType.VarChar).Value = p.Name;
				args.Add("?pavarde", MySqlDbType.VarChar).Value = p.Surname;
				args.Add("?adresas", MySqlDbType.VarChar).Value = p.Address;
				args.Add("?tel", MySqlDbType.VarChar).Value = p.Phone;
				args.Add("?atsisk", MySqlDbType.VarChar).Value = p.MethodOfPayment;
				args.Add("?pristBud", MySqlDbType.VarChar).Value = p.DeliveryMethod;
				args.Add("?id", MySqlDbType.Int32).Value = p.Code;
			});
		}

		public static void Insert(Pirkejas p)
		{
			var query =
				$@"INSERT INTO `{Config.TblPrefix}pirkejai`
				(
					vardas,
					pavarde,
					adresas,
					telefonas,
					atsiskaitymo_budas,
					pristatymo_budas,
					kodas
				)
				VALUES(
					?vardas,
					?pavarde,
					?adresas,
					?tel,
					?atsisk,
					?pristBud,
					?id
				)";

			Sql.Insert(query, args => {
				args.Add("?vardas", MySqlDbType.VarChar).Value = p.Name;
				args.Add("?pavarde", MySqlDbType.VarChar).Value = p.Surname;
				args.Add("?adresas", MySqlDbType.VarChar).Value = p.Address;
				args.Add("?tel", MySqlDbType.VarChar).Value = p.Phone;
				args.Add("?atsisk", MySqlDbType.VarChar).Value = p.MethodOfPayment;
				args.Add("?pristBud", MySqlDbType.VarChar).Value = p.DeliveryMethod;
				args.Add("?id", MySqlDbType.Int32).Value = p.Code;
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

			var query = $@"DELETE FROM `{Config.TblPrefix}pirkejai` WHERE kodas=?id";
			Sql.Delete(query, args => {
				args.Add("?id", MySqlDbType.Int32).Value = id;
			});
		}
	}
}
