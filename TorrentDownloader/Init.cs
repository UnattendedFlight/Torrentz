using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TorrentDownloader
{
    class Init
    {
        public static void Starter()
        {
            DownLocation:
            Console.WriteLine();
            Console.WriteLine("Enter magnet link or .torrent location.");
            string downLoc = Console.ReadLine();
            Console.Clear();
            if (downLoc == null) { downLoc = Directory.GetCurrentDirectory(); }
            if (downLoc == "") { downLoc = Directory.GetCurrentDirectory(); }
            if (!File.Exists(downLoc))
            {
                if (Directory.Exists(downLoc))
                {
                    DirectoryInfo files = new DirectoryInfo(downLoc);
                    FileInfo torrent = files.GetFiles("*.torrent", SearchOption.AllDirectories).FirstOrDefault();
                    Console.WriteLine("Torrents found in folder:");
                    int length = 0;
                    foreach (FileInfo torre in files.GetFiles("*.torrent", SearchOption.AllDirectories))
                    {
                        int thisLength = torre.Name.Length;
                        if (length < thisLength)
                        {
                            length = thisLength;
                        }
                        
                    }
                    int lengthUno = 0;

                    while (lengthUno != length) { Console.Write("-"); lengthUno++; }
                    Console.WriteLine("--");
                    lengthUno = 0;
                    int nm = 1;
                    int fileCount = files.GetFiles("*.torrent", SearchOption.AllDirectories).Length;
                    string[] myList = new string[fileCount + 1];
                    string[] myListFull = new string[fileCount + 1];
                    foreach  (FileInfo torre in files.GetFiles("*.torrent", SearchOption.AllDirectories))
                    {
                        Console.WriteLine(nm + ". " + torre.Name);
                         
                            myList[nm] = (Path.GetFileNameWithoutExtension(torre.Name));
                            myListFull[nm++] = torre.FullName;
                    }
                    while (lengthUno != length) { Console.Write("-"); lengthUno++; }
                    Console.WriteLine("--");
                    lengthUno = 0;
                    int ch;
                    Number:
                    if (nm == 2)
                    {
                        ch = 1;

                    } else
                    {
                        if (nm-1 > 9)
                        {
                            Console.WriteLine("Enter number between 1 and " + (nm - 1));
                            string choice = Console.ReadLine();
                            try
                            {
                                ch = Convert.ToInt32(choice);
                            } catch
                            {
                                Console.WriteLine("Not a number");
                                goto Number;
                            }
                            
                        } else
                        {
                            Console.WriteLine("Enter number between 1 and " + (nm - 1));
                            ConsoleKeyInfo choice = Console.ReadKey();

                            if (char.IsDigit(choice.KeyChar))
                            {
                                ch = int.Parse(choice.KeyChar.ToString());
                            }
                            else
                            {
                                Console.WriteLine("Not a number");
                                goto Number;
                            }
                        }

                    }
                    Console.WriteLine();
                    string torrentName = myList[ch];
                    string torrentFullName = myListFull[ch];
                    Console.Clear();
                    Console.WriteLine("Torrent added: ");
                    Console.WriteLine(torrentName);
                    downLoc = "\"" + torrentFullName + "\"";
                } else if (!Directory.Exists(downLoc))
                {
                    string firstFivChar = new string(downLoc.Take(6).ToArray());
                    if (firstFivChar == "magnet")
                    {
                        Console.Clear();
                        Console.WriteLine("Magnet link added..");
                    } else if (firstFivChar == "\"magne")
                    {
                        Console.Clear();
                        Console.WriteLine("Magnet link added..");
                    } else
                    {
                        Console.WriteLine("\"" + downLoc + "\"" + " is not a valid torrent file or a magnet link.");
                        goto DownLocation;
                    }
                }
            } else if (File.Exists(downLoc))
            {
                Console.Clear();
                Console.WriteLine("Torrent file added..");
                downLoc = "\"" + downLoc + "\"";
            }
            DirectoryEnter:
            Console.WriteLine("Enter location to download to: (Press enter to download to current location)");
            string loc = Console.ReadLine();
            if (!Directory.Exists(loc))
            {
                if (loc == null) { loc = Directory.GetCurrentDirectory(); goto startDownload; }
                if (loc == "") { loc = Directory.GetCurrentDirectory(); goto startDownload; }
                Console.WriteLine(loc + " is not a directory.");
                goto DirectoryEnter;
            }
            startDownload:
            Console.WriteLine("Starting download..");
            Thread.Sleep(2000);
            Start.Download(loc, downLoc);
        }
    }
}
