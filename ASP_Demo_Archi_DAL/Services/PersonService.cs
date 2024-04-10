using ASP_Demo_Archi_DAL.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP_Demo_Archi_DAL.Services
{
    internal class PersonService
    {
        private string connectionString = @"Data Source=GOS-VDI912\TFTIC;Initial Catalog=IMDB;Integrated Security=True;Connect Timeout=60;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False";
        public PersonService()
        {
            //maListe = new List<Movie>();
            //maListe.Add(new Movie
            //{
            //    Id = 1,
            //    Title = "A New Hope",
            //    Description = "Un wookie et un pirate veulent se taper la princesse ...",
            //    Realisator = "George Lucas"
            //});
            //maListe.Add(new Movie
            //{
            //    Id = 2,
            //    Title = "Empire strikes back",
            //    Description = "Les méchants gagnent pour une fois",
            //    Realisator = "George Lucas"
            //});
        }

        private Person Converter(SqlDataReader reader)
        {
            return new Person
            {
                Id = (int)reader["Id"],
                Lastname = (string)reader["Lastname"],
                Firstname = (string)reader["Firstname"],
                PictureURL = (string)reader["PictureURL"]
            };
        }

        public List<Person> GetAll()
        {
            List<Person> list = new List<Person>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM Person";
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(Converter(reader));
                        }
                    }
                    connection.Close();
                }
            }
            return list;
        }

        public Person GetById(int id)
        {
            Person p = new Person();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM Person WHERE Id = @id";
                    command.Parameters.AddWithValue("id", id);
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            p = Converter(reader);
                        }
                    }
                    connection.Close();
                }
            }
            return p;
        }

        public bool Create(Person p)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = "INSERT INTO Person (Lastname, Firstname, PictureURL) " +
                        "VALUES (@Lastname, @Firstname, @PictureURL)";

                    cmd.Parameters.AddWithValue("Lastname", p.Lastname);
                    cmd.Parameters.AddWithValue("Firstname", p.Firstname);
                    cmd.Parameters.AddWithValue("PictureURL", p.PictureURL);

                    try
                    {
                        connection.Open();
                        return cmd.ExecuteNonQuery() > 0;
                    }
                    catch (SqlException ex)
                    {
                        //Gérer l'exception
                        throw ex;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    connection.Close();
                }
            }

            //movie.Id = (maListe.Count() > 0) ? maListe.Max(s => s.Id) + 1 : 1;
            //maListe.Add(movie);
        }

        public void Edit(Person p)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = "UPDATE Movie SET Lastname = @Lastname, Firstname = @Firstname, PictureURL = @PictureURL " +
                        "WHERE Id = @id";

                    cmd.Parameters.AddWithValue("id", p.Id);
                    cmd.Parameters.AddWithValue("Lastname", p.Lastname);
                    cmd.Parameters.AddWithValue("Firstname", p.Firstname);
                    cmd.Parameters.AddWithValue("PictureURL", p.PictureURL);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            //int index = maListe.FindIndex(f => f.Id == movie.Id);
            //maListe[index] = movie;
        }

        public void Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = "DELETE FROM Movie WHERE Id = @id";

                    cmd.Parameters.AddWithValue("id", id);


                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            //Movie aSupprimer = maListe.SingleOrDefault(f => f.Id == id);
            //maListe.Remove(aSupprimer);
        }
    }
}
