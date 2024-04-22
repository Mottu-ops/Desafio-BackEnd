using Motorent.Domain.Common.Entities;
using Motorent.Domain.Common.ValueObjects;
using Motorent.Domain.Renters.ValueObjects;
using Motorent.Domain.Users.ValueObjects;

namespace Motorent.Domain.Renters;

public sealed class Renter : Entity<RenterId>, IAggregateRoot
{
    // ReSharper disable UnusedMember.Local - Necessário para o EF Core
    private Renter() : base(default!)
    // ReSharper restore UnusedMember.Local
    {
    }

    internal Renter(RenterId id, UserId userId, Cnpj cnpj, Cnh cnh, Name name, Birthdate birthdate) : base(id)
    {
        UserId = userId;
        Cnpj = cnpj;
        Cnh = cnh;
        Name = name;
        Birthdate = birthdate;
    }

    public UserId UserId { get; private init; } = null!;

    public Cnpj Cnpj { get; private init; } = null!;

    public Cnh Cnh { get; private set; } = null!;

    public Name Name { get; private set; } = null!;

    public Birthdate Birthdate { get; private set; } = null!;
}