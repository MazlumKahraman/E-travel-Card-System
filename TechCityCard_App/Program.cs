using Spectre.Console;
using System.Net.NetworkInformation;
using TechCityCard_Lib.Abstract;
using TechCityCard_Lib.Concrete;

string cardNumber = "", identityNumber = "", firstName = "", middleName = "", lastName = "";
CardType cardType = CardType.Standart;
Card currentCard = null;
decimal toll = 5, credit = 10;


ShowAnimatedText();

var cardHeader = AnsiConsole.Prompt(
    new SelectionPrompt<string>()
        .Title("Select [red]card [/] to continue...")
        .PageSize(50)
        .MoreChoicesText("[grey](Move up and down to reveal more options)[/]")
        .AddChoices(new[] {
            "Personal", "Non Personal"
        }));

if (cardHeader == "Personal")
{
    var subCard = AnsiConsole.Prompt(
    new SelectionPrompt<string>()
        .Title("Select [red]card [/] to continue...")
        .PageSize(50)
        .MoreChoicesText("[grey](Move up and down to reveal more options)[/]")
        .AddChoices(new[] {
            "Physical Card", "Digital Card"
        }));

    var newCardType = AnsiConsole.Prompt(
    new SelectionPrompt<string>()
        .Title("Select [red]card type[/] to continue...")
        .PageSize(10)
        .MoreChoicesText("[grey](Move up and down to reveal more options)[/]")
        .AddChoices(new[] {
            "Standart", "Elder","Worker","Student"
        }));
    cardType = newCardType switch
    {
        "Standart" => CardType.Standart,
        "Elder" => CardType.Elder,
        "Worker" => CardType.Worker,
        "Student" => CardType.Student,
    };
    currentCard = CreateCard(subCard, cardType);
    Console.WriteLine("Press any key to continue...");
    Console.ReadKey();
}
else if (cardHeader == "Non Personal")
{
    do
    {
        Console.WriteLine("Please enter 16 digit card number");
        cardNumber = Console.ReadLine();
    } while (!cardNumber.All(char.IsDigit) || cardNumber.Length != 16);
    Console.WriteLine("Please enter normal toll");
    toll = Convert.ToDecimal(Console.ReadLine());
    currentCard = new AnonymousCard(cardNumber, toll, credit);

    Console.WriteLine("Your Anonymous Card created as Physical Card");
    Console.WriteLine("Press any key to continue...");
    Console.ReadKey();
}

ShowMainMenu(currentCard);

void ShowMainMenu(Card card)
{
    bool status = true;
    while (status)
    {
        Console.Clear();
        ShowAnimatedText();
        var selection = AnsiConsole.Prompt(
        new SelectionPrompt<string>()
            .Title("Select [red]action[/] to continue...")
            .PageSize(10)
            .MoreChoicesText("[grey](Move up and down to reveal more options)[/]")
            .AddChoices(new[] {
            "Get my card info","Add credit","Calculate payment","Do payment"
            }));

        switch (selection)
        {
            case "Get my card info":
                ShowUserInfo(card);
                break;
            case "Add credit":
                AddCredit(card);
                break;
            case "Calculate payment":
                CalculatePayment(card);
                break;
            case "Do payment":
                DoPayment(card);
                break;

        }
        Console.Write("\nDo you want to do another proccess? [Y-y]: ");
        string statusKey = Console.ReadLine();
        status = statusKey.ToLower().Equals("y");
    }

}

static void ShowAnimatedText()
{
    AnsiConsole.Write(
       new FigletText("TechCity Card")
           .Centered()
           .Color(Color.Red));
}

Card CreateCard(string subCard, CardType cardType)
{
    Card card = null;
    Console.WriteLine("Please enter your first name");
    firstName = Console.ReadLine();
    Console.WriteLine("Please enter your middle name if you have (optional)");
    middleName = Console.ReadLine();
    Console.WriteLine("Please enter your last name");
    lastName = Console.ReadLine();
    do
    {
        Console.WriteLine("Please enter your 11 digit identity number");
        identityNumber = Console.ReadLine();
    } while (!identityNumber.All(char.IsDigit) || identityNumber.Length != 11);

    do
    {
        Console.WriteLine("Please enter 16 digit card number");
        cardNumber = Console.ReadLine();
    } while (!cardNumber.All(char.IsDigit) || cardNumber.Length != 16);
    Console.WriteLine("Please enter normal toll");
    toll = Convert.ToDecimal(Console.ReadLine());

    switch (subCard)
    {//  "Physical Card", "Digital Card"
        case "Physical Card":
            card = new PhysicalCard(cardNumber, cardType, toll, 0, identityNumber, firstName, middleName, lastName);
            break;
        case "Digital Card":
            card = new DigitalCard(cardNumber, cardType, toll, 0, identityNumber, firstName, middleName, lastName);
            break;
    }
    Console.WriteLine("Your {0} Card created as {1}.", cardType, subCard);
    return card;
}

void ShowUserInfo(Card card)
{
    if(card is PersonalCard)
    {
        PersonalCard personalCard = (PersonalCard)card;
        Console.Write("{0} {1} {2}\n{3}\t{4}\t{5}\nNormal toll: {6}\tCredit: {7}", 
            personalCard.FirstName, personalCard.MiddleName, personalCard.LastName, 
            personalCard.CardNumber, personalCard.CardType, personalCard.IdentityNumber, 
            personalCard.Toll, personalCard.Credit);
    }
    else
    {
        AnonymousCard anonymousCard = (AnonymousCard)card;
        Console.Write("{0} {1}\nNormal toll: {2}\tCredit: {3}",
           anonymousCard.CardNumber, anonymousCard.CardType,
           anonymousCard.Toll, anonymousCard.Credit);
    }
   
}

void AddCredit(Card card)
{
    Console.WriteLine("Please enter amount of credit to add");
    decimal addCredit = Convert.ToDecimal(Console.ReadLine());
    currentCard.AddCredit(addCredit);
    credit += addCredit;
    Console.WriteLine("Credit added successfully");
    ShowUserInfo(card);
}

void CalculatePayment(Card card)
{
    if (card.CardType == CardType.Standart)
    {
        Console.WriteLine("Your card type is Standart Card, there is no discount for you.");
    }
    else
    {
        Console.WriteLine("Your card type is {0} you are taking advantage of discount.", card.CardType);
        Console.WriteLine("Normal price is {0}", card.Toll);
    }
    Console.WriteLine("You have to pay {0}", card.GetDiscountedToll());
}

void DoPayment(Card card)
{
    Console.WriteLine("Press any key to make payment");
    Console.ReadKey();
    bool status = card.Pay();
    if (status)
        Console.WriteLine("\nPayment done.");
    else
        Console.WriteLine("\nYou don't have enough credit.");
    ShowUserInfo(card);
}


Console.ReadKey();