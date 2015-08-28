using RPiAqua.Libary;
using System;
using System.Timers;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPiAqua.Main
{
	/// <summary>
	/// Main Class for RPiAqua
	/// </summary>
	public class RPiAqua
	{
		#region Instance
		private static RPiAqua _instance;
		/// <summary>
		/// Get an Instance of class RPiAqua
		/// </summary>
		public static RPiAqua Instance
		{
			get
			{
				if (_instance == null)
					_instance = new RPiAqua();

				return _instance;
			}
		}
		#endregion

		//=====================================================================

		#region Fields

		private BMP085 _bmp;
		private Display _display;
		private DS18B20 _ds18b20;
		private DTH _dth;
		private FanControl _fanControl;
		private HCSR04 _hcsr;

		private bool _isRunning = false;

		private Timer _measureTimer;

		#endregion

		//=====================================================================

		#region Properties
		/// <summary>
		/// Gibt an ob die Messungen gestartet sind
		/// </summary>
		public bool IsRunning
		{
			get { return _isRunning; }
		}


		#endregion

		//=====================================================================

		#region ctor's and Init

		private RPiAqua()
		{
			Init();
		}

		private void Init()
		{
			//_bmp = new BMP085();
			_display = new Display();
			_ds18b20 = new DS18B20(0);
			//Console.WriteLine(string.Format("{0}", _ds18b20.GetValue()));
			_dth = new DTH();
			_fanControl = new FanControl();
			_hcsr = new HCSR04();

			ConfigTimer();
		}

		private void ConfigTimer()
		{
			_measureTimer = new Timer();
			_measureTimer.Elapsed += MeasureFunction;
			_measureTimer.Interval = 1000;
			_measureTimer.AutoReset = true;
		}

		#endregion

		//=====================================================================

		#region Public Functions

		/// <summary>
		/// Starts the measurement
		/// </summary>
		/// <returns>(bool) TRUE if Start sucssess</returns>
		public bool Start()
		{
			bool bRet = true;
			try
			{
				_measureTimer.Start();
			}
			catch
			{
				bRet = false;
			}
			this._isRunning = _measureTimer.Enabled;
			return bRet;
		}

		/// <summary>
		/// Starts the measurement
		/// </summary>
		/// <returns>(bool) TRUE if Stop sucssessful</returns>
		public bool Stop()
		{
			bool bRet = true;
			try
			{
				_measureTimer.Stop();
			}
			catch
			{
				bRet = false;
			}
			this._isRunning = _measureTimer.Enabled;
			return bRet;
		}

		#endregion

		//=====================================================================

		#region Private Functions
		private int index = 2;
		double height, humidity, humTemp = 0.0;
		private void MeasureFunction(object sender, ElapsedEventArgs e)
		{
			//_display.ClearDisplay();
			double temp = _ds18b20.GetValue();

			if ((index % 2) == 0)
			{
				height = Math.Round(_hcsr.GetValue(), 1);
				index = 0;
			}
			//Console.WriteLine(string.Format("{0} C", temp));
			this._fanControl.Check(temp);
			//_dth.GetVaues(out humidity, out humTemp);

			_display.WriteDisplay(string.Format("Water  = {0:00.0}", Math.Round(temp, 1)), DisplayLines.One);
			_display.WriteDisplay(string.Format("Height = {0:00.0}", height), DisplayLines.Two);
			_display.WriteDisplay(string.Format("Humidity = {0:00.0}", Math.Round(humidity, 1)), DisplayLines.Three);
			_display.WriteDisplay(string.Format("Temp1 = {0:00.0}", Math.Round(humTemp, 1)), DisplayLines.Four);
			index++;
		}

		#endregion

		//=====================================================================

		#region Dispose

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

				if (_fanControl != null)
				{
					_fanControl.Dispose();
				}

				if (_measureTimer != null && _measureTimer.Enabled)
				{
					_measureTimer.Stop();
					_measureTimer.Close();
					_measureTimer = null;
				}
				disposed = true;
			}
		}

		// Use C# destructor syntax for finalization code.
		~RPiAqua()
		{
			// Simply call Dispose(false).
			Dispose(false);
		}

		#endregion

		//=====================================================================
	}
}
