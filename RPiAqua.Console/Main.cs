using System;
using RPiAqua.Libary;
using System.Collections.Generic;

namespace RPiAqua.ConsoleApp
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			//Console.WriteLine("RPiAqua Version: " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString());
			//Display display = new Display();
			//display.ClearDisplay();

			//display.WriteDisplay("Hallo_du_da_vorne____", DisplayLines.One);
			//display.WriteDisplay("Hallo_du_da_vorne___", DisplayLines.Two);
			//display.WriteDisplay("Hallo_du_da_vorne___", DisplayLines.Three);
			//display.WriteDisplay("Hallo_du_da_vorne___", DisplayLines.Four);

			//System.Threading.Thread.Sleep(5000);
			//display.ClearDisplay();

			//HCSR04 hcsr = new HCSR04(2000);

			DTH dth = new DTH();

			BMP085 bmp = new BMP085();

			Dictionary<string, double> values = bmp.GetValues();
			Console.WriteLine(values["Temp"].ToString());
			Console.WriteLine(values["Bar"].ToString());
			Console.WriteLine(values["Meter"].ToString());
		}
	}
}
