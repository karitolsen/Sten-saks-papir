using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week5Exersice2
{
    /// <summary>
    /// Klassen Person.
    /// </summary>
    public class HumanPerson : IPerson
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public HumanPerson()
        {
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="navn"></param>
        public HumanPerson(string navn)
        {
            Navn = navn;
        }

        /// <summary>
        /// Navn. 
        /// </summary>
        public string Navn { get; private set; }

        /// <summary>
        /// Valg.
        /// Må kun være af typen Valg, som svarer til int-værdierne 0,1 eller 2. 
        /// </summary>
        public Valg Valg { get; set; }

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
        /// Tildeler en Person et navn.
        /// Navnet må ikke være tom eller null.
        /// 
        /// Precondition: <see cref="Navn"/> = null, nummer > 0.
        /// 
        /// Postcondition: <see cref="Navn"/> har en gyldig værdi (ikke null eller empty).
        /// </summary>
        public void SetNavn(int nummer)
        {
            string navn = null;
            while (string.IsNullOrEmpty(navn))
            {
                Console.WriteLine(string.Format("Spiller {0}, vær venlig at indtaste dit navn: ", nummer));
                navn = Console.ReadLine();
            }
            this.Navn = navn;
        }

        /// <summary>
        /// Tildeler <see cref="HumanPerson"/> et valg.
        /// Valget skal have en gyldig værdi <see cref="HumanPerson.Valg"/>.
        /// 
        /// Postcondition: Person.Valg har en gyldig værdi (<see cref="HumanPerson.Valg"/>). 
        /// </summary>
        void IPerson.Vaelg()
        {
            var inputValid = false;
            while (!inputValid)
            {
                Console.WriteLine(Navn + " vælg sten (0), saks (1) eller papir (2).");
                var input = Console.ReadKey(true);
                Console.WriteLine();
                var valg = -1;
                if (int.TryParse(input.KeyChar.ToString(), out valg) && valg >= 0 && valg <= 2)
                {
                    this.Valg = (Valg)valg;
                    inputValid = true;
                }
                else Console.WriteLine(Navn + ", din indtastede værdi er ugyldig.");
            }
        }
    }
}
