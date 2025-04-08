using Common;
using Data;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.ImpI
{
    public class OperationRepository : IOperationRepository
    {
       private readonly GaiaDbContext _context;

       public OperationRepository(GaiaDbContext context)
       {
          _context = context;
       }

       public async Task SaveOperationAsync(OperationRequest operation)
       {
            try
            {
                _context.Operations.Add(operation);
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                Console.WriteLine("❌ Save failed: " + ex.Message);
                Console.WriteLine("➡️ Inner: " + ex.InnerException?.Message);
                throw;
            }
       }

        public async Task<List<string>> GetSupportedOperationsAsync()
        {
            var operations = new List<string> { "add", "subtract", "multiply", "divide" };
            return await Task.FromResult(operations);
        }
    }
}
