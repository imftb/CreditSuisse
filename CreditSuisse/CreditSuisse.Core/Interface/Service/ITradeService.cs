using CreditSuisse.Core.Model;

namespace CreditSuisse.Core.Interface.Service
{
    public interface ITradeService
    {
        List<string> GetOperationCategory(OperationModel operation);
    }
}
