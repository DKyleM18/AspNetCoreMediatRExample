using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace RazorPagesLab.Pages.AddressBook;

public class DeleteAddressHandler : IRequestHandler<DeleteAddressRequest>
{
    private readonly IRepo<AddressBookEntry> _repo;

    public DeleteAddressHandler(IRepo<AddressBookEntry> repo)
    {
        _repo = repo;
    }

    public Task<Unit> Handle(DeleteAddressRequest request, CancellationToken cancellationToken)
    {
        var spec = new EntryByIdSpecification(request.Id);
        var entry = _repo.Find(spec).FirstOrDefault();
        if (entry == null)
        {
            return Task.FromResult(Unit.Value);
        }

        _repo.Remove(entry);

        return Task.FromResult(Unit.Value);
    }
}