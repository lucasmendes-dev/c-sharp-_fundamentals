class Program
{
    static void Main()
    {
        var calculate = new Calculate(Sum);

        Run(calculate);
    }

    static void Run(Calculate calc)
    {
        Console.WriteLine(calc(20, 30));
    }

    static int Sum(int a, int b)
    {
        return a + b;
    }
    
}

delegate int Calculate(int x, int y);

class DataStore<T>  //tipo genérico
{
    public T Value {get; set;}
}

class FileLogger : ILogger
{
    private readonly string filePath;

    public FileLogger(string filePath)
    {
        this.filePath = filePath;
    }

    public void Log(string message)
    {
        File.AppendAllText(filePath, $"{message}{Environment.NewLine}");   //Cria um novo arquivo
    }
}

class ConsoleLogger : ILogger   //aqui o 'extends' é representado por :
{
    
}

interface ILogger
{
    void Log(string message)
    {
        Console.WriteLine($"LOGGER: {message}");
    }
}

class BankAccount
{
    private string name;        //por padrão, fields de classes são privados se não colocar 'public'
    private decimal balance;
    private readonly ILogger logger;   //readonly só pode atribuir valor dentro de construtor

    public decimal Balance  //prop  
    { 
        get { return balance; }
        private set { this.balance = value; } 
    }    

    public BankAccount(string name, decimal balance, ILogger logger)     //constructor
    {
        if(string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Nome inválido", nameof(name));
        }
        if(balance < 0)
        {
            throw new Exception("Saldo não pode ser negativo");
        }
        this.name = name;
        this.balance = balance;
        this.logger = logger;
    }

    public void Deposit(decimal amount)
    {
        if(amount <= 0)
        {
            logger.Log($"Não é possível depositar {amount} na conta de {name}");
            return;
        } 
        balance += amount;              
    }
}

