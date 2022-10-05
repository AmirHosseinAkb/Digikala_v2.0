using UserManagement.Domain.UserAgg;

namespace UserManagement.Domain.AddressAgg;

public class Address
{
    public long AddressId { get; private set; }
    public long UserId { get;private set; }
    public string ReceiverFirstName { get; private set; }
    public string ReceiverLastName { get; set; }
    public string ReceiverPhoneNumber { get; private set; }
    public string State { get;private set; }
    public string City { get;private set; }
    public string NeighborHood { get;private set; }
    public string Number { get;private set; }
    public string PostCode { get;private set; }
    public bool IsDefault { get; private set; }
    public bool IsDeleted { get;private set; }
    public User User { get;private set; }

    protected Address()
    {

    }

    public Address(long userId,string receiverFirst,string receiverLastName,string receiverPhoneNumber, string state, string city
        , string neighborHood, string number, string postCode, bool isDefault)
    {
        UserId = userId;
        ReceiverFirstName = receiverFirst;
        ReceiverLastName=receiverLastName;
        ReceiverPhoneNumber=receiverPhoneNumber;
        State = state;
        City = city;
        NeighborHood = neighborHood;
        Number = number;
        PostCode = postCode;
        IsDefault = isDefault;
    }

    public void Edit(string state,string receiverFirst,string receiverLastName,string receiverPhoneNumber, string city, string neighborHood
        , string number, string postCode)
    {
        ReceiverFirstName = receiverFirst;
        ReceiverLastName=receiverLastName;
        ReceiverPhoneNumber = receiverPhoneNumber;
        State = state;
        City = city;
        NeighborHood = neighborHood;
        Number = number;
        PostCode = postCode;
    }

    public void SetDefault()
    {
        IsDefault = true;
    }

    public void Undefault()
    {
        IsDefault = false;
    }

    public void Delete()
    {
        IsDeleted = true;
    }
}
