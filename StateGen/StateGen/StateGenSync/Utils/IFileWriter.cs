using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateGen.StateGenSync.Utils
{
    public interface IFileWriter
    {
       void WriteProduct(string path, StateGenSync.Types.Product product);
    }
}
