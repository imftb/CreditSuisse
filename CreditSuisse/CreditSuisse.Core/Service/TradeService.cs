using CreditSuisse.Core.Enum;
using CreditSuisse.Core.Interface.Service;
using CreditSuisse.Core.Model;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CreditSuisse.Core.Service
{
    public class TradeService : ITradeService
    {
        public List<string> GetOperationCategory(OperationModel operation)
        {
            List<string> Result = new List<string>();

            try
            {
                foreach (var item in operation.Operations)
                {
                    if (item.NextPaymentDate > operation.ReferenceDate.AddDays(30))
                        item.Category = CategoryOperationEnum.EXPIRED;
                    else if (item.Value > 1000000 && item.ClientSector.ToUpper() == ClientSectorEnum.Private.ToString().ToUpper())
                        item.Category = CategoryOperationEnum.HIGHRISK;
                    else if (item.Value > 1000000 && item.ClientSector.ToUpper() == ClientSectorEnum.Public.ToString().ToUpper())
                        item.Category = CategoryOperationEnum.MEDIUMRISK;

                    Result.Add(item.DescriptionCategory);
                }
                return Result;
            }
            catch (Exception ex)
            {
               Result.Add("Operation Failed");

                return Result;
            }
        }
    }
}
