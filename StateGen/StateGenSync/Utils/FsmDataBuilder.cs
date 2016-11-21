using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using StateGen.StateGenSync.Types;

namespace StateGen.StateGenSync.Utils
{
    class FsmDataBuilder : IFsmDataBuilder
    {
        private string NONE = "";
        private string ELSE = "else";

        public Product CreateProduct(StateMachineData fsmData, string filename)
        {
            Product product = new Product();

            product.SetFilename(filename);

            // header
            product.Append(CreateHeader(filename));

            // actions
            product.Append(CreateActions(fsmData.GetActions()));

            // pseudo actions
            product.Append(CreatePseudoActions(fsmData.GetPseudoActions()));

            // guards
            product.Append(CreateGuards(fsmData.GetGuards()));

            // class footer
            product.Append(CreateClassFooter(filename));

            // transition table
            product.Append(CreateTransitionTable(fsmData.GetTransitionTable()));

            // footer
            product.Append(CreateFooter(filename));

            return product;
        }

        private string CreateHeader(string filename)
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine("#ifndef " + ConvertToClassname(filename).ToUpper() + "_HPP");
            result.AppendLine("#define " + ConvertToClassname(filename).ToUpper() + "_HPP");
            result.AppendLine("");
            result.AppendLine("#include \"Activity.hpp\"");
            result.AppendLine("#include \"Events.hpp\"");
            result.AppendLine("#include \"IActionHandler.hpp\"");
            result.AppendLine("#include \"IConditionHandler.hpp\"");
            result.AppendLine("#include \"TransitionRow.hpp\"");
            result.AppendLine("");
            result.AppendLine("class " + ConvertToClassname(filename));
            result.AppendLine("{");
            result.AppendLine("");
            result.AppendLine("public:");
            result.AppendLine("    " + ConvertToClassname(filename) + "(IActionHandler& actionHandler, IConditionHandler& conditionHandler) :");
            result.AppendLine("        m_IActionHandler(actionHandler),");
            result.AppendLine("        m_IConditionHandler(conditionHandler)");
            result.AppendLine("{");
            result.AppendLine("    /* Intentionally left blank */");
            result.AppendLine("}");
            result.AppendLine("");
            result.AppendLine("virtual ~" + ConvertToClassname(filename) + "()");
            result.AppendLine("{");
            result.AppendLine("    /* Intentionally left blank */");
            result.AppendLine("}");
            result.AppendLine("");

            return result.ToString();
        }

        private string ConvertToClassname(string filename)
        {
            string result = "";

            result = filename.Remove(filename.IndexOf("."));

            return result;
        }

        private string CreateActions(List<Method> actions)
        {
            StringBuilder result = new StringBuilder();

            foreach (Method m in actions)
            {
                result.AppendLine("static " + m.GetReturnType() + " " + m.GetFunctionName() + "(FsmData& fsmData)");
                result.AppendLine("{");
                result.AppendLine("}");
                result.AppendLine("");
            }

            return result.ToString();
        }

        private string CreatePseudoActions(List<Method> pseudoActions)
        {
            StringBuilder result = new StringBuilder();

            if (pseudoActions.Count > 0)
            {
                result.AppendLine("// pseudoactions");
            }

            List<string> adjustedPseudoActions = new List<string>();

            foreach (Method m in pseudoActions)
            {
                if (!adjustedPseudoActions.Contains(m.GetFunctionName()))
                {
                    result.AppendLine("static " + m.GetReturnType() + " " + m.GetFunctionName() + "(FsmData& fsmData)");
                    result.AppendLine("{");
                    result.AppendLine("    /* Intentionally left blank */");
                    result.AppendLine("}");
                    result.AppendLine("");

                    adjustedPseudoActions.Add(m.GetFunctionName());
                }
            }

            return result.ToString();
        }

        private string CreateGuards(List<Method> guards)
        {
            StringBuilder result = new StringBuilder();

            List<string> adjustedGuards = new List<string>();

            foreach (Method m in guards)
            {
                if (IsLegalGuard(m.GetFunctionName()))
                {
                    if (!adjustedGuards.Contains(m.GetFunctionName()))
                    {
                        result.AppendLine("static " + m.GetReturnType() + " " + RemoveBrackets(m.GetFunctionName()) + "(FsmData& fsmData)");
                        result.AppendLine("{");
                        // Todo [cb] maybe add check for brackets and add them if they dont exist.
                        result.AppendLine("    return fsmData.GetConditionHandler()." + m.GetFunctionName() + ";");
                        result.AppendLine("}");
                        result.AppendLine("");

                        adjustedGuards.Add(m.GetFunctionName());
                    }
                }
            }

            return result.ToString();
        }

        private string CreateClassFooter(string filename)
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine("inline IActionHandler& GetActionHandler() { return m_IActionHandler; }");
            result.AppendLine("inline IConditionHandler& GetConditionHandler() { return m_IConditionHandler; }");
            result.AppendLine("");
            result.AppendLine("private:");
            result.AppendLine("");
            result.AppendLine("IActionHandler& m_IActionHandler;");
            result.AppendLine("IConditionHandler& m_IConditionHandler;");
            result.AppendLine("};");
            result.AppendLine("");

            return result.ToString();
        }

        private string CreateTransitionTable(TransitionTable transitionTable)
        {
            StringBuilder result = new StringBuilder();

            transitionTable.CalculateColumnSizes();

            result.AppendLine("//******************************************************");
            result.AppendLine("// Transition table");
            result.AppendLine("//******************************************************");
            result.AppendLine("const TransitionRow trans[] = {");
            // todo [cb] write a formatter for this
            result.AppendLine("// CURRENT ACTIVITY " + insertSpaces(transitionTable.GetColOneLen() - "CURRENT ACTIVITY".Length)
                                + "EVENT"     + insertSpaces(transitionTable.GetColTwoLen() - "EVENT".Length + "  ".Length)
                                + "ACTION"    + insertSpaces(transitionTable.GetColThreeLen() - "ACTION".Length + "  ".Length)
                                + "NEXT ACTIVITY" + insertSpaces(transitionTable.GetColFourLen() - "NEXT ACTIVITY".Length + "  ".Length)
                                + "GUARD");

            foreach (Row r in transitionTable.GetRows())
            {
                Int32 lenColFive = (transitionTable.GetColFiveLen() - r.GetGuard().Length - (r.GetGuard().Contains("(") ? 2 : 0) - 10);

                if (ConsumeGuardForTransitionTable(r.GetGuard()) == "NULL")
                {
                    if (r.GetGuard() != "else")
                    {
                        lenColFive -= 4;
                    }
                }
                // Todo [cb] maybe take "activity::" to the repository handler??
                result.AppendLine("{ ::Activity::" + r.GetCurrentActivity().GetName() + insertSpaces(transitionTable.GetColOneLen() - r.GetCurrentActivity().GetName().Length - 12)
                                     + ", ::Events::Any"
                                     + ", &FsmData::" + r.GetAction() + insertSpaces(transitionTable.GetColThreeLen() - r.GetAction().Length - 10)
                                     + ", ::Activity::" + r.GetNextActivity().GetName() + insertSpaces(transitionTable.GetColFourLen() - r.GetNextActivity().GetName().Length - 12)
                                     + GuardHelper(r) + insertSpaces( lenColFive > 0 ? lenColFive : 0 )
                                     + " },");
            }

            RemoveLastSign(result);

            result.AppendLine("};");
            result.AppendLine("");

            return result.ToString();
        }

        private string CreateFooter(string filename)
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine("");
            result.AppendLine("#endif // " + ConvertToClassname(filename).ToUpper() + "_HPP");

            return result.ToString();
        }

        private void RemoveLastSign(StringBuilder builder)
        {
            builder = builder.Remove(builder.ToString().LastIndexOf(","), 1);
        }

        private bool IsLegalGuard(string guard)
        {
            bool result = false;

            if (guard != ELSE && guard != NONE)
            {
                result = true;
            }

            return result;
        }

        private string RemoveBrackets(string function)
        {
            string result = function;

            result = result.Remove(function.IndexOf("("), 2);

            return result;
        }

        private string ConsumeGuardForTransitionTable(string function)
        {
            string result = "";

            if (IsLegalGuard(function))
            {
                result = RemoveBrackets(function);
            }
            else
            {
                result = "NULL";
            }
            
            return result;
        }

        private string insertSpaces(Int32 len)
        {
            string result = string.Empty;

            for (Int32 j = 0; j < len; j++)
            {
                result += " ";
            }

            return result;
        }

        private string insertHyphen(Int32 len)
        {
            string result = string.Empty;

            for (Int32 j = 0; j < len; j++)
            {
                result += "-";
            }

            return result;
        }

        private string GuardHelper(Row r)
        {
            string result = string.Empty;

            if (r.GetGuard() != NONE && r.GetGuard() != ELSE)
            {
                result += ", &FsmData::" + ConsumeGuardForTransitionTable(r.GetGuard());
            }
            else
            {
                result += ", " + ConsumeGuardForTransitionTable(r.GetGuard()) + insertSpaces(10);
            }

            return result;
        }

    }
}
