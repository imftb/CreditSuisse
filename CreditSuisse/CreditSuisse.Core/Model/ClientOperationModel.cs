using CreditSuisse.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditSuisse.Core.Model
{
    public class ClientOperationModel
    {
        public ClientOperationModel() { }
        public double Value { get; set; }
        public string ClientSector { get; set; }
        public DateTime NextPaymentDate { get; set; }
        public CategoryOperationEnum Category { get; set; }

        public string DescriptionCategory
        {

            get
            {
                switch (this.Category)
                {
                    case CategoryOperationEnum.EXPIRED:
                        {
                            return "EXPIRED";
                        }

                    case CategoryOperationEnum.HIGHRISK:
                        {
                            return "HIGHRISK";
                        }
                    case CategoryOperationEnum.MEDIUMRISK:
                        {
                            return "MEDIUMRISK";
                        }

                    default:
                        {
                            return " ";
                        }
                }
            }
        }

    }
}