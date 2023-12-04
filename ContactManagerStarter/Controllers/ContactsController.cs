using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactManager.Data;
using ContactManager.Hubs;
using ContactManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using MailKit;
using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;

namespace ContactManager.Controllers
{
    public class ContactsController : Controller
    {
        private readonly ApplicationContext _context;
        private readonly IHubContext<ContactHub> _hubContext;
        private readonly ILogger<ContactsController> _logger;

        public ContactsController(ApplicationContext context, IHubContext<ContactHub> hubContext, ILogger<ContactsController> logger)
        {
            _context = context;
            _hubContext = hubContext;
            _logger = logger;
        }

        [HttpPost]
        public IActionResult SetPrimaryEmail(string emailAddress)
        {
            try
            {

                var email = _context.EmailAddresses
                    // Including all the parents emailAddresses to insure only one can be primary
                    .Include(e => e.Contact.EmailAddresses)
                    .SingleOrDefault(e => e.Email == emailAddress);

                if (email != null)
                {
                    email.SetAsPrimary();

                    _context.SaveChanges();

                    return Ok(email);

                }

                return NotFound();
            }
            catch (Exception ex) {
                _logger.LogError(ex, "An error occurred in SetPrimaryEmail");
                return StatusCode(500, "Internal Server Error");
            }
        
        }


        public async Task<IActionResult> DeleteContact(Guid id)
        {
            try
            {

                var contactToDelete = await _context.Contacts
                    .Include(x => x.EmailAddresses)
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (contactToDelete == null)
                {
                    return BadRequest();
                }

                _context.EmailAddresses.RemoveRange(contactToDelete.EmailAddresses);
                _context.Contacts.Remove(contactToDelete);

                await _context.SaveChangesAsync();

                await _hubContext.Clients.All.SendAsync("Update");

                return Ok();

            }
            catch (Exception ex) {
                _logger.LogError(ex, "An error occurred in Delete Contact");
                return StatusCode(500, "Internal Server Error");
            }
        }

        public async Task<IActionResult> EditContact(Guid id)
        {
            try  
            { 
                var contact = await _context.Contacts
                    .Include(x => x.EmailAddresses)
                    .Include(x => x.Addresses)
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (contact == null)
                {
                    return NotFound();
                }

                var viewModel = new EditContactViewModel
                {
                    Id = contact.Id,
                    Title = contact.Title,
                    FirstName = contact.FirstName,
                    LastName = contact.LastName,
                    DOB = contact.DOB,
                    EmailAddresses = contact.EmailAddresses,
                    Addresses = contact.Addresses
                };

                return PartialView("_EditContact", viewModel);

                }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in EditContact");
                return StatusCode(500, "Internal Server Error");
            }

        }

        public async Task<IActionResult> GetContacts()
        {
            try
            {
                var contactList = await _context.Contacts
                    // Including EmailAddresses so  contact table view can display
                    .Include(c => c.EmailAddresses)
                    .OrderBy(x => x.FirstName)
                    .ToListAsync();

                return PartialView("_ContactTable", new ContactViewModel { Contacts = contactList });

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in GetContacts");
                return StatusCode(500, "Internal Server Error");
            }
        }

        public IActionResult Index()
            {
                return View();
            }

        public IActionResult NewContact()
        {
            return PartialView("_EditContact", new EditContactViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> SaveContact([FromBody]SaveContactViewModel model)
        {
            try { 
            
            var contact = model.ContactId == Guid.Empty
                ? new Contact { Title = model.Title, FirstName = model.FirstName, LastName = model.LastName, DOB = model.DOB }
                : await _context.Contacts.Include(x => x.EmailAddresses).Include(x => x.Addresses).FirstOrDefaultAsync(x => x.Id == model.ContactId);

            if (contact == null)
            {
                return NotFound();
            }

            _context.EmailAddresses.RemoveRange(contact.EmailAddresses);
            _context.Addresses.RemoveRange(contact.Addresses);


            foreach (var email in model.Emails)
            {
                contact.EmailAddresses.Add(new EmailAddress
                {
                    Type = email.Type,
                    Email = email.Email,
                    IsPrimaryDisplay = email.IsPrimaryDisplay,
                    Contact = contact
                });
            }

            foreach (var address in model.Addresses)
            {
                contact.Addresses.Add(new Address
                {
                    Street1 = address.Street1,
                    Street2 = address.Street2,
                    City = address.City,
                    State = address.State,
                    Zip = address.Zip,
                    Type = address.Type
                });
            }

            contact.Title = model.Title;
            contact.FirstName = model.FirstName;
            contact.LastName = model.LastName;
            contact.DOB = model.DOB;

            if (model.ContactId == Guid.Empty)
            {
                await _context.Contacts.AddAsync(contact);
            }
            else
            {
                _context.Contacts.Update(contact);
            }


            await _context.SaveChangesAsync();
            await _hubContext.Clients.All.SendAsync("Update");

            SendEmailNotification(contact.Id);

            return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in SaveContact");
                return StatusCode(500, "Internal Server Error");
            }

        }

        private void SendEmailNotification(Guid contactId)
        {
            try {
                var message = new MimeMessage();

                message.From.Add(new MailboxAddress("noreply", "noreply@contactmanager.com"));
                message.To.Add(new MailboxAddress("SysAdmin", "Admin@contactmanager.com"));
                message.Subject = "ContactManager System Alert";

                message.Body = new TextPart("plain")
                {
                    Text = "Contact with id:" + contactId.ToString() +" was updated"
                };

                using (var client = new SmtpClient())
                {
                    // For demo-purposes, accept all SSL certificates (in case the server supports STARTTLS)
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                    client.Connect("127.0.0.1", 25, false);

                    client.Send(message);
                    client.Disconnect(true);
                }
            
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in GetContacts");
            }


        }

    }

}