using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itim.Web.UI.Tester
{
    public class User
    {
        public string OrganisationSelector { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string OrganisationName { get; set; }

        public override string ToString()
        {
            return string.Format("Organisation : '{0}' | Organisation Selector: '{1}' | Username: '{2}' | Password: '{3}'", OrganisationName, OrganisationSelector, Username, Password);
        }
    }
}
