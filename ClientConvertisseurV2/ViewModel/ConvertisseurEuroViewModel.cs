using ClientConvertisseurV2.Models;
using ClientConvertisseurV2.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientConvertisseurV2.ViewModel
{
    public class ConvertisseurEuroViewModel: ObservableObject
    {

        public event PropertyChangedEventHandler PropertyChanged;
        private ObservableCollection<Devise> devises;


        private double montantEnEuros;
        private Devise selectedDevise;
        private double montantEnDevise;

        WSService service = new WSService("https://localhost:7175/api/");

        public ConvertisseurEuroViewModel()
        {

        }


        public ObservableCollection<Devise> Devises
        {
            get
            {
                return devises;
            }

            set
            {
                devises = value;
                OnPropertyChanged("Devises");

            }
        }

        public double MontantEnEuros
        {
            get
            {
                return montantEnEuros;
            }

            set
            {
                montantEnEuros = value;
                OnPropertyChanged("MontantEnEuros");
            }
        }

        public Devise SelectedDevise
        {
            get
            {
                return selectedDevise;
            }

            set
            {
                selectedDevise = value;
                OnPropertyChanged("SelectedDevise");
            }
        }

        public double MontantEnDevise
        {
            get
            {
                return montantEnDevise;
            }

            set
            {
                montantEnDevise = value;
                OnPropertyChanged("MontantEnDevise");
            }
        }

        private async void GetDataOnLoadAsync()
        {
            
            List<Devise> result = await service.GetDevisesAsync("devises");
            if (result == null)
            {
                
            }
            else
            {
                Devises = new ObservableCollection<Devise>(result);
            }
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

      

    }
}
