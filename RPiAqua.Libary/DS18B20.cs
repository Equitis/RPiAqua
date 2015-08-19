using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Raspberry.IO.Components.Sensors.Temperature.Ds18b20;
using System.Diagnostics;

namespace RPiAqua.Libary
{
	public class DS18B20
	{
		Ds18b20Connection connection = null;
		public DS18B20()
		{
			Ds18b20Connection ds = new Ds18b20Connection(0);
			double temp =  ds.GetTemperature().DegreesCelsius;
			Console.WriteLine("{0:0.000}", temp);
		}

		public StdResult Init()
		{
			StdResult result = StdResult.StdError;
			return result;
		}
	}
}
