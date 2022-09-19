using UserManagement.Application.Contracts.Address;
using UserManagement.Domain.AddressAgg;

namespace UserManagement.Application;

public class AddressApplication:IAddressApplication
{
    private readonly IAddressRepository _addressRepository;

    public AddressApplication(IAddressRepository addressRepository)
    {
        _addressRepository = addressRepository;
    }

    public List<AddressViewModel> GetAddresses()
    {
        return _addressRepository.GetAll().Select(a => new AddressViewModel()
        {
            AddressId = a.AddressId,
            State = a.State,
            City = a.City,
            UserFullName = a.User.FirstName+" "+a.User.LastName,
            UserPhoneNumber = a.User.PhoneNumber!,
            PostCode = a.PostCode
        }).ToList();
    }
}