using OpenQA.Selenium;

namespace TEDU_MVC.UITests.Selenium.Support
{
    public abstract class SeleniumStepsBase
    {
// ReSharper disable InconsistentNaming
        protected IWebDriver Browser
// ReSharper restore InconsistentNaming
        {
            get { return SeleniumController.Instance.Browser; }
        }
    }
}