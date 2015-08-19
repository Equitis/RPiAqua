using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Raspberry.IO;
using Raspberry.IO.GeneralPurpose;
using Raspberry.IO.Components.Sensors.Distance.HcSr04;
using System.Globalization;
using Raspberry.Timers;

namespace RPiAqua.Libary
{
	public class HCSR04
	{
		private const ConnectorPin ECHOPIN = ConnectorPin.P1Pin16;
		private const ConnectorPin TRIGGERPIN = ConnectorPin.P1Pin15;

		public HCSR04(int interval)
		{
			Console.CursorVisible = false;

			var intervalValue = GetInterval(interval);
			var driver = GpioConnectionSettings.DefaultDriver;

			Console.WriteLine("HC-SR04 Sample: measure distance");
			Console.WriteLine();
			Console.WriteLine("\tTrigger: {0}", TRIGGERPIN);
			Console.WriteLine("\tEcho: {0}", ECHOPIN);
			Console.WriteLine();

			using (var connection = new HcSr04Connection(driver.Out(TRIGGERPIN), driver.In(ECHOPIN)))
			{
				//while (!Console.KeyAvailable)
				//{
					try
					{
						var distance = connection.GetDistance().Centimeters;
						Console.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0:0.0}cm", distance).PadRight(16));
						Console.CursorTop--;
					}
					catch (TimeoutException tex)
					{
						Console.WriteLine("(Timeout): " + tex.Message);
					}

					Timer.Sleep(intervalValue);
				//}
			}

			Console.CursorVisible = true;

		}

		private TimeSpan GetInterval(int value)
		{
			return TimeSpan.FromMilliseconds((double)value);
		}
	}
}
