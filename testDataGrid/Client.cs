using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testDataGrid
{
    class Client
    {
        public int ID { get; set; }
        public string fname { get; set; }
        public string lname { get; set; }
        public string email { get; set; }

        private int _height = 0;
        public int height {
            get
            {
                return this._height;
            }
                
            set{
                this._height = value;
            }
                }

        public Client()
        {
            this.height = 72;
        }
    }
}
