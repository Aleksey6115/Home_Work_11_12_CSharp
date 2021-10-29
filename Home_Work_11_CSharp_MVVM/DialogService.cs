using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;
using System.Windows;

namespace Home_Work_11_CSharp_MVVM
{
    /// <summary>
    /// Сервис окна работы с файлами
    /// </summary>
    class DialogService : IDialogService
    {
        public string FilePath { get; set; }

        public bool OpenFileDialog()
        {
            OpenFileDialog openfiledialog = new OpenFileDialog();

            if (openfiledialog.ShowDialog() == true) // Если пользователь нажал кнопку ОК
            {
                FilePath = openfiledialog.FileName;
                return true;
            }
            return false;
        }

        public bool SaveFileDialog()
        {
            SaveFileDialog savefiledialog = new SaveFileDialog();

            if (savefiledialog.ShowDialog() == true) // Если пользователь нажал ОК
            {
                FilePath = savefiledialog.FileName;
                return true;
            }
            return false;
        }

        public void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }
    }
}
