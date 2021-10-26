using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.IO;
using Newtonsoft.Json;

namespace Home_Work_11_CSharp_MVVM
{
    /// <summary>
    /// Работа с файлами
    /// </summary>
    class File_Service : IFile_Service
    {
        public ObservableCollection<Client_Model> OpenFile(string path)
        {
            ObservableCollection<Client_Model> clients;
            clients = JsonConvert.DeserializeObject<ObservableCollection<Client_Model>>(File.ReadAllText(path));
            return clients;
        }

        public void SaveFile(string path, ObservableCollection<Client_Model> clients)
        {
            File.WriteAllText(path, JsonConvert.SerializeObject(clients));
        }
    }
}
