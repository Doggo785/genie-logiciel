namespace GraphicMVVM;

public class Program
{
    public static void Main(string[] args)
    {
        ViewModel Vm = new ViewModel();
        Console.WriteLine("Enter a string to convert to uppercase:");
        string input = Console.ReadLine() ?? string.Empty;
        string result = Vm.ConvertToUpper(input);
        Console.WriteLine($"Uppercase: {result}");
    }
}