using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace CSBombmanServer
{
    public partial class Form1 : Form
    {
        public delegate void Update_label_delegate();
        public Update_label_delegate update_delegate;

        public System.Timers.Timer my_timer;

        private List<Player> Players;
        private List<Bomb> bombs;
        private List<Item> items;
        private List<Block> blocks;
        private List<Position> walls;
        private List<Position> fires;
        private List<string> histories;
        private MapData mapData;
        private int turn;
        private int showTurn;

        private You you;

        public Form1()
        {
            InitializeComponent();
            Text = $"C# ボムマン {Utils.VERSION}";
            NewGame();
        }

        public void fnUpdate_Label()
        {
            field.BeginInvoke(new MethodInvoker(UpdateTask));
            //Application.DoEvents();
        }


        public void DisplayTimeEvent(object source, ElapsedEventArgs e)
        {
            update_delegate.BeginInvoke(null, null);
        }

        private void AppendTextWithFire(string text)
        {
            //field.Text = text;
            field.ResetText();
            var array = text.Split('火');
            for (var i = 0; i < array.Length - 1; i++)
            {
                var s = array[i];
                if (!string.IsNullOrEmpty(s))
                    AppendText(s, field.ForeColor);

                AppendText("火", Color.Red);
            }
            AppendText(array.Last(), field.ForeColor);

            field.SelectionColor = field.ForeColor;
        }

        private void AppendText(string text, Color color)
        {
            field.SelectionStart = field.TextLength;
            field.SelectionLength = 0;

            field.SelectionColor = color;
            field.AppendText(text);
            // 後処理は呼び出し元にした
            //field.SelectionColor = field.ForeColor;
        }

        private void DisposePlayers()
        {
            Players.ForEach(p => p.Dispose());
        }

        private static Position RandomPosition()
        {
            Random rnd = new Random();
            return new Position(rnd.Next(Utils.WIDTH), rnd.Next(Utils.HEIGHT));
        }

        public void KeyDownYouUp()
        {
            if (!you.keyStates[0])
            {
                you.direction = Utils.UP;
                you.keyStates[0] = true;
            }
        }

        public void KeyDownYouDown()
        {
            if (!you.keyStates[1])
            {
                you.direction = Utils.DOWN;
                you.keyStates[1] = true;
            }
        }

        public void KeyDownYouLeft()
        {
            if (!you.keyStates[2])
            {
                you.direction = Utils.LEFT;
                you.keyStates[2] = true;
            }
        }

        public void KeyDownYouRight()
        {
            if (!you.keyStates[3])
            {
                you.direction = Utils.RIGHT;
                you.keyStates[3] = true;
            }
        }

        public void KeyDownYouSpace()
        {
            you.putBomb = true;
        }

        public void KeyUpYouUp()
        {
            you.keyStates[0] = false;
        }

        public void KeyUpYouDown()
        {
            you.keyStates[1] = false;
        }

        public void KeyUpYouLeft()
        {
            you.keyStates[2] = false;
        }

        public void KeyUpYouRight()
        {
            you.keyStates[3] = false;
        }

        private void ShowHistoryMap(int targetTurn)
        {
            ShowMap(Utils.JsonToObject<MapData>(histories[targetTurn]), histories[targetTurn]);
        }

        public void ShowMap(MapData mapData, string mapJson)
        {
            string mapString = MapToString(mapData);
            AppendTextWithFire(mapString);
            var infoBuilder = new StringBuilder();
            Players.ForEach(p =>
            {
                infoBuilder.Append(p.Name + "\r\n"
                              + "力:" + p.power + " 弾:" + p.setBombLimit
                              + " 計:" + p.totalSetBombCount
                              + "\r\n");
            });
            infoArea.Text = infoBuilder.ToString();

            Console.Write(mapString);
            Console.WriteLine(mapJson + "\n");
        }



        private void Fill2(char[,] array, char a)
        {
            for (int i = 0; i < array.GetLength(0); i++)
                for (int j = 0; j < array.GetLength(1); j++)
                    array[i, j] = a;
        }

        public string MapToString(MapData map)
        {
            char[,] mapArray = new char[Utils.HEIGHT, Utils.WIDTH];

            Fill2(mapArray, '　');

            foreach (int[] b in map.Blocks)
                mapArray[b[1], b[0]] = '□';

            foreach (Bomb b in map.Bombs)
                mapArray[b.pos.y, b.pos.x] = '●';

            foreach (Item i in map.Items)
                mapArray[i.pos.y, i.pos.x] = i.name;

            foreach (int[] f in map.Fires)
                mapArray[f[1], f[0]] = '火';

            foreach (int[] p in map.Walls)
                mapArray[p[1], p[0]] = '■';

            foreach (Player p in map.Players)
                mapArray[p.pos.y, p.pos.x] = p.ch;

            StringBuilder result = new StringBuilder();
            for (int y = 0; y < Utils.HEIGHT; y++)
            {
                for (int x = 0; x < Utils.WIDTH; x++)
                {
                    result.Append(mapArray[y, x]);
                }
                result.Append('\n');
            }
            return "Turn " + map.Turn + "\n" + result.ToString();
        }

        public void NewGame()
        {
            if (Players != null)
                DisposePlayers();

            // 同一階層か二つ上の階層でconfigファイルを探す
            string configPath = null;
            if (System.IO.File.Exists(System.IO.Path.GetFullPath("./config.config")))
                configPath = System.IO.Path.GetFullPath("./config.config");
            else if (System.IO.File.Exists(System.IO.Path.GetFullPath("../../config.config")))
                configPath = System.IO.Path.GetFullPath("../../config.config");
            else
                throw new InvalidOperationException("Please set file as \"config.config\" at the same directory.");

            string[] lines = System.IO.File.ReadAllLines(configPath);

            var tmp = new List<string>();
            lines.Take(4).ToList().ForEach(line => tmp.Add(line.Substring(line.IndexOf("=") + 1)));
            tmp.RemoveAll((s => s.Trim() == ""));

            Players = new List<Player>();
            tmp.ForEach(s => Players.Add(new ExAI(s)));

            if (Players.Count() < 4)
            {
                you = new You("あなた");
                Players.Add(you);
            }

            while (Players.Count() < 4)
            {
                Players.Add(new AIPlayer("敵"));
            }

            // プレイヤーを初期位置に移動
            // order players rondomly
            Players = Players.OrderBy(p => Guid.NewGuid()).ToList();
            for (var i = 0; i < Players.Count(); i++)
            {
                Players[i].pos = Utils.INIT_POSITIONS[i];
                Players[i].SetId(i);
            }

            foreach(var p in Players)
            {
                textArea.AppendText($"{p.Id}:{p.Name}\n");
            }

            walls = new List<Position>();
            for (int x = 0; x < Utils.WIDTH; x++)
                for (int y = 0; y < Utils.HEIGHT; y++)
                    if (Utils.MAP_ARRAY[y][x] == '■')
                        walls.Add(new Position(x, y));

            blocks = new List<Block>();
            while (blocks.Count() < 90)
            {
                Block newBlock = new Block(RandomPosition());
                if (!Utils.IsNearInitPosition(newBlock.pos)
                   && !walls.Contains(newBlock.pos)
                   && !blocks.Any(b => b.pos.Equals(newBlock.pos)))
                {
                    blocks.Add(newBlock);
                }
            }

            int index = 0;
            for (; index < Utils.ITEM_COUNT / 2; index++)
            {
                Block b = blocks[index];
                b.SetItem(new Item('力', b.pos));
            }
            for (; index < Utils.ITEM_COUNT; index++)
            {
                Block b = blocks[index];
                b.SetItem(new Item('弾', b.pos));
            }

            turn = 0;
            showTurn = 0;
            bombs = new List<Bomb>();
            items = new List<Item>();
            fires = new List<Position>();
            mapData = new MapData(turn, walls, blocks, Players, bombs, items, fires);
            histories = new List<string>();
            var mapJson = Utils.ObjectToJson(mapData);
            histories.Add(mapJson);
            ShowMap(mapData, mapJson);
            textArea.AppendText("TURN 0: ゲームが開始されました\n");

            // 最初の開始時には1秒待つ
            System.Threading.Thread.Sleep(1000);

            update_delegate = new Update_label_delegate(fnUpdate_Label);
            my_timer = new System.Timers.Timer(Utils.DEFAULT_SLEEP_TIME);
            my_timer.Elapsed += new ElapsedEventHandler(DisplayTimeEvent);
            my_timer.Start();
        }

        static List<int[]> FindFireIndex(string str)
        {
            // 配列は[0]:火のstart位置, [1]:長さ
            var result = new List<int[]>();
            int len = str.Length;
            bool found = false;
            int start = 0;
            for (int i = 0; i < len; i++)
            {
                if (str[i] == '火')
                {
                    if (!found)
                    {
                        found = true;
                        start = i;
                    }
                }
                else
                {
                    if (found)
                    {
                        result.Add(new int[] { start, i - start });
                        found = false;
                    }
                }
            }
            return result;
        }



        public void Field_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    KeyDownYouUp();
                    break;
                case Keys.Down:
                    KeyDownYouDown();
                    break;
                case Keys.Left:
                    KeyDownYouLeft();
                    break;
                case Keys.Right:
                    KeyDownYouRight();
                    break;
                case Keys.Space:
                    KeyDownYouSpace();
                    break;
                default:
                    break;
            }
        }

        public void Field_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    KeyUpYouUp();
                    break;
                case Keys.Down:
                    KeyUpYouDown();
                    break;
                case Keys.Left:
                    KeyUpYouLeft();
                    break;
                case Keys.Right:
                    KeyUpYouRight();
                    break;
                default:
                    break;
            }
        }

        private void next_Click(object sender, EventArgs e)
        {
            my_timer.Stop();
            showTurn += 1;
            if (showTurn > turn)
            {
                my_timer.Start();
            }
            else
            {
                ShowHistoryMap(showTurn);
            }
        }

        private void prev2_Click(object sender, EventArgs e)
        {
            my_timer.Stop();
            showTurn -= 10;
            if (showTurn < 0)
                showTurn = 0;

            ShowHistoryMap(showTurn);
        }


        private void prev_Click(object sender, EventArgs e)
        {
            my_timer.Stop();
            showTurn -= 1;
            if (showTurn < 0)
                showTurn = 0;

            ShowHistoryMap(showTurn);
        }

        private void next2_Click(object sender, EventArgs e)
        {
            my_timer.Stop();
            showTurn += 10;
            if (showTurn > turn)
            {
                my_timer.Start();
            }
            else
            {
                ShowHistoryMap(showTurn);
            }
        }


        private void stop_Click(object sender, EventArgs e)
        {
            my_timer.Stop();
        }

        private void play_Click(object sender, EventArgs e)
        {
            my_timer.Stop();
            my_timer.Interval = Utils.DEFAULT_SLEEP_TIME;
            Console.WriteLine("st" + my_timer.Interval);
            my_timer.Start();
        }

        private void fast_Click(object sender, EventArgs e)
        {
            my_timer.Stop();
            my_timer.Interval = Utils.DEFAULT_SLEEP_TIME * 2d / 3.0d;
            Console.WriteLine("f1" + my_timer.Interval);
            my_timer.Start();
        }

        private void faster_Click(object sender, EventArgs e)
        {
            my_timer.Stop();
            my_timer.Interval = Utils.DEFAULT_SLEEP_TIME / 3.0d;
            Console.WriteLine("f2" + my_timer.Interval);
            my_timer.Start();
        }

        private void retry_Click(object sender, EventArgs e)
        {
            my_timer.Stop();
            NewGame();
        }

        public TextBox GetTextArea()
        {
            return textArea;
        }

        public CheckBox GetStopCheckBox()
        {
            return checkBox1;
        }

        private async void UpdateTask()
        {
            // キャラクタの行動
            var jsonMapData = histories.Last();

            Func<ParallelQuery<ActionData>> asyncJob = () =>
            {
                var tasks = Players.Where(p => p.isAlive).AsParallel().Select(p => p.Action(jsonMapData));
                return tasks.AsParallel().Where(t => t.Wait((int)(my_timer.Interval * 0.8))).Select(t => t.Result);
            };

            var actions = await Task.Run(asyncJob);
            foreach (var action in actions)
            {
                EvalPutBombAction(action);
                EvalMoveAction(action);
            }

            turn += 1;
            showTurn = turn;

            // 壁が落ちてくる
            if (turn >= 360)
            {
                int i = turn - 360;
                if (i < Utils.FALLING_WALL.Length)
                {
                    Position p = new Position(Utils.FALLING_WALL[i][0], Utils.FALLING_WALL[i][1]);
                    walls.Add(p);
                    blocks.RemoveAll(block => block.pos.Equals(p));
                    items.RemoveAll(item => item.pos.Equals(p));
                    bombs.RemoveAll(b =>
                    {
                        if (!b.pos.Equals(p))
                            return false;

                        b.owner.setBombCount--;
                        return true;
                    });
                }
            }

            // if (turn == 600) {
            //     int max = 0;
            //     Player winner = Players.get(0);
            //     for(Player p : Players){
            //         if (max < p.totalSetBombCount) {
            //             max = p.totalSetBombCount;
            //             winner = p;
            //         }
            //     }
            //     final Player winner2 = winner;
            //     Players.forEach(p->{
            //             if (!(p == winner2)){
            //                 p.ch = '墓';
            //                 p.isAlive = false;
            //             }
            //         });
            // }

            foreach (Bomb b in bombs)
                b.timer -= 1;

            // get item
            List<Item> usedItems = new List<Item>();
            foreach (Player p in Players)
            {
                foreach (Item i in items)
                {
                    if (p.pos.Equals(i.pos))
                    {
                        i.Effect(p);
                        usedItems.Add(i);
                    }
                }
            }
            items.RemoveAll(i => usedItems.Contains(i));

            // bomb explosion
            fires = new List<Position>();
            List<Bomb> explodeBombs = new List<Bomb>();
            foreach (Bomb b in bombs)
            {
                if (b.timer <= 0)
                    explodeBombs.Add(b);
            }
            // chaining
            while (explodeBombs.Count() != 0)
            {
                explodeBombs.ForEach(b => b.owner.setBombCount -= 1);
                fires.AddRange(Explodes(explodeBombs));
                bombs.RemoveAll(b => explodeBombs.Contains(b));
                explodeBombs = new List<Bomb>();
                foreach (Bomb b in bombs)
                    foreach (Position p in fires)
                    {
                        if (b.pos.Equals(p))
                        {
                            explodeBombs.Add(b);
                            break;
                        }
                    }
            }
            fires = RemoveDuplicates(fires);

            // item burning
            items.RemoveAll(i => fires.Contains(i.pos));

            // block burning
            var burningBlocks = blocks.Where(b => fires.Contains(b.pos));
            items.AddRange(burningBlocks.Where(b => b.GetItem() != null).Select(b => b.GetItem()));
            blocks.RemoveAll(b => burningBlocks.Contains(b));

            foreach(var p in Players)
            {
                if (!p.isAlive)
                    continue;
                if (fires.Contains(p.pos) || walls.Contains(p.pos))
                {
                    p.ch = '墓';
                    p.isAlive = false;
                }
            }

            mapData = new MapData(turn, walls, blocks, Players, bombs, items, fires);
            var mapJson = Utils.ObjectToJson(mapData);
            histories.Add(mapJson);

            ShowMap(mapData, mapJson);

            var living = Players.Where(p => p.isAlive);
            if (living.Count() == 1)
            {

                textArea.AppendText("TURN " + turn + " "
                                + living.First().Name
                                + "の勝ちです！\n");
                if (checkBox1.Checked)
                {
                    my_timer.Stop();
                }


                // debugようこーど
                // try{
                //     Thread.sleep(5000);
                //     newGame();
                // }catch(InterruptedException e){}
            }
            else if (living.Count() == 0)
            {
                textArea.AppendText("引き分けです！\n");
                if (checkBox1.Checked)
                {
                    my_timer.Stop();
                }

                // debugようこーど

                // try{
                //     Thread.sleep(5000);
                //     newGame();
                // }catch(InterruptedException e){}
            }
        }

        private void EvalPutBombAction(ActionData action)
        {
            try
            {
                //Console.WriteLine(action.ToString());
                Player p = action.p;
                if (!action.message.Equals(""))
                    textArea.AppendText(action.p.Name + "「" + action.message + "」\n");

                if (action.putBomb)
                {
                    Bomb bomb = new Bomb(p);
                    bool existingBomb = bombs.Any(b => b.pos.Equals(bomb.pos));

                    if (!existingBomb
                        && p.CanSetBomb())
                    {
                        p.setBombCount += 1;
                        p.totalSetBombCount += 1;
                        bombs.Add(bomb);
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine(action.p.Name + ": Invalid Action");
            }
        }

        private void EvalMoveAction(ActionData action)
        {
            Player p = action.p;

            Position nextPos = null;
            switch (action.dir)
            {
                case "UP":
                    nextPos = new Position(p.pos.x, p.pos.y - 1);
                    break;
                case "DOWN":
                    nextPos = new Position(p.pos.x, p.pos.y + 1);
                    break;
                case "LEFT":
                    nextPos = new Position(p.pos.x - 1, p.pos.y);
                    break;
                case "RIGHT":
                    nextPos = new Position(p.pos.x + 1, p.pos.y);
                    break;
            }

            if (nextPos != null
                && !walls.Contains(nextPos)
                && !blocks.Any(b => b.pos.Equals(nextPos))
                && !bombs.Any(b => b.pos.Equals(nextPos)))
            {
                p.pos = nextPos;
            }
        }

        private List<Position> Explodes(List<Bomb> bombs)
        {
            return bombs.SelectMany(b => Explode(b)).ToList();
        }

        private List<Position> RemoveDuplicates(List<Position> list)
        {
            return list.Distinct().ToList();
        }

        private List<Position> Explode(Bomb bomb)
        {
            var result = new List<Position>();
            result.Add(bomb.pos);
            result.AddRange(Rec("up", 1, bomb.power, bomb));
            result.AddRange(Rec("down", 1, bomb.power, bomb));
            result.AddRange(Rec("left", 1, bomb.power, bomb));
            result.AddRange(Rec("right", 1, bomb.power, bomb));
            return result;
        }

        private List<Position> Rec(string dir, int p, int power, Bomb bom)
        {
            List<Position> result = new List<Position>();
            while (p <= power)
            {
                Position tmpPos = (dir == "up") ? new Position(bom.pos.x, bom.pos.y - p) :
                    (dir == "down") ? new Position(bom.pos.x, bom.pos.y + p) :
                    (dir == "left") ? new Position(bom.pos.x - p, bom.pos.y) :
                    new Position(bom.pos.x + p, bom.pos.y);
                if (walls.Contains(tmpPos))
                {
                    break;
                }
                else if (blocks.Any(b => b.pos.Equals(tmpPos)) || items.Any(i => i.pos.Equals(tmpPos)))
                {
                    result.Add(tmpPos);
                    break;
                }
                else
                {
                    result.Add(tmpPos);
                    p += 1;
                }
            }
            return result;
        }

    }
}
