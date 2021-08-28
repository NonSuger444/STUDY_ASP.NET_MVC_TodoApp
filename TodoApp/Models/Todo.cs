using System;
using System.ComponentModel;

namespace TodoApp.Models
{
    /// <summary>
    /// プロパティ : Todoesテーブル
    /// </summary>
    public class Todo
    {
        /// <summary>
        /// ID [主キー]
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 概要
        /// </summary>
        [DisplayName("概要")]
        public string Summary { get; set; }
        /// <summary>
        /// 詳細
        /// </summary>
        [DisplayName("詳細")]
        public string Datail { get; set; }
        /// <summary>
        /// 期限
        /// </summary>
        [DisplayName("期限")]
        public DateTime Limit { get; set; }
        /// <summary>
        /// 完了フラグ
        /// </summary>
        [DisplayName("期限")]
        public bool Done { get; set; }
    }
}
