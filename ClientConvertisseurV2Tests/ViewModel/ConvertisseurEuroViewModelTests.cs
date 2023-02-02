using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClientConvertisseurV2.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientConvertisseurV2.Services;
using System.Collections.ObjectModel;
using ClientConvertisseurV2.Models;

namespace ClientConvertisseurV2.ViewModel.Tests
{
    [TestClass()]
    public class ConvertisseurEuroViewModelTests
    {
        [TestMethod()]
        public void ConvertisseurEuroViewModelTest()
        {
            ConvertisseurEuroViewModel convertisseurEuro = new ConvertisseurEuroViewModel();
            Assert.IsNotNull(convertisseurEuro);

        }

        /// <summary>
        /// Test GetDataOnLoadAsyncTest OK
        /// </summary> 
        public ObservableCollection<Devise> devises;
        [TestMethod()]
        public void GetDataOnLoadAsyncTest_OK()
        {
            WSService service = new WSService("https://localhost:7175/api/");
            //Arrange
            ConvertisseurEuroViewModel convertisseurEuro = new ConvertisseurEuroViewModel();
            //Act
            var result = service.GetDevisesAsync("devises").Result;
            convertisseurEuro.GetDataOnLoadAsync();
            devises = new ObservableCollection<Devise>(result);
            Thread.Sleep(1000);
            //Assert
            Assert.IsNotNull(convertisseurEuro.Devises);
            CollectionAssert.AreEqual(result.ToList(),devises, "Les listes ne sont pas les mêmes");
        }


        /// <summary>
        /// Test conversion OK
        /// </summary>
        [TestMethod()]
        public void ActionSetConversionTest()
        {
            //Arrange
            ConvertisseurEuroViewModel convertisseurEuro = new ConvertisseurEuroViewModel();

            //Création d'un objet de type ConvertisseurEuroViewModel 
            //Property MontantEuro = 100 (par exemple)
            convertisseurEuro.MontantEnEuros = 100;
            //Création d'un objet Devise, dont Taux=1.07
            Devise devise = new Devise(4, "yallah", 1.07);
            //Property DeviseSelectionnee = objet Devise instancié
            convertisseurEuro.SelectedDevise= devise;
            //Act
            //Appel de la méthode ActionSetConversion
            convertisseurEuro.ActionSetConversion();
            //Assert
            //Assertion : MontantDevise est égal à la valeur espérée 107
            Assert.AreEqual(107, convertisseurEuro.MontantEnDevise);
        }

      /*  [TestMethod()]
        public void GetDataOnLoadAsyncTest_NonOK()
        {
            WSService service = new WSService("https://localhost:7175/api/");
            //Arrange
            ConvertisseurEuroViewModel convertisseurEuro = new ConvertisseurEuroViewModel();
            //Act
            var result = service.GetDevisesAsync("devises").Result;
            convertisseurEuro.GetDataOnLoadAsync();
            devises = new ObservableCollection<Devise>(result);
            Thread.Sleep(1000);
            //Assert
            Assert.IsNull(convertisseurEuro.Devises);
            CollectionAssert.AreEqual(result.ToList(), devises, "Les listes ne sont pas les mêmes");
        }*/
    }
}