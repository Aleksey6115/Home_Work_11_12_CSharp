using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.ComponentModel;

namespace Home_Work_11_CSharp_MVVM
{
    /// <summary>
    /// Класс представляющий Model
    /// </summary>
    class ClientModel : INotifyPropertyChanged
    {
        private string firstName; // Имя клиента
        private string lastName; // Фамилия клиента
        private int telefonNumber; // Телефонный номер клиента
        private int passportNumber; // Серия паспорта клиента

        #region Свойства
        /// <summary>
        /// Имя клиента
        /// </summary>
        /// 
        public string FirstName
        {
            get { return firstName; }
            set
            {
                firstName = value;

                if (PropertyChanged != null)
                    changes.Add(new Changes("Имя", ClientViewModel.currentUser.Name, value));

                OnPropertyChanged("FirstName");
            }
        }

        /// <summary>
        /// Фамилия клиента
        /// </summary>
        public string LastName
        {
            get { return lastName; }
            set
            {
                lastName = value;

                if (PropertyChanged != null)
                    changes.Add(new Changes("Фамилия", ClientViewModel.currentUser.Name, value));

                OnPropertyChanged("LastName");
            }
        }

        /// <summary>
        /// Телефонный номер клиента
        /// </summary>
        public int TelefonNumber
        {
            get { return telefonNumber; }
            set
            {
                telefonNumber = value;

                if (PropertyChanged != null)
                    changes.Add(new Changes("Телефонный номер", ClientViewModel.currentUser.Name, $"{value}")); 

                OnPropertyChanged("TelefonNumber");
            }
        }

        /// <summary>
        /// Серия паспорта клиента
        /// </summary>
        public int PassportNumber
        {
            get { return passportNumber; }
            set
            {
                passportNumber = value;

                if (PropertyChanged != null)
                    changes.Add(new Changes("Номер паспорта", ClientViewModel.currentUser.Name, $"{value}"));

                OnPropertyChanged("PassportNumber");
            }
        }

        /// <summary>
        /// Список изменений
        /// </summary>
        public ObservableCollection<Changes> changes { get; set; }
        #endregion

        public ClientModel()
        {
            firstName = "Имя";
            lastName = "Фамилия";
            telefonNumber = 1111111;
            passportNumber = 1111111;
            changes = new ObservableCollection<Changes>();
        }

        // Оповещение внешних клиентов об изменениях
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
