using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Protection.PlayReady;
using WPFConvertisseurClient.Models;

namespace WPFConvertisseurClient.Services
{
    public class WSService: IService
    {
        private HttpClient client;
        public WSService(String url)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

        }


        public async Task<List<Devise>> GetDevisesAsync(string nomControleur)
        {
            try
            {
                return await client.GetFromJsonAsync<List<Devise>>(nomControleur);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public double CalculMontantDevise(double montant, double taux)
        {
            if (montant < 0)
            {
                throw new ArgumentException("Impossible de calculer un montant inférieur a 0 !");
            }

            return Math.Round(montant * taux, 2);
        }
    }
}
