using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Raspberry.IO.Components.Sensors.Pressure.Bmp085;
using Raspberry.IO.InterIntegratedCircuit;
using Raspberry.IO.GeneralPurpose;
using UnitsNet;

namespace RPiAqua.Libary
{
	public class BMP085
	{
		private const ConnectorPin SDAPIN = ConnectorPin.P1Pin03;
		private const ConnectorPin SCLPIN = ConnectorPin.P1Pin05;

		private const int ADDRESS = 0x77;

		private I2cDriver driver = null;
		private Bmp085I2cConnection connection;

		public BMP085()
		{
			driver = new I2cDriver(SDAPIN.ToProcessor(), SCLPIN.ToProcessor());
			connection = new Bmp085I2cConnection(driver.Connect(ADDRESS));
		}


		public Dictionary<string, double> GetValues()
		{
			Dictionary<string, double> values = new Dictionary<string, double>();
			Bmp085Data data = connection.GetData();
			values.Add("Temp", data.Temperature.DegreesCelsius);
			values.Add("Bar", data.Pressure.Bars * 1000);
			Pressure presure = Pressure.FromPascals((double)(101325 / 10));
			values.Add("Meter", connection.GetAltitude(presure).Meters);
			return values;
		}
	}
}
