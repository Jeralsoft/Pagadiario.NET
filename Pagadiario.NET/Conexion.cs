using System;
using System.Data;
using System.Data.OleDb; 
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pagadiario.NET {
    
    class Conexion{

        internal static string conectar() {
            string cnn;
            cnn = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=pagadiario.accdb; Jet OLEDB:Database Password=;";

            return cnn; 
        }
 
    }

}
