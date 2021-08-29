using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TodoApp.Models;

namespace TodoApp.Controllers
{
    [Authorize(Roles = "Administrators")]
    public class UsersController : Controller
    {
        /// <summary>
        /// Todoesテーブル情報
        /// </summary>
        private TodoesContext db = new TodoesContext();

        /// <summary>
        /// CustomMembershipProvider情報
        /// </summary>
        private readonly CustomMembershipProvider membershipProvider = new CustomMembershipProvider();

        // GET: Users
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            // ロール情報取得
            this.SetRoles(new List<Role>());
            return View();
        }

        // POST: Users/Create
        // 過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        // 詳細については、https://go.microsoft.com/fwlink/?LinkId=317598 をご覧ください。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserName,Password,RoleIds")] User user)
        {
            // 画面で選択したロール情報をDBから取得する
            // => TodoesテーブルのRolesカラムからロールIDが一致する情報を取得
            List<Role> roles = db.Roles.Where(role => user.RoleIds.Contains(role.Id)).ToList();

            if (ModelState.IsValid)
            {
                // ロール情報を設定
                user.Roles = roles;

                // パスワードをハッシュ化
                user.Password = this.membershipProvider.GeneratePasswordHash(user.UserName, user.Password);

                // DBにuser情報を設定
                db.Users.Add(user);
                // 設定した情報をDBに登録
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            // 選択したロール情報を復元
            this.SetRoles(roles);
            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            // ロール情報を取得
            this.SetRoles(user.Roles);
            return View(user);
        }

        // POST: Users/Edit/5
        // 過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        // 詳細については、https://go.microsoft.com/fwlink/?LinkId=317598 をご覧ください。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserName,Password,RoleIds")] User user)
        {
            // 画面で選択したロール情報をDBから取得する
            // => TodoesテーブルのRolesカラムからロールIDが一致する情報を取得
            List<Role> roles = db.Roles.Where(role => user.RoleIds.Contains(role.Id)).ToList();

            if (ModelState.IsValid)
            {
                // DBからUser情報を取得
                User dbUser = db.Users.Find(user.Id);

                // User情報が無ければNotFooundを返す
                if (dbUser == null)
                    return HttpNotFound();

                // DBから取得したユーザー名 => 画面で入力したユーザー名へ変換
                // ユーザー名の変更がある場合に限る
                if (!dbUser.UserName.Equals(user.UserName))
                    dbUser.UserName = user.UserName;

                // DBから取得したパスワード => 画面で入力したパスワードへ変換
                // パスワードの変更がある場合に限る => ★ ハッシュ化された内容を画面に入力してしまうと、ログイン時にパスワードが一致しなくなると思う
                if (!dbUser.Password.Equals(user.Password))
                    // DBに反映するのはハッシュ化されたパスワード
                    dbUser.Password = this.membershipProvider.GeneratePasswordHash(user.UserName, user.Password);

                // DBに登録されているロール情報をクリア
                dbUser.Roles.Clear();
                // 画面で指定されたロール情報を指定
                roles.ForEach(role => dbUser.Roles.Add(role));


                db.Entry(dbUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            // ロール情報取得 => 画面で指定した内容が消えないようにする為 
            this.SetRoles(user.Roles);
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// ロール情報取得
        /// </summary>
        /// <param name="userRoles">現在所属しているロールの情報</param>
        private void SetRoles(ICollection<Role> userRoles)
        {
            // Collectionからint配列へ変換
            int[] roles = userRoles.Select(item => item.Id).ToArray();
            // DBからロール情報を取得
            List<SelectListItem> list = db.Roles.Select(item => new SelectListItem()
            {
                // 項目名
                Text = item.RoleName,
                // 値
                Value = item.Id.ToString(),
                // 選択状態
                Selected = roles.Contains(item.Id)
            }).ToList();
            // Viewへデータを引き渡す
            ViewBag.RoleIds = list;
        }
    }
}
