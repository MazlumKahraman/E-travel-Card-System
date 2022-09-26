using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechCityCard_Lib.Abstract
{
    public abstract class NonPersonalCard:Card
    {
        public NonPersonalCard(string cardNumber, decimal toll, decimal credit) : base(cardNumber, CardType.Standart, toll, credit)
        {
        }
    }
}
