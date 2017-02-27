using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week5Exersice2
{
    /// <summary>
    /// Klassen Tournament (turnering).
    /// Precondition: AntalSpillere > 0.
    /// </summary>
    public class Tournament : ITournament
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="antalSpillere"></param>
        public Tournament(int antalSpillere)
        {
            PlayerCount = antalSpillere;
            Games = new List<Game>();
            Personer = new List<IPerson>();
            Initialize();
        }

        /// <summary>
        /// Liste af spil. Antal spil er afhængig af antalSpillere.
        /// </summary>
        public List<Game> Games { get; private set; }

        /// <summary>
        /// Liste af personer i turneringen. Vil minimum være 2 (hvoraf den ene kan være computeren).
        /// </summary>
        public List<IPerson> Personer { get; private set; }

        /// <summary>
        /// The count of players, playing the game. 
        /// PlayerCount > 0. PlayerCount er p.t. max 3.
        /// </summary>
        public int PlayerCount { get; }

        /// <summary>
        /// Initialiserer en turnering.
        /// Hver spiller skal angive et gyldigt navn (ikke tom eller null).
        /// Ud fra antal spillere (<see cref="PlayerCount"/>) beregnes hvor mange spil der kræves og hvem der spiller mod hvem.
        /// 
        /// Precondition: 
        /// Personer.Count = 0, 
        /// Games.Count = 0
        /// 
        /// Postcondition: 
        /// Personer.Count = PlayerCount ved PlayerCount > 1. Ved PlayerCount = 1, Personer.Count = 2 (1 reel person og computeren). 
        /// Games.Count = n!/(q!*(n-q)!), hvor n = antal spillere og q = 2 (det er lige meget, om A spiller mod B, eller B mod A).
        /// </summary>
        public void Initialize()
        {
            if (PlayerCount == 1)
            {
                var computerPerson = new ComputerPerson();
                computerPerson.SetNavn(0);
                Personer.Add(computerPerson);
            }

            for (var i = 1; i <= PlayerCount; i++)
            {
                var person = new HumanPerson();
                person.SetNavn(i);
                Personer.Add(person);
            }
            CalculateGames();
        }

        /// <summary>
        /// Finder vinderen på hele turneringen ved at sammenligne turneringsdeltagernes <see cref="Tournament.Personer"/> <see cref="Person.WinGameCount"/>.
        /// Personen med den højeste score vinder turneringen.
        /// 
        /// Frame rule: Objekterne i spillet bliver ikke nulstillet, efter at vinderen er blevet fundet.
        /// Dette kan give et hint til udvikleren, at dette skal ske i en anden metode, hvis dette ønskes.
        /// </summary>
        public void FindTurneringsVinderen()
        {
            // Find vinderen på hele turneringen.
            List<IPerson> vindere = new List<IPerson>();

            int max = Personer.Max(i => i.WinGameCount);
            vindere.AddRange(Personer.Where(x => x.WinGameCount == max));

            if (vindere.Count == 1)
                Console.WriteLine(vindere.First().Navn + " har vundet hele turneringen.");
            else if (!vindere.Any())
                Console.WriteLine("Der er ikke fundet en vinder i turneringen.");
            else
            {
                string s = string.Empty;
                foreach (var vinder in vindere)
                    s += vinder.Navn + " og ";
                s = s.Substring(0, s.Length - 3);
                Console.WriteLine(s + "deles om at vinde turneringen.");
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Finder de spil (Game), der er nødvendig, for at alle spillere spiller mod alle i turneringen.
        /// CalculateGames definerer modstanderne i hvert spil.
        /// </summary>
        private void CalculateGames()
        {
            switch (PlayerCount)
            {
                case 1:
                case 2:
                    Games.Add(new Game { Spiller1 = Personer[0], Spiller2 = Personer[1] });
                    break;
                case 3:
                    for (var i = 0; i < PlayerCount; i++)
                        Games.Add(new Game { Spiller1 = Personer[i], Spiller2 = Personer[(i + 1) % PlayerCount] });
                    break;
                default: throw new Exception("Indtil videre kan man max spille med 3 spillere.");
            }
        }
    }
}
