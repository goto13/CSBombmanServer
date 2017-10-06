﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSBombmanServer
{
    public class ExAI : Player
    {
        // TODO このデータの受け渡しは本来どうやればいいのだろう。
        [NonSerialized]
        System.IO.StreamWriter writer;
        [NonSerialized]
        System.IO.StreamReader reader;
        [NonSerialized]
        System.IO.StreamReader errorReader;
        [NonSerialized]
        Process proc;

        public ExAI(string command) : base("未接続")
        {
            try
            {
                proc = ProcessStart(command);
                reader = (proc.StandardOutput);
                writer = (proc.StandardInput);
                errorReader = (proc.StandardError);

                // 標準エラー出力はサーバの標準出力に垂れ流す
                //new Thread(new ThreadStart(ThreadFunction)).Start();
                Name = reader.ReadLine();
                ch = Name.ToCharArray()[0];
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                ch = '落';
                isAlive = false;
            }
        }

        static Process ProcessStart(string cmd)
        {
            Process process = new Process();
            ProcessStartInfo psi = new ProcessStartInfo(cmd);
            psi.UseShellExecute = false;
            psi.RedirectStandardInput = true;
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardError = true;
            psi.StandardOutputEncoding = Encoding.UTF8;
            psi.StandardErrorEncoding = Encoding.UTF8;
            process.StartInfo = psi;
            process.Start();
            return process;
        }

        void ThreadFunction()
        {
            try
            {
                for (string line = errorReader.ReadLine();
                    line != null;
                    line = errorReader.ReadLine())
                {
                    Console.WriteLine(line);
                }
            }
            catch (Exception)
            {
            }
        }


        public async override Task<ActionData> Action(string mapData)
        {
            try
            {
                writer.WriteLine(mapData);
                //writer.Flush();
                // TODO どう書くのがいいのか
                var raw = await reader.ReadLineAsync();

                Console.WriteLine("RAW: " + Name + ": " + raw);
                string[] data = raw.Split(',');
                if (data.Length == 3)
                    return new ActionData(this, data[0], bool.Parse(data[1]), data[2]);
                else
                    return new ActionData(this, data[0], bool.Parse(data[1]));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                this.ch = '落';
                isAlive = false;
                return new ActionData(this, "STAY", false);
            }
        }


        public override void SetId(int value)
        {
            Id = value;
            try
            {
                writer.Write(Id + "\n");
                writer.Flush();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public override void Dispose()
        {
            // TODO この終了のところもうまくやりたい
            try
            {
                if (writer != null)
                {
                    Console.WriteLine($"{Name}との接続を切断しています。");
                    writer.Close();
                    writer.Dispose();
                }
                if (proc != null)
                {
                    Console.WriteLine($"{Name}の終了を待っています。");
                    proc.CloseMainWindow();
                    Console.WriteLine($"{Name}が終了しました。");
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
