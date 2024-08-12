using Shop.DataAccess.DTOs;

namespace Shop.Service.IService
{
    public interface ICountryService
    {
        IEnumerable<CountryDto> GetAllCountries();
        CountryDto GetCountryById(int id);
        void CreateCountry(CountryDto countryDto);
        void UpdateCountry(CountryDto countryDto);
        void DeleteCountry(int id);
    }
}
