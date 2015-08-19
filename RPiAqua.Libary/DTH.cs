using Raspberry.IO.Components.Sensors.Temperature.Dht;
using Raspberry.IO.GeneralPurpose;
using Raspberry.Timers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPiAqua.Libary
{
	public class DTH
	{
		public DTH (bool autoStart = true)
		{
			ConnectorPin dthPin = ConnectorPin.P1Pin11;
			var driver = GpioConnectionSettings.GetBestDriver(GpioConnectionDriverCapabilities.CanChangePinDirectionRapidly);
			var pin = driver.InOut(dthPin);
			Dht22Connection connection = new Dht22Connection(pin);
			//while (!Console.KeyAvailable)
			//{
				var data = connection.GetData();
				if (data != null)
					Console.WriteLine("{0:0.00}% humidity, {1:0.0}°C, {2} attempts", data.RelativeHumidity.Percent, data.Temperature.DegreesCelsius, data.AttemptCount);
				else
					Console.WriteLine("Unable to read data");

				Timer.Sleep(TimeSpan.FromSeconds(2));
			//}
		}
	}
}
