using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TodoApp.Models
{
    /// <summary>
    /// Login時に必要な情報
    /// </summary>
    public class LoginViewModel
    {
        /// <summary>
        /// ユーザー名
        /// </summary>
        [Required]
        [DisplayName("ユーザー名")]
        public string UserName { get; set; }
        /// <summary>
        /// パスワード
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [DisplayName("パスワード")]
        public string Password { get; set; }
    }
}