using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using TodoApp.Migrations;
using TodoApp.Models;

namespace TodoApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// アプリケーション起動
        /// </summary>
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            // EntityFrameworkの初期化処理(Configurationクラスの実行処理)
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<TodoesContext, Configuration>());

        }
    }
}
