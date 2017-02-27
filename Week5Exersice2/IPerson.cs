namespace Week5Exersice2
{
    public interface IPerson
    {
        /// <summary>
        /// Navnet på personen
        /// </summary>
        string Navn { get; }

        /// <summary>
        /// Antal spil vundet af personen.
        /// </summary>
        int WinGameCount { get; set; }

        /// <summary>
        /// Antal runder vundet af personen.
        /// </summary>
        int WinRoundCount { get; set; }

        /// <summary>
        /// Valg.
        /// Må kun være af typen Valg, som svarer til int-værdierne 0,1 eller 2. 
        /// </summary>
        Valg Valg { get; set; }

        /// <summary>
        /// Tildeler personen et navn
        /// 
        /// Precondition: <paramref name="nummer"/> er > 0.
        /// Postcondition: IPersonen har et gyldigt værdi.
        /// </summary>
        /// <param name="nummer"></param>
        void SetNavn(int nummer);

        /// <summary>
        /// Tildeler Personen et valg.
        /// Valget skal have en gyldig værdi <see cref="IPerson.Valg"/>.
        /// 
        /// Precondition: Modstander.Valg = null;
        /// Postcondition: Modstander.Valg har en gyldig værdi (<see cref="IPerson.Valg"/>). 
        /// 
        /// </summary>
        void Vaelg();
    }
}