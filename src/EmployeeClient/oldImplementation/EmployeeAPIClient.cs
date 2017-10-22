using Model;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Console;

namespace EmployeeClient
{
    public class EmployeeAPIClient : HttpClientHelper<EmployeeDTO>
    {
        public EmployeeAPIClient(string baseAddress)
            : base(baseAddress) { }

        public override async Task<IEnumerable<EmployeeDTO>> GetAllAsync(string requestUri)
        {
            IEnumerable<EmployeeDTO> employees = await base.GetAllAsync(requestUri);
            return employees;
        }
    }
}