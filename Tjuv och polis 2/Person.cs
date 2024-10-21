using System;
using System.Collections.Generic;


namespace Tjuv_och_polis_2
{
    // Bas-klassen Person
    abstract class Person
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int XDirection { get; set; }
        public int YDirection { get; set; }
        public List<string> Inventory { get; set; }

        public Person(int x, int y, int xDir, int yDir)
        {
            X = x;
            Y = y;
            XDirection = xDir;
            YDirection = yDir;
            Inventory = new List<string>();
        }

        public void Move(int cityWidth, int cityHeight)
        {
            X += XDirection;
            Y += YDirection;

            if (X < 0) X = cityWidth - 1;
            if (X >= cityWidth) X = 0;
            if (Y < 0) Y = cityHeight - 1;
            if (Y >= cityHeight) Y = 0;
        }

        public abstract char GetSymbol(); // Används för att rita ut personen i staden
    }

    class Medborgare : Person
    {
        public Medborgare(int x, int y, int xDir, int yDir) : base(x, y, xDir, yDir)
        {
            Inventory.AddRange(new List<string> { "Nycklar", "Mobiltelefon", "Pengar", "Klocka" });
        }

        public override char GetSymbol()
        {
            return 'M';
        }
    }

    class Tjuv : Person
    {
        public Tjuv(int x, int y, int xDir, int yDir) : base(x, y, xDir, yDir)
        {
            Inventory = new List<string>(); // Börjar utan stöldgods
        }

        public override char GetSymbol()
        {
            return 'T';
        }
    }

    class Polis : Person
    {
        public Polis(int x, int y, int xDir, int yDir) : base(x, y, xDir, yDir)
        {
            Inventory = new List<string>(); // Börjar utan beslagtaget
        }

        public override char GetSymbol()
        {
            return 'P';
        }
    }
}
