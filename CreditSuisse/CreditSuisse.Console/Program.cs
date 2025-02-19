// See https://aka.ms/new-console-template for more information

using CreditSuisse.Core.Enum;
using CreditSuisse.Core.Interface.Service;
using CreditSuisse.Core.Model;
using CreditSuisse.Core.Service;
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;
using System.Globalization;
using static System.Runtime.InteropServices.JavaScript.JSType;

ITradeService tradeService = new TradeService();

OperationModel Operation = new OperationModel();

DateTime Auxdate;
DateTime.TryParseExact(Console.ReadLine(), "MM/dd/yyyy",
                           CultureInfo.InvariantCulture,
                           DateTimeStyles.None,
                           out Auxdate);

Operation.ReferenceDate = Auxdate;

Operation.NumberOperation = Convert.ToInt32(Console.ReadLine());

Operation.Operations = new List<ClientOperationModel>();

for (int i = 0; i < Operation.NumberOperation; i++)
{   
    string[] client = (Console.ReadLine()).Split(" ");

    ClientOperationModel Client = new ClientOperationModel();

    Client.Value = Convert.ToDouble(client[0]);

    Client.ClientSector = client[1];

    DateTime.TryParseExact(client[2], "MM/dd/yyyy",
                           CultureInfo.InvariantCulture,
                           DateTimeStyles.None,
                           out Auxdate);
    Client.NextPaymentDate = Auxdate;

    Operation.Operations.Add(Client);

}

var result = tradeService.GetOperationCategory(Operation);

foreach (var item in result)
    Console.WriteLine(item);





