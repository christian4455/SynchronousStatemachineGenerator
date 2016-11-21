using System;
using System.Windows.Forms;

namespace StateGen
{
    public class main
    {
        // define menu constants
       
        const string menuHeader = "-&State machine generator";
        const string generateSyncStatemachine = "&Generate synchronous state machine";

        private PluginManager.PluginManager m_PluginManager = new PluginManager.PluginManager();
        private StateGenSync.Types.StateMachineData m_StateMachineData = new StateGenSync.Types.StateMachineData();
        private StateGenSync.Utils.RepositoryHandler m_RepositoryHandler = new StateGenSync.Utils.RepositoryHandler();
        private StateGenSync.Utils.InterfaceBuilder m_InterfaceBuilder = new StateGenSync.Utils.InterfaceBuilder();
        private StateGenSync.StatemachineGeneratorSync m_StatemachineGeneratorSync = new StateGenSync.StatemachineGeneratorSync();
        private StateGenSync.Utils.FileWriter m_FileWriter = new StateGenSync.Utils.FileWriter();
        private StateGenSync.Utils.FsmDataBuilder m_FsmDataBuilder = new StateGenSync.Utils.FsmDataBuilder();
        private StateGenSync.Utils.EnumBuilder m_EnumBuilder = new StateGenSync.Utils.EnumBuilder();
        private StateGenSync.Utils.ClassHeaderBuilder m_ClassHeaderBuilder = new StateGenSync.Utils.ClassHeaderBuilder();
        private StateGenSync.Utils.ClassImplBuilder m_ClassImplBuilder = new StateGenSync.Utils.ClassImplBuilder();
        private StateGenSync.Utils.FsmHandlerHeaderBuilder m_FsmHandlerHeaderBuilder = new StateGenSync.Utils.FsmHandlerHeaderBuilder();
        private StateGenSync.Utils.FsmHandlerImplBuilder m_FsmHandlerImplBuilder = new StateGenSync.Utils.FsmHandlerImplBuilder();
        private StateGenSync.Utils.TransitionRowBuilder m_TransitionRowBuilder = new StateGenSync.Utils.TransitionRowBuilder();

        public void SetupStatemachineGeneratorSync()
        {
            m_StatemachineGeneratorSync.SetRepositoryHandler(m_RepositoryHandler);
            m_StatemachineGeneratorSync.SetInterfaceBuilder(m_InterfaceBuilder);
            m_StatemachineGeneratorSync.SetFileWriter(m_FileWriter);
            m_StatemachineGeneratorSync.SetFsmDataBuilder(m_FsmDataBuilder);
            m_StatemachineGeneratorSync.SetEnumBuilder(m_EnumBuilder);
            m_StatemachineGeneratorSync.SetClassHeaderBuilder(m_ClassHeaderBuilder);
            m_StatemachineGeneratorSync.SetClassImplBuilder(m_ClassImplBuilder);
            m_StatemachineGeneratorSync.SetFsmHandlerHeaderBuilder(m_FsmHandlerHeaderBuilder);
            m_StatemachineGeneratorSync.SetFsmHandlerImplBuilder(m_FsmHandlerImplBuilder);
            m_StatemachineGeneratorSync.SetTransitonRowBuilder(m_TransitionRowBuilder);
            // register the plugin
            m_PluginManager.RegisterPlugin(m_StatemachineGeneratorSync, PluginManager.Types.PluginType.Enum.StatemachineGeneratorSync);
        }

        ///
        /// Called Before EA starts to check Add-In Exists
        /// Nothing is done here.
        /// This operation needs to exists for the addin to work
        ///
        /// <param name="Repository" />the EA repository
        /// a string
        public String EA_Connect(EA.Repository Repository)
        {
            SetupStatemachineGeneratorSync();
            return "a string";
        }

        ///
        /// Called when user Clicks Add-Ins Menu item from within EA.
        /// Populates the Menu with our desired selections.
        /// Location can be "TreeView" "MainMenu" or "Diagram".
        ///
        /// <param name="Repository" />the repository
        /// <param name="Location" />the location of the menu
        /// <param name="MenuName" />the name of the menu
        ///
        public object EA_GetMenuItems(EA.Repository Repository, string Location, string MenuName)
        {

            switch (MenuName)
            {
                // defines the top level menu option
                case "":
                    return menuHeader;
                // defines the submenu options
                case menuHeader:
                    string[] subMenus = { generateSyncStatemachine};
                    return subMenus;
            }

            return "";
        }

        ///
        /// returns true if a project is currently opened
        ///
        /// <param name="Repository" />the repository
        /// true if a project is opened in EA
        bool IsProjectOpen(EA.Repository Repository)
        {
            try
            {
                EA.Collection c = Repository.Models;
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
        public void EA_GetMenuState(EA.Repository Repository, string Location, string MenuName, string ItemName, ref bool IsEnabled, ref bool IsChecked)
        {
            if (IsProjectOpen(Repository))
            {
                switch (ItemName)
                {
                    // define the state of the hello menu option
                    case generateSyncStatemachine:
                        IsEnabled = true;
                        break;
                    // there shouldn't be any other, but just in case disable it.
                    default:
                        IsEnabled = false;
                        break;
                }
            }
            else
            {
                // If no open project, disable all menu options
                IsEnabled = false;
            }
        }

        ///
        /// Called when user makes a selection in the menu.
        /// This is your main exit point to the rest of your Add-in
        ///
        /// <param name="Repository" />the repository
        /// <param name="Location" />the location of the menu
        /// <param name="MenuName" />the name of the menu
        /// <param name="ItemName" />the name of the selected menu item
        public void EA_MenuClick(EA.Repository Repository, string Location, string MenuName, string ItemName)
        {
            switch (ItemName)
            {
                // user has clicked the generateSyncStateMachine menu option
                case generateSyncStatemachine:
                    m_PluginManager.ProcessRepository(Repository, PluginManager.Types.PluginType.Enum.StatemachineGeneratorSync);
                    break;
            }
        }

        ///
        /// EA calls this operation when it exists. Can be used to do some cleanup work.
        ///
        public void EA_Disconnect()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }
}