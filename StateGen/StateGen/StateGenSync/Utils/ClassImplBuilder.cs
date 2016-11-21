using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using StateGen.StateGenSync.Types;

namespace StateGen.StateGenSync.Utils
{
    public class ClassImplBuilder : IClassBuilder
    {
        private string ELSE = "else";
        private string NONE = "";

        public Product CreateProduct(List<Method> actions, string filename)
        {
            Product product = new Product();

            product.SetFilename(filename);

            product.Append(CreateHeader(filename));
            product.Append(CreateFunctions(actions, filename));

            return product;
        }

        private string CreateHeader(string filename)
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine("#include \"" + ConvertToHPP(filename) +"\"");
            result.AppendLine("");
            result.AppendLine(ConvertToClassname(filename) + "::" + ConvertToClassname(filename) + "()");
            result.AppendLine("{");
            result.AppendLine("    /* Intentionally left blank */");
            result.AppendLine("}");
            result.AppendLine("");
            result.AppendLine(ConvertToClassname(filename) + "::~" + ConvertToClassname(filename) + "()");
            result.AppendLine("{");
            result.AppendLine("    /* Intentionally left blank */");
            result.AppendLine("}");
            result.AppendLine("");

            return result.ToString();
        }

        private string CreateFunctions(List<Method> functions, string filename)
        {
            StringBuilder result = new StringBuilder();

            foreach (Method m in functions)
            {
                if (IsLegalFunctionName(m.GetFunctionName()))
                {
                    result.AppendLine(m.GetReturnType() + " " + ConvertToClassname(filename) + "::" + m.GetFunctionName() + (m.GetReturnType() == "bool" ? "" : "()"));
                    result.AppendLine("{");
                    result.AppendLine("    // not implemented yet");
                    if (m.GetReturnType() == "bool")
                    {
                        result.AppendLine("    return true;");
                    }
                    result.AppendLine("}");
                    result.AppendLine("");
                }
            }

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
