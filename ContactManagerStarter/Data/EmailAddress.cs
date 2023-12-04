using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ContactManager.Data
{
    public class EmailAddress : Entity
    {
        public string Email { get; set; }
        public EmailType Type { get; set; }

        public bool IsPrimaryDisplay { get; set; }
        public virtual Contact Contact { get; set; }

        public void SetAsPrimary()
        {
            var oldPrimaryEmail = Contact.EmailAddresses.SingleOrDefault(e => e.IsPrimaryDisplay);

            Debug.WriteLine($"Old Primary Email: {oldPrimaryEmail?.Email ?? "None"}");

            if (oldPrimaryEmail != null && oldPrimaryEmail.Email != this.Email)
            {
                Debug.WriteLine($"SetAsPrimary: Old primary email found for {Contact.FirstName}");
                oldPrimaryEmail.IsPrimaryDisplay = false;
            }

            this.IsPrimaryDisplay = true;

        }
    }
}
