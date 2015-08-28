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
		Dht22Connection connection;
		public DTH ()
		{
			ConnectorPin dthPin = ConnectorPin.P1Pin11;
			var driver = GpioConnectionSettings.GetBestDriver(GpioConnectionDriverCapabilities.CanChangePinDirectionRapidly);
			var pin = driver.InOut(dthPin);
			connection = new Dht22Connection(pin);
		}

		public bool GetVaues(out double humidity, out double temp)
		{

			DhtData data = null;
			humidity = temp = 0.0;
			do
			{
				data = connection.GetData();

				if (data != null)
				{
					humidity = data.RelativeHumidity.Percent;
					temp = data.Temperature.DegreesCelsius;
				}
			} while (data == null);

			return true;
		}
	}
}
