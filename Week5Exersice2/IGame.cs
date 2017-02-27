namespace Week5Exersice2
{
    public interface IGame
    {
        /// <summary>
        /// Spiller 1 af typen <see cref="IPerson"/>.
        /// </summary>
        IPerson Spiller1 { get; set; }

        /// <summary>
        /// Spiller 2 af typen <see cref="IPerson"/>.
        /// </summary>
        IPerson Spiller2 { get; set; }

        /// <summary>
        /// Starter et spil med <paramref name="rounds"/> antal runder.
        /// (For hver runde bedes <see cref="IPerson.Spiller1"/> og <see cref="IPerson.Spiller2"/> at vælge deres ting (sten, saks eller papir), og vinderen afgøres.
        /// Antal vundne runder tælles op på Spilleren (<see cref="IPerson.WinRoundCount"/>).)
        /// Precondition: <paramref name="rounds"/> skal være større end 0.
        /// Postcondition: Hvis en Spiller har vundet, tælles WinRoundCount op på vedkommende.
        /// </summary>
        /// <param name="rounds"></param>
        void Start(int rounds);

        /// <summary>
        /// Finder vinderen af spillet <see cref="Game"/>. Hvis ingen har vundet en eneste runde af de to spillere, spilles en ny runde indtil en vinder er fundet.
        /// 
        /// Precondition: <paramref name="antalRunderSpillet"/> vil p.t. altid være 5.
        /// Postcondition: Hvis en spiller har vundet flest runder, vinder vedkommende, og WinGameCount tælles én op på vedkommendes <see cref="IPerson"/>. 
        /// Hvis det bliver uafgjort, tilkendegives dette og intet tælles op.
        /// </summary>
        /// <param name="antalRunderSpillet"></param>
        void FindVinderenAfSpillet(int antalRunderSpillet);
    }
}