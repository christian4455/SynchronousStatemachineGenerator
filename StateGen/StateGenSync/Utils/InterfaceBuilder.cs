using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using StateGen.StateGenSync.Types;

namespace StateGen.StateGenSync.Utils
{
    class InterfaceBuilder : IInterfaceBuilder
    {
        private string ELSE = "else";
        private string NONE = "";

        public Product CreateProduct(List<Method> actions, string filename)
        {
            Product product = new Product();

            product.SetFilename(filename);

            product.Append(CreateHeader(filename));
            product.Append(CreateBody(actions));
            product.Append(CreateFooter(filename));

            return product;
        }

        private string CreateHeader(string filename)
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine("#ifndef " + ConvertToClassname(filename).ToUpper() +"_HPP");
            result.AppendLine("#define " + ConvertToClassname(filename).ToUpper() + "_HPP");
            result.AppendLine("");
            result.AppendLine("class " + ConvertToClassname(filename));
            result.AppendLine("{");
            result.AppendLine("");
            result.AppendLine("public:");
            result.AppendLine("");
            result.AppendLine(ConvertToClassname(filename) + "() {}");
            result.AppendLine("virtual ~"+ ConvertToClassname(filename) + "() {}");
            result.AppendLine("");

            return result.ToString();
        }

        private string CreateBody(List<Method> methods)
        {
            StringBuilder result = new StringBuilder();

            foreach(Method m in methods)
            {
                if (IsLegalFunctionName(m.GetFunctionName()))
                {
                    // Todo [cb] This is very unhandy. Maybe set a enum for the interface?
                    if (m.GetReturnType() != "bool")
                    {
                        result.AppendLine("virtual " + m.GetReturnType() + " " + m.GetFunctionName() + "() = 0;");
                    }
                    else
                    {
                        result.AppendLine("virtual " + m.GetReturnType() + " " + m.GetFunctionName() + " = 0;");
                    }

                    result.AppendLine("");
                }
            }

            return result.ToString();
        }

        private string CreateFooter(string filename)
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine("private:");
            result.AppendLine("    /// @brief Automatically generated forbidden copy-constructor");
            result.AppendLine("    " + ConvertToClassname(filename) + "(const " + ConvertToClassname(filename) + "&)");
            result.AppendLine("    {");
            result.AppendLine("    }");
            result.AppendLine("");
            result.AppendLine("    /// @brief Automatically generated forbidden assignment operator");
            result.AppendLine("    " + ConvertToClassname(filename) + "& operator =(const " + ConvertToClassname(filename) + "&)");
            result.AppendLine("    {");
            result.AppendLine("        return *this;");
            result.AppendLine("    }");
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
