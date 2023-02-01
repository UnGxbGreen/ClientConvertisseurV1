// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

 
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Portable;
using Windows.Foundation;
using Windows.Foundation.Collections;
using WPFConvertisseurClient.Models;
using WPFConvertisseurClient.Services;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WPFConvertisseurClient.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ConvertisseurEuroPage : Page, INotifyPropertyChanged
    {
        public ConvertisseurEuroPage()
        {
            this.InitializeComponent();
            this.DataContext = this;
            GetDataOnLoadAsync();
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private ObservableCollection<Devise> devises;
       

        private double montantEnEuros;
        private Devise selectedDevise;
        private double montantEnDevise;

        WSService service = new WSService("https://localhost:7175/api/");




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
            WSService service = new WSService("https://localhost:7175/api/");
            List<Devise> result = await service.GetDevisesAsync("devises");
            if (result==null)
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

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            MontantEnDevise = MontantEnEuros*SelectedDevise.TauxDevise;
        }
    }
}
