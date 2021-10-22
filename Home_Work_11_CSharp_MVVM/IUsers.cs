using System;
using System.Collections.Generic;
using System.Text;

namespace Home_Work_11_CSharp_MVVM
{
    interface IUsers
    {
        /// <summary>
        /// Свойства для установки параметра IsReadOnle в DataGrid
        /// </summary>
        bool IsReadOnly { get; set; }

        /// <summary>
        /// Название должности
        /// </summary>
        string Name { get; set; }
    }
}
