using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Input;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UITest.Common.UIMap;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UITest.Extension;
using Keyboard = Microsoft.VisualStudio.TestTools.UITesting.Keyboard;


namespace Kask.UIAutomationTests
{
    /// <summary>
    /// Summary description for KaskUIAutomation
    /// </summary>
    [CodedUITest]
    public class KaskUIAutomation
    {
        public KaskUIAutomation()
        {
        }

        [ClassInitialize]
        public static void OpenApplication(TestContext context)
        {
            Playback.Initialize();
            BrowserWindow browser = BrowserWindow.Launch(new Uri("http://localhost:51309"));
            browser.CloseOnPlaybackCleanup = false;
            var ui = new UIMap();
        }


        [TestMethod]
        public void ApplicantApplysForAJob()
        {

            this.UIMap.ViewJobOpenning();
            this.UIMap.ApplyForAChosenJob();
            this.UIMap.PersonalPage();
            this.UIMap.PositionPage();
            this.UIMap.EmployementHistory1();
            this.UIMap.EmployementHistory2();
            this.UIMap.EmployementHistory3();
            this.UIMap.EducationPage1();
            this.UIMap.EducationPage2();
            this.UIMap.EducationPage3();
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
