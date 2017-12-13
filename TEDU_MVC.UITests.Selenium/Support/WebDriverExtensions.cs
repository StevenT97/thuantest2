using System;
using System.Configuration;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System.Threading;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;

namespace TEDU_MVC.UITests.Selenium.Support
{
    public static class WebDriverExtensions
    {
        public static string GetTextBoxValue(this IWebDriver browser, string field)
        {
            var control = GetFieldControl(browser, field);

            return control.GetAttribute("value");

        }

        public static void SetTextBoxValue(this IWebDriver browser, string field, string value)
        {
            var control = GetFieldControl(browser, field);
            var wait = new WebDriverWait(browser, Browsers.DefaultTimeout);
            if (!string.IsNullOrEmpty(control.GetAttribute("value")))
            {
                control.Clear();
                wait.Until(_ => string.IsNullOrEmpty(control.GetAttribute("value")));
            }
            
            control.SendKeys(value);
            //            wait.Until( _ => control.Value == value);
            Thread.Sleep(1000);
        }

        public static void SubmitForm(this IWebDriver browser, string formId = null)
        {
            var form = formId == null ? GetForm(browser) : browser.FindElements(By.Id(formId)).First();
            form.Submit();
            Thread.Sleep(1000);
        }

        // Điền text
        public static void FieldText(this IWebDriver browser, string id,string text)
        {
            browser.FindElement(By.Id(id)).SendKeys(text);
            Thread.Sleep(1000);
        }

        public static void ClickButton(this IWebDriver browser, string buttonId)
        {
            browser.FindElements(By.Id(buttonId)).First().Click();
            Thread.Sleep(1000);
        }

        public static void ClickLinkByHref(this IWebDriver browser, string bookId)
        {
            //Fetch all links by href
            List<IWebElement> anchors = new List<IWebElement>(browser.FindElements(By.TagName("a")));
            
            if (anchors.Count > 0)
            {
                int i = 0;
                while (i < anchors.Count())
                {
                    IWebElement anchor = anchors[i];

                    string hreflink = ((IWebElement)anchors).GetAttribute("href");
                    if (hreflink.Substring(hreflink.IndexOf("?")).Contains(bookId))
                    {
                        anchor.Click();
                        break;
                    }
                    i++;
                }
            }
            Thread.Sleep(1000);
        }

        private static IWebElement GetFieldControl(IWebDriver browser, string field)
        {
            var form = GetForm(browser);
            Thread.Sleep(1000);
            return form.FindElement(By.Id(field));

        }

        private static IWebElement GetForm(IWebDriver browser)
        {
            Thread.Sleep(1000);
            return browser.FindElements(By.TagName("form")).First();
        }

        public static void NavigateTo(this IWebDriver browser, string relativeUrl)
        {
      
           browser.Navigate().GoToUrl(new Uri(new Uri(ConfigurationManager.AppSettings["BaseUrl"]), relativeUrl));
            //browser.Navigate().GoToUrl(new Uri(new Uri("http://localhost:2489/"), relativeUrl));
           // browser.Navigate().GoToUrl("");
        }

        public static DropDown GetDropDown(this IWebDriver browser, string id)
        {
            Thread.Sleep(1000);
            return browser.FindElement(By.Id(id)).AsDropDown();
        }

        public static DropDown AsDropDown(this IWebElement e)
        {
            Thread.Sleep(1000);
            return new DropDown(e);
        }

        public class DropDown
        {
            private readonly IWebElement _dropDown;

            public DropDown(IWebElement dropDown)
            {
                this._dropDown = dropDown;

                if (dropDown.TagName != "select")
                    throw new ArgumentException("Invalid web element type");
            }

            public string SelectedValue
            {
                get
                {
                    var selectedOption = _dropDown.FindElements(By.TagName("option")).FirstOrDefault(e => e.Selected);

                    return selectedOption?.GetAttribute("value");

                }
                set
                {
                    new SelectElement(_dropDown).SelectByValue(value);
                }
            }
        }
    }
}
