using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace Podstawy_widoków_testy;

public class Tests
{
    private IWebDriver _web = null!;
    [SetUp]
    public void Setup()
    {
        _web = new FirefoxDriver();
        _web.Url = "http://localhost:5134/";
    }

    [TearDown]
    public void Shutdown()
    {
        _web.Close();
        _web.Quit();
    }

    [Test]
    public void FrontPage()
    {
        _web.FindElement(By.Id("login")).Click();
        _web.FindElement(By.Id("Input_Email")).SendKeys("user@localhost");
        _web.FindElement(By.Id("Input_Password")).SendKeys("User!1");
        _web.FindElement(By.Id("login-submit")).Click();
        _web.FindElement(By.LinkText("Lista rowerów")).Click();
        _web.FindElement(By.CssSelector("main>div>a:nth-child(2)")).Click();
        _web.FindElement(By.LinkText("Zarezerwuj")).Click();
        var date = DateTime.Now.AddDays(-3);
        _web.FindElement(By.Name("Begin")).SendKeys(Keys.Tab);
        _web.FindElement(By.Name("Begin")).SendKeys($"{date.Year}-{date.Month:D2}-{date.Day:D2}");
        date = DateTime.Now.AddDays(3);
        _web.FindElement(By.Name("End")).SendKeys($"{date.Year}-{date.Month:D2}-{date.Day:D2}");
        _web.FindElement(By.CssSelector("input[type=submit]")).Click();
        _web.FindElement(By.LinkText("Rezerwacje")).Click();
        var test = _web.FindElement(By.CssSelector("main div:nth-child(3)"));
        Assert.That(test, Is.Not.Null);
    }
}