using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MediatR;

namespace RazorPagesLab.Pages.AddressBook;

public class DeleteAddressRequest
    : IRequest
{
    [Required]
    public Guid Id { get; set; }
}