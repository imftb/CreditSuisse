using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditSuisse.Core.Model
{
    public class OperationModel
    {
        public OperationModel() { }
        public DateTime ReferenceDate { get; set; }
        public int NumberOperation { get; set; } = 0;
        public List<ClientOperationModel> Operations { get; set; }

    }
}
