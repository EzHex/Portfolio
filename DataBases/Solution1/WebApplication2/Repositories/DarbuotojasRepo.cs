using System.Data;
using MySql.Data.MySqlClient;
using WebApplication2.Models;


namespace WebApplication2.Repositories
{
	/// <summary>
	/// Database operations related to 'Darbuotojas' entity.
	/// </summary>
	public class DarbuotojasRepo
	{
		public static List<DarbuotojasList> List()
		{
			var darbuotojai = new List<DarbuotojasList>();
			
			string query = $@"SELECT * FROM `{Config.TblPrefix}darbuotojai` ORDER BY vardas ASC";
			var dt = Sql.Query(query);

			foreach( DataRow item in dt )
			{
				darbuotojai.Add(new DarbuotojasList
				{
					Name = Convert.ToString(item["vardas"]),
					Duties = Convert.ToString(item["pareigos"]),
					Id = Convert.ToString(item["id_Darbuotojas"]),
					FkDepartment = Convert.ToString(item["fk_Padalinysid_Padalinys"])
				});
			}

			return darbuotojai;
		}

		public static Darbuotojas Find(int id)
		{
			var query = $@"SELECT * FROM `{Config.TblPrefix}darbuotojai` WHERE id_Darbuotojas=?id";

			var dt = 
				Sql.Query(query, args => {
					args.Add("?id", MySqlDbType.VarChar).Value = id;
				});

			if( dt.Count > 0 )
			{
				var darbuotojas = new Darbuotojas();

				foreach( DataRow item in dt )
				{
					darbuotojas.Name = Convert.ToString(item["vardas"]);
					darbuotojas.Duties = Convert.ToString(item["pareigos"]);
					darbuotojas.Id = Convert.ToInt32(item["id_Darbuotojas"]);
					darbuotojas.FkDepartment = Convert.ToInt32(item["fk_Padalinysid_Padalinys"]);
				}

				return darbuotojas;
			}

			return null;
		}

		public static void Update(Darbuotojas darb)
		{						
			var query = 
				$@"UPDATE `{Config.TblPrefix}darbuotojai`
				SET 
					vardas=?vardas,
					pareigos=?pareigos,
					fk_Padalinysid_Padalinys=?fkPadalinys
				WHERE 
					id_Darbuotojas=?id";

			Sql.Update(query, args => {
				args.Add("?vardas", MySqlDbType.VarChar).Value = darb.Name;
				args.Add("?pareigos", MySqlDbType.VarChar).Value = darb.Duties;
				args.Add("?id", MySqlDbType.Int32).Value = darb.Id;
				args.Add("?fkPadalinys", MySqlDbType.Int32).Value = darb.FkDepartment;
			});				
		}

		public static void Insert(Darbuotojas darb)
		{							
			var query = 
				$@"INSERT INTO `{Config.TblPrefix}darbuotojai`
				(
					vardas,
					pareigos,
					id_Darbuotojas,
					fk_Padalinysid_Padalinys
				)
				VALUES(
					?vardas,
					?pareigos,
					?id,
					?fkPadalinys
				)";

			Sql.Insert(query, args => {
				args.Add("?vardas", MySqlDbType.VarChar).Value = darb.Name;
				args.Add("?pareigos", MySqlDbType.VarChar).Value = darb.Duties;
				args.Add("?id", MySqlDbType.Int32).Value = darb.Id;
				args.Add("?fkPadalinys", MySqlDbType.Int32).Value = darb.FkDepartment;
			});				
		}

		public static void Delete(int id)
		{			
			var query = $@"DELETE FROM `{Config.TblPrefix}darbuotojai` WHERE id_Darbuotojas=?id";
			Sql.Delete(query, args => {
				args.Add("?id", MySqlDbType.VarChar).Value = id;
			});			
		}
	}
}