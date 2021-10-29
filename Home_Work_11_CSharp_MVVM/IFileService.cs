using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace Home_Work_11_CSharp_MVVM
{
    interface IFileService
    {
        /// <summary>
        /// Открыть файл
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        ObservableCollection<ClientModel> OpenFile(string path);

        /// <summary>
        /// Сохранить файл
        /// </summary>
        /// <param name="path"></param>
        /// <param name="clients"></param>
        void SaveFile(string path, ObservableCollection<ClientModel> clients);
    }
}
