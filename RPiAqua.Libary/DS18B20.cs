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
		private int _deviceID;
		public DS18B20(int deviceID)
		{
			_deviceID = deviceID;
			Init();
		}

		private void Init()
		{
			this.connection = new Ds18b20Connection(_deviceID);
		}

		public double GetValue()
		{
			return this.connection.GetTemperature().DegreesCelsius;
		}
	}
}
