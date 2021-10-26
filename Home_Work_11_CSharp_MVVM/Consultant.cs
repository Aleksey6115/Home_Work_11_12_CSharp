using System;
using System.Collections.Generic;
using System.Text;

namespace Home_Work_11_CSharp_MVVM
{
    /// <summary>
    /// Пользователь консультант
    /// </summary>
    class Consultant : IUsers
    {
        public bool IsReadOnly
        {
            get { return true; }
            set { }
        }
        public string Name
        {
            get { return "Консультант"; }
            set { }
        }

        /// <summary>
        /// Для коректного отображения в ComboBox
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{Name}";
        }
    }
}
