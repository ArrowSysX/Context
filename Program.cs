using System;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Context
{
	
    #region Native methods
    static class WindowUtility
	{
		[DllImport("user32.dll", SetLastError = true)]
		static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

		[DllImport("user32.dll", SetLastError = true)]
		static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

		const uint SWP_NOSIZE = 0x0001;
		const uint SWP_NOZORDER = 0x0004;

		private static Size GetScreenSize() => new Size(GetSystemMetrics(0), GetSystemMetrics(1));

		private struct Size
		{
			public int Width { get; set; }
			public int Height { get; set; }

			public Size(int width, int height)
			{
				Width = width;
				Height = height;
			}
		}

		[DllImport("User32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
		private static extern int GetSystemMetrics(int nIndex);

		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool GetWindowRect(HandleRef hWnd, out Rect lpRect);

		[StructLayout(LayoutKind.Sequential)]
		private struct Rect
		{
			public int Left;        // x position of upper-left corner
			public int Top;         // y position of upper-left corner
			public int Right;       // x position of lower-right corner
			public int Bottom;      // y position of lower-right corner
		}

		private static Size GetWindowSize(IntPtr window)
		{
			if (!GetWindowRect(new HandleRef(null, window), out Rect rect))
				throw new Exception("Unable to get window rect!");

			int width = rect.Right - rect.Left;
			int height = rect.Bottom - rect.Top;

			return new Size(width, height);
		}

		public static void MoveWindowToCenter()
		{
			IntPtr window = Process.GetCurrentProcess().MainWindowHandle;

			if (window == IntPtr.Zero)
				throw new Exception("Couldn't find a window to center!");

			Size screenSize = GetScreenSize();
			Size windowSize = GetWindowSize(window);

			int x = (screenSize.Width - windowSize.Width) / 2;
			int y = (screenSize.Height - windowSize.Height) / 2;

			SetWindowPos(window, IntPtr.Zero, x, y, 0, 0, SWP_NOSIZE | SWP_NOZORDER);
		}
	}
    #endregion
    public class Program
	{
		private static readonly int prefWidth = Console.LargestWindowWidth,
									prefHeight = Console.LargestWindowHeight;
		static void Main(string[] _)
		{
			Console.TreatControlCAsInput = true;
			Console.SetWindowSize(prefWidth - 15, prefHeight - 5);
			WindowUtility.MoveWindowToCenter();
			int linenum = 1,
				col = 1;
			Console.OutputEncoding = Encoding.UTF8;
			for (int i = 0; i <= 20; i++) ConsoleUtility.WriteProgressBar(i);
			bool newline = false;
			List<string> lines = new List<string>();
			string currentLine = "";
			RenderStatus("CRLF", 1, 1, 4, 0);
			bool interceptCol = false;
			while (true)
			{
				Console.ForegroundColor = ConsoleColor.DarkGray;
				if (newline)
				{
					currentLine = string.Empty;
					lines.Add(currentLine);
					Console.ForegroundColor = ConsoleColor.DarkGray;
					Console.Write($"{linenum} ");
				}
				newline = false;
				Console.ResetColor();
				ConsoleKeyInfo key = Console.ReadKey(true);
				col = !interceptCol ? col + 1 : col != 1 ? col - 1 : col;
				ConsoleKey keya = key.Key;
				switch(keya)
                {
					case ConsoleKey.Enter:
						linenum++;
						newline = true;
						col = 1;
						break;
					case ConsoleKey.Escape:
						return;
				    case ConsoleKey.Tab:
						col += 4;
						Console.Write("    ");
						break;
                }
				switch(keya)
                {
					case ConsoleKey.A:
						currentLine += (key.Modifiers & ConsoleModifiers.Shift) != 0 ? 'A' : 'a'; break;
					case ConsoleKey.B:
						currentLine += (key.Modifiers & ConsoleModifiers.Shift) != 0 ? 'B' : 'b'; break;
					case ConsoleKey.C:
						currentLine += (key.Modifiers & ConsoleModifiers.Shift) != 0 ? 'C' : 'c'; break;
					case ConsoleKey.D:
						currentLine += (key.Modifiers & ConsoleModifiers.Shift) != 0 ? 'D' : 'd'; break;
					case ConsoleKey.E:
						currentLine += (key.Modifiers & ConsoleModifiers.Shift) != 0 ? 'E' : 'e'; break;
					case ConsoleKey.F:
						currentLine += (key.Modifiers & ConsoleModifiers.Shift) != 0 ? 'F' : 'f'; break;
					case ConsoleKey.G:
						currentLine += (key.Modifiers & ConsoleModifiers.Shift) != 0 ? 'G' : 'g'; break;
					case ConsoleKey.H:
						currentLine += (key.Modifiers & ConsoleModifiers.Shift) != 0 ? 'H' : 'h'; break;
					case ConsoleKey.I:
						currentLine += (key.Modifiers & ConsoleModifiers.Shift) != 0 ? 'I' : 'i'; break;
					case ConsoleKey.J:
						currentLine += (key.Modifiers & ConsoleModifiers.Shift) != 0 ? 'J' : 'j'; break;
					case ConsoleKey.K:
						currentLine += (key.Modifiers & ConsoleModifiers.Shift) != 0 ? 'K' : 'k'; break;
					case ConsoleKey.L:
						currentLine += (key.Modifiers & ConsoleModifiers.Shift) != 0 ? 'L' : 'l'; break;
					case ConsoleKey.M:
						currentLine += (key.Modifiers & ConsoleModifiers.Shift) != 0 ? 'M' : 'm'; break;
					case ConsoleKey.N:
						currentLine += (key.Modifiers & ConsoleModifiers.Shift) != 0 ? 'N' : 'n'; break;
					case ConsoleKey.O:
						currentLine += (key.Modifiers & ConsoleModifiers.Shift) != 0 ? 'O' : 'o'; break;
					case ConsoleKey.P:
						currentLine += (key.Modifiers & ConsoleModifiers.Shift) != 0 ? 'P' : 'p'; break;
					case ConsoleKey.Q:
						currentLine += (key.Modifiers & ConsoleModifiers.Shift) != 0 ? 'Q' : 'q'; break;
					case ConsoleKey.R:
						currentLine += (key.Modifiers & ConsoleModifiers.Shift) != 0 ? 'R' : 'r'; break;
					case ConsoleKey.S:
						currentLine += (key.Modifiers & ConsoleModifiers.Shift) != 0 ? 'S' : 's'; break;
					case ConsoleKey.T:
						currentLine += (key.Modifiers & ConsoleModifiers.Shift) != 0 ? 'T' : 't'; break;
					case ConsoleKey.U:
						currentLine += (key.Modifiers & ConsoleModifiers.Shift) != 0 ? 'U' : 'u'; break;
					case ConsoleKey.V:
						currentLine += (key.Modifiers & ConsoleModifiers.Shift) != 0 ? 'V' : 'v'; break;
					case ConsoleKey.W:
						currentLine += (key.Modifiers & ConsoleModifiers.Shift) != 0 ? 'W' : 'w'; break;
					case ConsoleKey.X:
						currentLine += (key.Modifiers & ConsoleModifiers.Shift) != 0 ? 'X' : 'x'; break;
					case ConsoleKey.Y:
						currentLine += (key.Modifiers & ConsoleModifiers.Shift) != 0 ? 'Y' : 'y'; break;
					case ConsoleKey.Z:
						currentLine += (key.Modifiers & ConsoleModifiers.Shift) != 0 ? 'Z' : 'z'; break;
					case ConsoleKey.D1:
						currentLine += (key.Modifiers & ConsoleModifiers.Shift) != 0 ? '!' : '1'; break;
					case ConsoleKey.D2:
						currentLine += (key.Modifiers & ConsoleModifiers.Shift) != 0 ? '@' : '2'; break;
					case ConsoleKey.D3:
						currentLine += (key.Modifiers & ConsoleModifiers.Shift) != 0 ? '#' : '3'; break;
					case ConsoleKey.D4:
						currentLine += (key.Modifiers & ConsoleModifiers.Shift) != 0 ? '$' : '4'; break;
					case ConsoleKey.D5:
						currentLine += (key.Modifiers & ConsoleModifiers.Shift) != 0 ? '%' : '5'; break;
					case ConsoleKey.D6:
						currentLine += (key.Modifiers & ConsoleModifiers.Shift) != 0 ? '^' : '6'; break;
					case ConsoleKey.D7:
						currentLine += (key.Modifiers & ConsoleModifiers.Shift) != 0 ? '&' : '7'; break;
					case ConsoleKey.D8:
						currentLine += (key.Modifiers & ConsoleModifiers.Shift) != 0 ? '*' : '8'; break;
					case ConsoleKey.D9:
						currentLine += (key.Modifiers & ConsoleModifiers.Shift) != 0 ? '(' : '9'; break;
					case ConsoleKey.D0:
						currentLine += (key.Modifiers & ConsoleModifiers.Shift) != 0 ? ')' : '0'; break;
					case ConsoleKey.Backspace:
						Console.SetCursorPosition(linenum.ToString().Length + 3 + col, linenum - 1);
						Console.Write("\b\b \b");
						Console.SetCursorPosition(linenum.ToString().Length + 3 + col, linenum - 1);
						interceptCol = true;
						break;
					case ConsoleKey.Spacebar:
				}
				Console.SetCursorPosition(linenum.ToString().Length + 3 + (col - 1), linenum - 1);
				Console.Write(currentLine[currentLine.Length - 1]);
				Console.SetBufferSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
				RenderStatus("CRLF", col, linenum, linenum.ToString().Length + 3 + (col - 1), linenum - 1);
			}
		}
		static void RenderStatus(string encoding2, int col, int line, int backx, int backy)
		{
			string encoding = encoding2 == "LF" ? "  LF" : "CRLF";
			Console.SetCursorPosition(0, Console.WindowHeight - 2);
			Console.ForegroundColor = ConsoleColor.White;
			Console.BackgroundColor = ConsoleColor.Green;
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < Console.WindowWidth; i++)
			{
				if (i == 0)
				{
					sb.Append("   Context | Line " + line + ", Col " + col );
					i = 23 + line.ToString().Length + col.ToString().Length;
				}
				else if (i == Console.WindowWidth - 7)
				{
					sb.Append(encoding);
					i = Console.WindowWidth - 4;
				}
				else sb.Append(" ");
			}
			Console.Write(sb.ToString());
			Console.ResetColor();
			Console.SetCursorPosition(backx, backy);
		}
	}
	#region Loading animation
	public class ConsoleUtility
	{
		const char _block = '=';
		const string _back = "\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b";

		public static void WriteProgressBar(int percent, int speed = 80)
		{
			if (percent == 20)
			{
				Console.Clear();
				return;
			}
			Console.CursorVisible = false;
			Console.SetCursorPosition(0, Console.CursorTop);
			Console.Write("[");
			for(int i = 0; i < 20; i++)
			{
				Console.ForegroundColor = ConsoleColor.Green;
				char[] nums = (percent * 5).ToString().ToCharArray();
				if (i == 8) Console.Write(' ');
				else if (i == 9) Console.Write(nums.Length == 2 ? nums[0] : i < percent ? _block : ' ');
				else if (i == 10) Console.Write(nums.Length == 2 ? nums[1] : nums[0]);
				else if (i == 11) Console.Write('%');
				else if (i == 12) Console.Write(' ');
				else if (i < percent) Console.Write(_block);
				else Console.Write(" ");
			}
			Console.ForegroundColor = ConsoleColor.White;
			Console.Write($"] | {percent * 5}%");
			Thread.Sleep(speed);
			Console.Write(_back);
		}
	}
    #endregion
}
