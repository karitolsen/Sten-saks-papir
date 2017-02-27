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
    public class ComputerPerson : IPerson
    {
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
        /// Precondition: <see cref="Navn"/> = null, nummer > 0, nummer kan være hvilket som helst integer. // Weeker
        /// 
        /// Postcondition: <see cref="Navn"/> er "Computeren". // Stronger.
        /// </summary>
        public void SetNavn(int nummer)
        {
            Navn = "Computeren";
        }

        /// <summary>
        /// Tildeler <see cref="ComputerPerson"/> et valg.
        /// Da <see cref="ComputerPerson"/> er computeren, findes valget med tilfældigheds-funktionen (random).
        /// 
        /// Postcondition: Valg har en gyldig værdi (<see cref="ComputerPerson.Valg"/>). 
        /// </summary>
        void IPerson.Vaelg()
        {
            Random rnd = new Random();
            this.Valg = (Valg)rnd.Next(0, 2);
            Console.WriteLine("Computeren har valgt.");
            Console.WriteLine();
        }
    }
}
