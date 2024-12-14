using AreTheyUsingAI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace AreTheyUsingAI.Services
{
    public class PostService : DbService<Post>
    {
        public PostService(string connectionString)
        {
            _connectionString = connectionString;
        }
        public override List<Post> Get(long id)
        {
            var posts = new List<Post>();
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("SELECT * FROM POST WHERE ID = @id", connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@id",id);
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var newPost = new Post();
                            newPost.Id = Convert.ToInt64(reader["Id"]);
                            newPost.PostTitle = reader["PostTitle"].ToString();
                            newPost.PostDesc = reader["PostDesc"].ToString();
                            newPost.ThumbsUp = Convert.ToInt32(reader["ThumbsUp"]);
                            newPost.ThumbsDown = Convert.ToInt32(reader["ThumbsDown"]);
                            posts.Add(newPost);
                        }
                    }
                }
            }
            return posts;
        }

        public override bool Post(Post newObj)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("INSERT INTO POST (PostTitle, PostDesc) VALUES @PostTitle, @PostDesc", connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@PostTitle", newObj.PostTitle);
                    command.Parameters.AddWithValue("@PostDesc", newObj.PostDesc);
                    connection.Open();
                    command.ExecuteNonQuery();
                    return true;
                }
            }
            return false;
        }
    }
}
