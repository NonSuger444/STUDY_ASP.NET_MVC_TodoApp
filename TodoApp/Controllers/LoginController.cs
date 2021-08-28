using System.Web.Mvc;
using System.Web.Security;
using TodoApp.Models;

namespace TodoApp.Controllers
{
    /// <summary>
    /// ログインコントローラー
    /// </summary>
    [AllowAnonymous]
    public class LoginController : Controller
    {
        /// <summary>
        /// 認証機能
        /// </summary>
        readonly CustomMembershipProvider membershipProvider = new CustomMembershipProvider();

        /// <summary>
        /// GET : Login
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// POST : Login
        /// </summary>
        /// <param name="model">LoginViewModel</param>
        /// <returns>Signin成功 => Todoes.Index / Signin失敗 => Login.Index</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "UserName,Password")] LoginViewModel model)
        {
            // LoginViewModelに指定したバリデーションにエラーがあるかを確認
            if (ModelState.IsValid)
            {
                // 入力されたユーザー名･パスワードが正しいかを確認
                if (this.membershipProvider.ValidateUser(model.UserName, model.Password))
                {
                    // 認証クッキーをブラウザに保持させる
                    FormsAuthentication.SetAuthCookie(model.UserName, false);
                    // TodoesコントローラーのIndexアクションメソッドへリダイレクト
                    return RedirectToAction("Index", "Todoes");
                }
            }
            // ログイン失敗を示すエラーメッセージ
            ViewBag.Message = "ログインに失敗しました。";
            // Login画面に戻る
            return View(model);
        }

        /// <summary>
        /// Logout
        /// </summary>
        /// <returns>Login.Index</returns>
        public ActionResult SignOut()
        {
            // 認証クッキーを削除する
            FormsAuthentication.SignOut();
            // Login画面に戻る
            return RedirectToAction("Index");
        }
    }
}