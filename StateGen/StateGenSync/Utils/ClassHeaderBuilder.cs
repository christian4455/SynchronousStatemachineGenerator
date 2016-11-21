using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using StateGen.StateGenSync.Types;

namespace StateGen.StateGenSync.Utils
{
    public class ClassHeaderBuilder : IClassHeaderBuilder
    {
        private string ELSE = "else";
        private string NONE = "";

        public Product CreateProduct(List<Method> actions, string filename, string interfaceName)
        {
            Product product = new Product();

            product.SetFilename(filename);

            product.Append(CreateHeader(filename, interfaceName));
            product.Append(CreateFunctions(actions));
            product.Append(CreateFooter(filename));

            return product;
        }

        private string CreateHeader(string filename, string interfaceName)
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine("#ifndef " + ConvertToClassname(filename).ToUpper() + "_HPP");
            result.AppendLine("#define " + ConvertToClassname(filename).ToUpper() + "_HPP");
            result.AppendLine("");
            result.AppendLine("#include \""+ interfaceName +"\"");
            result.AppendLine("");
            result.AppendLine("class " + ConvertToClassname(filename) + " : public " + ConvertToClassname(interfaceName));
            result.AppendLine("{");
            result.AppendLine("");
            result.AppendLine("public:");
            result.AppendLine("");
            result.AppendLine(ConvertToClassname(filename) + "();");
            result.AppendLine("virtual ~" + ConvertToClassname(filename) + "();");
            result.AppendLine("");

            return result.ToString();
        }

        private string CreateFunctions(List<Method> functions)
        {
            StringBuilder result = new StringBuilder();

            foreach (Method m in functions)
            {
                if (IsLegalFunctionName(m.GetFunctionName()))
                {
                    result.AppendLine("virtual " + m.GetReturnType() + " " + m.GetFunctionName() + (m.GetReturnType() == "bool" ? ";" : "();"));
                    result.AppendLine("");
                }
            }

            return result.ToString();
        }

        private string CreateFooter(string filename)
        {
            StringBuilder result = new StringBuilder();

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
