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
    class ClientViewModel : INotifyPropertyChanged
    {
        private ClientModel selectedClient; // Выбранный клиент
        private IDialogService dialogService; // Сервис диалогового окна "Открыть/Сохранить"
        private IFileService fileService; // Сервис для работы с файлами
        private bool isReadOnly; // установки параметра IsReadOnly в DataGrid
        private DialogAuthorizationService dialogAuthorization = new DialogAuthorizationService(); // Авторизация
        public static IUsers currentUser; // Текущий пользователь


        #region Свойства
        /// <summary>
        /// Выбранный клиент
        /// </summary>
        public ClientModel SelectedClient
        {
            get { return selectedClient; }
            set
            {
                selectedClient = value;
                OnPropertyChanged("SelectedClient");
            }
        }

        /// <summary>
        /// Список клиентов
        /// </summary>
        public ObservableCollection<ClientModel> Clients { get; set; }

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
        public ClientViewModel(IDialogService dialog, IFileService file)
        {
            Clients = new ObservableCollection<ClientModel>();
            dialogService = dialog;
            fileService = file;

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
                        if (dialogService.OpenFileDialog() == true)
                        {
                            Clients.Clear();
                            var clients = fileService.OpenFile(dialogService.FilePath);

                            foreach (var c in clients)
                                Clients.Add(c);

                            dialogService.ShowMessage("Файл загружен");
                        }
                    }
                    catch
                    {
                        dialogService.ShowMessage("Ошибка");
                    }
                },
                obj => currentUser != null));
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
                        if (dialogService.SaveFileDialog() == true)
                        {
                            fileService.SaveFile(dialogService.FilePath, Clients);
                            dialogService.ShowMessage("Файл сохранён");
                        }
                    }
                    catch
                    {
                        dialogService.ShowMessage("Ошибка");
                    }
                },
                obj => currentUser != null));
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
                        Clients.Add(new ClientModel
                        {
                            FirstName = $"Имя {i + 1}",
                            LastName = $"Фамилия {i + 1}",
                            TelefonNumber = rand.Next(1000000, 2000000),
                            PassportNumber = rand.Next(2000000, 3000000),
                            changes = new ObservableCollection<Changes>(),
                        });
                    }
                },
                obj => currentUser != null));
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
                    Clients.Add(new ClientModel());
                },
                obj => isReadOnly == false && currentUser != null));
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
                    ClientModel client = obj as ClientModel;
                    Clients.Remove(client);
                },
                obj => (Clients.Count > 0 && isReadOnly == false && currentUser != null)
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
                    currentUser = null;
                    while (currentUser == null)
                    {
                        try
                        {
                            if (dialogAuthorization.OpenAuthorizationDialog() == true)
                            {
                                currentUser = dialogAuthorization.SelectedUser;
                            }
                        }
                        catch
                        {
                            dialogAuthorization.ShowMessage("Ошибка");
                        }

                        if (currentUser == null)
                            dialogAuthorization.ShowMessage("Нужно выбрать пользователя");
                    }
                    IsReadOnly = currentUser.IsReadOnly;
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
