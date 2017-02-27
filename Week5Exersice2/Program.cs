namespace Week5Exersice2
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Sten, saks, papir spil
    /// 1-3 spillere, best af 5, valide valg.
    /// Spillet slutter, når der er fundet en vinder.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            int BestOfFive = 5;
            Console.WriteLine("Velkommen til spillet sten-saks-papir.");
            Console.WriteLine();

            var tournament = new Tournament(GetAntalSpillere());
            foreach (var game in tournament.Games)
            {
                game.Spiller1.WinRoundCount = 0;
                game.Spiller2.WinRoundCount = 0;
                game.Start(BestOfFive);
                game.FindVinderenAfSpillet(BestOfFive);
            }
            if (tournament.Games.Count > 1)
            {
                tournament.FindTurneringsVinderen();
            }
            Console.WriteLine("Tillykke!");
            Console.ReadLine();
        }

        /// <summary>
        /// Prompter brugeren angående antal spillere i turneringen.
        /// Gyldige antal spillere er mellem 1 og 3.
        /// Postcondition: Der returneres et antal mellem 1 og 3.
        /// </summary>
        /// <returns>antal spillere i turneringen.</returns>
        private static int GetAntalSpillere()
        {
            var antalSpillere = -1;
            var antalSpillereValid = false;
            while (!antalSpillereValid)
            {
                Console.WriteLine("Hvor mange ønsker at spille? (mellem 1 og 3)");
                var input = Console.ReadLine();

                if (int.TryParse(input, out antalSpillere) && antalSpillere >= 0 && antalSpillere <= 3)
                    antalSpillereValid = true;
                else Console.WriteLine("Din indtastede værdi er ugyldig.");
            }
            return antalSpillere;
        }
    }
}
