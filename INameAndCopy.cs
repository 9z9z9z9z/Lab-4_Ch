using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_4
{
    public interface INameAndCopy
    {
        string Name { get; set; }
        object DeepCopy();
    }
}
