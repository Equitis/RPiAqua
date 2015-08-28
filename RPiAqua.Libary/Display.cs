using Raspberry.IO.GeneralPurpose;
using Raspberry.IO.InterIntegratedCircuit;
using Raspberry.IO;
using System;
using System.Threading;

namespace RPiAqua.Libary
{
	public enum DisplayLines : byte
	{
		One = 0x80,
		Two = 0xC0,
		Three = 0x94,
		Four = 0xD4
	}

	public class Display
	{
		//PINS
		private const ConnectorPin sdaPin = ConnectorPin.P1Pin03;
		private const ConnectorPin sclPin = ConnectorPin.P1Pin05;

		//Adressen
		private const byte LCDADRESS = 0x27;

		//Commands
		private const byte LCD_CLEARDISPLAY = 0x01;
		private const byte LCD_RETURNHOME = 0x02;
		private const byte LCD_ENTRYMODESET = 0x04;
		private const byte LCD_DISPLAYCONTROL = 0x08;
		private const byte LCD_CURSORSHIFT = 0x10;
		private const byte LCD_FUNCTIONSET = 0x20;
		private const byte LCD_SETCGRAMADDR = 0x40;
		private const byte LCD_SETDDRAMADDR = 0x80;

		// flags for display entry mode
		private const byte LCD_ENTRYRIGHT = 0x00;
		private const byte LCD_ENTRYLEFT = 0x02;
		private const byte LCD_ENTRYSHIFTINCREMENT = 0x01;
		private const byte LCD_ENTRYSHIFTDECREMENT = 0x00;

		// flags for display on/off control
		private const byte LCD_DISPLAYON = 0x04;
		private const byte LCD_DISPLAYOFF = 0x00;
		private const byte LCD_CURSORON = 0x02;
		private const byte LCD_CURSOROFF = 0x00;
		private const byte LCD_BLINKON = 0x01;
		private const byte LCD_BLINKOFF = 0x00;

		// flags for display/cursor shift
		private const byte LCD_DISPLAYMOVE = 0x08;
		private const byte LCD_CURSORMOVE = 0x00;
		private const byte LCD_MOVERIGHT = 0x04;
		private const byte LCD_MOVELEFT = 0x00;

		// flags for function set
		private const byte LCD_8BITMODE = 0x10;
		private const byte LCD_4BITMODE = 0x00;
		private const byte LCD_2LINE = 0x08;
		private const byte LCD_1LINE = 0x00;
		private const byte LCD_5x10DOTS = 0x04;
		private const byte LCD_5x8DOTS = 0x00;

		// flags for backlight control
		private const byte LCD_BACKLIGHT = 0x08;
		private const byte LCD_NOBACKLIGHT = 0x00;

		private const byte En = 0x04; // Enable bit
		private const byte Rw = 0x02; // Read/Write bit
		private const byte Rs = 0x01; // Register select bit

		private I2cDriver driver = null;
		private I2cDeviceConnection connection;
		public Display ()
		{
			driver = new I2cDriver(sdaPin.ToProcessor(), sclPin.ToProcessor());
			//driver.ClockDivider = 400;
			connection = driver.Connect(LCDADRESS);
			//connection.WriteByte();
			this.Write(0x03);
			this.Write(0x03);
			this.Write(0x03);
			this.Write(0x02);

			this.Write(LCD_FUNCTIONSET | LCD_2LINE | LCD_5x8DOTS | LCD_4BITMODE);
			this.Write(LCD_DISPLAYCONTROL | LCD_DISPLAYON);
			this.Write(LCD_CLEARDISPLAY);
			this.Write(LCD_ENTRYMODESET | LCD_ENTRYLEFT);
		}

		public void Write(byte cmd, byte mode = 0x00)
		{
			//Console.WriteLine("I2c.Write");
			this.write_four_bits((byte)(mode | (cmd & 0xF0)));
			this.write_four_bits((byte)(mode | ((cmd << 4 & 0xF0))));
		}

		private void write_four_bits(byte data)
		{
			//Console.WriteLine("I2c.write_four_bits");
			connection.WriteByte((byte)(data | LCD_BACKLIGHT));
			this.Strobe(data);
		}

		private void Strobe(byte data)
		{
			//Console.WriteLine("I2c.Strobe");
			connection.WriteByte((byte)(data | En | LCD_BACKLIGHT));
			Thread.Sleep(5);
			connection.WriteByte((byte)((data & En) | LCD_BACKLIGHT));
			Thread.Sleep(1);
		}

		public void WriteDisplay(string text, DisplayLines line)
		{
			//Console.WriteLine("I2c.WriteDisplay");
			this.Write((byte)line);

			for (int i = 0; i < text.Length; i++)
			{
				this.Write(Convert.ToByte(text[i]), Rs);
			}
		}

		public void ClearDisplay()
		{
			this.Write(LCD_CLEARDISPLAY);
			this.Write(LCD_RETURNHOME);
		}
	}
}

