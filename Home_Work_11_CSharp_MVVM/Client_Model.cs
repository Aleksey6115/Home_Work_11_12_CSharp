﻿using System;
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
    class Client_Model : INotifyPropertyChanged
    {
        private string first_name; // Имя клиента
        private string last_name; // Фамилия клиента
        private int telefon_number; // Телефонный номер клиента
        private int passport_number; // Серия паспорта клиента

        #region Свойства
        /// <summary>
        /// Имя клиента
        /// </summary>
        /// 
        public string First_name
        {
            get { return first_name; }
            set
            {
                first_name = value;
                //changes.Add(new Changes("Имя", Client_ViewModel.current_user.Name, value));
                OnPropertyChanged("First_name");
            }
        }

        /// <summary>
        /// Фамилия клиента
        /// </summary>
        public string Last_name
        {
            get { return last_name; }
            set
            {
                last_name = value;
                //changes.Add(new Changes("Фамилия", Client_ViewModel.current_user.Name, value));
                OnPropertyChanged("Last_name");
            }
        }

        /// <summary>
        /// Телефонный номер клиента
        /// </summary>
        public int Telefon_number
        {
            get { return telefon_number; }
            set
            {
                telefon_number = value;
                //changes.Add(new Changes("Телефонный номер", Client_ViewModel.current_user.Name, $"{value}")); 
                OnPropertyChanged("Telefon_number");
            }
        }

        /// <summary>
        /// Серия паспорта клиента
        /// </summary>
        public int Passport_number
        {
            get { return passport_number; }
            set
            {
                passport_number = value;
                //changes.Add(new Changes("Номер паспорта", Client_ViewModel.current_user.Name, $"{value}"));
                OnPropertyChanged("Passport_number");
            }
        }

        /// <summary>
        /// Список изменений
        /// </summary>
        public ObservableCollection<Changes> changes { get; set; }
        #endregion

        public Client_Model()
        {
            first_name = "Имя";
            last_name = "Фамилия";
            telefon_number = 1111111;
            passport_number = 1111111;
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
