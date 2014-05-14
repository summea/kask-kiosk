using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Input;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UITest.Extension;
using Keyboard = Microsoft.VisualStudio.TestTools.UITesting.Keyboard;


namespace Kask.UIAutomationTests
{
    /// <summary>
    /// Summary description for UIApplyTest
    /// </summary>
    [CodedUITest]
    public class ApplyForJob
    {
        public ApplyForJob()
        {
        }
        
        [ClassInitialize]
        public static void OpenApplication(TestContext context)
        {
            Playback.Initialize();
            BrowserWindow browser = BrowserWindow.Launch(new Uri("http://localhost:51309/App/Create?JobOpeningIDReferenceNumber=1"));
            //BrowserWindow browser = BrowserWindow.Launch(new Uri("http://localhost:51309"));
            browser.CloseOnPlaybackCleanup = false;
            var ui = new UIMap();
            ui.ApplyAndNavigateToWelcomeScreen();
            
        }


        [TestMethod]
        public void FilleUpPersonalPageTest()
        {
            
            this.UIMap.FilleUpPersonalPage();
            this.UIMap.FilleUpPositionPage();
            this.UIMap.EmployementHistroy1();
            this.UIMap.FilleUpEmploymentHistory2Page();
            this.UIMap.FillUpEmploymentHistory3Page();
            this.UIMap.FillUpEducation1Page();
            this.UIMap.FillUpEducation2Page();
            this.UIMap.FillUpEducation3Page();
            this.UIMap.SubmitApplication();

        }

    
        #region Additional test attributes

        // You can use the following additional attributes as you write your tests:

        ////Use TestInitialize to run code before running each test 
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{        
        //    // To generate code for this test, select "Generate Code for Coded UI Test" from the shortcut menu and select one of the menu items.
        //    // For more information on generated code, see http://go.microsoft.com/fwlink/?LinkId=179463
        //}

        ////Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{        
        //    // To generate code for this test, select "Generate Code for Coded UI Test" from the shortcut menu and select one of the menu items.
        //    // For more information on generated code, see http://go.microsoft.com/fwlink/?LinkId=179463
        //}

        #endregion

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }
        private TestContext testContextInstance;

        public UIMap UIMap
        {
            get
            {
                if ((this.map == null))
                {
                    this.map = new UIMap();
                }

                return this.map;
            }
        }

        private UIMap map;
    }
}
