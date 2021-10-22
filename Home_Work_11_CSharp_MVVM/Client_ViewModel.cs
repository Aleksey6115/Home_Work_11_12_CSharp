using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.ComponentModel;


namespace Home_Work_11_CSharp_MVVM 
{
    class Client_ViewModel : INotifyPropertyChanged
    {
        private Client_Model selected_client; // Выбранный клиент
        private IDialog_Service dialog_service; // Сервис диалогового окна "Открыть/Сохранить"
        private IFile_Service file_service; // Сервис для работы с файлами
        private bool isReadOnly; // установки параметра IsReadOnly в DataGrid

        #region Свойства
        /// <summary>
        /// Выбранный клиент
        /// </summary>
        public Client_Model Selected_client
        {
            get { return selected_client; }
            set
            {
                selected_client = value;
                OnPropertyChanged("Selected_client");
            }
        }

        /// <summary>
        /// Список клиентов
        /// </summary>
        public ObservableCollection<Client_Model> Clients { get; set; }

        /// <summary>
        /// Свойства для установки параметра IsReadOnly в DataGrid
        /// </summary>
        public bool IsReadOnly
        {
            get { return isReadOnly; }
            set
            {
                isReadOnly = value;
                OnPropertyChanged("IsReadOnly");
            }
        }
        #endregion

        /// <summary>
        /// Конструктор класса Client_ViewModel
        /// </summary>
        public Client_ViewModel(IDialog_Service dialog, IFile_Service file, IUsers user)
        {
            Clients = new ObservableCollection<Client_Model>();
            dialog_service = dialog;
            file_service = file;
            isReadOnly = user.IsReadOnly;
        }

        #region Комманды
        private RelayCommand openFileCommand; // Комманда открытие файла
        private RelayCommand saveFileCommand; // Комманда сохранения файла
        private RelayCommand generatorBase; // Комманда генерирование БД
        private RelayCommand addClient; // Комманда добавить клиента
        private RelayCommand removeClient; // Комманда удаление клиента

        /// <summary>
        /// Комманда открытие файла
        /// </summary>
        public RelayCommand OpenFileCommand
        {
            get
            {
                return openFileCommand ?? (openFileCommand = new RelayCommand(obj =>
                {
                    try
                    {
                        if (dialog_service.OpenFileDialog() == true)
                        {
                            Clients.Clear();
                            var clients = file_service.OpenFile(dialog_service.FilePath);

                            foreach (var c in clients)
                                Clients.Add(c);

                            dialog_service.ShowMessage("Файл загружен");
                        }
                    }
                    catch
                    {
                        dialog_service.ShowMessage("Ошибка");
                    }
                }));
            }
        }

        /// <summary>
        /// Комманда сохранения файла
        /// </summary>
        public RelayCommand SaveFileCommand
        {
            get
            {
                return saveFileCommand ?? (saveFileCommand = new RelayCommand(obj =>
                {
                    try
                    {
                        if (dialog_service.SaveFileDialog() == true)
                        {
                            file_service.SaveFile(dialog_service.FilePath, Clients);
                            dialog_service.ShowMessage("Файл сохранён");
                        }
                    }
                    catch
                    {
                        dialog_service.ShowMessage("Ошибка");
                    }
                }));
            }
        }

        /// <summary>
        /// Комманда генерирование БД
        /// </summary>
        public RelayCommand GeneratorBase
        {
            get
            {
                return generatorBase ?? (generatorBase = new RelayCommand(obj =>
                {
                    Random rand = new Random();
                    Clients.Clear();

                    for (int i = 0; i < 30; i++)
                    {
                        Clients.Add(new Client_Model
                        {
                            First_name = $"Имя {i + 1}",
                            Last_name = $"Фамилия {i + 1}",
                            Telefon_number = rand.Next(1000000, 2000000),
                            Passport_number = rand.Next(2000000, 3000000)
                        });
                    }
                }));
            }
        }

        /// <summary>
        /// Комманда добавить клиента
        /// </summary>
        public RelayCommand AddClient
        {
            get
            {
                return addClient ?? (addClient = new RelayCommand(obj =>
                {
                    Clients.Add(new Client_Model());
                },
                obj => isReadOnly == false));
            }
        }

        /// <summary>
        /// Комманда удаление клиента
        /// </summary>
        public RelayCommand RemoveClient
        {
            get
            {
                return removeClient ?? (removeClient = new RelayCommand(obj =>
                {
                    Client_Model client = obj as Client_Model;
                    Clients.Remove(client);
                },
                obj => (Clients.Count > 0 && isReadOnly == false)
                ));
            }
        }
        #endregion

        // Оповещение внешних клиентов об изменениях
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
