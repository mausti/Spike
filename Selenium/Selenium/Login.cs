using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;

namespace SeleniumTests
{
    [TestFixture]
    public class LogonNHTAdminWebdriver
    {
        private string baseURL = "https://172.21.10.15/";
        private IWebDriver driver;
        private StringBuilder verificationErrors;
        private bool acceptNextAlert = true;

        [SetUp]
        public void SetupTest()
        {
            driver = new FirefoxDriver();
            verificationErrors = new StringBuilder();
        }

        [TearDown]
        public void TeardownTest()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
            Assert.AreEqual("", verificationErrors.ToString());
        }

        [Test]
        public void TheLogonNHTAdminWebdriverTest()
        {
            string error;
            PerformLogin("new", "nht_admin", "DemoMLS1", "Newcastle upon Tyne Hosp NHST", out error);
        }

        public bool PerformLogin(string orgStartingCharacters, string username, string password, string orgName, out string errorMessage)
        {
            errorMessage = string.Empty;
            try
            {
                using (driver = new FirefoxDriver())
                //using (driver = new InternetExplorerDriver())
                {
 
                    driver.Navigate().GoToUrl(baseURL + "/Login");
                    //driver.FindElement(By.Id("overridelink")).Click();
                    
                    if(string.Compare("Optimall Pathology Services - Login", driver.Title, StringComparison.InvariantCultureIgnoreCase)!= 0)
                    {
                        errorMessage = string.Format("Expecting page title '{0}' but received '{1}'", "Optimall Pathology Services - Login", driver.Title);
                        return false;

                    }

                    

                    for (var i = 0; i < orgStartingCharacters.Length; i++)
                    {
                        driver.FindElement(By.Id("ctlLogin_ctlOrg_Input")).SendKeys(orgStartingCharacters[i].ToString());
                    }

                    driver.FindElement(By.Id("ctlLogin_ctlOrg_Input")).SendKeys(Keys.Tab);
                    driver.FindElement(By.Id("ctlLogin_UserName")).Clear();
                    driver.FindElement(By.Id("ctlLogin_UserName")).SendKeys(username);
                    driver.FindElement(By.Id("ctlLogin_Password")).Clear();
                    driver.FindElement(By.Id("ctlLogin_Password")).SendKeys(password);
                    driver.FindElement(By.Id("ctlLogin_LoginButton")).Click();
                    if (string.Compare("Optimall Pathology Services - Home", driver.Title, StringComparison.InvariantCultureIgnoreCase) != 0)
                    {
                        errorMessage = string.Format("Expecting page title '{0}' but received '{1}'", "Optimall Pathology Services - Home", driver.Title);
                        return false;
                    }

                    driver.Navigate().GoToUrl(baseURL + "/Acc/ManageUsers");
                    Assert.AreEqual("Optimall Pathology Services - Maintain Users", driver.Title);
                    if (string.Compare("Optimall Pathology Services - Maintain Users", driver.Title, StringComparison.InvariantCultureIgnoreCase) != 0)
                    {
                        errorMessage = string.Format("Expecting page title '{0}' but received '{1}'", "Optimall Pathology Services - Maintain Users", driver.Title);
                        return false;
                    }

                    string orgNameFound = driver.FindElement(By.Id("ctl00_cp_txtOrganisation")).GetAttribute("value");

                    if (string.Compare(orgName, orgNameFound, StringComparison.InvariantCultureIgnoreCase) != 0)
                    {
                        errorMessage = string.Format("Expecting organisation '{0}' but found '{1}'", orgName, orgNameFound);
                        return false;
                    }
                }

                return true;
            }
            catch(Exception e)
            {
                errorMessage = e.Message;
                return false;
            }

        }

        private bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        private bool IsAlertPresent()
        {
            try
            {
                driver.SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException)
            {
                return false;
            }
        }

        private string CloseAlertAndGetItsText()
        {
            try
            {
                IAlert alert = driver.SwitchTo().Alert();
                string alertText = alert.Text;
                if (acceptNextAlert)
                {
                    alert.Accept();
                }
                else
                {
                    alert.Dismiss();
                }
                return alertText;
            }
            finally
            {
                acceptNextAlert = true;
            }
        }
    }
}
