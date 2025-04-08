using Common;
using Repository.Interface;
using Service.Interface;
using static Common.OperationRequest;

public class OperationService : IOperationService
{
    private readonly IOperationRepository _repository;

    // מילון של פעולות נתמך
    private readonly Dictionary<string, Func<double, double, double>> _operations = new()
    {
        { "add", (a, b) => a + b },
        { "subtract", (a, b) => a - b },
        { "multiply", (a, b) => a * b },
        { "www", (a, b) => a * b },
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

        return new OperationResult { Result = result };
    }

    public async Task<List<string>> GetSupportedOperationsAsync()
    {
        // כאן אפשר להחזיר מהמילון או מהדאטהבייס
        return await Task.FromResult(_operations.Keys.ToList());
    }
}
