using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Lync.Model;
using LyncAvailabilityPublisher;

namespace LyncIndicator
{
    class LyncStatusWatcher
    {
        private LyncClient lyncClient;
        private ILyncAvailabilityPublisher lyncAvailabilityPublisher;

        public LyncStatusWatcher(ILyncAvailabilityPublisher publisher)
        {
            this.lyncAvailabilityPublisher = publisher;
            this.lyncClient = LyncClient.GetClient();
            lyncClient.Self.Contact.ContactInformationChanged +=
                new EventHandler<ContactInformationChangedEventArgs>(SelfContact_ContactInformationChanged);
        }

        #region Lync Event Handlers
        /// <summary>
        /// Handler for the ContactInformationChanged event of the contact. Used to update the contact's information in the user interface.
        /// </summary>
        private void SelfContact_ContactInformationChanged(object sender, ContactInformationChangedEventArgs e)
        {
            //Only update the contact information in the user interface if the client is signed in.
            //Ignore other states including transitions (e.g. signing in or out).
            if (lyncClient.State == ClientState.SignedIn)
            {
                //Get from Lync only the contact information that changed.
                if (e.ChangedContactInformation.Contains(ContactInformationType.Availability))
                {
                    this.UpdateAvailability();
                }
            }
        }

        /// <summary>
        /// Gets the contact's current availability value from Lync and updates the corresponding elements in the user interface
        /// </summary>
        private void UpdateAvailability()
        {
            //Get the current availability value from Lync
            ContactAvailability currentAvailability = currentAvailability = 
                (ContactAvailability)lyncClient.Self.Contact.GetContactInformation(ContactInformationType.Availability);
            this.lyncAvailabilityPublisher.Send(currentAvailability.ToString());
        }

        # endregion
        
    }
}
