using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week5Exersice2
{
    /// <summary>
    /// Klassen Game (spil).
    /// </summary>
    public class Game : IGame
    {
        /// <summary>
        /// Spiller 1 af typen <see cref="IPerson"/>
        /// </summary>
        public IPerson Spiller1 { get; set; }

        /// <summary>
        /// Spiller 2 af typen <see cref="IPerson"/>
        /// </summary>
        public IPerson Spiller2 { get; set; }

        /// <summary>
        /// Starter et spil med <paramref name="rounds"/> antal runder.
        /// (For hver runde bedes <see cref="IPerson.Spiller1"/> og <see cref="IPerson.Spiller2"/> at vælge deres ting (sten, saks eller papir), og vinderen afgøres.
        /// Antal vundne runder tælles op på Spilleren (<see cref="IPerson.WinRoundCount"/>).)
        /// Precondition: <paramref name="rounds"/> skal være større end 0.
        /// Postcondition: Hvis en Spiller har vundet, tælles WinRoundCount op på vedkommende.
        /// </summary>
        /// <param name="rounds"></param>
        public void Start(int rounds)
        {
            for (var i = 0; i < rounds; i++)
            {
                Spiller1.Vaelg();
                Spiller2.Vaelg();
                FindVinderenAfRunden();
            }
        }

        /// <summary>
        /// Finder vinderen i runden mellem <see cref="IPerson.Spiller1"/> og <see cref="IPerson.Spiller2"/>.
        /// Bliver det uafgjort, bekendtgøres dette, uden at der foretages andet.
        /// En vunden runde tælles op på Spilleren (<see cref="IPerson.WinRoundCount"/>).
        /// Precondition: To gyldige spillere af typen IPerson har foretaget et gyldigt valg.
        /// Postcondition: Hvis ikke Spiller1.Valg er lige med Spiller2.Valg, tælles vinderens Person.WinRoundCount én op.
        /// 
        /// Frame rule: WinGameCount bliver ikke berørt.
        /// Definerer scopen af metoden.
        /// </summary>
        private void FindVinderenAfRunden()
        {
            if (Spiller1.Valg != Spiller2.Valg)
            {
                if ((Spiller1.Valg < Spiller2.Valg && Spiller2.Valg - Spiller1.Valg == 1) || (Spiller1.Valg == (Valg)2 && Spiller2.Valg == 0))
                {
                    Spiller1.WinRoundCount++;
                    Console.WriteLine(Spiller1.Navn + " har vundet denne runde!");
                }
                else
                {
                    Spiller2.WinRoundCount++;
                    Console.WriteLine(Spiller2.Navn + " har vundet denne runde!");
                }
            }
            else Console.WriteLine("Det blev uafgjort.");
            Console.WriteLine();
        }

        /// <summary>
        /// Finder vinderen af spillet <see cref="Game"/>. Hvis ingen har vundet en eneste runde af de to spillere, spilles en ny runde indtil en vinder er fundet.
        /// 
        /// Precondition: <paramref name="antalRunderSpillet"/> vil p.t. altid være 5.
        /// Postcondition: Hvis en spiller har vundet flest runder, vinder vedkommende, og WinGameCount tælles én op på vedkommendes <see cref="IPerson"/>. 
        /// Hvis det bliver uafgjort, tilkendegives dette og intet tælles op.
        /// </summary>
        /// <param name="antalRunderSpillet"></param>
        public void FindVinderenAfSpillet(int antalRunderSpillet)
        {
            if (Spiller1.WinRoundCount == Spiller2.WinRoundCount && Spiller2.WinRoundCount == 0)
            {
                var uafgjort = true;
                while (uafgjort)
                {
                    Console.WriteLine(string.Format("Efter {0} uafgjorte runder spil en runde mere, for at finde en vinder.", antalRunderSpillet));
                    Console.WriteLine();
                    Start(1);
                    antalRunderSpillet++;
                    if (Spiller1.WinRoundCount != Spiller2.WinRoundCount)
                        uafgjort = false;
                }
            }
            if (Spiller1.WinRoundCount == Spiller2.WinRoundCount)
                Console.WriteLine(string.Format("Det blev uafgjort. I har begge vundet {0} runder.", Spiller2.WinRoundCount));
            else if (Spiller1.WinRoundCount > Spiller2.WinRoundCount)
            {
                Spiller1.WinGameCount++;
                Console.WriteLine(Spiller1.Navn + " har vundet spillet med " + Spiller1.WinRoundCount + " point!");
            }
            else
            {
                Spiller2.WinGameCount++;
                Console.WriteLine(Spiller2.Navn + " har vundet spillet med " + Spiller2.WinRoundCount + " point!");
            }
            Console.WriteLine();
        }
    }
}
