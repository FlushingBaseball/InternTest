using System;
using System.Collections.Generic;
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

            if (oldPrimaryEmail != null && oldPrimaryEmail.Email != this.Email)
            {
                oldPrimaryEmail.IsPrimaryDisplay = false;
            }

            this.IsPrimaryDisplay = true;

        }
    }
}
