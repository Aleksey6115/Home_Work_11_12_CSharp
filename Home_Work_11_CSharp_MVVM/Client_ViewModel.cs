using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Windows;


namespace Home_Work_11_CSharp_MVVM 
{
    /// <summary>
    /// ViewModel
    /// </summary>
    class Client_ViewModel : INotifyPropertyChanged
    {
        private Client_Model selected_client; // Выбранный клиент
        private IDialog_Service dialog_service; // Сервис диалогового окна "Открыть/Сохранить"
        private IFile_Service file_service; // Сервис для работы с файлами
        private bool isReadOnly; // установки параметра IsReadOnly в DataGrid
        private Dialog_Authorization_Service dialog_authorization = new Dialog_Authorization_Service(); // Авторизация
        public static IUsers current_user; // Текущий пользователь


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
        public Client_ViewModel(IDialog_Service dialog, IFile_Service file)
        {
            Clients = new ObservableCollection<Client_Model>();
            dialog_service = dialog;
            file_service = file;

        }

        #region Комманды
        private RelayCommand openFileCommand; // Комманда открытие файла
        private RelayCommand saveFileCommand; // Комманда сохранения файла
        private RelayCommand generatorBase; // Комманда генерирование БД
        private RelayCommand addClient; // Комманда добавить клиента
        private RelayCommand removeClient; // Комманда удаление клиента
        private RelayCommand selectedUser; // Комманда выбора пользователя

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
                },
                obj => current_user != null));
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
                },
                obj => current_user != null));
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
                            Passport_number = rand.Next(2000000, 3000000),
                            changes = new ObservableCollection<Changes>(),
                        });
                    }
                },
                obj => current_user != null));
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
                obj => isReadOnly == false && current_user != null));
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
                obj => (Clients.Count > 0 && isReadOnly == false && current_user != null)
                ));
            }
        }

        /// <summary>
        /// Комманда выбора пользователя
        /// </summary>
        public RelayCommand SelectedUser
        {
            get
            {
                return selectedUser ?? (selectedUser = new RelayCommand(obj =>
                {
                    current_user = null;
                    while (current_user == null)
                    {
                        try
                        {
                            if (dialog_authorization.OpenAuthorizationDialog() == true)
                            {
                                current_user = dialog_authorization.SelectedUser;
                            }
                        }
                        catch
                        {
                            dialog_authorization.ShowMessage("Ошибка");
                        }

                        if (current_user == null)
                            dialog_authorization.ShowMessage("Нужно выбрать пользователя");
                    }
                    IsReadOnly = current_user.IsReadOnly;
                }));
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
