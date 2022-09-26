using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechCityCard_Lib.Abstract
{
    public abstract class PersonalCard : Card
    {
        public string IdentityNumber { get; }
        public string FirstName { get; }
        public string MiddleName { get; }
        public string LastName { get; }

        protected PersonalCard(string cardNumber, CardType cardType, decimal toll, decimal credit, string identityNumber, string firstName, string middleName, string lastName) : base(cardNumber, cardType, toll, credit)
        {
            IdentityNumber = identityNumber;
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
        }
    }
}
