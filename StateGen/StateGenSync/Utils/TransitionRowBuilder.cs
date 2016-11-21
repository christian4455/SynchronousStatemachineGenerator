using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using StateGen.StateGenSync.Utils;
using StateGen.StateGenSync.Types;

namespace StateGen.StateGenSync.Utils
{
    public class TransitionRowBuilder : ITransitionRowBuilder
    {
        public Product CreateProduct(string filename)
        {
            Product product = new Product();

            product.SetFilename(filename);

            product.Append(CreateHeader());

            product.Append(CreateBody());

            product.Append(CreateFooter());

            return product;
        }

        private string CreateHeader()
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine("#ifndef TRANSITIONROW_HPP");
            result.AppendLine("#define TRANSITIONROW_HPP");
            result.AppendLine("");
            result.AppendLine("#include \"Activity.hpp\"");
            result.AppendLine("#include \"Events.hpp\"");
            result.AppendLine("#include \"FsmData.hpp\"");
            result.AppendLine("");
            result.AppendLine("class FsmData;");
            result.AppendLine("// Each row of the transition table");
            result.AppendLine("struct TransitionRow");
            result.AppendLine("{");

            return result.ToString();
        }

        private string CreateBody()
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine("Activity::Enum m_StartActivity;             // start activity");
            result.AppendLine("Events::Enum m_Event;                       // event identifier");
            result.AppendLine("// TODO check if we can use datapool parameter");
            result.AppendLine("void(*fn)(FsmData &);                       // function to be called for action");
            result.AppendLine("Activity::Enum m_NextActivity;              // next activity");
            result.AppendLine("bool(*conditionfn)(FsmData &);              // condition to be evaluated");

            return result.ToString();
        }

        private string CreateFooter()
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine("};");
            result.AppendLine("");
            result.AppendLine("#endif // TRANSITIONROW_HPP");

            return result.ToString();
        }
    }
}
