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
                game.Spiller1.Person.WinRoundCount = 0;
                game.Spiller2.Person.WinRoundCount = 0;
                game.Start(BestOfFive);
                if (game.Spiller1.Person.WinRoundCount == game.Spiller2.Person.WinRoundCount && game.Spiller2.Person.WinRoundCount == 0)
                {
                    var uafgjort = true;
                    while (uafgjort)
                    {
                        Console.WriteLine(string.Format("Efter {0} uafgjorte runder spil en runde mere, for at finde en vinder.", BestOfFive));
                        Console.WriteLine();
                        game.Start(1);
                        BestOfFive++;
                        if (game.Spiller1.Person.WinRoundCount != game.Spiller2.Person.WinRoundCount)
                            uafgjort = false;
                    }
                }
                if (game.Spiller1.Person.WinRoundCount == game.Spiller2.Person.WinRoundCount)
                    Console.WriteLine(string.Format("Det blev uafgjort. I har begge vundet {0} runder.", game.Spiller2.Person.WinRoundCount));
                else if (game.Spiller1.Person.WinRoundCount > game.Spiller2.Person.WinRoundCount)
                {
                    game.Spiller1.Person.WinGameCount++;
                    Console.WriteLine(game.Spiller1.Person.Navn + " har vundet spillet med " + game.Spiller1.Person.WinRoundCount + " point!");
                }
                else 
                {
                    game.Spiller2.Person.WinGameCount++;
                    Console.WriteLine(game.Spiller2.Person.Navn + " har vundet spillet med " + game.Spiller2.Person.WinRoundCount + " point!");
                }
                Console.WriteLine();
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

    /// <summary>
    /// Klassen Person.
    /// </summary>
    public class Person
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public Person()
        {
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="navn"></param>
        /// <param name="isComputer"></param>
        public Person(string navn, bool isComputer)
        {
            Navn = navn;
            IsComputer = isComputer;
        }

        /// <summary>
        /// Navn. 
        /// </summary>
        public string Navn { get; private set; }

        /// <summary>
        /// Antal gange, personen med navn <see cref="Navn"/> har vundet en runde.
        /// Er altid større eller lige med nul.
        /// </summary>
        public int WinRoundCount { get; set; }

        /// <summary>
        /// Antal gange, personen med navn <see cref="Navn"/> har vundet et spil.
        /// Er altid større eller lige med nul.
        /// </summary>
        public int WinGameCount { get; set; }

        /// <summary>
        /// Sand, hvis Personen er computeren.
        /// </summary>
        public bool IsComputer { get; set; }

        /// <summary>
        /// Tildeler en Person et navn.
        /// Navnet må ikke være tom eller null.
        /// 
        /// Precondition: <see cref="Navn"/> = null;
        /// Postcondition: <see cref="Navn"/> har en gyldig værdi.
        /// </summary>
        public void AddNavn(string spiller)
        {
            string navn = null;
            while (string.IsNullOrEmpty(navn))
            {
                Console.WriteLine(spiller + ", vær venlig at indtaste dit navn: ");
                navn = Console.ReadLine();
            }
            this.Navn = navn;
        }
    }


    /// <summary>
    /// Klassen Modstander.
    /// </summary>
    public class Modstander
    {
        /// <summary>
        /// Person. 
        /// </summary>
        public Person Person { get; set; }

        /// <summary>
        /// Valg.
        /// Må kun være af typen Valg, som svarer til int-værdierne 0,1 eller 2. 
        /// </summary>
        public Valg Valg { get; set; }

        /// <summary>
        /// Tildeler <see cref="Modstander"/> et valg.
        /// Valget skal have en gyldig værdi <see cref="Modstander.Valg"/>.
        /// Hvis <see cref="Modstander"/> er computeren, findes valget med tilfældigheds-funktionen (random).
        /// 
        /// Precondition: Modstander.Valg = null;
        /// Postcondition: Modstander.Valg har en gyldig værdi (<see cref="Modstander.Valg"/>). 
        /// </summary>
        /// <param name="spiller"></param>
        public void Vaelg()
        {
            if (this.Person.IsComputer)
            {
                Random rnd = new Random();
                this.Valg = (Valg)rnd.Next(0, 2);
                Console.WriteLine("Computeren har valgt.");
                Console.WriteLine();
            }
            else
            {
                var inputValid = false;
                while (!inputValid)
                {
                    Console.WriteLine(this.Person.Navn + " vælg sten (0), saks (1) eller papir (2).");
                    var input = Console.ReadKey(true);
                    Console.WriteLine();
                    var valg = -1;
                    if (int.TryParse(input.KeyChar.ToString(), out valg) && valg >= 0 && valg <= 2)
                    {
                        this.Valg = (Valg)valg;
                        inputValid = true;
                    }
                    else Console.WriteLine(this.Person.Navn + ", din indtastede værdi er ugyldig.");
                }
            }
        }
    }

    /// <summary>
    /// Klassen Game (spil).
    /// </summary>
    public class Game
    {
        /// <summary>
        /// Spiller 1 af typen <see cref="Modstander"/>
        /// </summary>
        public Modstander Spiller1 { get; set; }

        /// <summary>
        /// Spiller 2 af typen <see cref="Modstander"/>
        /// </summary>
        public Modstander Spiller2 { get; set; }

        /// <summary>
        /// Starter et spil med <paramref name="rounds"/> antal runder.
        /// (For hver runde bedes <see cref="Modstander.Spiller1"/> og <see cref="Modstander.Spiller2"/> at vælge deres ting (sten, saks eller papir), og vinderen afgøres.
        /// Antal vundne runder tælles op på Spilleren (<see cref="Person.WinRoundCount"/>).)
        /// Precondition: <paramref name="rounds"/> skal være større end 0.
        /// </summary>
        /// <param name="rounds"></param>
        public void Start(int rounds)
        {
            for (var i = 0; i < rounds; i++)
            {
                Spiller1.Vaelg();
                Spiller2.Vaelg();
                FindVinderen();
            }
        }

        /// <summary>
        /// Finder vinderen i spillet mellem <paramref name="spiller1"/> og <paramref name="spiller2"/>.
        /// Bliver det uafgjort, bekendtgøres dette, uden at der foretages andet.
        /// En vunden runde tælles op på Spilleren (<see cref="Person.WinRoundCount"/>).
        /// Precondition: To gyldige spillere af typen Modstander.
        /// Postcondition: Hvis ikke Spiller1.Valg er lige med Spiller2.Valg, tælles vinderens Person.WinRoundCount én op.
        /// </summary>
        private void FindVinderen()
        {
                if (Spiller1.Valg != Spiller2.Valg)
                {
                    if ((Spiller1.Valg < Spiller2.Valg && Spiller2.Valg - Spiller1.Valg == 1) || (Spiller1.Valg == (Valg)2 && Spiller2.Valg == 0))
                    {
                        Spiller1.Person.WinRoundCount++;
                        Console.WriteLine(Spiller1.Person.Navn + " har vundet denne runde!");
                    }
                    else
                    {
                        Spiller2.Person.WinRoundCount++;
                        Console.WriteLine(Spiller2.Person.Navn + " har vundet denne runde!");
                    }
                }
                else Console.WriteLine("Det blev uafgjort.");
            Console.WriteLine();
        }
    }

    /// <summary>
    /// Klassen Tournament (turnering).
    /// Precondition: AntalSpillere > 0.
    /// </summary>
    public class Tournament
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="antalSpillere"></param>
        public Tournament(int antalSpillere)
        {
            PlayerCount = antalSpillere;
            Games = new List<Game>();
            Personer = new List<Person>();
            Initialize();
        }

        /// <summary>
        /// Liste af spil. Antal spil er afhængig af antalSpillere.
        /// </summary>
        public List<Game> Games { get; private set; }

        /// <summary>
        /// Liste af personer i turneringen. Vil minimum være 2 (hvoraf den ene kan være computeren).
        /// </summary>
        public List<Person> Personer { get; private set; }

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
            if (PlayerCount == 1) Personer.Add(new Person("Computeren", true));
            for(var i = 1; i <= PlayerCount; i++)
            {
                var person = new Person();
                person.AddNavn("Spiller " + i);
                Personer.Add(person);
            }
            CalculateGames();
        }

        /// <summary>
        /// Finder vinderen på hele turneringen ved at sammenligne turneringsdeltagernes <see cref="Tournament.Personer"/> <see cref="Person.WinRoundCount"/>.
        /// Personen med den højeste score vinder turneringen.
        /// </summary>
        public void FindTurneringsVinderen()
        {
            // Find vinderen på hele turneringen.
            List<Person> vindere = new List<Person>();

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
                    Games.Add(new Game { Spiller1 = new Modstander() { Person = Personer[0] }, Spiller2 = new Modstander { Person = Personer[1] } });
                    break;
                case 3:
                    for (var i = 0; i < PlayerCount; i++)
                        Games.Add(new Game { Spiller1 = new Modstander() { Person = Personer[i] }, Spiller2 = new Modstander { Person = Personer[(i + 1) % PlayerCount] } });
                    break;
                default: throw new Exception("Indtil videre kan man max spille med 3 spillere.");
            }
        }
    }

    public enum Valg
    {
        sten,
        saks,
        papir
    }
}
