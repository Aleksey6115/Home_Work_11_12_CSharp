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
    class FileService : IFileService
    {
        public ObservableCollection<ClientModel> OpenFile(string path)
        {
            ObservableCollection<ClientModel> clients;
            clients = JsonConvert.DeserializeObject<ObservableCollection<ClientModel>>(File.ReadAllText(path));
            return clients;
        }

        public void SaveFile(string path, ObservableCollection<ClientModel> clients)
        {
            File.WriteAllText(path, JsonConvert.SerializeObject(clients));
        }
    }
}
