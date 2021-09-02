using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoApp.Models
{
    /// <summary>
    /// Userテーブル
    /// </summary>
    public class User
    {
        /// <summary>
        /// ID [主キー]
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// ユーザー名
        /// </summary>
        [Required]
        [Index(IsUnique = true)]
        [StringLength(256)]
        [DisplayName("ユーザー名")]
        public string UserName { get; set; }
        /// <summary>
        /// パスワード
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [DisplayName("パスワード")]
        public string Password { get; set; }
        /// <summary>
        /// ナビゲーションプロパティ : Roles
        /// </summary>
        public virtual ICollection<Role> Roles { get; set; }
        /// <summary>
        /// リストボックスで選択されたロール情報
        /// </summary>
        [NotMapped]
        [DisplayName("ロール")]
        public List<int> RoleIds { get; set; }

        /// <summary>
        /// Todoクラス情報
        /// </summary>
        public virtual ICollection<Todo> Todos { get; set; }
    }
}