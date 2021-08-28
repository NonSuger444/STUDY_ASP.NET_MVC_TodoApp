using System.Data.Entity;

namespace TodoApp.Models
{
    /// <summary>
    /// コンテキスト : DBから取得したデータを格納
    /// </summary>
    public class TodoesContext : DbContext
    {
        /// <summary>
        /// Todo
        /// </summary>
        public DbSet<Todo> Todoes { get; set; }
        /// <summary>
        /// User
        /// </summary>
        public DbSet<User> Users { get; set; }
        /// <summary>
        /// Role
        /// </summary>
        public DbSet<Role> Roles { get; set; }
    }
}