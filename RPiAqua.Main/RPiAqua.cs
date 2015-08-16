using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
				if (_instance != null)
					_instance = new RPiAqua();

				return _instance;
			}
		}
		#endregion

		//=====================================================================

		#region Fields
		


		#endregion

		//=====================================================================

		#region Properties



		#endregion

		//=====================================================================

		#region ctor's

		private RPiAqua() { }

		#endregion

		//=====================================================================
		#region Public Functions

		/// <summary>
		/// 
		/// </summary>
		/// <returns>0 if Start was successful</returns>
		public int Start()
		{
			int iRet = 0;

			return iRet;
		}

		#endregion

		//=====================================================================

	}
}
