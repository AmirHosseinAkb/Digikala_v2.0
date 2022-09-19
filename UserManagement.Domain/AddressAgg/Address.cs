using UserManagement.Domain.UserAgg;

namespace UserManagement.Domain.AddressAgg;

public class Address
{
    public long AddressId { get; private set; }
    public long UserId { get;private set; }
    public string State { get;private set; }
    public string City { get;private set; }
    public string NeighborHood { get;private set; }
    public string Number { get;private set; }
    public string PostCode { get;private set; }
    public bool IsDefault { get; private set; }
    public User User { get; set; }

}
