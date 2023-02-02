using ClientConvertisseurV2.Models;
using ClientConvertisseurV2.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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


     

        public ObservableCollection<Devise> Devises
        {
            get
            {
                return devises;
            }

            set
            {
                devises = value;
                OnPropertyChanged();

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
                OnPropertyChanged();
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
                OnPropertyChanged();
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
                OnPropertyChanged();
            }
        }

        public async void GetDataOnLoadAsync()
        {
            WSService service = new WSService("https://localhost:7175/api/");
            List<Devise> result = await service.GetDevisesAsync("devises");
            if (result == null)
            {
                ShowAsync("API non dispo");
            }
            else
            {
                Devises = new ObservableCollection<Devise>(result);
            }
        }

        public IRelayCommand BtnSetConversion { get; }
        public ConvertisseurEuroViewModel()
        {
            BtnSetConversion = new RelayCommand(ActionSetConversion);
            GetDataOnLoadAsync();

        }
        public void ActionSetConversion()
        {
            if (SelectedDevise == null)
            {
                ShowAsync("Sélectionner une devise !");

            }
            else
                MontantEnDevise = MontantEnEuros * SelectedDevise.TauxDevise;
        }

        private async void ShowAsync(string message)
        {
            ContentDialog errorDialog = new ContentDialog
            {
                Title = "ERROR",
                Content = message,
                CloseButtonText = "Oui Monsieur"
            };

            errorDialog.XamlRoot = App.MainRoot.XamlRoot;
            ContentDialogResult result = await errorDialog.ShowAsync();
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
