using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using StateGen.Utils.Logger;

namespace StateGen.StateGenSync.Utils
{
    class FileWriter : IFileWriter
    {
        public FileWriter()
        {
            /* Intentionally left blank */
        }

        public void WriteProduct(string path, StateGenSync.Types.Product product)
        {
            Log.Info("");
            try
            {
                System.IO.StreamWriter file = new System.IO.StreamWriter(path + product.GetFilename());
                file.WriteLine(product.GetProduct().ToString());
                file.Close();
            }
            catch (Exception e)
            {
                Log.Info("Exception:" + e.Message);
            }
            finally
            {
                // nothing
            }
        }
    }
}
