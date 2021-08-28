using System.Collections.Generic;

namespace TodoApp.Models
{
    /// <summary>
    /// Roleテーブル
    /// </summary>
    public class Role
    {
        /// <summary>
        /// ID [主キー]
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// ロール名
        /// </summary>
        public string RoleName { get; set; }
        /// <summary>
        /// ナビゲーションプロパティ : Users
        /// </summary>
        public virtual ICollection<User> Users { get; set; }
    }
}