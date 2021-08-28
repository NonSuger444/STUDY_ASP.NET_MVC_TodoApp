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
        /// �A�v���P�[�V�����N��
        /// </summary>
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            // EntityFramework�̏���������(Configuration�N���X�̎��s����)
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<TodoesContext, Configuration>());

        }
    }
}
