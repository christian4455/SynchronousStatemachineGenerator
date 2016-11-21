using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using StateGen.PluginManager;

using StateGen.Utils.Logger;

namespace StateGen.StateGenSync
{
    class StatemachineGeneratorSync : IPlugin
    {
        private string FILENAME_FSMDATA = "FsmData.hpp";
        private string FILENAME_IACTIONHANDLER = "IActionHandler.hpp";
        private string FILENAME_ICONDITIONHANDLER = "IConditionHandler.hpp";
        private string FILENAME_ACTIONHANDLER = "ActionHandler.hpp";
        private string FILENAME_CONDITIONHANDLER = "ConditionHandler.hpp";
        private string FILENAME_ACTIONHANDLERIMPL = "ActionHandler.cpp";
        private string FILENAME_CONDITIONHANDLERIMPL = "ConditionHandler.cpp";
        private string FILENAME_EVENTS = "Events.hpp";
        private string FILENAME_ACTIVITIES = "Activity.hpp";
        private string FILENAME_FSMHANDLERHEADER = "FsmHandler.hpp";
        private string FILENAME_FSMHANDLERIMPL = "FsmHandler.cpp";
        private string DEFAULT_TARGETPATH = "D:\\";

        StateGenSync.Utils.IRepositoryHandler m_IRepositoryHandler = null;
        StateGenSync.Utils.IInterfaceBuilder m_IInterfaceBuilder = null;
        StateGenSync.Utils.IFileWriter m_IFileWriter = null;
        StateGenSync.Utils.IFsmDataBuilder m_IFsmDataBuilder = null;
        StateGenSync.Utils.IEnumBuilder m_IEnumBuilder = null;
        StateGenSync.Utils.IClassBuilder m_IClassHeaderBuilder = null;
        StateGenSync.Utils.IClassBuilder m_IClassImplBuilder = null;
        StateGenSync.Utils.IFsmHandlerHeaderBuilder m_IFsmHandlerHeaderBuilder = null;
        StateGenSync.Utils.IFsmHandlerImplBuilder m_IFsmHandlerImplBuilder = null;


        public StatemachineGeneratorSync()
        {
            /* Intentionally left blank */
        }

        public void SetRepositoryHandler(StateGenSync.Utils.IRepositoryHandler iRepositoryHandler)
        {
            m_IRepositoryHandler = iRepositoryHandler;
        }

        public void SetInterfaceBuilder(StateGenSync.Utils.IInterfaceBuilder iInterfaceBuilder)
        {
            m_IInterfaceBuilder = iInterfaceBuilder;
        }

        public void SetFileWriter(StateGenSync.Utils.IFileWriter iFileWriter)
        {
            m_IFileWriter = iFileWriter;
        }

        public void SetFsmDataBuilder(StateGenSync.Utils.IFsmDataBuilder iFsmDataBuilder)
        {
            m_IFsmDataBuilder = iFsmDataBuilder;
        }

        public void SetEnumBuilder(StateGenSync.Utils.IEnumBuilder iEnumBuilder)
        {
            m_IEnumBuilder = iEnumBuilder;
        }

        public void SetClassHeaderBuilder(StateGenSync.Utils.IClassBuilder iClassHeaderBuilder)
        {
            m_IClassHeaderBuilder = iClassHeaderBuilder;
        }

        public void SetClassImplBuilder(StateGenSync.Utils.IClassBuilder iClassImplBuilder)
        {
            m_IClassImplBuilder = iClassImplBuilder;
        }

        public void SetFsmHandlerHeaderBuilder(StateGenSync.Utils.IFsmHandlerHeaderBuilder iFsmHandlerHeaderBuilder)
        {
            m_IFsmHandlerHeaderBuilder = iFsmHandlerHeaderBuilder;
        }

        public void SetFsmHandlerImplBuilder(StateGenSync.Utils.IFsmHandlerImplBuilder iFsmHandlerImplBuilder)
        {
            m_IFsmHandlerImplBuilder = iFsmHandlerImplBuilder;
        }

        void IPlugin.ProcessRepository(EA.Repository repository)
        {
            Log.Info("");

            StateGenSync.Types.StateMachineData data = m_IRepositoryHandler.HandleRepository(repository);

            StateGenSync.Types.Product iActionHandler = m_IInterfaceBuilder.CreateProduct(data.GetActions(), FILENAME_IACTIONHANDLER);

            StateGenSync.Types.Product iConditionHandler = m_IInterfaceBuilder.CreateProduct(data.GetGuards(), FILENAME_ICONDITIONHANDLER);

            StateGenSync.Types.Product iFsm = m_IInterfaceBuilder.CreateProduct(data.GetFsm().GetMethods(), data.GetFsm().GetInferfaceName() + ".hpp");

            StateGenSync.Types.Product fsmData = m_IFsmDataBuilder.CreateProduct(data, FILENAME_FSMDATA);

            StateGenSync.Types.Product events = m_IEnumBuilder.CreateProduct(data.GetEnumEvents(), FILENAME_EVENTS);

            StateGenSync.Types.Product activities = m_IEnumBuilder.CreateProduct(data.GetEnumActivities(), FILENAME_ACTIVITIES);

            StateGenSync.Types.Product conditionHandlerHeader = m_IClassHeaderBuilder.CreateProduct(data.GetGuards(), FILENAME_CONDITIONHANDLER);

            StateGenSync.Types.Product actionHandlerHeader = m_IClassHeaderBuilder.CreateProduct(data.GetActions(), FILENAME_ACTIONHANDLER);

            StateGenSync.Types.Product conditionHandlerImpl = m_IClassImplBuilder.CreateProduct(data.GetGuards(), FILENAME_CONDITIONHANDLERIMPL);

            StateGenSync.Types.Product actionHandlerImpl = m_IClassImplBuilder.CreateProduct(data.GetActions(), FILENAME_ACTIONHANDLERIMPL);

            StateGenSync.Types.Product fsmHandlerHeader = m_IFsmHandlerHeaderBuilder.CreateProduct(data.GetFsm().GetMethods(), FILENAME_FSMHANDLERHEADER);

            StateGenSync.Types.Product fsmHandlerImpl = m_IFsmHandlerImplBuilder.CreateProduct(FILENAME_FSMHANDLERIMPL);

            m_IFileWriter.WriteProduct(DEFAULT_TARGETPATH, iActionHandler);
            m_IFileWriter.WriteProduct(DEFAULT_TARGETPATH, iConditionHandler);
            m_IFileWriter.WriteProduct(DEFAULT_TARGETPATH, iFsm);
            m_IFileWriter.WriteProduct(DEFAULT_TARGETPATH, fsmData);
            m_IFileWriter.WriteProduct(DEFAULT_TARGETPATH, events);
            m_IFileWriter.WriteProduct(DEFAULT_TARGETPATH, activities);
            m_IFileWriter.WriteProduct(DEFAULT_TARGETPATH, conditionHandlerHeader);
            m_IFileWriter.WriteProduct(DEFAULT_TARGETPATH, actionHandlerHeader);
            m_IFileWriter.WriteProduct(DEFAULT_TARGETPATH, conditionHandlerImpl);
            m_IFileWriter.WriteProduct(DEFAULT_TARGETPATH, actionHandlerImpl);
            m_IFileWriter.WriteProduct(DEFAULT_TARGETPATH, fsmHandlerHeader);
            m_IFileWriter.WriteProduct(DEFAULT_TARGETPATH, fsmHandlerImpl);
        }
    }
}
