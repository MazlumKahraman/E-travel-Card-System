using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechCityCard_Lib.Abstract
{
    public enum CardType
    {
        Standart,
        Elder,
        Worker,
        Student
    }

    public abstract class Card
    {
        private string _cardNumber;
        private decimal _credit;

        public string CardNumber
        {
            get
            {
                return _cardNumber;
            }
            set
            {
                if (value.ToString().Length != 16)
                    throw new Exception("CardNumber must be 16 digit number only!");
                _cardNumber = value;
            }
        }
        public virtual CardType CardType { get; }
        public decimal Toll { get; }
        public decimal Credit { get { return _credit; } }

        public Card(string cardNumber, CardType cardType, decimal toll, decimal credit)
        {
            _cardNumber = cardNumber;
            CardType = cardType;
            Toll = toll;
            _credit = credit;
        }

        public bool Pay()
        {
            decimal toll = GetDiscountedToll();
            if (_credit >= toll) 
            {
                _credit -= toll;
                return true;
            }
            else
            {
                return false;
            }

        }
        public Decimal GetDiscountedToll()
        {
            decimal toll = Toll;
            switch (CardType)
            {
                case CardType.Standart:
                    break;
                case CardType.Elder:
                    toll -= (toll * 0.50m);
                    break;
                case CardType.Worker:
                    toll -= (toll * 0.25m);
                    break;
                case CardType.Student:
                    toll -= (toll * 0.75m);
                    break;
            }
            return toll;
        }
        public void AddCredit(decimal addedCredit)
        {
            _credit += addedCredit;
        }
    }
}
