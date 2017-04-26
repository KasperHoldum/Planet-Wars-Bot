using System;
using System.Threading;
using System.Windows.Forms;
using System.Globalization;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;
using System.Drawing;

namespace BatchRunner
{
    /// <summary>
    /// FUCKING SHORT DISCLAIMER: Use this software any way you like.
    /// IF YOU USE IT FOR GOOD  : Give me credits, kuddos or cash.
    /// IF YOU USE IT FOR BAD   : I don't know you.
    /// .
    /// WARNING: No guarantees given.
    /// WARNING: May contain hard file or path locations.
    /// WARNING: May not run with your version of C#.
    /// WARNING: May not always make sense.
    /// .
    /// MEGAWARNING: Game results do not match original 100%, still tying to find out why.
    /// </summary>
    public partial class BotDebuggerForm : Form
    {
        /// <summary>
        /// Expected solution path, contains maps.
        /// This paths is also used as the base to launch the original engine on tab Two and Three.
        /// </summary>
        public string MapFolder
        {
            get
            {
                return Path.Combine(Properties.Settings.Default.SolutionBase, "maps");
            }
        }

        /// <summary>
        /// When rendering the game ourself, hit a brake point on this turn number.
        /// </summary>
        protected int BreakPointTurn;

        public string DebugStringForCurrentBotOnTabTwo { get; set; }

        readonly ArrayList<Brush> renderColors = new ArrayList<Brush>();

        public BotDebuggerForm()
        {
            InitializeComponent();
            //Change the player rendering colors here.
            renderColors.add(Brushes.Blue);
            renderColors.add(Brushes.Green);
            renderColors.add(Brushes.Red);

            comboBoxOpponent1.SelectedIndex = 0;
            comboBoxOpponent2.SelectedIndex = 0;

            comboBoxOpponent1.BackColor = Color.Green;
            comboBoxOpponent2.BackColor = Color.Red;

            this.SetStyle(ControlStyles.DoubleBuffer, true);

            #region Make gui behave
            cbOpponentOneOwnBot.Checked = true;
            System.Threading.Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            labelRenderDelay.DataBindings.Add("text", trackBarRenderDelay, "Value");
            textBox1.TextChanged += (sender, e) =>
            {
                if (!int.TryParse(textBox1.Text, out BreakPointTurn))
                {
                    BreakPointTurn = -1;
                }
            };
            #endregion
        }

        /// <summary>
        /// Fills the list with maps.
        /// </summary>
        /// <param name="list">The list.</param>
        private void FillListWithMaps(ListBox list)
        {
            list.BeginUpdate();
            list.Items.Clear();
            try
            {
                foreach (string file in Directory.GetFiles(MapFolder))
                {
                    list.Items.Add(Path.GetFileName(file));
                }
            }
            finally
            {
                list.EndUpdate();
            }
        }

        #region Tab One - Render the entire game ourselve so we can debug every line of it

        Game engine;
        private void ButtonPlayClick(object sender, EventArgs e)
        {
            checkBoxPause.Checked = false;
            engine = null;
            aborted = false;
            if (listBox2.SelectedItem == null)
            {
                //you forgot to chose a map, try again.
                MessageBox.Show(@"select map");
                return;
            }

            buttonPlay.Enabled = false;
            try
            {

                PlanetDebug.botDebugBase player1 = GetBotInstance(cbOpponentOneOwnBot.Checked, comboBoxOpponent1.SelectedIndex);
                PlanetDebug.botDebugBase player2 = GetBotInstance(cbOpponentTwoOwnBot.Checked, comboBoxOpponent2.SelectedIndex);
                int turnsLeft;

                if (!int.TryParse(textBoxTurns.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out turnsLeft)) { turnsLeft = 200; }

                string filename = Path.Combine(MapFolder, listBox2.SelectedItem.ToString());

                engine = new Game(filename, turnsLeft, 0, null);
                engine.Init();
                if (cbRender.Checked)
                {
                    panelRender.Refresh();
                }
                int turnsPlayed = 0;
                tabControlGameEngine.SelectedTab = tabPageRenderer;
                // I looked at the original engine, which had a lot of overhead because it was working with application instances
                // and was capturing the output. Since I have a direct instance to the bot (wall, the debug wrapper) I can skip a lot of that
                // and just use the bare essentials.
                while (turnsLeft > 0 && engine.Winner() < 0)
                {
                    if (checkBoxBreakpoint.Checked && BreakPointTurn == turnsPlayed)
                    {
                        MessageBox.Show(@"Breakpoint");
                    }

                    string gameboard = engine.PovRepresentation(1);
                    string mirroredGameBoard = engine.PovRepresentation(2);
                    GetPlayerMoves(player1, 1, engine, gameboard);
                    GetPlayerMoves(player2, 2, engine, mirroredGameBoard);

                    if (!engine.IsAlive(1))
                    {
                        engine.DropPlayer(2);
                    }
                    if (!engine.IsAlive(2))
                    {
                        engine.DropPlayer(3);
                    }

                    turnsLeft--;
                    if (aborted || !(engine.IsAlive(1) && engine.IsAlive(2)))
                    {
                        break;
                    }

                    engine.DoTimeStep();

                    #region Rendering and pausing
                    turnsPlayed++;
                    labelTurn.Text = turnsPlayed.ToString();
                    Application.DoEvents();

                    if (cbRender.Checked)
                    {
                        panelRender.Refresh();
                        Application.DoEvents();
                        System.Threading.Thread.Sleep(trackBarRenderDelay.Value);
                    }
                    if (checkBoxPause.Checked)
                    {
                        while (checkBoxPause.Checked)
                        {
                            Application.DoEvents();
                        }
                    }

                    #endregion
                }
            }
            finally
            {
                buttonPlay.Enabled = true;
            }
        }

        static readonly char[] Newline = new char[] { '\n' };
        /// <summary>
        /// Gets the player moves.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <param name="playerId">The player id.</param>
        /// <param name="engine">The engine.</param>
        /// <param name="gameBoard">The game board.</param>
        private static void GetPlayerMoves(PlanetDebug.botDebugBase player, int playerId, Game engine, string gameBoard)
        {
            TextWriter old = Console.Out;
            using (ConsoleRedirect consoleRedirect = new ConsoleRedirect(Console.Out))
            {
                try
                {
                    //Catch output of the bot.
                    Console.SetOut(consoleRedirect);

                    player.CreateGameBoardInstance(gameBoard);
                    player.DoMove();

                    string allMoves = consoleRedirect.ToString().Replace("go\r\n", "");
                    //Windows & Unix compatability.
                    allMoves = allMoves.Replace("\r\n", "\n");

                    //submit moves.
                    foreach (string move in allMoves.Split(Newline, StringSplitOptions.RemoveEmptyEntries))
                    {
                        engine.IssueOrder(playerId, move);
                    }
                }
                finally
                {
                    Console.SetOut(old);
                }
            }
        }

        /// <summary>
        /// Gets the bot instance.
        /// Change this method any way you like, I choose the simples form.
        /// Just including all the required name spaces and just create an instance of the desired bot.
        /// It would be NICE to make it more configurable, where you just select an assembly and 
        /// type name and this method would create the instance. (Lot of more work too.)
        /// </summary>
        /// <param name="ownBot">if set to <c>true</c> [own bot].</param>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        private static PlanetDebug.botDebugBase GetBotInstance(bool ownBot, int index)
        {
            if (ownBot)
            {
                return new AthenaBot.BotDebug();
            }

            switch (index)
            {
                default:
                case 0: return new BullyBotDebugger.botDebug();
                case 1: return new DualBotDebugger.botDebug();
                case 2: return new ProspectorBotDebugger.botDebug();
                case 3: return new RageBotDebugger.botDebug();
                case 4: return new RandomBotDebugger.botDebug();
            }
        }

        #endregion


        #region Tab two - launching selected bot in a map with the default renderer
        private void Button3Click(object sender, EventArgs e)
        {
            aborted = true;
            allBotsPassed = false;
        }

        private void Button1Click(object sender, EventArgs e)
        {
            RunBot("PlanetWarsBot.exe");
        }

        private void Button4Click(object sender, EventArgs e)
        {
            RunBot("DualBot.exe");
        }

        private void Button5Click(object sender, EventArgs e)
        {
            RunBot("ProspectorBot.exe");
        }

        private void Button6Click(object sender, EventArgs e)
        {
            RunBot("RageBot.exe");
        }

        private void Button7Click(object sender, EventArgs e)
        {
            RunBot("RandomBot.exe");
        }

        private void RunBot(string opponent)
        {
            if (listBox1.SelectedItem != null)
            {
                aborted = false;
                string file = Path.Combine(Properties.Settings.Default.SolutionBase, listBox1.SelectedItem.ToString());
                StartMatch(file, opponent, true);
            }
        }

        private void Button9Click(object sender, EventArgs e)
        {
            FillListWithMaps(listBox1);
        }
        #endregion


        #region Tab Three, Play all maps, remove all maps where we could beat all bots.

        bool aborted;
        bool allBotsPassed = true;
        private void Button2Click(object sender, EventArgs e)
        {
            string[] opponents = new string[] { "ProspectorBot.exe", "BullyBot.exe", "DualBot.exe", "RageBot.exe", "RandomBot.exe" };

            textBoxLost.Text = "";
            aborted = false;
            button2.Enabled = false;

            try
            {
                foreach (string file in Directory.GetFiles(MapFolder))
                {
                    if (aborted)
                        break;
                    allBotsPassed = true;

                    string file1 = file;
                    //System.Threading.Tasks.Parallel.ForEach(opponents, s => StartMatch(file1, s, false));
                    foreach (string opponent in opponents)
                    {
                        StartMatch(file, opponent, false);
                    }

                    textBoxLost.Lines = linesLost.ToArray();
                    textBoxWin.Lines = linesWon.ToArray();
                    textBoxDraw.Lines = linesDrawn.ToArray();

                    labelWins.Text = @"Wins  : " + linesWon.Count;
                    labelLosses.Text = @"Losses: " + linesLost.Count;
                    labelDraw.Text = @"Draws : " + linesDrawn.Count;

                    //if (allBotsPassed)
                    //{
                    //    string lastPart = Path.Combine(MapFolder, "won\\" + Path.GetFileName(file));
                    //    File.Move(file, lastPart);
                    //}
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                button2.Enabled = true;
            }
        }

        private void StartMatch(string file, string opponent, bool showVisualizer)
        {
            if (aborted) return;
            string argumentsTemplate = "-jar tools\\PlayGame.jar maps\\[MAPNAME] 200 200 \"AthenaBot.exe\" \"[OPPONENT]\"";
            ProcessStartInfo info = new ProcessStartInfo();

            if (showVisualizer)
            {
                argumentsTemplate += " | java.exe -jar tools\\ShowGame.jar";
                info.FileName = Path.Combine(Properties.Settings.Default.SolutionBase, "ShellExecuter.cmd");
                info.CreateNoWindow = false;
                info.RedirectStandardOutput = false;
                info.RedirectStandardError = false;
                info.UseShellExecute = false;
                info.ErrorDialog = true;
            }
            else
            {
                info.FileName = "java.exe";
                info.CreateNoWindow = true;
                info.RedirectStandardOutput = true;
                info.RedirectStandardError = true;
                info.UseShellExecute = false;
            }
            info.WorkingDirectory = Properties.Settings.Default.SolutionBase;

            lastTurn = string.Empty;
            string mapName = Path.GetFileName(file);
            DebugStringForCurrentBotOnTabTwo = "Map: " + mapName.PadRight(15) + " Bot: " + opponent.PadRight(15);

            string actualArguments = argumentsTemplate.Replace("[MAPNAME]", mapName);
            actualArguments = actualArguments.Replace("[OPPONENT]", opponent);

            try
            {
                if (showVisualizer)
                {
                    File.WriteAllText(info.FileName, @"java.exe " + actualArguments);
                }
                else
                {
                    info.Arguments = actualArguments;
                }
                using (Process proc = Process.Start(info))
                {
                    proc.ErrorDataReceived += ProcErrorDataReceived;
                    Application.DoEvents();
                    if (!showVisualizer)
                    {
                        proc.BeginErrorReadLine();
                        proc.StandardOutput.ReadToEnd();
                        Application.DoEvents();
                        proc.WaitForExit();
                    }

                    Application.DoEvents();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        readonly List<String> linesWon = new List<string>();
        readonly List<String> linesLost = new List<string>();
        readonly List<String> linesDrawn = new List<string>();

        string lastTurn;
        void ProcErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e.Data != null)
            {
                string data = e.Data.ToUpper();
                if (data.Contains("TURN"))
                {
                    lastTurn = e.Data.Replace("Turn ", "");
                }
                else
                {
                    if (data.Contains("DRAW"))
                    {
                        linesDrawn.Insert(0, DebugStringForCurrentBotOnTabTwo);
                        allBotsPassed = false;
                    }
                    else
                    {
                        if (data.Contains("2"))
                        {
                            int turnCount;

                            if (int.TryParse(lastTurn, out turnCount))
                            {
                                //too slow for the competition
                                if (turnCount > 180)
                                {
                                    linesWon.Insert(0, DebugStringForCurrentBotOnTabTwo + lastTurn);
                                }
                            }
                        }
                        else
                        {
                            linesLost.Insert(0, DebugStringForCurrentBotOnTabTwo + lastTurn);
                            allBotsPassed = false;
                        }
                    }
                }
            }
            Application.DoEvents();
        }
        #endregion

        #region Event handlers GAME-ENGINE


        /// <summary>
        /// RENDERING !!!
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PanelRenderPaint(object sender, PaintEventArgs e)
        {

            if (engine != null)
            {

                Bitmap b = new Bitmap(panelRender.Width, panelRender.Height);
                Graphics gfx = Graphics.FromImage(b);



                if (cbRender.Checked || checkBoxPause.Checked)
                {
                    engine.Render(panelRender.Width, panelRender.Height, null, renderColors, gfx);

                    gfx.Dispose();

                    e.Graphics.DrawImage(b, 0, 0);

                    b.Dispose();
                }
            }
        }

        private void CbOpponentTwoOwnBotCheckedChanged(object sender, EventArgs e)
        {
            comboBoxOpponent2.Enabled = !cbOpponentTwoOwnBot.Checked;
        }

        private void CbOpponentOneOwnBotCheckedChanged(object sender, EventArgs e)
        {
            comboBoxOpponent1.Enabled = !cbOpponentOneOwnBot.Checked;
        }

        private void Button11Click(object sender, EventArgs e)
        {
            FillListWithMaps(listBox2);
            tabControlGameEngine.SelectedTab = tabPageMaps;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            aborted = true;
        }

        private void Button10Click(object sender, EventArgs e)
        {
            aborted = true;
            checkBoxPause.Checked = false;
        }
        #endregion


        private bool tcpStarted = false;
        private bool stopTcp = false;
        private void button8_Click(object sender, EventArgs e)
        {
            if (!tcpStarted)
            {
                button8.Text = "Stop TCP";
                tcpStarted = true;
                stopTcp = false;
                Thread t = new Thread(TcpRunner);

                t.Start();
            }
            else
            {
                button8.Text = "Start TCP";
                stopTcp = true;

            }
        }

        private void TcpRunner()
        {
            while (!stopTcp)
            {
                Process p = new Process
                                {
                                    StartInfo =
                                        new ProcessStartInfo(
                                        @"C:\Users\Qua\Desktop\Fanny_Heirdoo Suite\startTcpMatch.bat")
                                            {
                                                CreateNoWindow = false,
                                                WorkingDirectory = @"C:\Users\Qua\Desktop\Fanny_Heirdoo Suite\",
                                                UseShellExecute = true
                                                    }
                                };

                p.Start();
                p.WaitForExit();
            }
            tcpStarted = false;
        }
    }
}
