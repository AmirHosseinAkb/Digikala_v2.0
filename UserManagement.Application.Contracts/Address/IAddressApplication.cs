using _01_Framework.Application;

namespace UserManagement.Application.Contracts.Address;

public interface IAddressApplication
{
    List<AddressViewModel> GetUserAddresses();
    OperationResult Create(CreateAddressCommand command);
    OperationResult SetDefaultAddress(long addressId);
    EditAddressCommand GetAddressForEdit(long addressId);
    OperationResult Edit(EditAddressCommand command);
    OperationResult Delete(long addressId);

}