using System;
using System.Collections.Generic;
using System.Text;

namespace Home_Work_11_CSharp_MVVM
{
    class Maneger : IUsers
    {
        public bool IsReadOnly
        {
            get { return false; }
            set { }
        }
        public string Name
        {
            get { return "Менеджер"; }
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
