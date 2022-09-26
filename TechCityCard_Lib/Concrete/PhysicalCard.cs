using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechCityCard_Lib.Abstract;

namespace TechCityCard_Lib.Concrete
{
    public class PhysicalCard : PersonalCard
    {
        public PhysicalCard(string cardNumber, CardType cardType, decimal toll, decimal credit, string identityNumber, string firstName, string middleName, string lastName) : base(cardNumber, cardType, toll, credit, identityNumber, firstName, middleName, lastName)
        {

        }
    }
}
