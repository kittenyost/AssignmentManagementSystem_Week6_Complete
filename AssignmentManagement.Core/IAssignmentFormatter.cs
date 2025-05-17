using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentManagement.Core
{
    public interface IAssignmentFormatter
    {
        string Format(Assignment assignment);
    }
}
