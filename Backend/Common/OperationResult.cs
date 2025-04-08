using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class OperationResult
    {
        public double Result { get; set; }
        public List<OperationRequest> LastThreeOperations { get; set; } = new List<OperationRequest>();
        public int OperationsCountThisMonth { get; set; }
    }
}
