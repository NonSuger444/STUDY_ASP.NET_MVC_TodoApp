using System;
using System.Linq;
using System.Security.Cryptography;
using System.Web.Security;

namespace TodoApp.Models
{
    /// <summary>
    /// 認証機能
    /// </summary>
    public class CustomMembershipProvider : MembershipProvider
    {
        public override bool EnablePasswordRetrieval => throw new NotImplementedException();

        public override bool EnablePasswordReset => throw new NotImplementedException();

        public override bool RequiresQuestionAndAnswer => throw new NotImplementedException();

        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override int MaxInvalidPasswordAttempts => throw new NotImplementedException();

        public override int PasswordAttemptWindow => throw new NotImplementedException();

        public override bool RequiresUniqueEmail => throw new NotImplementedException();

        public override MembershipPasswordFormat PasswordFormat => throw new NotImplementedException();

        public override int MinRequiredPasswordLength => throw new NotImplementedException();

        public override int MinRequiredNonAlphanumericCharacters => throw new NotImplementedException();

        public override string PasswordStrengthRegularExpression => throw new NotImplementedException();

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// ユーザー情報確認
        /// </summary>
        /// <param name="username">ユーザー名</param>
        /// <param name="password">パスワード</param>
        /// <returns>ユーザー名とパスワードが有効 => ture / ユーザー名とパスワードが無効 => false</returns>
        public override bool ValidateUser(string username, string password)
        {
            // DBアクセス
            using (TodoesContext db = new TodoesContext())
            {
                // パスワードのハッシュ化
                string hash = this.GeneratePasswordHash(username, password);

                // 検索
                User user = db.Users
                    .Where(u => u.UserName == username && u.Password == hash)
                    .FirstOrDefault();

                // 結果確認
                if (user != null)
                {
                    // 認証成功
                    return true;
                }
            }

            // 認証失敗
            return false;
        }

        /// <summary>
        /// パスワードのハッシュ化
        /// </summary>
        /// <param name="username">ユーザー名</param>
        /// <param name="password">パスワード</param>
        /// <returns>ハッシュ化されたパスワード</returns>
        public string GeneratePasswordHash(string username, string password)
        {
            // ユーザー名にsaltを付加
            string rawSalt = $"secret_{username}";
            // ユーザー名のハッシュ化に「SHA256」を用いる
            SHA256CryptoServiceProvider sha256 = new SHA256CryptoServiceProvider();
            // ユーザー名をハッシュ化
            byte[] salt = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(rawSalt));

            // パスワードのハッシュ化に「PBKDF2」を用いる
            // ストレッチングは10000回とする
            Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            // パスワードをハッシュ化
            // 生成する疑似ランダムキーバイト数を32とする
            byte[] hash = pbkdf2.GetBytes(32);

            // バイト配列を文字列にコンバートした結果を返却する
            return Convert.ToBase64String(hash);
        }
    }
}