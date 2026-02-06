using System;
using System.Collections.Generic;
using System.Text;

namespace GestionMemoire
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //DelegateExercise delegateExercise = new DelegateExercise();
            //delegateExercise.Run();
            //Lambda lambda = new Lambda();
            //lambda.PrintSquare(5);
            LambdaThread lambdaThread = new LambdaThread();
            lambdaThread.Run();
        }
    }
}