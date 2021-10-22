using System;
using System.Collections.Generic;
using System.Text;

namespace Home_Work_11_CSharp_MVVM
{
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
    }
}
