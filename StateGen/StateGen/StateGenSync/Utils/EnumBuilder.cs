using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StateGen.StateGenSync.Types;

namespace StateGen.StateGenSync.Utils
{
    public class EnumBuilder : IEnumBuilder
    {
        public Product CreateProduct(List<string> enumValues, string filename)
        {
            Product product = new Product();

            product.SetFilename(filename);

            product.Append(CreateHeader(filename));
            product.Append(CreateEnumValues(enumValues));
            product.Append(CreateToString(enumValues));
            product.Append(CreateFooter(filename));

            return product;
        }

        private string CreateHeader(string filename)
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine("#ifndef " + ConvertToClassname(filename));
            result.AppendLine("#define " + ConvertToClassname(filename));
            result.AppendLine("");
            result.AppendLine("class " + ConvertToClassname(filename));
            result.AppendLine("{");
            result.AppendLine("");
            result.AppendLine("public:");
            result.AppendLine("");

            return result.ToString();
        }

        private string CreateEnumValues(List<string> enumValues)
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine("    enum Enum");
            result.AppendLine("    {");

            foreach (string e in enumValues)
            {
                result.AppendLine("        " + e + " = 0,");
            }

            RemoveLastSign(result, ",");

            result.AppendLine("    };");
            result.AppendLine("");

            return result.ToString();
        }

        private string CreateToString(List<string> enumValues)
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine("    static ::std::string ToString(const Enum e)");
            result.AppendLine("    {");
            result.AppendLine("        ::std::string ret;");
            result.AppendLine("");
            result.AppendLine("        switch (e)");
            result.AppendLine("        {");

            foreach (string e in enumValues)
            {
                result.AppendLine("        case "+ e +" : ret = \"" + e + "\"; break;");
            }

            result.AppendLine("            // no default case since we want to get a compiler warning in case enum value is added");
            result.AppendLine("        }");
            result.AppendLine("");
            result.AppendLine("        return ret;");
            result.AppendLine("    }");

            return result.ToString();
        }

        private string CreateFooter(string filename)
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine("private:");
            result.AppendLine("    " + ConvertToClassname(filename) + "();");
            result.AppendLine("    " + ConvertToClassname(filename) + "(const " + ConvertToClassname(filename) + "&);");
            result.AppendLine("    " + ConvertToClassname(filename) + " & operator= (const "+ ConvertToClassname(filename) + "&);");
            result.AppendLine("};");
            result.AppendLine("");
            result.AppendLine("#endif // " + ConvertToClassname(filename));

            return result.ToString();
        }

        private string ConvertToClassname(string filename)
        {
            string result = "";

            result = filename.Remove(filename.IndexOf("."));

            return result;
        }

        private void RemoveLastSign(StringBuilder builder, string val)
        {
            builder = builder.Remove(builder.ToString().LastIndexOf(val), 1);
        }
    }
}
