using System;
using System.Diagnostics;
namespace Game
{
	public class Mario
	{
		private static int x=3,y=5;
		public string[] map;
		public string[] hidemap;
		private bool isCharaLarge=true;
		private static int jumpspeed;
		private static bool isExit = false;
		public Mario()
		{}
		private void Check()
		{
			switch(this.hidemap[y][x])
			{
				case 'A':
					isExit=true;
					this.Draw();
					Console.SetCursorPosition(x,y);
					Console.Write("A");
					Console.SetCursorPosition(x,y-1);
					Console.Write("A");
					Console.SetCursorPosition(Console.WindowWidth/2-3,Console.WindowHeight/2);
					Console.Write("GameOver");
					Console.ReadKey();
					break;
			}
		}
		private void move(int h)
		{
			switch(h)
			{
				case 1:// <<
					x--;
					if(x<1)
						x=1;
					if(this.hidemap[y][x]=='-')
						x++;
					break;
				case 2:// >>
					x++;
					if(x>(this.hidemap[0].Length-2))
						x=this.map[0].Length-2;
					if(this.map[y][x]=='-')
						x--;
					break;
				case 3:// A
					if(y<(this.hidemap.Length-1))
					{
						if(this.map[y+1][x]=='-')
						jumpspeed = 2;
					}
					break;
			}
			this.Draw();

		}
		public void Run()
		{
			Console.CursorVisible=false;
			Stopwatch sw = new Stopwatch();
			sw.Start();
			while(!isExit)
			{
				if(Console.KeyAvailable)
				{
					ConsoleKeyInfo kb = Console.ReadKey(true);
					switch(kb.Key)
					{
						case ConsoleKey.LeftArrow:
							move(1);
							break;
						case ConsoleKey.RightArrow:
							move(2);
							break;
						case ConsoleKey.UpArrow:
							move(3);
							break;
						case ConsoleKey.Escape:
							isExit=true;
							Console.CursorVisible = true;
							break;
					}
				}
				TimeSpan ts = sw.Elapsed;
				if(ts.TotalSeconds>=0.2)
				{
					for(int a = System.Math.Abs(jumpspeed);a>-1;a--)
					{
						if(jumpspeed<0)
						{
						if(y<(this.map.Length-1))
						{
							if(this.map[y+1][x]=='-')
								y--;
						}
							y++;
						}
						if(jumpspeed>0)
						{
						if(y>0)
						{
							if(this.map[y-1][x] =='-')
								y++;
						}
							y--;
						}
					}
					jumpspeed--;
					isCharaLarge=!isCharaLarge;
					this.Draw();
					sw.Reset();
					sw.Start();
				} 
				Check();
			}
		}
		public void Draw()
		{
			Console.SetCursorPosition(0,0);
			foreach(string a in this.map)
			{
				Console.WriteLine("{0}",a);
			}
			Console.SetCursorPosition(x,y);
			if(isCharaLarge)
				Console.Write("O");
			if(!isCharaLarge)
				Console.Write("o");
		}
	}
	public class Program
	{
		public static void Main()
		{
			Console.Clear();
			Game.Mario[] mario = new Game.Mario[3];
			mario[0] = new Game.Mario();
			mario[0].map = new string[]{
				"+----------------------------------------+",
				"|                                        |",
				"|                                        |",
				"|                                        |",
				"|                                        |",
				"|                                        |",
				"|                                        |",
				"|                                        |",
				"|                                        |",
				"|                                        |",
				"|                                        |",
				"|                                        |",
				"|                                        |",
				"|                                        |",
				"|                                        |",
				"|                                        |",
				"+----------------------------------------+",};
			mario[0].hidemap = new string[]{
				"+----------------------------------------+",
				"|                                        |",
				"|                                        |",
				"|                                        |",
				"|                                        |",
				"|                                        |",
				"|                                        |",
				"|                                        |",
				"|                                        |",
				"|                                        |",
				"|                                        |",
				"|                                        |",
				"|                                        |",
				"|                                        |",
				"|                                        |",
				"|                    AAAAAAAAAAAAAAAAAAAA|",
				"+----------------------------------------+",};
			mario[0].Draw();
			mario[0].Run();

		}
	}
}
