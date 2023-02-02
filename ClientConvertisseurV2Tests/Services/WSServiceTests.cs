using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClientConvertisseurV2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientConvertisseurV2.ViewModel;
using System.Collections.ObjectModel;
using ClientConvertisseurV2.Models;
using System.Web.Mvc;

namespace ClientConvertisseurV2.Services.Tests
{
    [TestClass()]
    public class WSServiceTests
    {
        [TestMethod()]
        public void WSServiceTest()
        {
            WSService sv = new WSService("https://localhost:7175/api/");
            Assert.IsNotNull(sv);
        }

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
            CollectionAssert.AreEqual(result.ToList(), devises, "Les listes ne sont pas les mêmes");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Task<List<Devise>>>)
        }
    }
}