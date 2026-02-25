using System;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPagesLab.Pages.AddressBook
{
    public class DeleteModel : PageModel
    {
        private readonly IMediator _mediator;
        private readonly IRepo<AddressBookEntry> _repo;

        public DeleteModel(IRepo<AddressBookEntry> repo, IMediator mediator)
        {
            _repo = repo;
            _mediator = mediator;
        }

        [BindProperty]
        public DeleteAddressRequest DeleteAddressRequest { get; set; }

        public IActionResult OnGet(Guid id)
        {
            var spec = new EntryByIdSpecification(id);
            var entry = _repo.Find(spec).FirstOrDefault();
            if (entry == null)
            {
                return RedirectToPage("Index");
            }

            DeleteAddressRequest = new DeleteAddressRequest
            {
                Id = entry.Id
            };

            return Page();
        }

        public async Task<ActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                _ = await _mediator.Send(DeleteAddressRequest);
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}