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
		private static int jumpspeed=-2;
		private static bool isClear = false;
		private static bool isGameOver = false;
		public bool isbeforeFinal=false;
		public Mario()
		{}
		private void Check(bool flag)
		{
			if(!isClear && !isGameOver)
			{
				switch(this.hidemap[y][x])
				{
					case 'A':
						isGameOver=true;
						jumpspeed=0;
						this.Draw();
						Console.SetCursorPosition(x,y);
						Console.Write("A");
						Console.SetCursorPosition(x,y-1);
						Console.Write("A");
						break;
					case '>':
						this.Draw();
						Console.SetCursorPosition(x,y);
						Console.Write(">");
						Console.SetCursorPosition(x+1,y);
						Console.Write(">");
						isGameOver=true;
						jumpspeed=0;
						break;
					case '<':
						this.Draw();
						Console.SetCursorPosition(x,y);
						Console.Write("<");
						Console.SetCursorPosition(x-1,y);
						Console.Write("<");
						isGameOver=true;
						jumpspeed=0;
						break;
					case 'V':
						isGameOver=true;
						jumpspeed=0;
						this.Draw();
						Console.SetCursorPosition(x,y);
						Console.Write("V");
						Console.SetCursorPosition(x,y+1);
						Console.Write("V");
						break;
					case 'G':
						isClear=true;
						break;
				}
			}
		}
		private void Check()
		{
			if(!isClear && !isGameOver)
			{
				switch(this.hidemap[y][x])
				{
					case 'A':
						jumpspeed=0;
						isGameOver=true;
						this.Draw();
						Console.SetCursorPosition(x,y);
						Console.Write("A");
						Console.SetCursorPosition(x,y-1);
						Console.Write("A");
						break;
					case '>':
						this.Draw();
						Console.SetCursorPosition(x,y);
						Console.Write(">");
						Console.SetCursorPosition(x+1,y);
						Console.Write(">");
						jumpspeed=0;
						isGameOver=true;
						break;
					case '<':
						this.Draw();
						Console.SetCursorPosition(x,y);
						Console.Write("<");
						Console.SetCursorPosition(x-1,y);
						Console.Write("<");
						jumpspeed=0;
						isGameOver=true;
						break;
					case 'V':
						jumpspeed=0;
						isGameOver=true;
						this.Draw();
						Console.SetCursorPosition(x,y);
						Console.Write("V");
						Console.SetCursorPosition(x,y+1);
						Console.Write("V");
						break;
					case 'G':
						isClear=true;
						break;
					case '=':
						jumpspeed=3;
						break;
				}
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
					if(this.hidemap[y][x]=='-' || this.hidemap[y][x]=='|')
						x--;
					break;
				case 3:// A
					if(y<(this.hidemap.Length-1))
					{
						if(this.hidemap[y+1][x]!=' ')
						jumpspeed = 2;
					}
					if(y==(this.hidemap.Length-1))
						jumpspeed = 2;
					break;
			}
			this.Check();
			this.Draw();

		}
		public void Run()
		{
			isClear=false;
			Console.CursorVisible=false;
			while(!isClear)
			{
				Stopwatch sw = new Stopwatch();
				sw.Start();
				Console.Clear();
				jumpspeed=0;
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
							case ConsoleKey.C:
								isClear=true;
								break;
						}
					}
					TimeSpan ts = sw.Elapsed;
					if(ts.TotalSeconds>=0.2)
					{
						for(int a = System.Math.Abs(jumpspeed);a>0;a--)
						{
							if(jumpspeed<0)
							{
								if(y>(this.hidemap.Length-2))
								{
									y=this.hidemap.Length-2;
								}
								if(this.hidemap[y+1][x]=='-')
								{
									y--;
									jumpspeed=0;
								}
								y++;
							}
							if(jumpspeed>0)
							{
								if(y>0)
								{
									if(this.hidemap[y-1][x] =='-')
									{
										y++;
									}
								}
								y--;
							}
							this.Draw();
							this.Check(true);
						}
						jumpspeed--;
						isCharaLarge=!isCharaLarge;
						this.Draw();
						sw.Reset();
						sw.Start();
					} 
					Check();
				}
				if(isGameOver)
				{
					Console.SetCursorPosition(Console.WindowWidth/2-3,Console.WindowHeight/2);
					Console.Write("GameOver");
					Console.ReadKey();
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
			Console.CursorVisible=false;
			string tmptitle=Console.Title;
			Console.Title="Mario";
			Console.ForegroundColor=ConsoleColor.Black;
			Console.BackgroundColor=ConsoleColor.White;
			Console.Clear();
			Game.Mario[] mario = new Game.Mario[5];
			mario[0] = new Game.Mario();
			mario[0].map = new string[]{
					"+----------------------------------------+",
					"|                                        |",
					"|-------------------------------------- -|",
					"|                                        |",
					"|                                        |",
					"|                                        |",
					"|--------------------------------------- |",
					"|                                        |",
					"|                                        |",
					"|                                        |",
					"|--- ------------------------------------|",
					"|                                        |",
					"|                                        |",
					"|                                        |",
					"|                                        |",
					"|                                        |",
					"|                                        |",
					"|                                        |",
					"|                                       G|",
					"|--   --   --   --   --   --  --  --  ---|",
					"|AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA|",
					"+----------------------------------------+",};
			mario[0].hidemap = new string[]{
					"+----------------------------------------+",
					"|O                                       |",
					"|--- ---------------------------------- -|",
					"|                                        |",
					"|                                        |",
					"|                                        |",
					"|--- ----------------------------------- |",
					"|                                        |",
					"|                                        |",
					"|                                        |",
					"|--- ------ -----------------------------|",
					"|                                        |",
					"|                                        |",
					"|                                        |",
					"|                                        |",
					"|                                        |",
					"|                                        |",
					"|                                        |",
					"|                                       G|",
					"|--   --   --   --   --   --  --  --  ---|",
					"|AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA|",
					"+----------------------------------------+",};
			mario[1] = new Game.Mario();
			mario[1].map = new string[]{
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
					"|        =                          =    |",
					"|                                        |",
					"| -           --=      =      =          |",
					"|                                        |",
					"|      =                                 |",
					"|                                       G|",
					"|                                       -|",
					"|                                        |",
					"|                                        |",
					"|AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA|",
					"+----------------------------------------+",};
			mario[1].hidemap = new string[]{
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
					"|        =                          =    |",
					"| O      -                          -    |",
					"| -           --=      =      =          |",
					"|               -      -      -          |",
					"|      =                                 |",
					"|      -                                G|",
					"|                                       -|",
					"|                                        |",
					"|                                        |",
					"|AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA|",
					"+----------------------------------------+",};
			mario[2] = new Game.Mario();
			mario[2].map = new string[]{
					"+----------------------------------------+",
					"|                                        |",
					"|                                        |",
					"|                                        |",
					"|                                        |",
					"|                                        |",
					"|           G      =      =              |",
					"|           -                            |",
					"|                                        |",
					"|                                        |",
					"|                            =----       |",
					"|                                        |",
					"|                                        |",
					"|                                   =    |",
					"|     | |  |   | |                       |",
					"|     | |  |   | |                       |",
					"|     | |  |   | |                       |",
					"|   | | |  |   | |                       |",
					"|   |=| |AA| |=| | =      =      -------=|",
					"|   | | ---- |   - |      |      |       |",
					"|   | |      |     |      |      |       |",
					"+--=--------=-----=-AAAAAA-AAAAAA--------+",};
			mario[2].hidemap = new string[]{
					"+----------------------------------------+",
					"|                                        |",
					"|                                        |",
					"|                                        |",
					"|                                        |",
					"|                                        |",
					"|           G      =      =              |",
					"|           -      -      -              |",
					"|                                        |",
					"|                                        |",
					"|                            =----       |",
					"|                            -           |",
					"|                                        |",
					"|                                   =    |",
					"|     A A  -   A A                  -    |",
					"|     - -  -   - -                       |",
					"|     - -  -   - -                       |",
					"|   A - -  -   - -                       |",
					"|   -=- -AA- -=- - =      =      -------=|",
					"|   --- ---- -   - -      -      -      -|",
					"|O  - -      -     -      -      -       |",
					"+--=--------=-----=-AAAAAA-AAAAAA--------+",};
			mario[3] = new Game.Mario();
			mario[3].map = new string[]{
					"+----------------------------------------+",
					"|VVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVV|",
					"|                                        |",
					"|                                        |",
					"|                                        |",
					"|     |     --    --                     |",
					"|     |     |     |                      |",
					"|     |     |     |                      |",
					"|     |     |     |                      |",
					"|   = |   = |     |      -----------     |",
					"|     |     |     |                      |",
					"|     |     |     |                      |",
					"|     |     |     |                      |",
					"|     |     |     |   =                  |",
					"|     |     |     |                      |",
					"|  =  |  =  | =                          |",
					"|     |     |                            |",
					"|     |     |                            |",
					"| -   |     |     =                      |",
					"|     |     |     |                      |",
					"|     |     |     |                      |",
					"+-AAAA-=AAAA-=AAAA-AAAAAAAAAAAAAAA=AAAAAG+",};
			mario[3].hidemap = new string[]{
					"+----------------------------------------+",
					"|VVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVV|",
					"|                                        |",
					"|                       ==========       |",
					"|                                        |",
					"|     A     ==    ==                     |",
					"|     -     --    --                     |",
					"|     -     -     -                      |",
					"|     -     -     -                      |",
					"|   = -   = -     -      -== -===-=-     |",
					"|   - -   - -     -       -  ---   -     |",
					"|     -     -     -                      |",
					"|     -     -     -                      |",
					"|     -     -     -   =                  |",
					"|     -     -     V   -                  |",
					"|  =  -  =  - =                          |",
					"|  -  -  -  - -                          |",
					"|     -     -                            |",
					"| -   -     -     =                      |",
					"|     -     -     -                      |",
					"|O    -     -     -                      |",
					"+-AAAA-=AAAA-=AAAA-AAAAAAAAAAAAAAA=AAAAAG+",};
			mario[4] = new Game.Mario();
			mario[4].map = new string[]{
					"+----------------------------------------+",
					"|       |                                |",
					"|       |                                |",
					"|       |-  |   -|                       |",
					"|       |   |    |     |                 |",
					"|------ |   |    |     |                 |",
					"|       |  =|    |     |    |            |",
					"|       |   |    |     |    |            |",
					"|       |   |    |     |   =|=     G     |",
					"|       |   |    |     |    |      -     |",
					"|       |=  |    |          |            |",
					"|       |   |    |    ---   |            |",
					"| ------|   |    |     |    |            |",
					"|       |   |    |     |    |            |",
					"|       |  =|    |     |    |            |",
					"|       |   |    |     |    |            |",
					"|       |   |    |     |    |            |",
					"|       |   |    |     |    |            |",
					"|       |-  |    |     |    |            |",
					"|           |AAAA|AAAAA|AAAA|AAAAAAAAAAAA|",
					"+----------------------------------------+",
					"",};
			mario[4].hidemap = new string[]{
					"+VVVVVVV---------------------------------+",
					"|       -                                |",
					"|       -                                |",
					"|       --  A   -A                       |",
					"|O      -   -    -     A                 |",
					"|-===-= -   -    -     -                 |",
					"|------ -  =-    -     -    A            |",
					"|VVVVVV -  --    -     -    -            |",
					"|       -   -    -     -   =-=     G     |",
					"|       -   -    -     V   ---     -     |",
					"|       -=  -    -          -            |",
					"|       --  -    -    ---   -            |",
					"| =-===--   -    -     -    -            |",
					"| - --- -   -    -     -    -            |",
					"| VVVVVV-  =-    -     -    -            |",
					"|       -  --    -     -    -            |",
					"|       -   -    -     -    -            |",
					"|       -   -    -     -    -            |",
					"|       --  -    -     -    -            |",
					"|           -AAAA-AAAAA-AAAA-AAAAAAAAAAAA|",
					"+-===-=----------------------------------+",};
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
			Console.Clear();
			Console.SetCursorPosition(Console.WindowWidth/2-5,Console.WindowHeight/2);
			Console.Write("Final");
			Console.ReadKey();
			Console.Clear();
			Mario finalstage = new Game.Mario();
			finalstage.map = new string[]{
					"-----------------------------------------+",
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
					"| N                                      |",
					"| E                                      |",
					"| X                             |        |",
					"| T                  =          |        |",
					"|                            =  | -------|",
					"|                               | VVVVVVV|",
					"| |     =     =                 |        |",
					"| |                             |        |",
					"| V                             |        |",
					"|                               |        |",
					"+   AAAAAAAAAAAAAAAAAAAAAAAAAAAA-=-=-=-=--",};
			finalstage.hidemap = new string[]{
					"-----------------------------------------+",
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
					"|                               A        |",
					"|                    =          |        |",
					"|                    -       =  | =------|",
					"|                            -  | -VVVVVV|",
					"|       =     =                 |        |",
					"|       -     -                 |=       |",
					"|                               |        |",
					"|                               |       O|",
					"+ G AAAAAAAAAAAAAAAAAAAAAAAAAAAA-=-=-=-=--",};
			finalstage.Run();
			finalstage = new Game.Mario();
			finalstage.map = new string[]{
					"+----------------------------------------+",
					"|                                        |",
					"|                                        |",
					"|                                        |",
					"|                                        |",
					"|                                        |",
					"|                                        |",
					"|                                        |",
					"|                                G       |",
					"|                                        |",
					"|                                        |",
					"|                                        |",
					"|                                        |",
					"|                                   =    |",
					"|                                        |",
					"|                                        |",
					"|                                        |",
					"|                                       =|",
					"|                                        |",
					"|                                        |",
					"|                                        |",
					"+--==------=------=------=------=------=-+",};
			finalstage.hidemap = new string[]{
					"+----------------------------------------+",
					"| O                                      |",
					"|                                        |",
					"|                                        |",
					"|                                        |",
					"|                                        |",
					"|                                        |",
					"|                                        |",
					"|                                        |",
					"|                                G       |",
					"|                                        |",
					"|                                        |",
					"|                                        |",
					"|                                   =    |",
					"|                                   -    |",
					"|                                        |",
					"|                                        |",
					"|                                       =|",
					"|                                       -|",
					"|                                        |",
					"|                                        |",
					"+AA==AAAAAA=AAAAAA=AAAAAA=AAAAAA=AAAAAA=A+",};
			finalstage.Run();
			Console.Clear();
			Console.SetCursorPosition(Console.WindowWidth/2-3,Console.WindowHeight/2);
			Console.Write("Clear!!");
			Console.ReadKey();
			Console.Title=tmptitle;
			Console.ForegroundColor=ConsoleColor.White;
			Console.BackgroundColor=ConsoleColor.Black;
			Console.Clear();
			Console.CursorVisible=true;

		}
	}
}
