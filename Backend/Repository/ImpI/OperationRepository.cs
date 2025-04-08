using Common;
using Data;
using Microsoft.EntityFrameworkCore;
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

        public async Task<List<OperationRequest>> GetLastThreeOperationsAsync(string operationType)
        {
            return await _context.Operations
                .Where(op => op.Operator == operationType)
                .OrderByDescending(op => op.Timestamp)
                .Take(3)
                .ToListAsync();
        }

        public async Task<int> GetOperationsCountThisMonthAsync(string operationType)
        {
            var startOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            return await _context.Operations
                .Where(op => op.Operator == operationType && op.Timestamp >= startOfMonth)
                .CountAsync();
        }
    }
}

