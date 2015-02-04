
namespace Assets.Scripts.SceneManager
{
    /// <summary>
    /// Represents all of the scenes which will occur in the game
    /// </summary>
    public enum GameSceneIdentifier : int
    {
        /// <summary>
        /// The scene state of null (Before the game is initialised).
        /// </summary>
        Null,

        /// <summary>
        /// The intro scene, where the splash screen could be shown.
        /// </summary>
        Intro,

        /// <summary>
        /// The scene where the main menu should be displayed.
        /// </summary>
        MainMenu,

        /// <summary>
        /// The scene where the player can select their profile.
        /// </summary>
        SelectProfile,

        /// <summary>
        /// The scene where a new game is created.
        /// </summary>
        NewGame,

        /// <summary>
        /// The scene where a game is loaded.
        /// </summary>
        LoadGame,

        /// <summary>
        /// The scene where the game is played.
        /// </summary>
        PlayGame,

        /// <summary>
        /// The state where the game is saved.
        /// </summary>
        SaveGame,

        /// <summary>
        /// The scene where the game is quitted.
        /// </summary>
        QuitGame
    }
}