using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XamarinRestAPI.Models;

namespace XamarinRestAPI.Services
{
    public class CountryService : RestClient
    {
        protected const string FLAG_SERVICE = "http://www.geognos.com/api/en/countries/flag";

        //instancia o construtor baase passando a url da api desejada
        public CountryService() : base("https://restcountries.eu/rest/v1/all") { }

        public Task<IEnumerable<Country>> GetCountries()
        {
            return GetAsJson<Country>();
        }

        public Uri GetFlagSource(string alpha2Code)
        {
            return new Uri(FromUrl(FLAG_SERVICE, alpha2Code + ".png"));
        }
    }
}
