using System;
using System.Collections.Generic;
using System.Text;

namespace Home_Work_11_CSharp_MVVM
{
    /// <summary>
    /// Произведённые изменения
    /// </summary>
    class Changes
    {
        /// <summary>
        /// Дата последнего изменения
        /// </summary>
        public string DateLastChanges { get; set; }
        
        /// <summary>
        /// Какое изменения
        /// </summary>
        public string ChangesType { get; set; }

        /// <summary>
        /// Кто произвёл изменения
        /// </summary>
        public string AuthorOfChanges { get; set; }

        /// <summary>
        /// Изменённый параметр
        /// </summary>
        public string EditValue { get; set; }

        /// <summary>
        /// Конструктор класса Changes
        /// </summary>
        /// <param name="type"></param>
        /// <param name="author"></param>
        /// <param name="edit_value"></param>
        public Changes (string type, string author, string edit_value)
        {
            DateLastChanges = $"Дата: {DateTime.Now.ToString()}";
            ChangesType = $"Измененно: {type}";
            AuthorOfChanges = $"Изменил: {author}";
            EditValue = edit_value;
        }
    }
}
