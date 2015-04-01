using SeleniumTests;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Itim.Web.UI.Tester
{
	/* Adding comment */
	/* Adding additional comment */
    class Program
    {
        static void Main(string[] args)
        {
            string errorMessage;
            XDocument xDoc = XDocument.Load("users.xml");
            int randomNumber;

            List<User> users = xDoc.Descendants("user").Select(
                    u => new User
                    {
                        Username = u.Attribute("username").Value,
                        Password = u.Attribute("password").Value,
                        OrganisationSelector = u.Attribute("organisationSelector").Value,
                        OrganisationName = u.Attribute("organisationName").Value
                    }).ToList();

            Random randomSeed = new Random();
            LogonNHTAdminWebdriver runner = new LogonNHTAdminWebdriver();

            do
            {
                randomNumber = randomSeed.Next(users.Count);
                Console.WriteLine(string.Format("Running test user: {0}", users[randomNumber].ToString()));
                if (runner.PerformLogin(users[randomNumber].OrganisationSelector, users[randomNumber].Username, users[randomNumber].Password, users[randomNumber].OrganisationName, out errorMessage))
                {
                    Console.WriteLine(string.Format("Test success: {0}", users[randomNumber].ToString()));

                }
                else
                {
                    Console.WriteLine("Test failed '{0}' with '{1}'", users[randomNumber].ToString(), errorMessage);
                }

            } while (!Console.KeyAvailable);

        }
    }
}
