﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Home_Work_11_CSharp_MVVM
{
    /// <summary>
    /// Класс используется для установки значений в View через ViewModel
    /// </summary>
    class BindingProxy : Freezable
    {
        protected override Freezable CreateInstanceCore()
        {
            return new BindingProxy();
        }

        public object Data
        {
            get { return (object)GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }

        public static readonly DependencyProperty DataProperty =
        DependencyProperty.Register("Data", typeof(object), typeof(BindingProxy), 
            new UIPropertyMetadata(null));
    }
}
