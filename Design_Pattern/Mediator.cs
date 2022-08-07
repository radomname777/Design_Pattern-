namespace Mediator;
interface ICenter
{
    void AddDebtor(Debtor airplane);
    void RemoveDebtor(Debtor airplane);
}


class Center : ICenter
{
    private List<Debtor> _Debtor = new();

    public void AddDebtor(Debtor airplane)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        _Debtor.Add(airplane);
        Console.WriteLine($"--- {airplane.Name} New  Debtor");
        Console.ForegroundColor = ConsoleColor.White;
    }

    public void RemoveDebtor(Debtor airplane)
    {
        if (_Debtor != null)
        {

            for (int i = 0; i < _Debtor.Count; i++)
            {
                if (_Debtor[i]==airplane)
                {
                    _Debtor.RemoveAt(i);
                    airplane.HandleMessage(airplane, "Remove");
                }
            }
        }
    }

}



abstract class Debtor
{
    public string? Code { get; set; }
    public Guid? Id { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string Work { get; set; }
    public double Debt_Money { get; set; }
    public string Duration { get; set; }

    private readonly ICenter _center;
    public ICenter Center => _center;


    protected Debtor(ICenter center)
    {
        _center = center;
    }

    protected Debtor(string? code, string? name, string? surname, string work, double debt_Money, string duration, ICenter center)
    {
        Id = Guid.NewGuid();
        Code = code;
        Name = name;
        Surname = surname;
        Work = work;
        Debt_Money = debt_Money;
        Duration = duration;
        _center = center;
    }

    public virtual void HandleMessage(Debtor airplane, string message)
    {
        if (message=="Remove")
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n{Name} Debtor ID {Id} : {message}");
            Console.ForegroundColor = ConsoleColor.White;
        }
        else Console.WriteLine($"\n{Name} Debtor ID {Id} : {message}");
    }
    public virtual void Debtor_Info()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine();
        Console.WriteLine($"Name: {Name}\t Surname {Surname}\t ID {Code}");
        Console.WriteLine($"{Work}\nDebt money {Debt_Money}\t Duration {Duration}");
        Console.ForegroundColor = ConsoleColor.White;
    }
    public abstract void RemoveDebtor();

}


class BankOfBaku : Debtor
{

    public BankOfBaku(string? name, string? surname, string work, double debt_Money, string duration, ICenter center) :base(center)
    {
        Id = Guid.NewGuid();
        Code = "Bank of Baku " + Id;
        Name = name;
        Surname = surname;
        Work = work;
        Debt_Money = debt_Money;
        Duration = duration;
    
    }
    public override void RemoveDebtor()
    {
        Center.RemoveDebtor(this);
    }

}


class YellowBank : Debtor
{
    public YellowBank(string? name, string? surname, string work, double debt_Money, string duration, ICenter center) : base(center)
    {
        Id = Guid.NewGuid();
        Code = "Yellow bank" + Id;
        Name = name;
        Surname = surname;
        Work = work;
        Debt_Money = debt_Money;
        Duration = duration;

    }


    public override void RemoveDebtor()
    {
        Center.RemoveDebtor(this);
    }

}







class Program
{
    static void Main()
    {
        ICenter center = new Center();
        BankOfBaku Deb1 = new BankOfBaku("Nihad","Axundzade", "Developer ", 2200,"12",center);
        YellowBank Deb2 = new YellowBank("Nicat", "Nesirli", "developer ", 400, "6", center);
        YellowBank Deb3 = new YellowBank("Ibrahim", "Ibrahimovic", "Developer ", 200, "4", center);
        center.AddDebtor(Deb1);
        center.AddDebtor(Deb2);
        center.AddDebtor(Deb3);
        Deb3.Debtor_Info();
        Deb1.RemoveDebtor();
        Deb2.RemoveDebtor();
    }
}