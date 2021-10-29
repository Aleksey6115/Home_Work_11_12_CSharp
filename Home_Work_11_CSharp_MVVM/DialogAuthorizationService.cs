using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Collections.ObjectModel;

namespace Home_Work_11_CSharp_MVVM
{
    /// <summary>
    /// Окно авторизации
    /// </summary>
    class DialogAuthorizationService
    {
        ObservableCollection<IUsers> users = new ObservableCollection<IUsers>
        {
            new Maneger(),
            new Consultant()
        }; // Список пользователей

        /// <summary>
        /// Выбранный пользователь
        /// </summary>
        public IUsers SelectedUser { get; set; }

        /// <summary>
        /// Открытие окна авторизации
        /// </summary>
        /// <returns></returns>
        public bool OpenAuthorizationDialog()
        {
            WindowAuthorization WA = new WindowAuthorization();
            WA.comboUsers.Items.Clear();
            WA.comboUsers.ItemsSource = users;

            if (WA.ShowDialog() == true)
            {
                SelectedUser = WA.comboUsers.SelectedItem as IUsers;
                return true;
            }
            else return false;
        }

        /// <summary>
        /// Вывод сообщения
        /// </summary>
        /// <param name="message"></param>
        public void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }
    }
}
