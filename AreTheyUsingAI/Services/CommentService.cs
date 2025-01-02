using AreTheyUsingAI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace AreTheyUsingAI.Services
{
    public class CommentService : DbService<Comment>
    {
        public CommentService(string connectionString)
        {
            _connectionString = connectionString;
        }
        public override List<Comment> Get(long id = 0)
        {
            var comments = new List<Comment>();
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("SELECT * FROM COMMENT WHERE POSTID = @id", connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@id", id);
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var newComment = new Comment();
                            newComment.Id = Convert.ToInt64(reader["Id"]);
                            newComment.CommentText = reader["CommentText"].ToString();
                            newComment.PostId = Convert.ToInt32(reader["PostId"]);
                            comments.Add(newComment);
                        }
                    }
                }
            }
            return comments;
        }

        public override int Post(Comment newObj)
        {
            throw new NotImplementedException();
        }
    }
}
