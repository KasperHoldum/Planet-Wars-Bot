using PlanetDebug;

namespace AthenaBot
{
	/// <summary>
	/// DebugginPlugin
	/// </summary>
	public class BotDebug : botDebugBase
	{
		private PlanetWars gameboard;
	    private readonly Athena bot = new Athena();

		/// <summary>
		/// The pattern of the default bots would be to create a game board
		/// (GameWars) and then have the bot do a move on the board.
		/// I split this into two parts, here you should create the board and store it on a member.
		/// </summary>
		/// <param name="gameData">The game data.</param>
		public override void CreateGameBoardInstance(string gameData)
		{
			gameboard = new PlanetWars(gameData);
		}

		/// <summary>
		/// In CreateGameBoardInstance you have created the gameboard, now use it with your bot.
		/// </summary>
		public override void DoMove()
		{
			bot.DoTurn(gameboard);
            PlanetWars.FinishTurn();
		}
	}
}
