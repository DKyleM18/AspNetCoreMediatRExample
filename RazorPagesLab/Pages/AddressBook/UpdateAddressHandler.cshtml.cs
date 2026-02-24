using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace RazorPagesLab.Pages.AddressBook;

public class UpdateAddressHandler : IRequestHandler<UpdateAddressRequest>
{
    private readonly IRepo<AddressBookEntry> _repo;

    public UpdateAddressHandler(IRepo<AddressBookEntry> repo)
    {
        _repo = repo;
    }

    public Task<Unit> Handle(UpdateAddressRequest request, CancellationToken cancellationToken)
    {
        var spec = new EntryByIdSpecification(request.Id);
        var entry = _repo.Find(spec).FirstOrDefault();
        if (entry == null)
        {
            return Task.FromResult(Unit.Value);
        }

        entry.Update(
            request.Line1,
            request.Line2,
            request.City,
            request.State,
            request.PostalCode
        );

        _repo.Update(entry);

        return Task.FromResult(Unit.Value);
    }
}
