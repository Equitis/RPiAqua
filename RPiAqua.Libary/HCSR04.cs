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
		private IGpioConnectionDriver driver;

		public HCSR04()
		{
			Init();
		}

		private void Init()
		{
			driver = GpioConnectionSettings.DefaultDriver;
		}

		public double GetValue()
		{
			try
			{
				var distance = 0.0;
				using (var connection = new HcSr04Connection(
					driver.Out(TRIGGERPIN.ToProcessor()),
					driver.In(ECHOPIN.ToProcessor())))
				{
					distance = connection.GetDistance().Centimeters;
					return distance;
				}
			}
			catch (TimeoutException tex)
			{
				throw new TimeoutException("Timeout", tex);
			}
		}

		private TimeSpan GetInterval(int value)
		{
			return TimeSpan.FromMilliseconds((double)value);
		}
	}
}
