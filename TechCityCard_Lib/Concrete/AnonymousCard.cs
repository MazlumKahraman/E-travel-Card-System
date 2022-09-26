using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechCityCard_Lib.Abstract;

namespace TechCityCard_Lib.Concrete
{
    public class AnonymousCard : NonPersonalCard
    {
        public override CardType CardType
        {
            get
            {
                return CardType.Standart;
            }
        }
        public AnonymousCard(string cardNumber, decimal toll, decimal credit) : base(cardNumber, toll, credit)
        {
        }
    }
}
