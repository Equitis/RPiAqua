using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Raspberry.IO.GeneralPurpose;

namespace RPiAqua.Libary
{
	public class FanControl
	{
		GpioConnection connection = null;
		ConnectorPin pin = ConnectorPin.P1Pin13;
		OutputPinConfiguration output = ConnectorPin.P1Pin13.Output();

		public FanControl()
		{
			Init();
		}

		private void Init()
		{
			connection = new GpioConnection(output);
		}

		public void Start()
		{
			if (!connection[pin])
			{
				connection.Toggle(output);
			}

		}

		public void Stop()
		{
			if (connection[pin])
			{
				connection.Toggle(output);
			}
		}

		private bool disposing = true;

		public void Dispose()
		{
			Dispose(disposing);
		}

		private void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (null != connection)
				{
					connection[pin] = false;
					connection.Clear();
					connection = null;
				}
				output.Disable();
				output = null;
			}

		}

		~FanControl()
		{
			Dispose(true);
		}
	}
}
