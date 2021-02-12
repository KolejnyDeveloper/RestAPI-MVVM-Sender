using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using rest02.Model;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows;
using System.Diagnostics;

namespace rest02.ModelWidoku
{
    class Client1 : INotifyPropertyChanged
    {
        public KlasaMVVM klasa = new KlasaMVVM();

        public event PropertyChangedEventHandler PropertyChanged;

        public string Typ
        {
            get
            {
                return klasa.Typ;
            }
            set
            {
                klasa.Typ = value;
                onPropertyChanged(nameof(Typ));
            }
        }

        public string Pliki
        {
            get
            {
                return klasa.Pliki;
            }
            set
            {
                klasa.Pliki = value;
                onPropertyChanged(nameof(Pliki));
            }
        }

        public string Lokacja
        {
            get
            {
                return klasa.Lokacja;
            }
            set
            {
                klasa.Lokacja = value;
                onPropertyChanged(nameof(Lokacja));
            }
        }
        public string Logi
        {
            get
            {
                return klasa.Logi;
            }
            set
            {
                klasa.Lokacja = value;
                onPropertyChanged(nameof(Logi));
            }
        }
        public string Mail
        {
            get
            {
                return klasa.Mail;
            }
            set
            {
                klasa.Mail = value;
                onPropertyChanged(nameof(Mail));
            }
        }
        public string[] Files
        {
            get
            {
                return klasa.Files;
            }
            set
            {
                klasa.Files = value;
                onPropertyChanged(nameof(Files));
            }
        }

        private ICommand send = null;

        public ICommand Send
        {
            get
            {
                if (send == null)
                {
                    send = new RelayCommand((object o) =>
                    {
                        klasa.Send();
                        onPropertyChanged(nameof(Mail));
                        onPropertyChanged(nameof(Typ));
                        onPropertyChanged(nameof(Lokacja));
                        onPropertyChanged(nameof(Pliki));
                        onPropertyChanged(nameof(Logi));
                    },
                    (object o) =>
                    {
                        return Mail != string.Empty;
                    });
                }
                return send;
            }
        }

        private ICommand drop = null;

        public ICommand Drop
        {
            get
            {
                if (drop == null)
                {
                    drop = new RelayCommand((object o) =>
                    {
                        klasa.przygotowanie();
                        onPropertyChanged(nameof(Mail));
                        onPropertyChanged(nameof(Typ));
                        onPropertyChanged(nameof(Lokacja));
                        onPropertyChanged(nameof(Pliki));
                        onPropertyChanged(nameof(Logi));
                    },
                    (object o) =>
                    {
                        return Mail != string.Empty;
                    });
                }
                return drop;
            }
        }







        private void onPropertyChanged(string nazwa)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(nazwa));
            }
        }


        public void post(string[] file)
        {
            klasa.Files = file;
            klasa.przygotowanie();
            onPropertyChanged(nameof(Pliki));
            onPropertyChanged(nameof(Files));
            onPropertyChanged(nameof(Logi));
        }

    }

    
}
