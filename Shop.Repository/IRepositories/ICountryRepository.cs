using Shop.DataAccess.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Repository.IRepositories
{
    public interface ICountryRepository
    {
        IEnumerable<CountryDto> GetAllCountries();
        CountryDto GetCountryById(int id);
        void AddCountry(CountryDto entity);
        void UpdateCountry(CountryDto entity);
        void DeleteCountry(int id);
    }
}
