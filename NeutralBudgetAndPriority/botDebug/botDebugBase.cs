
namespace PlanetDebug
{
	/// <summary>
	/// Base class for the 'debugging system'
	/// It has not been thought out, I just tried to create a system that
	/// would leave as much intact from the original samples as possible.
	/// .
	/// If they ever update the default bots, integrating the new version into
	/// the debugger should be as simple as possible.
	/// </summary>
	public abstract class botDebugBase
	{
		/// <summary>
		/// The pattern of the default bots would be to create a game board
		/// (GameWars) and then have the bot do a move on the board.
		/// I split this into two parts, here you should create the board and store it on a member.
		/// </summary>
		/// <param name="gameData">The game data.</param>
		public abstract void CreateGameBoardInstance(string gameData);

		/// <summary>
		/// In CreateGameBoardInstance you have created the gameboard, now use it with your bot.
		/// </summary>
		public abstract void DoMove();
	}
}
