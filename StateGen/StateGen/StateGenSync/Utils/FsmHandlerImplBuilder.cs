using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using StateGen.StateGenSync.Types;

namespace StateGen.StateGenSync.Utils
{
    public class FsmHandlerImplBuilder : IFsmHandlerImplBuilder
    {
        public Product CreateProduct(string filename)
        {
            Product product = new Product();

            product.SetFilename(filename);

            product.Append(CreateClass(filename));

            return product;
        }

        private string CreateClass(string filename)
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine("#include \"" + ConvertToHPP(filename) + "\"");
            result.AppendLine("");
            result.AppendLine("#include <cstdio>");
            result.AppendLine("");
            result.AppendLine("#include \"Events.hpp\"");
            result.AppendLine("");
            result.AppendLine(ConvertToClassname(filename) + "::" + ConvertToClassname(filename) + "(FsmData & fsmData) :");
            result.AppendLine("    m_FsmData(fsmData),");
            result.AppendLine("    m_CurrentActivity(::Activity::ActivityInital),");
            result.AppendLine("    m_NumActivity(TRANS_COUNT),");
            result.AppendLine("    m_pTransitionTable(trans)");
            result.AppendLine("{");
            result.AppendLine("    /* Intentionally left blank */");
            result.AppendLine("}");
            result.AppendLine("");
            result.AppendLine(ConvertToClassname(filename) + "::~" + ConvertToClassname(filename) + "()");
            result.AppendLine("{");
            result.AppendLine("    /* Intentionally left blank */");
            result.AppendLine("}");
            result.AppendLine("");
            result.AppendLine("//******************************************************");
            result.AppendLine("// This function starts the state machine. ");
            result.AppendLine("//******************************************************");
            result.AppendLine("void " + ConvertToClassname(filename) + "::Start()");
            result.AppendLine("{");
            result.AppendLine("    // Todo decleare init activity in a special member");
            result.AppendLine("    RunStateMachine(this, ::Activity::ActivityInital);");
            result.AppendLine("}");
            result.AppendLine("");
            result.AppendLine("//******************************************************");
            result.AppendLine("// This function determines what the next state is. ");
            result.AppendLine("//******************************************************");
            result.AppendLine("int " + ConvertToClassname(filename) + "::GetNextEvent(FsmHandler * fsm)");
            result.AppendLine("{");
            result.AppendLine("    // We have a synchronous state machine, so we fire some Event.");
            result.AppendLine("    return ::Events::Any;");
            result.AppendLine("}");
            result.AppendLine("");
            result.AppendLine("//******************************************************");
            result.AppendLine("// Run the state machine while there is something to be read.");
            result.AppendLine("//******************************************************");
            result.AppendLine("void " + ConvertToClassname(filename) + "::RunStateMachine(" + ConvertToClassname(filename) + "* fsm, const Activity::Enum initActivity)");
            result.AppendLine("{");
            result.AppendLine("    FsmData & fsmData = fsm->m_FsmData;");
            result.AppendLine("");
            result.AppendLine("    // set the init state");
            result.AppendLine("    fsm->m_CurrentActivity = initActivity;");
            result.AppendLine("");
            result.AppendLine("    // cycle through the state machine");
            result.AppendLine("    // Todo declare final activity in a special member like start activity");
            result.AppendLine("    while (fsm->m_CurrentActivity != ::Activity::ActivityFinal)");
            result.AppendLine("    {");
            result.AppendLine("        int event = GetNextEvent(fsm);");
            result.AppendLine("");
            result.AppendLine("        const Activity::Enum prevActivity = fsm->m_CurrentActivity;");
            result.AppendLine("");
            result.AppendLine("        RunEvent(fsm, event);");
            result.AppendLine("");
            result.AppendLine("        printf(\"transition %s -> %s \\n\", ::Activity::ToString(prevActivity).c_str(), ::Activity::ToString(fsm->m_CurrentActivity).c_str());");
            result.AppendLine("    }");
            result.AppendLine("}");
            result.AppendLine("");
            result.AppendLine("//******************************************************");
            result.AppendLine("// Performs an event handling on the FSM.");
            result.AppendLine("// Make sure there is a wildcard activity at the end of");
            result.AppendLine("// your table, otherwise; the event will be ignored.");
            result.AppendLine("//******************************************************");
            result.AppendLine("void " + ConvertToClassname(filename) + "::RunEvent(" + ConvertToClassname(filename) + "* fsm, int event)");
            result.AppendLine("{");
            result.AppendLine("    int i;");
            result.AppendLine("");
            result.AppendLine("    // walk over the transition table");
            result.AppendLine("    // to find a relevant entry for this activity and event");
            result.AppendLine("    //");
            result.AppendLine("    for (i = 0; i < fsm->m_NumActivity; i++)");
            result.AppendLine("    {");
            result.AppendLine("        // see if this is the Activity we are looking for");
            result.AppendLine("        if ((fsm->m_CurrentActivity == fsm->m_pTransitionTable[i].m_StartActivity) || (::Activity::Any == fsm->m_pTransitionTable[i].m_StartActivity))");
            result.AppendLine("        {");
            result.AppendLine("            // is this the event we are looking for");
            result.AppendLine("            if ((event == fsm->m_pTransitionTable[i].ev) || (::Events::Any == fsm->m_pTransitionTable[i].ev))");
            result.AppendLine("            {");
            result.AppendLine("                int iHasConditionSatisfied = (fsm->m_pTransitionTable[i].conditionfn && ");
            result.AppendLine("                fsm->m_pTransitionTable[i].conditionfn(fsm->m_FsmData));");
            result.AppendLine("");
            result.AppendLine("                // See if there is a condition associated");
            result.AppendLine("                // or we are not looking for any condition");
            result.AppendLine("                //");
            result.AppendLine("                if (iHasConditionSatisfied || (fsm->m_pTransitionTable[i].conditionfn == NULL))");
            result.AppendLine("                {");
            result.AppendLine("                    // call the activity callback function");
            result.AppendLine("                    fsm->m_pTransitionTable[i].fn(fsm->m_FsmData);");
            result.AppendLine("");
            result.AppendLine("                    // set the next activity");
            result.AppendLine("                    fsm->m_CurrentActivity = fsm->m_pTransitionTable[i].nextActivity;");
            result.AppendLine("");
            result.AppendLine("                    break;");
            result.AppendLine("                }");
            result.AppendLine("            }");
            result.AppendLine("        }");
            result.AppendLine("    }");
            result.AppendLine("}");

            return result.ToString();
        }
        
        private string ConvertToClassname(string filename)
        {
            string result = "";

            result = filename.Remove(filename.IndexOf("."));

            return result;
        }

        private string ConvertToHPP(string filename)
        {
            StringBuilder result = new StringBuilder(filename);

            result[filename.LastIndexOf("c")] = 'h';

            return result.ToString();
        }
    }
}
