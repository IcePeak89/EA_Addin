using System;
using System.Windows;
using System.Windows.Annotations;

using EA;

using FunctionalAllocator.ViewModels;
using FunctionalAllocator.Views;

namespace FunctionalAllocator
{
    public class FunctionalAllocator
    {
        public event EventHandler<string> OnElementSelected;
        ///
        /// Called Before EA starts to check Add-In Exists
        /// Nothing is done here.
        /// This operation needs to exists for the addin to work
        ///
        /// <param name="Repository" />the EA repository
        /// a string
        public string EA_Connect(Repository Repository) => string.Empty;
        
        const string _menuHeader = "-&Test Add-in";
        const string _showPopUp = "&TestPopUp";
        const string _showTestWindow = "&TestWindow";
        const string _showTestTab = "&TestTab";

        ///
        /// Called when user Clicks Add-Ins Menu item from within EA.
        /// Populates the Menu with our desired selections.
        /// Location can be "TreeView" "MainMenu" or "Diagram".
        ///
        /// <param name="Repository" />the repository
        /// <param name="Location" />the location of the menu
        /// <param name="MenuName" />the name of the menu
        ///
        public object EA_GetMenuItems(Repository Repository, string Location, string MenuName)
        {
            switch (MenuName)
            {
                // defines the top level menu option
                case "":
                    return _menuHeader;
                // defines the submenu options
                case _menuHeader:
                    string[] subMenus = { _showPopUp, _showTestWindow, _showTestTab };
                    return subMenus;
            }

            return "";
        }

        ///
        /// returns true if a project is currently opened
        ///
        /// <param name="Repository" />the repository
        /// true if a project is opened in EA
        bool IsProjectOpen(Repository Repository)
        {
            try
            {
                var c = Repository.Models;
                return true;
            }
            catch
            {
                return false;
            }
        }

        ///
        /// Called once Menu has been opened to see what menu items should active.
        ///
        /// <param name="Repository" />the repository
        /// <param name="Location" />the location of the menu
        /// <param name="MenuName" />the name of the menu
        /// <param name="ItemName" />the name of the menu item
        /// <param name="IsEnabled" />boolean indicating whethe the menu item is enabled
        /// <param name="IsChecked" />boolean indicating whether the menu is checked
        public void EA_GetMenuState(Repository Repository, string Location, string MenuName, string ItemName, ref bool IsEnabled, ref bool IsChecked)
        {
            IsEnabled = IsProjectOpen(Repository);
        }

        ///
        /// Called when user makes a selection in the menu.
        /// This is your main exit point to the rest of your Add-in
        ///
        /// <param name="repository" />the repository
        /// <param name="Location" />the location of the menu
        /// <param name="MenuName" />the name of the menu
        /// <param name="ItemName" />the name of the selected menu item
        public void EA_MenuClick(Repository repository, string Location, string MenuName, string ItemName)
        {
            switch (ItemName)
            {
                // user has clicked the menuOpen menu option
                case _showPopUp:
                    ShowTestPopUpWindow(repository);
                    break;
                case _showTestWindow:
                    ShowTestWindow(repository);
                    break;
                case _showTestTab:
                    ShowTestTab(repository);
                    break;
            }

            ObjectType ff = repository.GetTreeSelectedItemType();
            
        }

        ///
        /// EA calls this operation when it exists. Can be used to do some cleanup work.
        ///
        public void EA_Disconnect()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        TestView _mainWindow;
        private TestViewModel _testModel;


        private void ShowTestPopUpWindow(Repository repository)
        {
            if (_testModel == null) _testModel = new TestViewModel(repository);

            if (!_testModel.IsWindowOpen)
            {
                var eaSelectedItemType = repository.GetTreeSelectedItemType();

                _mainWindow = new TestView(_testModel);
                _mainWindow.Show();

                _testModel.IsWindowOpen = true;
            }
            else
            {
                MessageBox.Show("Test Window is already opened!");
            }
        }

        private void ShowTestWindow(Repository repository)
        {
            var testModel = new TestViewModel(repository);
            var testUserControl = repository.AddWindow("TestWindow", "FunctionalAllocator.Views.TestUserControl") as TestUserControl;
            if(testUserControl!=null) testUserControl.DataContext = testModel;

            repository.ShowAddinWindow("TestWindow");
        }


        private void ShowTestTab(Repository repository)
        {
            var testModel = new TestViewModel(repository);
            var testUserControl = repository.AddTab("TestTabWindow", "FunctionalAllocator.Views.TestUserControl") as TestUserControl;
            if (testUserControl != null) testUserControl.DataContext = testModel;
        }

        #region EA

        public void EA_OnContextItemChanged(EA.Repository Repository, string GUID, EA.ObjectType ot)
        {

        }

        void EA_OnOutputItemDoubleClicked(Repository repository, string tabName , string lineText,long id)
        {

        }

        void EA_GetContextObject()
        {

        }

        void EA_OnOutputItemClicked(Repository repository, string tab, string LineText, long id)
        {

        }

        void EA_OnNotifyContextItemModified(Repository repository, string guid, ObjectType objectType)
        {

        }

        void EA_OnContextItemDoubleClicked(Repository repository, string guid, ObjectType objectType)
        {

        }

        void EA_Disconnect(Repository repository, string guid, ObjectType objectType)
        {

        }
        #endregion
    }
}
