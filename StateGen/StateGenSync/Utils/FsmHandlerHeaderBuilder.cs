using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using StateGen.StateGenSync.Types;

namespace StateGen.StateGenSync.Utils
{
    public class FsmHandlerHeaderBuilder : IFsmHandlerHeaderBuilder
    {
        private string ELSE = "else";
        private string NONE = "";

        public Product CreateProduct(List<Method> methods, string filename)
        {
            Product product = new Product();

            product.SetFilename(filename);

            product.Append(CreateHeader(filename));

            product.Append(CreateFunctions(methods));

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
            result.AppendLine("#include \"FsmData.hpp\"");
            result.AppendLine("#include \"IFsmHandler.hpp\"");
            result.AppendLine("#include \"TransitionRow.hpp\"");
            result.AppendLine("");
            result.AppendLine("// FsmHandler data structure");
            result.AppendLine("class " + ConvertToClassname(filename)+ " : public IFsmHandler");
            result.AppendLine("{");
            result.AppendLine("");
            result.AppendLine("static const int TRANS_COUNT = sizeof(trans) / sizeof(*trans);");
            result.AppendLine("");
            result.AppendLine("public:");
            result.AppendLine("");
            result.AppendLine("explicit " + ConvertToClassname(filename) + "(FsmData& fsmData);");
            result.AppendLine("virtual ~" + ConvertToClassname(filename) + "();");
            result.AppendLine("");

            return result.ToString();
        }

        private string CreateFunctions(List<Method> methods)
        {
            StringBuilder result = new StringBuilder();

            foreach (Method m in methods)
            {
                if (IsLegalFunctionName(m.GetFunctionName()))
                {
                    result.AppendLine("virtual " + m.GetReturnType() + " " + m.GetFunctionName() + "();");
                    result.AppendLine("");
                }
            }

            return result.ToString();
        }

        private string CreateFooter(string filename)
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine("private:");
            result.AppendLine("");
            result.AppendLine("int GetNextEvent(FsmHandler* fsm);");
            result.AppendLine("");
            result.AppendLine("void RunStateMachine(FsmHandler* fsm, const Activity::Enum initActivity);");
            result.AppendLine("");
            result.AppendLine("void RunEvent(FsmHandler* fsm, int event);");
            result.AppendLine("");
            result.AppendLine("FsmData& m_FsmData;                      // pointer to a structure carrying context");
            result.AppendLine("Activity::Enum m_CurrentActivity;        // Current activity");
            result.AppendLine("int m_NumActivity;                       // Number of entries in the table");
            result.AppendLine("const TransitionRow* m_pTransitionTable; // FSM table");
            result.AppendLine("};");
            result.AppendLine("");
            result.AppendLine("#endif // " + ConvertToClassname(filename).ToUpper() + "_HPP");

            return result.ToString();
        }

        private string ConvertToClassname(string filename)
        {
            string result = "";

            result = filename.Remove(filename.IndexOf("."));

            return result;
        }

        private bool IsLegalFunctionName(string functionName)
        {
            bool result = false;

            if (functionName != ELSE && functionName != NONE)
            {
                result = true;
            }

            return result;
        }
    }
}
