using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace testDataGrid
{
    class ClientCollection
    {
        public int current_id { get; set; }

        public List<Client> clientList {get; set;} 

        static public ClientCollection GetJSON()
        {
            ClientCollection fileClients = null;

            string rawJSON = System.IO.File.ReadAllText(@"C:\Users\~\source\repos\testDataGrid\testDataGrid\clients.json");
            fileClients = JsonConvert.DeserializeObject<ClientCollection>(rawJSON);
           
            return fileClients;
        }
    }
}
