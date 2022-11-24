using System;
namespace enigma
{
	class Program
	{
		static void Main()
		{
			char[] alphabet = new char[]
			{
				'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'
			};
			
			int[] r1 = new int[]
			{
				7, 19, 4, 18, 24, 11, 1, 13, 6, 17, 25, 3, 14, 12, 23, 8, 20, 21, 0, 22, 10, 5, 15, 9, 16, 2
			};
			
			int[] r2 = new int[]
			{
				4, 22, 14, 17, 15, 5, 0, 3, 21, 19, 1, 16, 2, 6, 13, 7, 18, 23, 9, 11, 20, 25, 10, 8, 24, 12
			};
			
			int[] r3 = new int[]
			{
				22, 13, 15, 12, 7, 21, 23, 9, 10, 4, 20, 5, 3, 11, 0, 25, 17, 16, 18, 24, 1, 14, 19, 8, 2, 6
			};
			
			int[] u = new int[]
			{
				25, 24, 23, 22, 21, 20, 19, 18, 17, 16, 15, 14, 13, 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1, 0
			};
			
			Rotor r = new Rotor(r1);
			Rotor m = new Rotor(r2);
			Rotor l = new Rotor(r3);
			
			char letter;
			ConsoleKeyInfo input;
			
			string word = "";
			bool end = false;
			bool fEnd = false;
			
			int desiredSetting;
			int aIndex;
			
			while (fEnd == false)
			{
				end = false;
				
				Console.Clear();
				Console.WriteLine("ENIGMA\n~Cipher Machine~");
				if (Console.ReadKey(false).Key == ConsoleKey.Escape)
				{
					fEnd = true;
				};
				Console.Clear();
				if (fEnd == false)
				{
					Console.Write("Rotor 1: ");
					input = Console.ReadKey(false);
					desiredSetting = Array.IndexOf(alphabet, Char.ToUpper(input.KeyChar));
					while (r.setting != desiredSetting)
					{
						r.Rotate();
					}
					
					Console.Write("\nRotor 2: ");
					input = Console.ReadKey(false);
					desiredSetting = Array.IndexOf(alphabet, Char.ToUpper(input.KeyChar));
					while (m.setting != desiredSetting)
					{
						m.Rotate();
					}
					
					Console.Write("\nRotor 3: ");
					input = Console.ReadKey(false);
					desiredSetting = Array.IndexOf(alphabet, Char.ToUpper(input.KeyChar));
					while (l.setting != desiredSetting)
					{
						l.Rotate();
					}
					
					Console.ReadKey(false);
					
					while (end == false)
					{
						Console.Clear();
						Console.WriteLine($"{alphabet[r.setting]}{alphabet[m.setting]}{alphabet[l.setting]}");
						Console.Write(word);
						
						r.Rotate();
						if (r.setting % 5 == 0){ m.Rotate(); }
						if (m.setting == 25){ l.Rotate(); }
						
						input = Console.ReadKey(false);
						letter = Char.ToUpper(input.KeyChar);
						
						if (input.Key == ConsoleKey.Escape)
						{
							end = true;
							word = "";
						}
						
						aIndex = Array.IndexOf(alphabet, letter);
						if (aIndex != -1)
						{
							word += alphabet[r.Backward(m.Backward(l.Backward(u[l.Forward(m.Forward(r.Forward(aIndex)))])))].ToString();
						}
						else
						{
							word += letter;
						}
					}
				}
			}	
		}
		
		public static int[] Shuffle(int[] array)
		{
			Random rnd = new Random();
			int n = array.Length;
			while (n > 1)
			{
				int k = rnd.Next(n--);
				int template = array[n];
				array[n] = array[k];
				array[k] = template;
			}
			
			return array;
		}
	}
	
	class Rotor
	{
		public int[] nodes;
		public int setting;
		public Rotor(int[] nodes_)
		{
			nodes = nodes_;
			setting = 0;
		}
		
		public void Rotate()
		{
			setting += 1;
			if (setting >= nodes.Length)
			{
				setting = 0;
			}
			
			int[] nodesOld = (int[]) nodes.Clone();
			for (int i = 0; i < nodes.Length; i++)
			{
				if (i != 0)
				{
					nodes[i] = nodesOld[i - 1];
				}
				else
				{
					nodes[i] = nodesOld[nodes.Length - 1];
				}
			}
		}
		
		public int Forward(int n)
		{
			return nodes[n];
		}
		
		public int Backward(int n)
		{
			return Array.IndexOf(nodes, n);
		}
	}
}
