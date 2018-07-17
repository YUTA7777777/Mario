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
		private bool isClear = false;
		private static bool isGameOver = false;
		public Mario()
		{}
		private void Check()
		{
			switch(this.hidemap[y][x])
			{
				case 'A':
					isGameOver=true;
					this.Draw();
					Console.SetCursorPosition(x,y);
					Console.Write("A");
					Console.SetCursorPosition(x,y-1);
					Console.Write("A");
					Console.SetCursorPosition(Console.WindowWidth/2-3,Console.WindowHeight/2);
					Console.Write("GameOver");
					Console.ReadKey();
					break;
				case '>':
					this.Draw();
					Console.SetCursorPosition(x,y);
					Console.Write(">");
					Console.SetCursorPosition(x+1,y);
					Console.Write(">");
					isGameOver=true;
					Console.SetCursorPosition(Console.WindowWidth/2-3,Console.WindowHeight/2);
					Console.Write("GameOver");
					Console.ReadKey();
					break;
				case '<':
					this.Draw();
					Console.SetCursorPosition(x,y);
					Console.Write("<");
					Console.SetCursorPosition(x-1,y);
					Console.Write("<");
					isGameOver=true;
					Console.SetCursorPosition(Console.WindowWidth/2-3,Console.WindowHeight/2);
					Console.Write("GameOver");
					Console.ReadKey();
					break;
				case 'V':
					isGameOver=true;
					this.Draw();
					Console.SetCursorPosition(x,y);
					Console.Write("V");
					Console.SetCursorPosition(x,y+1);
					Console.Write("V");
					Console.SetCursorPosition(Console.WindowWidth/2-3,Console.WindowHeight/2);
					Console.Write("GameOver");
					Console.ReadKey();
					break;
				case 'G':
					Console.SetCursorPosition(Console.WindowWidth/2-3,Console.WindowHeight/2);
					Console.Write("Clear!!");
					Console.ReadKey();
					isClear=true;
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
					if(this.hidemap[y][x]=='-' || this.hidemap[y][x]=='|')
						x++;
					break;
				case 2:// >>
					x++;
					if(x>(this.hidemap[0].Length-2))
						x=this.hidemap[0].Length-2;
					if(this.map[y][x]=='-' || this.hidemap[y][x]=='|')
						x--;
					break;
				case 3:// A
					if(y<(this.hidemap.Length-1))
					{
						if(this.hidemap[y+1][x]=='-')
						jumpspeed = 2;
					}
					break;
			}
			this.Draw();

		}
		public void Run()
		{
			Console.CursorVisible=false;
			while(!isClear)
			{
				Stopwatch sw = new Stopwatch();
				sw.Start();
				Console.SetCursorPosition(Console.WindowWidth/2-3,Console.WindowHeight/2);
				Console.Write("Ready...");
				Console.ReadKey();
				Console.Clear();
				for(int c=0;c<(this.hidemap.Length);c++)
				{
					for(int d=0;d<(this.hidemap[c].Length);d++)
					{
						if(this.hidemap[c][d]=='O')
						{
							x=d;
							y=c;
						}
					}
				}
				this.Draw();
				isGameOver=false;
				while(!isGameOver && !isClear)
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
								Console.CursorVisible = true;
								Environment.Exit(0);
								break;
						}
					}
					TimeSpan ts = sw.Elapsed;
					if(ts.TotalSeconds>=0.2)
					{
						if(jumpspeed!=0){
							for(int a = System.Math.Abs(jumpspeed);a>-1;a--)
							{
								if(jumpspeed<0)
								{
									if(y<(this.hidemap.Length-1))
									{
										if(this.hidemap[y+1][x]=='-')
										{
											y--;
											jumpspeed=0;
										}
									}
									y++;
								}
								if(jumpspeed>0)
								{
									if(y>0)
									{
										if(this.hidemap[y-1][x] =='-')
											y++;
									}
									y--;
								}
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
			Game.Mario[] mario = new Game.Mario[2];
			mario[0] = new Game.Mario();
			mario[0].map = new string[]{
					"+----------------------------------------+",
					"|>                                       |",
					"|-----------------        -------------  |",
					"|>                                    |  |",
					"|-----        -------------------------  |",
					"|>                                    |  |",
					"|------------------       ------------- -|",
					"|>                                    |  |",
					"|------       -------------------------A |",
					"|>                                    |- |",
					"|------------------       -------------  |",
					"|>                                    | -|",
					"|------      --------------------------  |",
					"|>                                    |A |",
					"|-----------------        -------------- |",
					"|>                                    |  |",
					"|------       ------------------------- -|",
					"|>                                    |  |",
					"|------                     -----------A |",
					"|>                                   | - |",
					"|      AAAAAAA                       |  G|",
					"+----------------------------------------+",};
			mario[0].hidemap = new string[]{
					"+----------------------------------------+",
					"|>                                       |",
					"|-----------------        -------------  |",
					"|>                                    |  |",
					"|-----        -------------------------  |",
					"|>            O                       |  |",
					"|------------------       ------------- -|",
					"|>                                    |  |",
					"|------       ------- -----------------A |",
					"|>                                    |- |",
					"|------------------       -------------  |",
					"|>                                    | -|",
					"|------      -------- -----------------  |",
					"|>                                    |A |",
					"|-----------------        -------------- |",
					"|>                                    |  |",
					"|------       -------   --------------- -|",
					"|>                                    |  |",
					"|------                     -----------A |",
					"|>                                     - |",
					"|>     AAAAAAA       A                  G|",
					"+----------------------------------------+",};
			mario[1] = new Game.Mario();
			mario[1].map = new string[]{
					"+----------------------------------------+",
					"|>                                       |",
					"|-----------------        -------------  |",
					"|>                                    |  |",
					"|-----        -------------------------  |",
					"|>                                    |  |",
					"|------------------       ------------- -|",
					"|>                                    |  |",
					"|------       -------------------------A |",
					"|>                                    |- |",
					"|------------------       -------------  |",
					"|>                                    | -|",
					"|------      --------------------------  |",
					"|>                                    |A |",
					"|-----------------        -------------- |",
					"|>                                    |  |",
					"|------       ------------------------- -|",
					"|>                                    |  |",
					"|------                     -----------A |",
					"|>                                   | - |",
					"|      AAAAAAA                       |  G|",
					"+----------------------------------------+",};
			mario[1].hidemap = new string[]{
					"+----------------------------------------+",
					"|>                                       |",
					"|-----------------        -------------  |",
					"|>                                    |  |",
					"|-----        -------------------------  |",
					"|>            O                       |  |",
					"|------------------       ------------- -|",
					"|>                                    |  |",
					"|------       ------- -----------------A |",
					"|>                                    |- |",
					"|------------------       -------------  |",
					"|>                                    | -|",
					"|------      -------- -----------------  |",
					"|>                                    |A |",
					"|-----------------        -------------- |",
					"|>                                    |  |",
					"|------       -------   --------------- -|",
					"|>                                    |  |",
					"|------                     -----------A |",
					"|>                                     - |",
					"|>     AAAAAAA       A                  G|",
					"+----------------------------------------+",};
			int level=1;
			foreach(Mario tmpmario in mario)
			{
				Console.Clear();
				Console.SetCursorPosition(Console.WindowWidth/2-5,Console.WindowHeight/2);
				Console.Write("level{0}",level);
				Console.ReadKey();
				Console.Clear();
				tmpmario.Run();
				level++;
			}

		}
	}
}
