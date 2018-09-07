using System;
using System.IO;
using System.Diagnostics;
using System.Text;
using System.Xml.Serialization;
using System.Windows.Forms;
namespace Game
{
	public class Mario
	{
		private static int x=3,y=5;
		public string[] map;
		public string[] hidemap;
		public string[] movemap;
		private bool isCharaLarge=true;
		private static int jumpspeed=-2,interval=0;
		private static bool isClear = false;
		private static bool isGameOver = false;
		public Mario()
		{
			this.map=null;
			this.hidemap=null;
			this.movemap=null;
		}
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
					if(x<0)
						x=0;
					if(this.hidemap[y][x]=='-' || this.hidemap[y][x]=='|')
						x++;
					break;
				case 2:// >>
					x++;
					if(x>(this.hidemap[0].Length-1))
						x=this.hidemap[0].Length-1;
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
			string[] fmap = new string[map.Length];
			string[] fhidemap=new string[hidemap.Length];
			for(int i=0;i<map.Length;i++)
				fmap[i]=map[i];
			for(int i=0;i<hidemap.Length;i++)
				fhidemap[i]=hidemap[i];
			if(this.map==null || this.hidemap==null)
				return;
			isClear=false;
			Console.CursorVisible=false;
			while(!isClear)
			{
				interval=0;
				for(int i=0;i<fmap.Length;i++)
					map[i]=fmap[i];
				for(int i=0;i<fhidemap.Length;i++)
					hidemap[i]=fhidemap[i];
				Stopwatch sw = new Stopwatch();
				sw.Start();
				Console.Clear();
				this.Draw();
				jumpspeed=0;
				for(int c=0;c<(this.hidemap.Length);c++)
				{
					for(int d=0;d<(this.hidemap[c].Length);d++)
					{
						if(this.hidemap[c][d]=='O' || this.hidemap[c][d]=='o')
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
						interval++;
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
								}else
								{
									y++;
								}
								y--;
							}
							this.Draw();
							this.Check(true);
						}
						if(this.movemap!=null)
						{
							for(int i=0;i<movemap.Length;i++)
							{
								for(int j=0;j<movemap[i].Length;j++)
								{
									if(movemap[i][j]=='<')
									{
										int mod=interval%10;
										switch(mod)
										{
											case 0:
												if(0<j)
												{
													char[] tmpcharArray=map[i].ToCharArray();
													tmpcharArray[j-mod]=fmap[i][j];
													if(movemap[i][j-1]!='<'&&movemap[i][j-1]!='-')
														tmpcharArray[j-1]=' ';
													map[i]=new string(tmpcharArray);
													tmpcharArray=hidemap[i].ToCharArray();
													tmpcharArray[j]=fhidemap[i][j];
													if(movemap[i][j-1]!='<'&&movemap[i][j-1]!='-')
														tmpcharArray[j-1]=' ';
													hidemap[i]=new string(tmpcharArray);
												}
												break;
											case 1:
											case 2:
											case 3:
											case 4:
												if(0<j && j+1<movemap[i].Length-1)
												{
													char[] tmpcharArray=map[i].ToCharArray();
													tmpcharArray[j-mod]=fmap[i][j];
													if(movemap[i][j+1]!='<'&&movemap[i][j-mod+1]!='-')
														tmpcharArray[j-mod+1]=' ';
													map[i]=new string(tmpcharArray);
													tmpcharArray=hidemap[i].ToCharArray();
													tmpcharArray[j-mod]=fhidemap[i][j];
													if(movemap[i][j+1]!='<'&&movemap[i][j-mod+1]!='-')
														tmpcharArray[j-mod+1]=' ';
													hidemap[i]=new string(tmpcharArray);
												}
												break;
											case 5:
												if(map[i].Length-1>j && j-5>0)
												{
													char[] tmpcharArray=map[i].ToCharArray();
													tmpcharArray[j-5]=fmap[i][j];
													if(movemap[i][j-4]!='<')
														if(movemap[i][j-4]!='<'&&movemap[i][j-4]!='-')
															tmpcharArray[j-4]=' ';
													map[i]=new string(tmpcharArray);
													tmpcharArray=hidemap[i].ToCharArray();
													tmpcharArray[j-5]=fhidemap[i][j];
													if(movemap[i][j-4]!='<'&&movemap[i][j-4]!='-')
														tmpcharArray[j-4]=' ';
													hidemap[i]=new string(tmpcharArray);
												}
												break;
											case 6:
											case 7:
											case 8:
											case 9:
												if(map[i].Length-2>j && j+mod-11 >0)
												{
													char[] tmpcharArray=map[i].ToCharArray();
													tmpcharArray[j+mod-10]=fmap[i][j];
													if(movemap[i][j-1]!='<'&&movemap[i][j+mod-11]!='-')
														tmpcharArray[j+mod-11]=' ';
													map[i]=new string(tmpcharArray);
													tmpcharArray=hidemap[i].ToCharArray();
													tmpcharArray[j+mod-10]=fhidemap[i][j];
													if(movemap[i][j-1]!='<'&&movemap[i][j+mod-11]!='-')
														tmpcharArray[j+mod-11]=' ';
													hidemap[i]=new string(tmpcharArray);
												}
												break;
										}
									}
									if(movemap[i][j]=='>')
									{
										int mod=interval%10;
										switch(mod)
										{
											case 0:
												if(0<j)
												{
													char[] tmpcharArray=map[i].ToCharArray();
													tmpcharArray[j]=fmap[i][j];
													if(movemap[i][j+1]!='>'&&movemap[i][j+1]!='-')
														tmpcharArray[j+1]=' ';
													map[i]=new string(tmpcharArray);
													tmpcharArray=hidemap[i].ToCharArray();
													tmpcharArray[j]=fhidemap[i][j];
													if(movemap[i][j+1]!='>'&&movemap[i][j+1]!='-')
														tmpcharArray[j+1]=' ';
													hidemap[i]=new string(tmpcharArray);
												}
												break;
											case 1:
											case 2:
											case 3:
											case 4:
												if(0<j && j+1<movemap[i].Length-1)
												{
													char[] tmpcharArray=map[i].ToCharArray();
													tmpcharArray[j+mod]=fmap[i][j];
													if(movemap[i][j-1]!='>'&&movemap[i][j+mod-1]!='-')
														tmpcharArray[j+mod-1]=' ';
													map[i]=new string(tmpcharArray);
													tmpcharArray=hidemap[i].ToCharArray();
													tmpcharArray[j+mod]=fhidemap[i][j];
													if(movemap[i][j-1]!='>'&&movemap[i][j+mod-1]!='-')
														tmpcharArray[j+mod-1]=' ';
													hidemap[i]=new string(tmpcharArray);
												}
												break;
											case 5:
												if(map[i].Length-1>j && j-5>0)
												{
													char[] tmpcharArray=map[i].ToCharArray();
													tmpcharArray[j+5]=fmap[i][j];
													if(movemap[i][j-1]!='>'&&movemap[i][j+4]!='-')
														tmpcharArray[j+4]=' ';
													map[i]=new string(tmpcharArray);
													tmpcharArray=hidemap[i].ToCharArray();
													tmpcharArray[j+5]=fhidemap[i][j];
													if(movemap[i][j-1]!='>'&&movemap[i][j+4]!='-')
														tmpcharArray[j+4]=' ';
													hidemap[i]=new string(tmpcharArray);
												}
												break;
											case 6:
											case 7:
											case 8:
											case 9:
												if(map[i].Length-2>j && j+mod-11 >0)
												{
													char[] tmpcharArray=map[i].ToCharArray();
													tmpcharArray[j-mod+10]=fmap[i][j];
													if(movemap[i][j+1]!='>'&&movemap[i][j-mod+11]!='-')
														tmpcharArray[j-mod+11]=' ';
													map[i]=new string(tmpcharArray);
													tmpcharArray=hidemap[i].ToCharArray();
													tmpcharArray[j-mod+10]=fhidemap[i][j];
													if(movemap[i][j+1]!='>'&&movemap[i][j-mod+11]!='-')
														tmpcharArray[j-mod+11]=' ';
													hidemap[i]=new string(tmpcharArray);
												}
												break;
										}
									}
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
			Console.Clear();
			Game.Mario[] mario=new Game.Mario[1];
			try{
				var xmlSerializer2 = new XmlSerializer(typeof(Game.Mario[]));
				var xmlSettings = new System.Xml.XmlReaderSettings()
				{
					CheckCharacters = false,
				};
				using (var streamReader = new StreamReader("Data", Encoding.UTF8))
					using (var xmlReader
							= System.Xml.XmlReader.Create(streamReader, xmlSettings))
					{
						mario = (Game.Mario[])xmlSerializer2.Deserialize(xmlReader); // （3）
					}
			}catch{
				MessageBox.Show("Dataというファイルが見つかりません。");
				Console.Clear();
				Console.CursorVisible=true;
				Console.Title=tmptitle;
				Console.ForegroundColor=ConsoleColor.White;
				Console.BackgroundColor=ConsoleColor.Black;
				Environment.Exit(1);
			}
			foreach(Mario tmpmario in mario)
			{
				Console.Clear();
				tmpmario.Run();
			}
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
