using System.Collections.Generic;

namespace Week5Exersice2
{
    /// <summary>
    /// Interface for Tournament
    /// </summary>
    public interface ITournament
    {
        /// <summary>
        /// Liste af spil, der skal spilles, for at alle spillere får spillet med alle 1 gang.
        /// </summary>
        List<Game> Games { get; }

        /// <summary>
        /// Liste af personer, der er med i spillet.
        /// Listen må max indeholde 3 personer i spillet.
        /// </summary>
        List<IPerson> Personer { get; }

        /// <summary>
        /// Antal spillere i spillet.
        /// Må max være 3.
        /// </summary>
        int PlayerCount { get; }

        /// <summary>
        /// Finder vinderen af turneringen.
        /// 
        /// Precondition: <see cref="PlayerCount > 2"/>
        /// Postcondition: Vinderen af turneringen er fundet. Det er én af <see cref="Personer"/>.
        /// </summary>
        void FindTurneringsVinderen();

        /// <summary>
        /// Initialiserer spillet.
        /// </summary>
        void Initialize();
    }
}