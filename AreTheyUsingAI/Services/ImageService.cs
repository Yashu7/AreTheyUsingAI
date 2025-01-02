using AreTheyUsingAI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AreTheyUsingAI.Services
{
    public class ImageService : DbService<Image>
    {
        public ImageService(string connectionString)
        {
            _connectionString = connectionString;
        }
        public override List<Image> Get(long id = 0)
        {
            var images = new List<Image>();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    using (var command = new SqlCommand("SELECT * FROM IMAGE WHERE POSTID = @id", connection))
                    {
                        command.CommandType = CommandType.Text;
                        command.Parameters.AddWithValue("@id", id);
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var image = new Image();
                                image.Id = Convert.ToInt64(reader["Id"]);
                                image.ImageName = reader["ImageName"].ToString();
                                image.PostId = Convert.ToInt32(reader["PostId"]);
                                image.ImageData = Encoding.ASCII.GetBytes(reader["ImageData"].ToString());
                                images.Add(image);
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return images;
        }

        public override int Post(Image newObj)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    using (var command = new SqlCommand("INSERT INTO Image (PostId, ImageName ,ImageData) OUTPUT INSERTED.Id  VALUES (@PostId, @ImageName, @ImageData)", connection))
                    {
                        command.CommandType = CommandType.Text;
                        command.Parameters.AddWithValue("@PostId", newObj.PostId);
                        command.Parameters.AddWithValue("@ImageName", newObj.ImageName);
                        command.Parameters.AddWithValue("@ImageData", newObj.ImageData);
                        connection.Open();
                        var latestId = command.ExecuteScalar();
                        return Convert.ToInt32(latestId);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return 0;
        }
    }
}
