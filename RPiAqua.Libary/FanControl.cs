using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Raspberry.IO.GeneralPurpose;

namespace RPiAqua.Libary
{
	public class FanControl : IDisposable
	{
		private const double MAXVALUE = 27.0;
		private const double MINVALUE = 26.0;

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

		private bool disposed = false;
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!disposed)
			{
				if (disposing)
				{
					// Free other state (managed objects).
				}

				if (output != null)
				{
					output = null;
				}

				if (connection != null && connection.IsOpened)
				{
					connection.Close();
					connection = null;
				}
				disposed = true;
			}
		}

		// Use C# destructor syntax for finalization code.
		~FanControl()
		{
			// Simply call Dispose(false).
			Dispose(false);
		}

		public bool Check(double temp)
		{
			bool bRet = false;
			if (temp > MAXVALUE)
			{
				this.Start();
				bRet = true;
			}

			if (temp < MINVALUE)
			{
				this.Stop();
				bRet = false;
			}

			return bRet;
		}
	}
}
