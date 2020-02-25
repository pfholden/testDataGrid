using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testDataGrid
{
    public class Client
    {
        public static Random rnd;
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
            this.height = rnd.Next(42,84);
        }
    }
}
