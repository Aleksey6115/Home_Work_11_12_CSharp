using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace Home_Work_11_CSharp_MVVM
{
    interface IFile_Service
    {
        /// <summary>
        /// Открыть файл
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        ObservableCollection<Client_Model> OpenFile(string path);

        /// <summary>
        /// Сохранить файл
        /// </summary>
        /// <param name="path"></param>
        /// <param name="clients"></param>
        void SaveFile(string path, ObservableCollection<Client_Model> clients);
    }
}
