using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Runtime.CompilerServices;

namespace SeleniumTests
{
    public class SeleniumTests
    {
        public IWebDriver driver;
        [SetUp]
        public void SetUp()
        {
            this.driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://simeontodorovv-1.simeontodorovv.repl.co");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }
        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
        [Test]
        public void NavigateToContactListAndAssertFirstName()
        {
            driver.FindElement(By.XPath("(//span[@class='icon'])[1]")).Click();   
            IWebElement tbody = driver.FindElement(By.TagName("tbody"));
            IWebElement FirstName = tbody.FindElements(By.TagName("td"))[0];
            IWebElement LastName = tbody.FindElements(By.TagName("td"))[1];
            Assert.That(FirstName.Text, Is.EqualTo("Steve"));
            Assert.That(LastName.Text, Is.EqualTo("Jobs"));
        }
        [Test]
        public void SearchContactByKeywordAlbert()
        {
            driver.FindElement(By.XPath("(//span[@class='icon'])[3]")).Click();
            driver.FindElement(By.Id("keyword")).SendKeys("albert");
            driver.FindElement(By.Id("search")).Click();
            IWebElement tbody = driver.FindElement(By.TagName("tbody"));
            IWebElement FirstName = tbody.FindElements(By.TagName("td"))[0];
            IWebElement LastName = tbody.FindElements(By.TagName("td"))[1];
            Assert.That(FirstName.Text, Is.EqualTo("Albert"));
            Assert.That(LastName.Text, Is.EqualTo("Einstein"));
        }
        [Test]
        public void SearchContactByRandomKeyword()
        {
            driver.FindElement(By.XPath("(//span[@class='icon'])[3]")).Click();
            driver.FindElement(By.Id("keyword")).SendKeys("random");
            driver.FindElement(By.Id("search")).Click();
            var msg = driver.FindElement(By.Id("SearchResult")).Text;
            Assert.That(msg, Is.EqualTo("No contacts found."));
        }
        [Test]
        public void CreateContactWithInvalidDetails()
        {
            driver.FindElement(By.XPath("(//span[@class='icon'])[2]")).Click();
            driver.FindElement(By.Id("email")).SendKeys("example@gmail.com");
            driver.FindElement(By.Id("phone")).SendKeys("1234");
            driver.FindElement(By.Id("comments")).SendKeys("Hello SoftUni");
            driver.FindElement(By.Id("create")).Click();
            var errorMsg = driver.FindElement(By.XPath("//div[@class='err']")).Text;
            Assert.That(errorMsg, Is.EqualTo("Error: First name cannot be empty!"));
        }
        [Test]
        public void CreateContactWithValidDetails()
        {
            driver.FindElement(By.XPath("(//span[@class='icon'])[2]")).Click();
            driver.FindElement(By.Id("firstName")).SendKeys("simeon");
            driver.FindElement(By.Id("lastName")).SendKeys("todorov");
            driver.FindElement(By.Id("email")).SendKeys("example@gmail.com");
            driver.FindElement(By.Id("phone")).SendKeys("1234");
            driver.FindElement(By.Id("comments")).SendKeys("Hello SoftUni");
            driver.FindElement(By.Id("create")).Click();
            IList<IWebElement> contactElements = driver.FindElements(By.CssSelector("#contact4 > tbody"));
            IWebElement lastContactElement = contactElements[contactElements.Count - 1];

            IWebElement lastContactNameElement = lastContactElement.FindElement(By.CssSelector("#contact4 > tbody > tr.fname"));
            IWebElement secondContactNameElement = lastContactElement.FindElement(By.CssSelector("#contact4 > tbody > tr.lname"));
            Assert.That("First Name simeon", Is.EqualTo(lastContactNameElement.Text));
            Assert.That("Last Name todorov", Is.EqualTo(secondContactNameElement.Text));

        }
    }
}