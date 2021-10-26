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
        public string Date_last_changes { get; set; }

        /// <summary>
        /// Какое изменения
        /// </summary>
        public string Changes_type { get; set; }

        /// <summary>
        /// Кто произвёл изменения
        /// </summary>
        public string Author_of_changes { get; set; }

        /// <summary>
        /// Изменённый параметр
        /// </summary>
        public string Edit_value { get; set; }

        /// <summary>
        /// Конструктор класса Changes
        /// </summary>
        /// <param name="type"></param>
        /// <param name="author"></param>
        /// <param name="edit_value"></param>
        public Changes (string type, string author, string edit_value)
        {
            Date_last_changes = $"Дата: {DateTime.Now.ToString()}";
            Changes_type = $"Измененно: {type}";
            Author_of_changes = $"Изменил: {author}";
            Edit_value = edit_value;
        }
    }
}
