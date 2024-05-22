using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventec.NetCore
{
    public class ResultParam
    {

        public List<string> Messages { get; set; } = new List<string>();
        public List<string> BugCodes { get; set; } = new List<string>();

        private bool hasException;
        public bool HasException
        {
            get
            {
                return hasException;
            }
            set
            {
                //if (value) hasException = value; //Chi cho phep set true, ko cho phep set false
                hasException = value;
            }
        }


        public ResultParam()
        {
            Messages = new List<string>();
            BugCodes = new List<string>();
        }
    }
}
