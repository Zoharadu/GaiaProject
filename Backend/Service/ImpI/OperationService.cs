using Common;
using Repository.Interface;
using Service.Interface;

public class OperationService : IOperationService
{
    private readonly IOperationRepository _repository;

    private readonly Dictionary<string, Func<double, double, double>> _operations = new()
    {
        { "add", (a, b) => a + b },
        { "subtract", (a, b) => a - b },
        { "multiply", (a, b) => a * b },
        { "divide", (a, b) =>
            {
                if (b == 0) throw new DivideByZeroException();
                return a / b;
            }
        }
    };

    public OperationService(IOperationRepository repository)
    {
        _repository = repository;
    }

    public async Task<OperationResult> CalculateAsync(OperationRequest request)
    {
        if (!_operations.TryGetValue(request.Operator, out var operation))
        {
            throw new ArgumentException($"Unsupported operation: {request.Operator}");
        }

        double result = operation(request.A, request.B);

        request.Result = result;
        request.Timestamp = DateTime.Now;

        await _repository.SaveOperationAsync(request);

        var lastThreeOperations = await _repository.GetLastThreeOperationsAsync(request.Operator);
        var operationsCountThisMonth = await _repository.GetOperationsCountThisMonthAsync(request.Operator);

        return new OperationResult
        {
            Result = result,
            LastThreeOperations = lastThreeOperations,
            OperationsCountThisMonth = operationsCountThisMonth
        };
    }

    public async Task<List<string>> GetSupportedOperationsAsync()
    {
        return await Task.FromResult(_operations.Keys.ToList());
    }
}
