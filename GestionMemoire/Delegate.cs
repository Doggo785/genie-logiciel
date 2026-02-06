int methode(int a, int b)
{
        return a + b;
}

Callback handler = methode;

static void MethodWithCallback(int a, int b, Callback callback)
{
    Console.WriteLine("The result : " + callback(a, b) );
}

MethodWithCallback(5, 10, handler);

delegate int Callback(int a, int b);


