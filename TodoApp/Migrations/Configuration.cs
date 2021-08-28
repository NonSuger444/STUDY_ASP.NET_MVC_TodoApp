namespace TodoApp.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using TodoApp.Models;

    /// <summary>
    /// コンフィグレーション
    /// </summary>
    internal sealed class Configuration : DbMigrationsConfiguration<TodoApp.Models.TodoesContext>
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Configuration()
        {
            // DBの自動移行の有効化
            AutomaticMigrationsEnabled = true;
            // データ損失を許容(列が削除される等)
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "TodoApp.Models.TodoesContext";
        }

        /// <summary>
        /// マイグレーション後 - 自動的実行
        /// </summary>
        /// <param name="context"></param>
        protected override void Seed(TodoApp.Models.TodoesContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            // 初期情報 : 管理者ユーザー
            User admin = new User()
            {
                Id = 1,
                UserName = "admin",
                Password = "password",
                Roles = new List<Role>()
            };

            // 初期情報 : 一般ユーザー
            User sato = new User()
            {
                Id = 2,
                UserName = "sato",
                Password = "password",
                Roles = new List<Role>()
            };

            // 初期情報 : 管理者ロール
            Role administrators = new Role()
            {
                Id = 1,
                RoleName = "Administrators",
                Users = new List<User>()
            };

            // 初期情報 : 一般ユーザーロール
            Role users = new Role()
            {
                Id = 2,
                RoleName = "Users",
                Users = new List<User>()
            };

            // 管理者ユーザー > ロール => 管理者ロールを追加
            admin.Roles.Add(administrators);
            // 管理者ロール > ユーザー => 管理者ユーザーを追加
            administrators.Users.Add(admin);

            // 一般ユーザー > ロール => 一般ユーザーロールを追加
            sato.Roles.Add(users);
            // 一般ユーザーロール > ユーザー => 一般ユーザーを追加
            users.Users.Add(sato);

            // DB反映 : ユーザー情報
            context.Users.AddOrUpdate(user => user.Id, new User[] { admin, sato });
            // DB反映 : ロール情報
            context.Roles.AddOrUpdate(role => role.Id, new Role[] { administrators, users });
        }
    }
}
