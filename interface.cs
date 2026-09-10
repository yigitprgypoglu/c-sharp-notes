//interface hem struct hem de class icin kullanilabilir.
//interfacein icine yazdigimiz fonksiyon bir sozlesme olur ve interface i kullanicak class ya da struct in bu metoduda icinde bulundurmasi gerekir.
//istersek interface fonksiyonunu tek tek cagirabilir yada interface imizin adini girip yani mesela alttaki ornekten yola cikarak :
//-> Iprey prey = new Rabbit(); prey.Flee();
//boyle yazdigimizda bu flee fonksiyonunu icinde bulunduran butun class yada structlarimiz calisir.
using System;

class Program
{
    static void Main(string[] args)
    {
        Rabbit rabbit = new Rabbit();

        rabbit.Flee();

        Hawk hawk = new Hawk();

        hawk.Hunt();

        Console.ReadKey();
    }

    interface IPrey
    {
        void Flee();
    }

    interface IPredator
    {
      void Hunt();
    }

    class Rabbit : IPrey
    {
        public void Flee()
        {
            Console.WriteLine("The rabbit runs away...");
        }
    }

    class Fish
    {
    }

    class Hawk : IPredator
    {
      public void Hunt()
      {
        Console.WriteLine("The Hawk is searching for food.");
      }
    }
}
