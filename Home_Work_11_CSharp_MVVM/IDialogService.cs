using System;
using System.Collections.Generic;
using System.Text;

namespace Home_Work_11_CSharp_MVVM
{
    interface IDialogService
    {
        /// <summary>
        /// Вывод сообщения на экран
        /// </summary>
        /// <param name="message"></param>
        void ShowMessage(string message);

        /// <summary>
        /// Путь к файлу
        /// </summary>
        string FilePath { get; set; }

        /// <summary>
        /// Открытие окна для открвтия файла и получение пути к файлу
        /// </summary>
        /// <returns></returns>
        bool OpenFileDialog();

        /// <summary>
        /// Открытие окна для сохранения файла и получение пути к файлу
        /// </summary>
        /// <returns></returns>
        bool SaveFileDialog();
    }
}
