﻿using _01_Framework.Application;
using UserManagement.Application.Contracts.Address;
using UserManagement.Domain.AddressAgg;

namespace UserManagement.Application;

public class AddressApplication : IAddressApplication
{
    private readonly IAddressRepository _addressRepository;
    private readonly IAuthenticationHelper _authenticationHelper;
    public AddressApplication(IAddressRepository addressRepository, IAuthenticationHelper authenticationHelper)
    {
        _addressRepository = addressRepository;
        _authenticationHelper = authenticationHelper;
    }

    public List<AddressViewModel> GetUserAddresses()
    {
        return _addressRepository.GetUserAddresses(_authenticationHelper.GetCurrentUserId()).Select(a => new AddressViewModel()
        {
            AddressId = a.AddressId,
            State = a.State,
            City = a.City,
            UserFullName = a.User.FirstName + " " + a.User.LastName,
            UserPhoneNumber = a.User.PhoneNumber!,
            PostCode = a.PostCode,
            IsDefault = a.IsDefault
        }).ToList();
    }

    public OperationResult Create(CreateAddressCommand command)
    {
        var result = new OperationResult();
        if (_addressRepository.IsExistAddress(_authenticationHelper.GetCurrentUserId(), command.PostCode))
            return result.Failed(ApplicationMessages.DuplicatedAddress);
        bool isDefault = _addressRepository.GetUserAddresses(_authenticationHelper.GetCurrentUserId()).Count == 1;
        var address = new Address(_authenticationHelper.GetCurrentUserId(), command.State, command.City,
            command.NeighborHood, command.Number, command.PostCode, isDefault);
        _addressRepository.Add(address);
        return result.Succeeded();

    }

    public OperationResult SetDefaultAddress(long addressId)
    {
        var result = new OperationResult();
        var address = _addressRepository.GetUserAddress(addressId, _authenticationHelper.GetCurrentUserId());
        var defaultAddress = _addressRepository.GetUserDefaultAddress(_authenticationHelper.GetCurrentUserId());
        if (address==null)
            return result.Failed(ApplicationMessages.ProcessFailed);
        defaultAddress.Undefault();
        address.SetDefault();
        _addressRepository.SaveChanges();
        return result.Succeeded();
    }

    public EditAddressCommand GetAddressForEdit(long addressId)
    {
        var address = _addressRepository.GetAddressById(addressId);
        return new EditAddressCommand()
        {
            AddressId = address.AddressId,
            State = address.State,
            City = address.City,
            NeighborHood = address.NeighborHood,
            Number = address.Number,
            PostCode = address.PostCode
        };
    }

    public OperationResult Edit(EditAddressCommand command)
    {
        var result = new OperationResult();
        var address=_addressRepository.GetUserAddress(command.AddressId,_authenticationHelper.GetCurrentUserId());
        if (address == null)
            return result.Failed(ApplicationMessages.RecordNotFound);
        address.Edit(command.State,command.City,command.NeighborHood,command.Number,command.PostCode);
        _addressRepository.SaveChanges();
        return result.Succeeded();
    }

    public void Delete(long addressId)
    {
        var address = _addressRepository.GetUserAddress(addressId, _authenticationHelper.GetCurrentUserId());
        if (address != null)
        {
            address.Delete();
            _addressRepository.SaveChanges();
        }
    }
}