
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Globalization;
using System.Drawing;
using System.Linq;

namespace BatchRunner
{
	public class Game
	{
		MathMapper Math = new MathMapper();
		DoubleMapper Double = new DoubleMapper();
		IntegerMapper Integer = new IntegerMapper();

		// There are two modes:
		//   * If mode == 0, then s is interpreted as a filename, and the game is
		//     initialized by reading map data out of the named file.
		//   * If mode == 1, then s is interpreted as a string that contains map
		//     data directly. The string is parsed in the same way that the
		//     contents of a map file would be.
		// This constructor does not actually initialize the game object. You must
		// always call Init() before the game object will be in any kind of
		// coherent state.
		public Game(String s, int maxGameLength, int mode, String logFilename)
		{
			this.logFilename = logFilename;
			planets = new ArrayList<Planet>();
			fleets = new ArrayList<Fleet>();
			gamePlayback = new StringBuilder();
			initMode = mode;
			switch (initMode)
			{
				case 0:
					mapFilename = s;
					break;
				case 1:
					mapData = s;
					break;
				default:
					break;
			}
			this.maxGameLength = maxGameLength;
			numTurns = 0;
		}

		// Initializes a game of Planet Wars. Loads the map data from the file
		// specified in the constructor. Returns 1 on success, 0 on failure.
		public int Init()
		{
			// Delete the contents of the log file.
			if (logFilename != null)
			{
				try
				{
					//PORT: Simplified
					File.WriteAllText(logFilename, "initializing");
				}
				catch (Exception e)
				{
					// do nothing.
				}
			}

			switch (initMode)
			{
				case 0:
					return LoadMapFromFile(mapFilename);
				case 1:
					return ParseGameState(mapData);
				default:
					return 0;
			}
		}

		public void WriteLogMessage(String message)
		{
			if (logFilename == null)
			{
				return;
			}
			try
			{
				//PORT: Simplified
				File.AppendAllText(logFilename, message + Environment.NewLine);
			}
			catch (Exception e)
			{
				// whatev
			}
		}

		// Returns the number of planets. Planets are numbered starting with 0.
		public int NumPlanets()
		{
			return planets.size();
		}

		// Returns the planet with the given planet_id. There are NumPlanets()
		// planets. They are numbered starting at 0.
		public Planet GetPlanet(int planetID)
		{
			return planets.get(planetID);
		}

		// Returns the number of fleets.
		public int NumFleets()
		{
			return fleets.size();
		}

		// Returns the fleet with the given fleet_id. Fleets are numbered starting
		// with 0. There are NumFleets() fleets. fleet_id's are not consistent from
		// one turn to the next.
		public Fleet GetFleet(int fleetID)
		{
			return fleets.get(fleetID);
		}

		// Writes a string which represents the current game state. No point-of-
		// view switching is performed.
		public String toString()
		{
			return PovRepresentation(-1);
		}

		// Writes a string which represents the current game state. This string
		// conforms to the Point-in-Time format from the project Wiki.
		//
		// Optionally, you may specify the pov (Point of View) parameter. The pov
		// parameter is a player number. If specified, the player numbers 1 and pov
		// will be swapped in the game state output. This is used when sending the
		// game state to individual players, so that they can always assume that
		// they are player number 1.
		public String PovRepresentation(int pov)
		{
			StringBuilder s = new StringBuilder();
			foreach (Planet p in planets)
			{
				// We can't use String.format here because in certain locales, the ,
				// and . get switched for X and Y (yet just appending them using the
				// default toString methods apparently doesn't switch them?)
				s.Append("P " + p.X + " " + p.Y + " " + PovSwitch(pov, p.Owner) +
				  " " + p.NumShips + " " + p.GrowthRate + "\n");

			}
			foreach (Fleet f in fleets)
			{
				s.Append("F " + PovSwitch(pov, f.Owner) + " " + f.NumShips + " " +
					  f.SourcePlanet + " " + f.DestinationPlanet + " " +
					  f.TotalTripLength + " " + f.TurnsRemaining + "\n");

			}
			return s.ToString();
		}

		// Carries out the point-of-view switch operation, so that each player can
		// always assume that he is player number 1. There are three cases.
		// 1. If pov < 0 then no pov switching is being used. Return player_id.
		// 2. If player_id == pov then return 1 so that each player thinks he is
		//    player number 1.
		// 3. If player_id == 1 then return pov so that the real player 1 looks
		//    like he is player number "pov".
		// 4. Otherwise return player_id, since players other than 1 and pov are
		//    unaffected by the pov switch.
		public static int PovSwitch(int pov, int playerID)
		{
			if (pov < 0) return playerID;
			if (playerID == pov) return 1;
			if (playerID == 1) return pov;
			return playerID;
		}

		// Returns the distance between two planets, rounded up to the next highest
		// integer. This is the number of discrete time steps it takes to get
		// between the two planets.
		public int Distance(int sourcePlanet, int destinationPlanet)
		{
			Planet source = planets.get(sourcePlanet);
			Planet destination = planets.get(destinationPlanet);
			double dx = source.X - destination.X;
			double dy = source.Y - destination.Y;
			return Math.ceil(Math.sqrt(dx * dx + dy * dy));
		}

		//Resolves the battle at planet p, if there is one.
		//* Removes all fleets involved in the battle
		//* Sets the number of ships and owner of the planet according the outcome
		private void FightBattle(Planet p)
		{

			Map<int, int> participants = new TreeMap<int, int>();
			participants.put(p.Owner, p.NumShips);

			//PORT: Deleting items from iterators is not allowed.
			//      converted to deletable for loop
			for (int fleetIndex = fleets.Count-1; fleetIndex > -1; fleetIndex--)
			{
				Fleet fl = fleets[fleetIndex];
				if (fl.TurnsRemaining == 0 && fl.DestinationPlanet == p.PlanetID)
				{
					int attackForce;
					if (participants.TryGetValue(fl.Owner, out attackForce))
					{
						participants[fl.Owner] = attackForce + fl.NumShips;
					}
					else
					{
						participants.Add(fl.Owner, fl.NumShips);
					}
					fleets.Remove(fl);
				}
			}

			Fleet winner = new Fleet(0, 0);
			Fleet second = new Fleet(0, 0);
			foreach (var f in participants)
			{
				if (f.Value > second.NumShips)
				{
					if (f.Value > winner.NumShips)
					{
						second = winner;
						winner = new Fleet(f.Key, f.Value);
					}
					else
					{
						second = new Fleet(f.Key, f.Value);
					}
				}
			}

			if (winner.NumShips > second.NumShips)
			{
				p.SetNumShips(winner.NumShips - second.NumShips);
				p.SetOwner(winner.Owner);
			}
			else
			{
				p.SetNumShips(0);
			}
		}

		// Executes one time step.
		//   * Planet bonuses are added to non-neutral planets.
		//   * Fleets are advanced towards their destinations.
		//   * Fleets that arrive at their destination are dealt with.
		public void DoTimeStep()
		{
			// Add ships to each non-neutral planet according to its growth rate.
			foreach (Planet p in planets)
			{
				if (p.Owner > 0)
				{
					p.AddShips(p.GrowthRate);
				}
			}
			// Advance all fleets by one time step.
			foreach (Fleet f in fleets)
			{
				f.TimeStep();
			}
			// Determine the result of any battles
			foreach (Planet p in planets)
			{
				FightBattle(p);
			}

			Boolean needcomma = false;
			foreach (Planet p in planets)
			{
				if (needcomma)
					gamePlayback.Append(",");
				gamePlayback.Append(p.Owner);
				gamePlayback.Append(".");
				gamePlayback.Append(p.NumShips);
				needcomma = true;
			}
			foreach (Fleet f in fleets)
			{
				if (needcomma)
					gamePlayback.Append(",");
				gamePlayback.Append(f.Owner);
				gamePlayback.Append(".");
				gamePlayback.Append(f.NumShips);
				gamePlayback.Append(".");
				gamePlayback.Append(f.SourcePlanet);
				gamePlayback.Append(".");
				gamePlayback.Append(f.DestinationPlanet);
				gamePlayback.Append(".");
				gamePlayback.Append(f.TotalTripLength);
				gamePlayback.Append(".");
				gamePlayback.Append(f.TurnsRemaining);
			}
			gamePlayback.Append(":");
			// Check to see if the maximum number of turns has been reached.
			++numTurns;
		}

		// Issue an order. This function takes num_ships off the source_planet,
		// puts them into a newly-created fleet, calculates the distance to the
		// destination_planet, and sets the fleet's total trip time to that
		// distance. Checks that the given player_id is allowed to give the given
		// order. If not, the offending player is kicked from the game. If the
		// order was carried out without any issue, and everything is peachy, then
		// 0 is returned. Otherwise, -1 is returned.
		public int IssueOrder(int playerID,
							  int sourcePlanet,
							  int destinationPlanet,
							  int numShips)
		{
			Planet source = planets.get(sourcePlanet);
			if (source.Owner != playerID ||
				numShips > source.NumShips ||
				numShips < 0)
			{
				WriteLogMessage("Dropping player " + playerID +
					". source.Owner() = " + source.Owner + ", playerID = " +
					playerID + ", numShips = " + numShips +
					", source.NumShips() = " + source.NumShips);
				DropPlayer(playerID);
				return -1;
			}
			source.RemoveShips(numShips);
			int distance = Distance(sourcePlanet, destinationPlanet);
			Fleet f = new Fleet(source.Owner,
								numShips,
								sourcePlanet,
								destinationPlanet,
								distance,
								distance);
			fleets.add(f);
			return 0;
		}

		public void AddFleet(Fleet f)
		{
			fleets.add(f);
		}

		// Behaves just like the longer form of IssueOrder, but takes a string
		// of the form "source_planet destination_planet num_ships". That is, three
		// integers separated by space characters.
		public int IssueOrder(int playerID, String order)
		{
			String[] tokens = order.Split(' ');
			if (tokens.Length != 3)
			{
				return -1;
			}
			int sourcePlanet = Integer.parseInt(tokens[0]);
			int destinationPlanet = Integer.parseInt(tokens[1]);
			int numShips = Integer.parseInt(tokens[2]);
			return IssueOrder(playerID, sourcePlanet, destinationPlanet, numShips);
		}

		// Kicks a player out of the game. This is used in cases where a player
		// tries to give an illegal order or runs over the time limit.
		public void DropPlayer(int playerID)
		{
			foreach (Planet p in planets)
			{
				if (p.Owner == playerID)
				{
					p.SetOwner(0);
				}
			}
			foreach (Fleet f in fleets)
			{
				if (f.Owner == playerID)
				{
					f.Kill();
				}
			}
		}

		// Returns true if the named player owns at least one planet or fleet.
		// Otherwise, the player is deemed to be dead and false is returned.
		public Boolean IsAlive(int playerID)
		{
			foreach (Planet p in planets)
			{
				if (p.Owner == playerID)
				{
					return true;
				}
			}
			foreach (Fleet f in fleets)
			{
				if (f.Owner == playerID)
				{
					return true;
				}
			}
			return false;
		}

		// If the game is not yet over (ie: at least two players have planets or
		// fleets remaining), returns -1. If the game is over (ie: only one player
		// is left) then that player's number is returned. If there are no
		// remaining players, then the game is a draw and 0 is returned.
		public int Winner()
		{
			List<int> remainingPlayers = new List<int>();
			foreach (Planet p in planets)
			{
				if (!remainingPlayers.Contains(p.Owner))
				{
					remainingPlayers.Add(p.Owner);
				}
			}
			foreach (Fleet f in fleets)
			{
				if (!remainingPlayers.Contains(f.Owner))
				{
					remainingPlayers.Add(f.Owner);
				}
			}
			switch (remainingPlayers.Count)
			{
				case 0:
					return 0;
				case 1:
					return remainingPlayers[0];
				default:
					return -1;
			}
		}

		// Returns the game playback string. This is a complete record of the game,
		// and can be passed to a visualization program to playback the game.
		public String GamePlaybackString()
		{
			return gamePlayback.ToString();
		}


		// Returns the playback string so far, then clears it.
		// Used for live streaming output
		public String FlushGamePlaybackString()
		{
			StringBuilder oldGamePlayback = gamePlayback;
			gamePlayback = new StringBuilder();
			return oldGamePlayback.ToString();
		}

		// Returns the number of ships that the current player has, either located
		// on planets or in flight.
		public int NumShips(int playerID)
		{
			int numShips = 0;
			foreach (Planet p in planets)
			{
				if (p.Owner == playerID)
				{
					numShips += p.NumShips;
				}
			}
			foreach (Fleet f in fleets)
			{
				if (f.Owner == playerID)
				{
					numShips += f.NumShips;
				}
			}
			return numShips;
		}

		// Gets a color for a player (clamped)
		private Brush GetColor(int player, ArrayList<Brush> colors)
		{
			if (player > colors.size())
			{
				return Brushes.Pink;
			}
			else
			{
				return colors.get(player);
			}
		}

		private Point getPlanetPos(Planet p, double top, double left,
					   double right, double bottom, int width,
					   int height)
		{
			int x = (int)((p.X - left) / (right - left) * width);
			int y = height - (int)((p.Y - top) / (bottom - top) * height);
			return new Point(x, y);
		}

		// A planet's inherent radius is its radius before being transformed for
		// rendering. The final rendered radii of all the planets are proportional
		// to their inherent radii. The radii are scaled for maximum aesthetic
		// appeal.
		private double inherentRadius(Planet p)
		{
			return Math.sqrt(p.GrowthRate + 0.5);
			//return Math.log(p.GrowthRate() + 3.0);
			//return p.GrowthRate();
		}
		//PORT: some of these are disposable under widows, which means you need to clean them up.
		//by moving them out of the render funtion they leak a lof less, only once per instance.
		Font planetFont = new Font("Sans Serif", 12, FontStyle.Bold);
		Font fleetFont = new Font("Sans serif", 18, FontStyle.Bold);
		Color bgColor = Color.FromArgb(188, 189, 172);
		Brush textColor = Brushes.Black;

		// Renders the current state of the game to a graphics object
		//
		// The offset is a number between 0 and 1 that specifies how far we are
		// past this game state, in units of time. As this parameter varies from
		// 0 to 1, the fleets all move in the forward direction. This is used to
		// fake smooth animation.
		//
		// On success, return an image. If something goes wrong, returns null.
		public void Render(int width, // Desired image width
					int height, // Desired image height
					Image bgImage, // Background image
					ArrayList<Brush> colors, // Player colors
					Graphics g)
		{
			//PORT: I simplified the rendering, I do not care if it is pretty.
			// it is too much work and you need to dispose your brushes and pens, ugh
			// Rendering context
			// Also Colors are not mutable, which make the whole Darker/Brighter thing too difficult for me.

			if (bgImage != null)
			{
				g.DrawImage(bgImage, 0, 0);
			}
			else
			{
				g.FillRectangle(Brushes.LightGray, g.ClipBounds);
			}
			// Determine the dimensions of the viewport in game coordinates.
			double top = Double.MAX_VALUE;
			double left = Double.MAX_VALUE;
			double right = Double.MIN_VALUE;
			double bottom = Double.MIN_VALUE;
			foreach (Planet p in planets)
			{
				if (p.X < left) left = p.X;
				if (p.X > right) right = p.X;
				if (p.Y > bottom) bottom = p.Y;
				if (p.Y < top) top = p.Y;
			}
			double xRange = right - left;
			double yRange = bottom - top;
			double paddingFactor = 0.1;
			left -= xRange * paddingFactor;
			right += xRange * paddingFactor;
			top -= yRange * paddingFactor;
			bottom += yRange * paddingFactor;
			Point[] planetPos = new Point[planets.size()];

			// Determine the best scaling factor for the sizes of the planets.
			double minSizeFactor = Double.MAX_VALUE;
			for (int i = 0; i < planets.size(); ++i)
			{
				for (int j = i + 1; j < planets.size(); ++j)
				{
					Planet a = planets.get(i);
					Planet b = planets.get(j);
					double dx = b.X - a.X;
					double dy = b.Y - a.Y;
					double dist = Math.sqrt(dx * dx + dy * dy);
					double aSize = inherentRadius(a);
					double bSize = inherentRadius(b);
					double sizeFactor = dist / (Math.sqrt(a.GrowthRate));
					minSizeFactor = Math.min(sizeFactor, minSizeFactor);
				}
			}
			minSizeFactor *= 1.2;
			// Draw the planets.
			int idx = 0;
			foreach (Planet p in planets)
			{
				Point pos = getPlanetPos(p, top, left, right, bottom, width,
							 height);
				planetPos[idx++] = pos;
				int x = pos.X;
				int y = pos.Y;
				double size = minSizeFactor * inherentRadius(p);
				int r = (int)Math.min(size / (right - left) * width,
									  size / (bottom - top) * height);


				int cx = x - r / 2;
				int cy = y - r / 2;
				Brush br = GetColor(p.Owner, colors);
				g.FillEllipse(br, cx, cy, r, r);

				SizeF bounds = g.MeasureString(p.NumShips.ToString(), planetFont);
				x -= (int)bounds.Width;
				y += ((int)bounds.Height) / 2;

				g.DrawString(p.NumShips.ToString(), planetFont, Brushes.DarkCyan, x, y);
				g.DrawString("[" + p.PlanetID.ToString() + "]" , planetFont, Brushes.Magenta, x + bounds.Width, y);
			}

			foreach (Fleet f in fleets)
			{
				Point sPos = planetPos[f.SourcePlanet];
				Point dPos = planetPos[f.DestinationPlanet];
				double tripProgress = 1.0 - (double)f.TurnsRemaining / (double)f.TotalTripLength;

				//PORT: Make You know what, I do not care for this 'optimization
				//Right no, the screen is totally green, still there is one fleet hidden somewhere.
				//I should notice this never disappearing fleet.
				//if (tripProgress > 0.99 || tripProgress < 0.01)
				//{
				//continue;
				//}
				double dx = dPos.X - sPos.X;
				double dy = dPos.Y - sPos.Y;
				double x = sPos.X + dx * tripProgress;
				double y = sPos.Y + dy * tripProgress;

				SizeF tSize = g.MeasureString(f.NumShips.ToString(), fleetFont);
				
				g.DrawString(f.NumShips.ToString(), fleetFont, GetColor(f.Owner, colors),
							(int)(x - tSize.Width / 2),
							(int)(y + tSize.Height / 2));
			}
		}

		// Parses a game state from a string. On success, returns 1. On failure,
		// returns 0.
		private int ParseGameState(String s)
		{
			planets.Clear();
			fleets.Clear();
			//PORT: Make WindowsCompatible
			String[] lines = s.Replace("\r\n", "\n").Split('\n');
			int planetid = 0;
			for (int i = 0; i < lines.Length; ++i)
			{
				String line = lines[i];
				int commentBegin = line.IndexOf('#');
				if (commentBegin >= 0)
				{
					line = line.Substring(0, commentBegin);
				}
				if (line.Trim().Length == 0)
				{
					continue;
				}
				String[] tokens = line.Split(' ');
				if (tokens.Length == 0)
				{
					continue;
				}
				if (tokens[0].Equals("P"))
				{
					if (tokens.Length != 6)
					{
						return 0;
					}
					double x = Double.parseDouble(tokens[1]);
					double y = Double.parseDouble(tokens[2]);
					int owner = Integer.parseInt(tokens[3]);
					int numShips = Integer.parseInt(tokens[4]);
					int growthRate = Integer.parseInt(tokens[5]);
					Planet p = new Planet(planetid++, owner, numShips, growthRate, x, y);
					planets.add(p);
					if (gamePlayback.Length > 0)
					{
						gamePlayback.Append(":");
					}
					gamePlayback.Append("" + x + "," + y + "," + owner + "," + numShips + "," + growthRate);
				}
				else if (tokens[0].Equals("F"))
				{
					if (tokens.Length != 7)
					{
						return 0;
					}
					int owner = Integer.parseInt(tokens[1]);
					int numShips = Integer.parseInt(tokens[2]);
					int source = Integer.parseInt(tokens[3]);
					int destination = Integer.parseInt(tokens[4]);
					int totalTripLength = Integer.parseInt(tokens[5]);
					int turnsRemaining = Integer.parseInt(tokens[6]);
					Fleet f = new Fleet(owner,
							numShips,
							source,
							destination,
							totalTripLength,
							turnsRemaining);
					fleets.add(f);
				}
				else
				{
					return 0;
				}
			}
			gamePlayback.Append("|");
			return 1;
		}

		// Loads a map from a test file. The text file contains a description of
		// the starting state of a game. See the project wiki for a description of
		// the file format. It should be called the Planet Wars Point-in-Time
		// format. On success, return 1. On failure, returns 0.
		private int LoadMapFromFile(String mapFilename)
		{
			try
			{
				//PORT: Simplified
				return ParseGameState(File.ReadAllText(mapFilename));
			}
			catch (Exception ex)
			{
				WriteLogMessage(ex.ToString());
				return 0;
			}
		}

		// Store all the planets and fleets. OMG we wouldn't wanna lose all the
		// planets and fleets, would we!?
		private ArrayList<Planet> planets;
		private ArrayList<Fleet> fleets;

		// The filename of the map that this game is being played on.
		private String mapFilename;

		// The string of map data to parse.
		private String mapData;

		// Stores a mode identifier which determines how to initialize this object.
		// See the constructor for details.
		private int initMode;

		// This is the game playback string. It's a complete description of the
		// game. It can be read by a visualization program to visualize the game.
		private StringBuilder gamePlayback;

		// The maximum length of the game in turns. After this many turns, the game
		// will end, with whoever has the most ships as the winner. If there is no
		// player with the most ships, then the game is a draw.
		private int maxGameLength;
		private int numTurns;

		// This is the name of the file in which to write log messages.
		private String logFilename;

		private Game(Game _g)
		{
			planets = new ArrayList<Planet>();
			foreach (Planet p in _g.planets)
			{
				planets.add((Planet)(p.clone()));
			}
			fleets = new ArrayList<Fleet>();
			foreach (Fleet f in _g.fleets)
			{
				fleets.add((Fleet)(f.clone()));
			}

			//PORT: Strings are immutable
			if (_g.mapFilename != null)
				mapFilename = _g.mapFilename;

			if (_g.mapData != null)
				mapData = _g.mapData;

			initMode = _g.initMode;
			if (_g.gamePlayback != null)
				gamePlayback = new StringBuilder(_g.gamePlayback.ToString());
			maxGameLength = _g.maxGameLength;
			numTurns = _g.numTurns;
			// Dont need to init the drawing stuff (it does it itself)
		}
		public Object clone()
		{
			return new Game(this);
		}
	}
}