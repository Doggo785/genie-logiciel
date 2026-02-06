using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GestionMemoire
{
    public delegate void delg();

    public class CompleteDelegate
    {
        public void Run()
        {
            A aObject;
            B bObject;
            delg SimpleDeleg;
            delg[] TabDeDeleg;
            delg MulticastDeleg;
            aObject = new A();
            bObject = new B();
            SimpleDeleg = aObject.ma;
            TabDeDeleg = [aObject.ma, bObject.mb];
            MulticastDeleg = aObject.ma;
            MulticastDeleg += bObject.mb;
            aObject.ma();
            bObject.mb();
            SimpleDeleg();
            for (int i = 0; i < TabDeDeleg.Length; i++)
            {
                TabDeDeleg[i]();
            }
            MulticastDeleg();
            MulticastDeleg -= bObject.mb;
            MulticastDeleg();
        }
        
    }

    public class A
    {
        public void ma()
        {
            Console.WriteLine("ma");
        }
    }

    public class B
    {
        public void mb()
        {
            Console.WriteLine("mb");
        }
    }
}