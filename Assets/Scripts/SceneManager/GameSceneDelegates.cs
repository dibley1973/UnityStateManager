
namespace Assets.Scripts.SceneManager
{
    /// <summary>
    /// Defines the expected signature for a method that can handle when a game 
    /// scene has changed.
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="e">The event data.</param>
    public delegate void GameSceneChangedHandler(object sender, GameSceneChangedEventArgs e);

    /// <summary>
    /// Defines the expected signature for a method that can handle when a game 
    /// scene is about to be changed.
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="e">The event data.</param>
    public delegate void GameSceneChangingHandler(object sender, GameSceneChangingEventArgs e);

    /// <summary>
    /// Defines the expected signature for a method that should be called if a 
    /// game scene transition fails.
    /// </summary>
    /// <param name="reason">
    /// Identifies the reason the scene transition failed.
    /// </param>
    public delegate void GameSceneTransitionFailedDelegate(string reason);
}