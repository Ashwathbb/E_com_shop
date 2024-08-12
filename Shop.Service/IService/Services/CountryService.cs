using Shop.DataAccess.DTOs;
using Shop.Repository.IRepositories;

namespace Shop.Service.IService.Services
{
    public class CountryService : ICountryService
    {
        private readonly ICountryRepository _countryRepository;

        public CountryService(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public IEnumerable<CountryDto> GetAllCountries()
        {
            return _countryRepository.GetAllCountries();
        }

        public CountryDto GetCountryById(int id)
        {
            return _countryRepository.GetCountryById(id);
        }

        public void CreateCountry(CountryDto countryDto)
        {
            _countryRepository.AddCountry(countryDto);
        }

        public void DeleteCountry(int id)
        {
            _countryRepository.DeleteCountry(id);
        }
        public void UpdateCountry(CountryDto countryDto)
        {
            _countryRepository.UpdateCountry(countryDto);
        }
    }
}
