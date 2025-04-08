using Common;
using Repository.Interface;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Common.OperationRequest;

namespace Service.ImpI
{
    public class OperationService : IOperationService
    {
        private readonly IOperationRepository _repository;

        public OperationService(IOperationRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> CalculateAsync(OperationRequest request)
        {
            double result = request.Operator switch
            {
                "add" => request.A + request.B,
                "subtract" => request.A - request.B,
                "multiply" => request.A * request.B,
                "divide" => request.B != 0 ? request.A / request.B : throw new DivideByZeroException(),
                _ => throw new ArgumentException("Unknown operation")
            };

            request.Result = result;
            request.Timestamp = DateTime.Now;

            await _repository.SaveOperationAsync(request);

            return new OperationResult { Result = result };
        }
        public async Task<List<string>> GetSupportedOperationsAsync()
        {
            return await _repository.GetSupportedOperationsAsync();
        }
    }
}
