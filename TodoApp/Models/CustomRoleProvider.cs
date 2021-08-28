using System;
using System.Linq;
using System.Web.Security;

namespace TodoApp.Models
{
    public class CustomRoleProvider : RoleProvider
    {
        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// ロール取得
        /// </summary>
        /// <param name="username">ユーザー名</param>
        /// <returns>ユーザーが所属するロール</returns>
        public override string[] GetRolesForUser(string username)
        {
            // DBアクセス
            using (TodoesContext db = new TodoesContext())
            {
                // 検索
                User user = db.Users
                    .Where(u => u.UserName == username)
                    .FirstOrDefault();

                // 結果確認
                if (user != null)
                {
                    // ユーザーが所属するロールの名前を返却
                    return user.Roles.Select(role => role.RoleName).ToArray();
                }
            }

            // 結果が無い場合
            return new string[] { };
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// ロール確認
        /// </summary>
        /// <param name="username">ユーザー名</param>
        /// <param name="roleName">ロール名</param>
        /// <returns>ユーザーがロールに所属している => ture / ユーザーがロールに所属していない => false</returns>
        public override bool IsUserInRole(string username, string roleName)
        {
            // usernameが所属するロールを取得
            string[] roles = this.GetRolesForUser(username);
            // 取得したロールの中にroleNameの情報があるかを確認
            return roles.Contains(roleName);
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}