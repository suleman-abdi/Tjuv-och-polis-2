using System;



namespace Tjuv_och_polis_2

{

    class Program

    {
        static void Main(string[] args)

        {

            // Skapa en stad med storleken 100 x 25 

            Stad stad = new Stad(100, 25);

             // Skapa slumpmässiga personer 

            Random rnd = new Random();

            for (int i = 0; i < 10; i++)

            {

             stad.LäggTillPerson(new Polis(rnd.Next(100), rnd.Next(25), rnd.Next(-1, 2), rnd.Next(-1, 2)));

            }

            for (int i = 0; i < 13; i++)

            {

            stad.LäggTillPerson(new Tjuv(rnd.Next(100), rnd.Next(25), rnd.Next(-1, 2), rnd.Next(-1, 2)));

            }

            for (int i = 0; i < 20; i++)

            {

              stad.LäggTillPerson(new Medborgare(rnd.Next(100), rnd.Next(25), rnd.Next(-1, 2), rnd.Next(-1, 2)));

            }

              // Huvudloop: Uppdatera och rita ut staden 

            for (int i = 0; i < 100; i++) // Kör loopen 100 gånger som exempel 

            {

                Console.Clear();
                stad.RitaUtStaden();
                stad.Uppdatera();
                System.Threading.Thread.Sleep(500); // Vänta lite innan nästa uppdatering 

            }

            // Visa statistiken efter att loopen är klar 

            stad.VisaStatistik();

        }

    }

}