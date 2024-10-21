using System;
using System.Collections.Generic;
using System.Threading;
 
namespace Tjuv_och_polis_2
{
    class Stad
    {
        private int Width;
        private int Height;
        private List<Person> personer;
        private Random rnd = new Random();
        private int antalRånade = 0;   // Håller koll på hur många medborgare som blivit rånade
        private int antalGripna = 0;   // Håller koll på hur många tjuvar som blivit gripna

        public Stad(int width, int height)
        {
            Width = width;
            Height = height;
            personer = new List<Person>();
        }

        public void LäggTillPerson(Person person)
        {
            personer.Add(person);
        }

        public void Uppdatera()
        {
            foreach (var person in personer)
            {
                person.Move(Width, Height);
            }

            KollaMöten();
        }

        private void KollaMöten()
        {
            for (int i = 0; i < personer.Count; i++)
            {
                for (int j = i + 1; j < personer.Count; j++)
                {
                    if (personer[i].X == personer[j].X && personer[i].Y == personer[j].Y)
                    {
                        HanteraMöte(personer[i], personer[j]);
                    }
                }
            }
        }

        private void HanteraMöte(Person a, Person b)
        {
            if (a is Polis && b is Tjuv)
            {
                PolisTarTjuv((Polis)a, (Tjuv)b);
            }
            else if (a is Tjuv && b is Medborgare)
            {
                Rån((Tjuv)a, (Medborgare)b);
            }
            else if (a is Medborgare && b is Tjuv)
            {
                Rån((Tjuv)b, (Medborgare)a);
            }
            else if ((a is Medborgare && b is Polis) || (a is Polis && b is Medborgare))
            {
                // Inget händer
                Console.WriteLine("Medborgare möter polis: Inget händer.");
            }
        }

        // Polis tar tjuven och allt stöldgods
        private void PolisTarTjuv(Polis polis, Tjuv tjuv)
        {
            if (tjuv.Inventory.Count > 0)
            {
                polis.Inventory.AddRange(tjuv.Inventory);
                tjuv.Inventory.Clear();
                Console.WriteLine("Polis tar tjuv och beslagtar allt stöldgods!");
            }
            else
            {
                Console.WriteLine("Polis tar tjuv, men tjuven har inget stöldgods.");
            }

            antalGripna++;  // Öka antalet gripna tjuvar
            Thread.Sleep(2000);
        }

        // Tjuven rånar medborgaren och tar en slumpmässig sak
        private void Rån(Tjuv tjuv, Medborgare medborgare)
        {
            if (medborgare.Inventory.Count > 0)
            {
                // Välj en slumpmässig sak från medborgarens inventory
                int stöldIndex = rnd.Next(medborgare.Inventory.Count);
                string stöldgods = medborgare.Inventory[stöldIndex];

                // Lägg till stöldgodset i tjuvens inventory och ta bort det från medborgarens
                tjuv.Inventory.Add(stöldgods);
                medborgare.Inventory.RemoveAt(stöldIndex);

                Console.WriteLine($"Tjuv rånar medborgaren och stjäl {stöldgods}!");
            }
            else
            {
                Console.WriteLine("Tjuv försöker råna medborgaren, men medborgaren har inget att stjäla.");
            }

            Thread.Sleep(2000); // Paus i 2 sekunder
        }

        public void RitaUtStaden()
        {
            char[,] kartan = new char[Height, Width];

            // Fyll kartan med tomma rutor (du kan ändra ' ' till '.' om du vill se tomma platser)
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    kartan[y, x] = ' '; // Använd mellanslag istället för punkt
                }
            }

            // Placera ut personerna på kartan
            foreach (var person in personer)
            {
                kartan[person.Y, person.X] = person.GetSymbol();
            }

            // Skriv ut kartan
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    Console.Write(kartan[y, x]);
                }
                Console.WriteLine();
            }
        }

        public void VisaStatistik()
        {
            Console.WriteLine();
            Console.WriteLine("=== Stadens Statistik ==="); // Visas  medans programet körs
            Console.WriteLine($"Antal rånade medborgare: {antalRånade}");
            Console.WriteLine($"Antal gripna tjuvar: {antalGripna}");
        }
    }
}