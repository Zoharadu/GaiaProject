using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Common.OperationRequest;

namespace Service.Interface
{
    public interface IOperationService
    {
        Task<OperationResult> CalculateAsync(OperationRequest request);
        Task<List<string>> GetSupportedOperationsAsync();
    }
}
