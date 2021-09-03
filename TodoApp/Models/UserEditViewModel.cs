using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TodoApp.Models
{
    /// <summary>
    /// ユーザー編集画面
    /// </summary>
    public class UserEditViewModel
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// ユーザー名
        /// </summary>
        [Required]
        [StringLength(256)]
        [DisplayName("ユーザー名")]
        public string UserName { get; set; }
        /// <summary>
        /// パスワード
        /// </summary>
        [DataType(DataType.Password)]
        [DisplayName("新しいパスワード")]
        public string Password { get; set; }
        /// <summary>
        /// ロール
        /// </summary>
        [DisplayName("ロール")]
        public List<int> RoleIds { get; set; }
    }
}