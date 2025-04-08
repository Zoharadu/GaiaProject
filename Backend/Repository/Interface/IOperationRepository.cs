using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IOperationRepository
    {
        Task SaveOperationAsync(OperationRequest operation);
        Task<List<string>> GetSupportedOperationsAsync();
        Task<List<OperationRequest>> GetLastThreeOperationsAsync(string operationType);
        Task<int> GetOperationsCountThisMonthAsync(string operationType);
    }
}
