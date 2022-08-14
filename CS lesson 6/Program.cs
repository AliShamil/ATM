
namespace CS_lesson_6;



class Card
{
    private string _pin;
    private string _pan;
    private string _cvc;
    private decimal _balance;
    ExpireDate expire { get; set; }

    public Card(string pAN, string pin, string cVC, decimal balance, ExpireDate expireDate)
    {

        PIN = pin;
        PAN = pAN;
        CVC = cVC;
        Balance=balance;
        expire=expireDate;
    }


    public string PIN
    {
        get { return _pin; }
        set
        {

            if (value.Length == 4)
            {
                if (value.All(char.IsDigit))
                    _pin=value;
                else
                    throw new Exception("PIN code is must be only digit!");
            }
            else
                throw new Exception("PIN Code must be 4 digit!");

        }
    }

    public string PAN
    {
        get { return _pan; }
        set
        {
            if (value.Length == 16)
            {
                if (value.All(char.IsDigit))
                    _pan=value;
                else
                    throw new Exception("Pan code is must be only digit!");
            }
            else
                throw new Exception("PAN Code must be 16 digit!");

        }
    }

    public string CVC
    {
        get { return _cvc; }
        set
        {
            if (value.Length == 3)
            {
                if (value.All(char.IsDigit))
                    _cvc=value;
                else
                    throw new Exception("CVC code is must be only digit!");
            }
            else
                throw new Exception("CVC Code must be 3 digit!");

        }
    }
    public decimal Balance
    {
        get { return _balance; }
        set
        {

            if (_balance<0)
                _balance = 0;
            else
                _balance = value;

        }
    }




    public override string ToString()
    {
        return $@"PAN: {PAN}
PIN: {PIN}
Expire Date: {expire}
CVC: {CVC}
Balance: {Balance} AZN";
    }

}

public struct ExpireDate
{

    private int _month;
    private int _year;


    public ExpireDate()
    {
        _month = 1;
        _year = 2000;
    }

    public int Month
    {
        get { return _month; }
        set
        {
            if (value>0 && value <= 12)
                _month = value;
            else
                throw new Exception("Month must be 1 - 12 rane!");

        }
    }

    public int Year
    {
        get { return _year; }
        set
        {
            if (value >= 2000 && value <=2999)
                _year = value;
            else
                throw new Exception("Year must be greater than 1999 !");
        }
    }

    public ExpireDate(int month, int year) : this()
    {
        Month=month;
        Year=year;
    }





    public override string ToString()
    {
        return $"{Month}/{Year - 2000}";
    }
}
class User
{

    public string Name { get; set; }
    public string Surname { get; set; }
    public Card card { get; set; }


    public User(string name, string surname, Card card)
    {
        Name=name;
        Surname=surname;
        this.card=card;
    }


    public override string ToString()
    {
        return $@"Name: {Name}
Surname: {Surname}
 
~Card Info

{card}";
    }
}
internal class Program
{
    #region Helper_Func

    static User checkPAN(string pan, User[] users)
    {
        foreach (var user in users)
        {
            if (user.card.PAN == pan)
                return user;
        }
        return null!;
    }


    static bool checkPIN(string pin, User user)
    {
        if (pin == user.card.PIN)
            return true;
        return false;
    }



    static bool checkCash(decimal money, User user)
    {
        if (money <= user.card.Balance)
            return true;
        return false;
    }



    static void Add(string element, ref string[] arr)
    {
        if (arr == null)
        {
            arr = new string[0];
        }

        string[] newArray = new string[arr.Length + 1];
        int i;
        for (i = 0; i < arr.Length; i++)
        {
            newArray[i] = arr[i];
        }
        newArray[i] = element;
        arr = newArray;
    }


    #endregion

    static void Main()
    {
        User[] users = {
            new User("Ali","Shamil",new Card("4512789635547885","2005","451",4500,new ExpireDate(06,2024))),
            new User("Velma","Mckenzie",new Card("5258893243342214","9339","272",120,new ExpireDate(05,2025))),
            new User("Fatima","Strickland",new Card("4929668726834130","8854","562",1500,new ExpireDate(07,2023))),
            new User("Thane","Nunez",new Card("5476925452494969","6698","340",215,new ExpireDate(12,2025))),
            new User("Basil","Davidson",new Card("5567452234376653","4544","780",123.5m,new ExpireDate(04,2022))),
        };

        string[] previousOP = new string[] { };

        bool start = false;
        while (!start)
        {
            string? choice;
            bool enter = false;
            User currentUser;
            User otherUser;
            decimal cash = 0;

            while (!enter)
            {
                Console.Write("\nEnter PAN code: ");
                choice = Console.ReadLine();
                currentUser=checkPAN(choice!, users);

                if (currentUser != null)
                {
                    Console.Write("\nEnter PIN code: ");
                    choice = Console.ReadLine();
                    if (checkPIN(choice!, currentUser))
                    {
                        Console.Clear();
                        Console.WriteLine($"\n\n\n\n\t\t\t\tWELCOME {currentUser.Name} {currentUser.Surname} ");
                        Thread.Sleep(1500);
                        bool atmServices = false;
                        while (!atmServices)
                        {
                            bool cashOp = false;
                            Console.Clear();
                            Console.Write(@"
1. Balance
2. Cash
3. Previous Operations
4. Card to card
5. Exit

Pls select: ");
                            switch (Console.ReadLine())
                            {
                                case "1":
                                    Console.Clear();
                                    Console.WriteLine($"Your balance: {currentUser.card.Balance} AZN");
                                    Console.Write("\nEnter any key for back to menu...");
                                    Console.ReadKey(false);
                                    Add($"Show Balance {DateTime.UtcNow.ToString()}", ref previousOP);

                                    break;

                                case "2":
                                    Console.Clear();



                                    while (!cashOp)
                                    {
                                        Console.Clear();
                                        Console.Write(@"
1. 10 AZN
2. 20 AZN
3. 50 AZN
4. 100 AZN
5. Other 
0. Back

Pls select: ");
                                        switch (Console.ReadLine())
                                        {
                                            case "1":
                                                cash = 10;
                                                cashOp = true;
                                                break;
                                            case "2":
                                                cash = 20;
                                                cashOp = true;
                                                break;
                                            case "3":
                                                cash = 50;
                                                cashOp = true;
                                                break;
                                            case "4":
                                                cash = 100;
                                                cashOp = true;
                                                break;
                                            case "5":
                                                Console.Write("Enter other price:");
                                                try
                                                {
                                                    if (!decimal.TryParse(Console.ReadLine(), out cash))
                                                        throw new ArgumentException("\n\n\n\n\t\t\t\tUnknown command!");
                                                }
                                                catch (Exception ex)
                                                {

                                                    Console.WriteLine(ex.Message);

                                                    Console.ReadKey(false);
                                                    Console.Clear();
                                                    continue;
                                                }
                                                cashOp = true;
                                                break;
                                            case "0":
                                                Console.Clear();
                                                cashOp = true;
                                                break;
                                            default:

                                                try
                                                {
                                                    throw new ArgumentException("\n\n\n\n\t\t\t\tUnknown command!");
                                                }
                                                catch (Exception ex)
                                                {

                                                    Console.Clear();
                                                    Console.WriteLine(ex.Message);

                                                    Thread.Sleep(1500);
                                                    continue;
                                                }


                                        }
                                    }
                                    if (checkCash(cash, currentUser))
                                    {

                                        currentUser.card.Balance -= cash;
                                        Console.Clear();
                                        Console.WriteLine("\n\n\n\n\t\t\t\tOperation is successfully!");
                                        Thread.Sleep(1500);
                                    }

                                    else
                                    {
                                        Console.Clear();
                                        Console.WriteLine("\n\n\n\n\t\t\t\tYour balance is not enough!");
                                        Thread.Sleep(1500);
                                    }
                                    Add($"Cash {DateTime.UtcNow.ToString()}", ref previousOP);
                                    break;

                                case "3":
                                    Console.Clear();
                                    foreach (var p in previousOP)
                                        Console.WriteLine(p);

                                    Add($"Show previous operation {DateTime.UtcNow.ToString()}", ref previousOP);
                                    Console.ReadKey(false);
                                    break;

                                case "4":
                                    Console.Write("\nEnter PAN code for sending: ");
                                    choice = Console.ReadLine();
                                    otherUser=checkPAN(choice!, users);
                                    try
                                    {
                                        if (otherUser == null)
                                            throw new ArgumentNullException("\n\n\n\n\t\t\t\tThis card is not found !");
                                        if (otherUser == currentUser)
                                            throw new ArgumentException("\n\n\n\n\t\t\t\tThis card is your own card!");
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.Clear();
                                        Console.WriteLine(ex.Message);

                                        Thread.Sleep(1500);
                                        continue;

                                    }
                                    while (!cashOp)
                                    {
                                        Console.Clear();
                                        Console.Write(@"
1. 10 AZN
2. 20 AZN
3. 50 AZN
4. 100 AZN
5. Other 
0. Back

Pls select: ");
                                        switch (Console.ReadLine())
                                        {
                                            case "1":
                                                cash = 10;
                                                cashOp = true;
                                                break;
                                            case "2":
                                                cash = 20;
                                                cashOp = true;
                                                break;
                                            case "3":
                                                cash = 50;
                                                cashOp = true;
                                                break;
                                            case "4":
                                                cash = 100;
                                                cashOp = true;
                                                break;
                                            case "5":
                                                Console.Write("Enter other price:");
                                                try
                                                {
                                                    if (!decimal.TryParse(Console.ReadLine(), out cash))
                                                        throw new ArgumentException("\n\n\n\n\t\t\t\tUnknown command!");
                                                }
                                                catch (Exception ex)
                                                {

                                                    Console.WriteLine(ex.Message);

                                                    Console.ReadKey(false);
                                                    Console.Clear();
                                                    continue;
                                                }
                                                cashOp = true;
                                                break;
                                            case "0":
                                                Console.Clear();
                                                cashOp = true;
                                                break;
                                            default:
                                                try
                                                {
                                                    throw new ArgumentException("\n\n\n\n\t\t\t\tUnknown command!");
                                                }
                                                catch (Exception ex)
                                                {

                                                    Console.Clear();
                                                    Console.WriteLine(ex.Message);

                                                    Thread.Sleep(1500);
                                                    continue;
                                                }

                                        }
                                    }

                                    if (checkCash(cash, currentUser))
                                    {

                                        currentUser.card.Balance -= cash;
                                        otherUser.card.Balance += cash;
                                        Console.Clear();
                                        Console.WriteLine("\n\n\n\n\t\t\t\tMoney is successfully sended to other card!");
                                        Thread.Sleep(1500);
                                    }

                                    else
                                    {
                                        Console.Clear();
                                        Console.WriteLine("\n\n\n\n\t\t\t\tYour balance is not enough!");
                                        Thread.Sleep(1500);
                                    }
                                    Add($"Card to card {DateTime.UtcNow.ToString()}", ref previousOP);
                                    break;

                                case "5":
                                    Console.Clear();
                                    Console.WriteLine($"\n\n\n\n\t\t\t\tGOOD BYE {currentUser.Name} {currentUser.Surname}\n\n\n");
                                    atmServices = true;
                                    enter = true;
                                    start = true;
                                    break;
                                default:
                                    Console.Clear();
                                    try
                                    {
                                        throw new ArgumentException("\n\n\n\n\t\t\t\tUnknown command!");
                                    }
                                    catch (Exception ex)
                                    {

                                        Console.Clear();
                                        Console.WriteLine(ex.Message);

                                        Thread.Sleep(1500);
                                        continue;
                                    }
                            }

                        }
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("\n\n\n\n\t\t\t\tYou entered wrong PIN code!");
                        continue;
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("\n\n\n\n\t\t\t\tThis PAN code not found!");
                    continue;
                }


            }



        }
    }
}