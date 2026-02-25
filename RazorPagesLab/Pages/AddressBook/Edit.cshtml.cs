using System;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPagesLab.Pages.AddressBook;

public class EditModel : PageModel
{
	private readonly IMediator _mediator;
	private readonly IRepo<AddressBookEntry> _repo;

	public EditModel(IRepo<AddressBookEntry> repo, IMediator mediator)
	{
		_repo = repo;
		_mediator = mediator;
	}

	[BindProperty]
	public UpdateAddressRequest UpdateAddressRequest { get; set; }

	public IActionResult OnGet(Guid id)
	{
		// Todo: Use repo to get address book entry, set UpdateAddressRequest fields.

		var spec = new EntryByIdSpecification(id);
		var entry = _repo.Find(spec).FirstOrDefault();
		if (entry == null)
		{
			return RedirectToPage("Index");
		}

		UpdateAddressRequest = new UpdateAddressRequest();
		{
			UpdateAddressRequest.Id = entry.Id;
			UpdateAddressRequest.Line1 = entry.Line1;
			UpdateAddressRequest.Line2 = entry.Line2;
			UpdateAddressRequest.City = entry.City;
			UpdateAddressRequest.State = entry.State;
			UpdateAddressRequest.PostalCode = entry.PostalCode;
		}
		
		return Page();
	}

	public async Task<ActionResult> OnPost()
	{
		// Todo: Use mediator to send a "command" to update the address book entry, redirect to entry list.
		if (ModelState.IsValid)
		{
			_ = await _mediator.Send(UpdateAddressRequest);
			return RedirectToPage("Index");
		}
		return Page();
	}
}