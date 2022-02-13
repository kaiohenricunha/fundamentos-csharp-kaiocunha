Console.WriteLine("Digite o primeiro número:");
int input_1 = Convert.ToInt32(Console.ReadLine());
Console.WriteLine("Digite o segundo número:");
int input_2 = Convert.ToInt32(Console.ReadLine());

try
{
    int resultado = input_1 / input_2;
    Console.WriteLine(input_1 + " / " + input_2 + " = " + resultado);
} catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}