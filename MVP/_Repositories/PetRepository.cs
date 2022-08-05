using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using MVP.Models;


namespace MVP._Repositories
{
    public class PetRepository : BaseRepostitory, IPetRepositry
    {
        //construcotr
        public PetRepository(string connectionstring)
        {
            this.connectionstring = connectionstring;
        }
        public void Add(PetModel petModel)
        {
            var petList = new List<PetModel>();
            using (var connection = new SqlConnection(connectionstring))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "insert into Pet values(@name,@type,@color)";
                command.Parameters.Add("@name", SqlDbType.NVarChar).Value = petModel.Name;
                command.Parameters.Add("@type", SqlDbType.NVarChar).Value = petModel.Type;
                command.Parameters.Add("@color", SqlDbType.NVarChar).Value = petModel.Color;
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            var petList = new List<PetModel>();
            using (var connection = new SqlConnection(connectionstring))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "delete from Pet where id=@id";
                command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                command.ExecuteNonQuery();
            }
        }

        public void Edit(PetModel petModel)
        {
            var petList = new List<PetModel>();
            using (var connection = new SqlConnection(connectionstring))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"update Pet set name=@name,type=@type,color=@color where id=@id";
                command.Parameters.Add("@id", SqlDbType.Int).Value = petModel.Id;
                command.Parameters.Add("@name", SqlDbType.NVarChar).Value = petModel.Name;
                command.Parameters.Add("@type", SqlDbType.NVarChar).Value = petModel.Type;
                command.Parameters.Add("@color", SqlDbType.NVarChar).Value = petModel.Color;
                command.ExecuteNonQuery();
            }
        }

        public IEnumerable<PetModel> GetAll()
        {
            var petList = new List<PetModel>();
            using (var connection = new SqlConnection(connectionstring)) 
             using(var command=new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "select * from Pet order by ID desc";
                using (var reader=command.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        var petModel = new PetModel();
                        petModel.Id = (int)reader[0];
                        petModel.Name = reader[1].ToString();
                        petModel.Type = reader[2].ToString();
                        petModel.Color = reader[3].ToString();
                        petList.Add(petModel);

                    }
                }
            }
            return petList;
        }

        public IEnumerable<PetModel> GetByValue(string value)
        {
            var petList = new List<PetModel>();
            int petId = int.TryParse(value, out _) ? Convert.ToInt32(value) : 0;
            string petName = value;
            using (var connection = new SqlConnection(connectionstring))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"select * from Pet
                                        where ID=@id or name like @name+'%' 
                                        order by ID desc";
                command.Parameters.Add("@id", SqlDbType.Int).Value = petId;
                command.Parameters.Add("@name", SqlDbType.NVarChar).Value = petName;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var petModel = new PetModel();
                        petModel.Id = (int)reader[0];
                        petModel.Name = reader[1].ToString();
                        petModel.Type = reader[2].ToString();
                        petModel.Color = reader[3].ToString();
                        petList.Add(petModel);

                    }
                }
            }
            return petList;
        }
    }
}
