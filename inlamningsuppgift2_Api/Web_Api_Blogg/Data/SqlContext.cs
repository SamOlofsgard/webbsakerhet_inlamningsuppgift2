using Microsoft.EntityFrameworkCore;
using Web_Api_Blogg.Models.Entities;

namespace Web_Api_Blogg.Data
{
    public class SqlContext:DbContext
    {
        public SqlContext()
        {

        }

        public SqlContext(DbContextOptions<SqlContext> options) : base(options)
        {

        }

        public virtual DbSet<BloggPostsEntity> BloggPosts { get; set; }

    }
}
