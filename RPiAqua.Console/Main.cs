using System;
using RPiAqua.Main;
using RPiAqua.Libary;
using System.Collections.Generic;
using System.Threading;

namespace RPiAqua.ConsoleApp
{
	class MainClass
	{
		static RPiAqua.Main.RPiAqua rpi = RPiAqua.Main.RPiAqua.Instance;
		//private static bool _stop;
		public static void Main()
		{
			Console.WriteLine("RPiAqua Version: " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString());

			//while (!_stop)
			//{
			//	string state = Console.ReadLine().ToLower();
			//	if (state.Contains("start"))
			//	{
			//		_stop = false;
			//		rpi.Start();
			//	}

			//	if (state.Contains("stop"))
			//	{
			//		_stop = true;
			//		rpi.Stop();
			//	}
			//}

			rpi.Start();

			Thread.Sleep(60000);

			rpi.Stop();
			rpi.Dispose();

			//Display display = new Display();
			//display.ClearDisplay();

			//display.WriteDisplay("Hallo_du_da_vorne____", DisplayLines.One);
			//display.WriteDisplay("Hallo_du_da_vorne___", DisplayLines.Two);
			//display.WriteDisplay("Hallo_du_da_vorne___", DisplayLines.Three);
			//display.WriteDisplay("Hallo_du_da_vorne___", DisplayLines.Four);

			//Thread.Sleep(5000);
			//display.ClearDisplay();

			//FanControl fan = new FanControl();
			//for (int i = 0; i < 5; i++)
			//{
			//fan.Start();
			//Thread.Sleep(2000);
			//fan.Stop();
			//Thread.Sleep(2000);
			//}

			//Console.WriteLine("Scheife durch");

			//fan.Dispose();
			//fan = null;
			//Console.WriteLine("Scheife durch");
			//HCSR04 hcsr = new HCSR04(2000);

			//DTH dth = new DTH();
			//double temp, hum = 0.0;
			//dth.GetVaues(out hum, out temp);
			//Console.WriteLine(string.Format("Temp {0}", temp));
			//Console.WriteLine(string.Format("Humidity {0}", hum));
			//BMP085 bmp = new BMP085();

			//Dictionary<string, double> values = bmp.GetValues();
			//Console.WriteLine(values["Temp"].ToString());
			//Console.WriteLine(values["Bar"].ToString());
			//Console.WriteLine(values["Meter"].ToString());

			//DS18B20 ds = new DS18B20(0);
			//Console.WriteLine((ds.GetValue()).ToString());
		}

		~MainClass()
		{
			rpi.Stop();
			rpi.Dispose();
		}
	}
}
