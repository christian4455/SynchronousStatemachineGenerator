using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using StateGen.StateGenSync.Types;
using StateGen.StateGenSync.Utils;

using StateGen.Utils.Logger;

namespace StateGen.StateGenSync.Utils
{
    public class RepositoryHandler : IRepositoryHandler
    {
        private StateGenSync.Types.StateMachineData m_Data = new StateGenSync.Types.StateMachineData();
        private EA.Repository m_DiagramOfInterest = null;

        public RepositoryHandler()
        {
            /* Intentionally left blank */
        }

        StateGenSync.Types.StateMachineData IRepositoryHandler.HandleRepository(EA.Repository activityDiagram)
        {
            m_Data = new StateGenSync.Types.StateMachineData();

            EA.Package package = activityDiagram.GetTreeSelectedPackage();
            m_DiagramOfInterest = activityDiagram;

            //MessageBox.Show(package.Name);

            foreach (EA.Element e in package.Elements)
            {
                ElementType type = EnumUtil.ParseEnum<ElementType>(e.Type, ElementType.Unknown);
               // MessageBox.Show(e.Type.ToString());
                Log.Info(e.Type.ToString());
                HandleType(type, e);
            }

            m_Data.GetTransitionTable().PrintTable();

            CleanUpTransitionTable();

            DeleteMergeNodeRows();
            Log.Info("cleaned");
            m_Data.GetTransitionTable().PrintTable();

            m_Data.GetTransitionTable().GetRows().Sort(new TableComparer());

            Log.Info("sorted");
            m_Data.GetTransitionTable().PrintTable();

            FillActionsAndGuards();

            FetchEnumActivities();

            return m_Data;
        }

        private void HandleType(ElementType type, EA.Element element)
        {
            Log.Info("type=" + type.ToString());
            //MessageBox.Show("type=" + type.ToString() + " typeOfElement=" + element.Type.ToString() + " name=" + element.Name.ToString());
            switch (type)
            {
                case ElementType.Activity:
                {
                    HandleActivity(element);
                    break;
                }
                case ElementType.StateNode:
                {
                    HandleStateNode(element);
                    break;
                }
                case ElementType.Decision:
                {
                    HandleDecision(element);
                    break;
                }
            }
        }

        private void HandleDecision(EA.Element element)
        {
            foreach (EA.Connector c in element.Connectors)
            {
                Int32 supplierID = c.SupplierID;
                EA.Element nextActivity = m_DiagramOfInterest.GetElementByID(supplierID);

                ElementType type = EnumUtil.ParseEnum<ElementType>(nextActivity.Type, ElementType.Unknown);
                if (type == ElementType.MergeNode)
                {
                    Int32 clientID = c.ClientID;
                    EA.Element currentActivity = m_DiagramOfInterest.GetElementByID(clientID);

                    string transitionguard = c.TransitionGuard.ToString();
                    Int32 connectorID = c.ConnectorID;

                    Activity finalCurrentActivity = new Activity(currentActivity.Name.ToString(), EnumUtil.ParseEnum<ElementType>(currentActivity.Type, ElementType.Unknown), currentActivity.ElementID);
                    Activity finalNextActivity = new Activity(nextActivity.Name.ToString(), EnumUtil.ParseEnum<ElementType>(nextActivity.Type, ElementType.Unknown), nextActivity.ElementID);

                    Row row = new Row(finalCurrentActivity, "", nextActivity.Name.ToString(), finalNextActivity, transitionguard, connectorID);

                    // check if row already exists
                    if (!m_Data.Containes(row))
                    {
                        m_Data.AddRow(row);
                    }
                }
                else if(type == ElementType.Decision)
                {
                    Int32 clientID = c.ClientID;
                    EA.Element currentActivity = m_DiagramOfInterest.GetElementByID(clientID);

                    string transitionguard = c.TransitionGuard.ToString();
                    Int32 connectorID = c.ConnectorID;

                    Activity finalCurrentActivity = new Activity(currentActivity.Name.ToString(), EnumUtil.ParseEnum<ElementType>(currentActivity.Type, ElementType.Unknown), currentActivity.ElementID);
                    Activity finalNextActivity = new Activity(nextActivity.Name.ToString(), EnumUtil.ParseEnum<ElementType>(nextActivity.Type, ElementType.Unknown), nextActivity.ElementID);

                    Row row = new Row(finalCurrentActivity, "", nextActivity.Name.ToString(), finalNextActivity, transitionguard, connectorID);

                    // check if row already exists
                    if (!m_Data.Containes(row))
                    {
                        m_Data.AddRow(row);
                    }
                }
            }
        }

        private void HandleActivity(EA.Element element)
        {
            //MessageBox.Show("element=" + element.Name);

            foreach (EA.Connector c in element.Connectors)
            {
               // MessageBox.Show("connectorID=" + c.ConnectorID.ToString());
                Log.Info("connectorID=" + c.ConnectorID.ToString());

                Int32 clientID = c.ClientID;
                EA.Element currentActivity = m_DiagramOfInterest.GetElementByID(clientID);

                Int32 supplierID = c.SupplierID;
                EA.Element nextActivity = m_DiagramOfInterest.GetElementByID(supplierID);

                string transitionguard = c.TransitionGuard.ToString();
                Int32 connectorID = c.ConnectorID;

                
                Activity finalCurrentActivity = new Activity(currentActivity.Name.ToString(), EnumUtil.ParseEnum<ElementType>(currentActivity.Type, ElementType.Unknown), currentActivity.ElementID);
                Activity finalNextActivity = new Activity(nextActivity.Name.ToString(), EnumUtil.ParseEnum<ElementType>(nextActivity.Type, ElementType.Unknown), nextActivity.ElementID);

                Row row = new Row(finalCurrentActivity, "", nextActivity.Name.ToString(), finalNextActivity, transitionguard, connectorID);

                // check if row already exists
                if (!m_Data.Containes(row))
                {
                    m_Data.AddRow(row);
                }
            }
        }

        private void HandleStateNode(EA.Element stateNode)
        {
            foreach (EA.Connector c in stateNode.Connectors)
            {
                // MessageBox.Show("connectorID=" + c.ConnectorID.ToString());
                Log.Info("connectorID=" + c.ConnectorID.ToString());

                Int32 clientID = c.ClientID;
                EA.Element currentActivity = m_DiagramOfInterest.GetElementByID(clientID);

                Int32 supplierID = c.SupplierID;
                EA.Element nextActivity = m_DiagramOfInterest.GetElementByID(supplierID);

                string transitionguard = c.TransitionGuard.ToString();
                Int32 connectorID = c.ConnectorID;


                Activity finalCurrentActivity = new Activity(currentActivity.Name.ToString(), EnumUtil.ParseEnum<ElementType>(currentActivity.Type, ElementType.Unknown), currentActivity.ElementID);
                Activity finalNextActivity = new Activity(nextActivity.Name.ToString(), EnumUtil.ParseEnum<ElementType>(nextActivity.Type, ElementType.Unknown), nextActivity.ElementID);

                Row row = new Row(finalCurrentActivity, "", nextActivity.Name.ToString(), finalNextActivity, transitionguard, connectorID);

                // check if row already exists
                if (!m_Data.Containes(row))
                {
                    m_Data.AddRow(row);
                }
            }
        }

        private void CleanUpTransitionTable()
        {
            foreach (Row roi in m_Data.GetTransitionTable().GetRows())
            {
                if ((roi.GetNextActivity().GetElementType() != ElementType.Activity) && (roi.GetNextActivity().GetElementType() != ElementType.Decision))
                {
                    Log.Info("dirtyActivity=" + roi.GetNextActivity().GetName() + " elementType=" + roi.GetNextActivity().GetElementType());
                    EA.Connector dirtyConnector = m_DiagramOfInterest.GetConnectorByID(roi.GetID());
                    EA.Element dirtyActivity = m_DiagramOfInterest.GetElementByID(dirtyConnector.SupplierID);

                    Activity correctNextActivity = new Activity();
                    bool isCleanedUp = false;

                    foreach (EA.Connector c in dirtyActivity.Connectors)
                    {
                        EA.Element possibleActivity = m_DiagramOfInterest.GetElementByID(c.SupplierID);

                        if (possibleActivity.Name == "ActivityFinal")
                        {
                            correctNextActivity = new Activity(possibleActivity.Name, EnumUtil.ParseEnum<ElementType>(possibleActivity.Type, ElementType.Unknown), possibleActivity.ElementID);
                            isCleanedUp = true;
                        }
                        else
                        {
                            foreach (Row r in m_Data.GetTransitionTable().GetRows())
                            {
                                if (possibleActivity.Name == r.GetCurrentActivity().GetName() && (r.GetCurrentActivity().GetElementType() ==  ElementType.StateNode || r.GetCurrentActivity().GetElementType() == ElementType.Activity))
                                {
                                    correctNextActivity = new Activity(possibleActivity.Name, EnumUtil.ParseEnum<ElementType>(possibleActivity.Type, ElementType.Unknown), possibleActivity.ElementID);
                                    isCleanedUp = true;
                                }
                            }
                        }
                    }

                    if (isCleanedUp)
                    {
                        roi.SetNextActivity(correctNextActivity);
                        roi.SetAction(correctNextActivity.GetName());
                    }
                    else
                    {
                        m_Data.GetTransitionTable().GetRows().Remove(roi);
                    }
                }
            }
        }

        private void DeleteMergeNodeRows()
        {
            for (int i = 0; i < m_Data.GetTransitionTable().GetRows().Count; i++)
            {
                if (m_Data.GetTransitionTable().GetRows()[i].GetCurrentActivity().GetElementType() == ElementType.MergeNode)
                {
                    m_Data.GetTransitionTable().GetRows().RemoveAt(i);
                    i--;
                }
            }
        }

        private void FillActionsAndGuards()
        {
            foreach (Row r in m_Data.GetTransitionTable().GetRows())
            {
                Method guard = new Method(r.GetGuard(), "bool");
                m_Data.AddGuard(guard);

                if (r.GetNextActivity().GetElementType() == ElementType.Decision)
                {
                    Method pseudoAction = new Method(r.GetAction(), "void");

                    bool match = false;
                    foreach (Method p in m_Data.GetPseudoActions())
                    {
                        if (p.GetFunctionName() == pseudoAction.GetFunctionName())
                        {
                            match = true;
                        }
                    }

                    if (!match)
                    {
                        m_Data.AddPseudoAction(pseudoAction);
                    }
                }
                else
                {
                    Method action = new Method(r.GetAction(), "void");

                    bool match = false;

                    foreach (Method p in m_Data.GetActions())
                    {
                        if (p.GetFunctionName() == action.GetFunctionName())
                        {
                            match = true;
                        }
                    }

                    if (!match)
                    {
                        m_Data.AddAction(action);
                    }
                }
            }
        }

        private void FetchEnumActivities()
        {
            foreach (Row r in m_Data.GetTransitionTable().GetRows())
            {
                if (!m_Data.GetEnumActivities().Contains(r.GetCurrentActivity().GetName()))
                { 
                    m_Data.GetEnumActivities().Add(r.GetCurrentActivity().GetName());
                }

                if (!m_Data.GetEnumActivities().Contains(r.GetNextActivity().GetName()))
                {
                    m_Data.GetEnumActivities().Add(r.GetNextActivity().GetName());
                }
            }
        }
    }
}
