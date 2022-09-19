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
}