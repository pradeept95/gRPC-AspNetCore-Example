using Application.Data.DataContext;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace gRPC.Core.Server
{
    public class EmpService : Emp.EmpBase
    {
        private readonly ILogger<GreeterService> _logger;
        private readonly ApplicationDbContext _db;

        public EmpService(ILogger<GreeterService> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public override async Task<EmployeeModel> GetEmployee(EmployeeRequest request, ServerCallContext context)
        {
            var employee = await _db.Employee.FirstOrDefaultAsync(x => x.Id == request.Id);
            return new EmployeeModel()
            { 
                    Id = employee.Id,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Date = DateTime.UtcNow.ToTimestamp() 
            };
        }

        public override async Task GetEmployeeList(EmployeeRequest request, IServerStreamWriter<EmployeeModel> responseStream, ServerCallContext context)
        {
            var employees = _db.Employee;
            await foreach (var employee in employees)
            {
                await responseStream.WriteAsync(new EmployeeModel
                {
                     Id = employee.Id,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Date = DateTime.UtcNow.ToTimestamp() 
                });

                await Task.Delay(1000);
            }
        }
    }
}
