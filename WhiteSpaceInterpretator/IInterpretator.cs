using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhiteSpaceInterpretator
{
    interface IInterpretator
    {
        Task Execute(string source);
    }
}
