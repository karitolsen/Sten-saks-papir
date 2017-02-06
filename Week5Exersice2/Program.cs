namespace Week5Exersice2
{
    using System;

    /// <summary>
    /// Sten, saks, papir spil
    /// 2 spillere, 3 valide valg.
    /// Spillet slutter, når der er fundet en vinder.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Velkommen til spillet sten-saks-papir.");
            Console.WriteLine();
            Spiller spiller1 = AddNavn("Spiller 1");
            Spiller spiller2 = AddNavn("Spiller 2");

            var ingenVinderFundet = true;
            while(ingenVinderFundet)
            {
                Vaelg(spiller1);
                Vaelg(spiller2);
                var vinder = FindVinderen(spiller1, spiller2);
                if (vinder != null)
                {
                    Console.WriteLine(vinder.Navn + " har vundet spillet!");
                    ingenVinderFundet = false;
                }
                else Console.WriteLine("Det blev uafgjort.");
            }
            Console.ReadLine();
        }

        /// <summary>
        /// Opretter en spiller med et navn.
        /// Navnet må ikke være tom eller null.
        /// </summary>
        /// <returns>En spiller med et gyldigt navn.</returns>
        private static Spiller AddNavn(string spiller)
        {
            string navn = null;
            while (navn == null || navn == "")
            {
                Console.WriteLine(spiller + ", vær venlig at indtaste dit navn: ");
                navn = Console.ReadLine();
            }
            return new Spiller(navn);
        }

        /// <summary>
        /// Tildeler <paramref name="spiller"/> et valg.
        /// Valget skal have en gyldig værdi <see cref="Spiller.Valg"/>.
        /// </summary>
        /// <param name="spiller"></param>
        private static void Vaelg(Spiller spiller)
        {
            var inputValid = false;
            while (!inputValid)
            {
                Console.WriteLine(spiller.Navn + " vælg sten (0), saks (1) eller papir (2).");
                var input = Console.ReadLine();
                var valg = -1;
                if (int.TryParse(input, out valg) && valg >= 0 && valg <= 2)
                {
                    spiller.Valg = valg;
                    inputValid = true;
                }
                else Console.WriteLine(spiller.Navn + ", din indtastede værdi er ugyldig.");
            }
        }

        /// <summary>
        /// Finder vinderen i spillet mellem <paramref name="spiller1"/> og <paramref name="spiller2"/>.
        /// </summary>
        /// <param name="spiller1">Spiller 1</param>
        /// <param name="spiller2">Spiller 2</param>
        /// <returns>Spilleren, der har vundet. Hvis uafgjort, returneres null.</returns>
        private static Spiller FindVinderen(Spiller spiller1, Spiller spiller2)
        {
            if (spiller1.Valg == spiller2.Valg) return null;
            if ((spiller1.Valg < spiller2.Valg && spiller2.Valg - spiller1.Valg == 1) || (spiller1.Valg == 2 && spiller2.Valg == 0))
                return spiller1;
            return spiller2;
        }
    }

    /// <summary>
    /// Klassen Spiller.
    /// </summary>
    public class Spiller
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="navn"></param>
        public Spiller(string navn)
        {
            Navn = navn;
        }
       
        /// <summary>
        /// Navn. 
        /// Må ikke være tom eller null.
        /// </summary>
        public string Navn { get; private set; }

        /// <summary>
        /// Valg.
        /// Må kun indeholde værdierne 0,1 eller 2. 
        /// </summary>
        public int Valg { get; set; }
    }
}
