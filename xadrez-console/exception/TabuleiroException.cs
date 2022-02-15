using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xadrez_console.exception
{
    class TabuleiroException : Exception
    {
        public TabuleiroException(string strMsg): base(strMsg) { }
    }
}
