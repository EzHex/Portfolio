using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
	class Render
	{
		public static void Export(byte[] Map, uint Width, uint Height, uint Color0, uint Color1, string OutputName) // Color is subbited via RGB format
		{
			using (FileStream File = new FileStream(OutputName, FileMode.Create, FileAccess.Write))
			{
				File.Write(new byte[] { 0x42, 0x4D }); // BM
				File.Write(BitConverter.GetBytes((uint)(Map.Length + sizeof(uint) * 2 + 0x38))); // Size
				File.Write(BitConverter.GetBytes((ushort)0)); // Reserved (0)
				File.Write(BitConverter.GetBytes((ushort)0)); // Reserved (0)
				File.Write(BitConverter.GetBytes((uint)(sizeof(uint) * 2 + 0x38))); // Image Offset

				File.Write(BitConverter.GetBytes((uint)40)); // Header size (size is 40 bytes)
				File.Write(BitConverter.GetBytes((uint)Width)); // Width
				File.Write(BitConverter.GetBytes((uint)Height)); // Height
				File.Write(BitConverter.GetBytes((ushort)1)); // Color plane
				File.Write(BitConverter.GetBytes((ushort)1)); // bits per pixel
				File.Write(BitConverter.GetBytes((uint)0)); // Compression
				File.Write(BitConverter.GetBytes((uint)0)); // Image size (set 0 due to compression)
				File.Write(BitConverter.GetBytes((uint)0)); // Horizontal pixels per meter
				File.Write(BitConverter.GetBytes((uint)0)); // Vertical pixels per meter
				File.Write(BitConverter.GetBytes((uint)0)); // Used colors (0 all colors used)
				File.Write(BitConverter.GetBytes((uint)0)); // Important Colors (0 all colors are important)

				File.Write(BitConverter.GetBytes(Color0)); // Color[0]
				File.Write(BitConverter.GetBytes(Color1)); // Color[1]

				File.Write(BitConverter.GetBytes((ushort)0));

				File.Write(Map);
				File.Close();
			}
		}

		public static void DrawLine(byte[] Map, double X0, double Y0, double X1, double Y1, double Precision = 0.5, uint Color = 0)
		{
			double Length = Math.Sqrt(Math.Pow(X0 - X1, 2) + Math.Pow(Y0 - Y1, 2));

			double XStep = (X1 - X0) / (Length / Precision);
			double YStep = (Y1 - Y0) / (Length / Precision);

			double XRun = X0;
			double YRun = Y0;
			for (double i = 0; i < Length; i += Precision)
			{
				XRun += XStep;
				YRun += YStep;

				SetPixel(Map, XRun, YRun, Color);
			}
		}


		public static void RecursiveLine(byte[] Map, double X, double Y, double Size, double Rotation, uint Iteration)
		{
			if (Iteration == 0)
			{
				double SizeX = (Size / 2) * Math.Cos(Rotation * (Math.PI / 180));
				double SizeY = (Size / 2) * Math.Sin(Rotation * (Math.PI / 180));

				DrawLine(Map, X - SizeX, Y - SizeY, X + SizeX, Y + SizeY, 0.001, 1000);

				return;
			}

			double IterationSize = Math.Pow(4, Iteration);
			double LineSize = Math.Pow(4, Iteration - 1);

			double SizedNext = (LineSize / IterationSize) * Size;

			double LocationX = X + ((-(LineSize * 2) / IterationSize) * Size) * Math.Cos(Rotation * (Math.PI / 180));
			double LocationY = Y + ((-(LineSize * 2) / IterationSize) * Size) * Math.Sin(Rotation * (Math.PI / 180));

			double OffsetX = ((LineSize / IterationSize) * Size) * Math.Cos(Rotation * (Math.PI / 180));
			double OffsetY = ((LineSize / IterationSize) * Size) * Math.Sin(Rotation * (Math.PI / 180));

			RecursiveLine(Map, LocationX + OffsetX * 0.5, LocationY + OffsetY * 0.5, SizedNext, Rotation, Iteration - 1);

			LocationX += OffsetX;
			LocationY += OffsetY;

			OffsetX = ((LineSize / IterationSize) * Size) * Math.Cos((Rotation + 90) * (Math.PI / 180));
			OffsetY = ((LineSize / IterationSize) * Size) * Math.Sin((Rotation + 90) * (Math.PI / 180));

			RecursiveLine(Map, LocationX + OffsetX * 0.5, LocationY + OffsetY * 0.5, SizedNext, Rotation + 90, Iteration - 1);

			LocationX += OffsetX;
			LocationY += OffsetY;

			OffsetX = ((LineSize / IterationSize) * Size) * Math.Cos(Rotation * (Math.PI / 180));
			OffsetY = ((LineSize / IterationSize) * Size) * Math.Sin(Rotation * (Math.PI / 180));

			RecursiveLine(Map, LocationX + OffsetX * 0.5, LocationY + OffsetY * 0.5, SizedNext, Rotation, Iteration - 1);

			LocationX += OffsetX;
			LocationY += OffsetY;

			OffsetX = ((LineSize / IterationSize) * Size) * Math.Cos((Rotation - 90) * (Math.PI / 180));
			OffsetY = ((LineSize / IterationSize) * Size) * Math.Sin((Rotation - 90) * (Math.PI / 180));

			RecursiveLine(Map, LocationX + OffsetX * 0.5, LocationY + OffsetY * 0.5, SizedNext, Rotation - 90, Iteration - 1);

			LocationX += OffsetX;
			LocationY += OffsetY;

			OffsetX = ((LineSize / IterationSize) * Size) * Math.Cos((Rotation + 90) * (Math.PI / 180));
			OffsetY = ((LineSize / IterationSize) * Size) * Math.Sin((Rotation + 90) * (Math.PI / 180));

			LocationX -= OffsetX;
			LocationY -= OffsetY;

			RecursiveLine(Map, LocationX + OffsetX * 0.5, LocationY + OffsetY * 0.5, SizedNext, Rotation + 90, Iteration - 1);

			OffsetX = ((LineSize / IterationSize) * Size) * Math.Cos((Rotation + 180) * (Math.PI / 180));
			OffsetY = ((LineSize / IterationSize) * Size) * Math.Sin((Rotation + 180) * (Math.PI / 180));

			LocationX -= OffsetX;
			LocationY -= OffsetY;

			RecursiveLine(Map, LocationX + OffsetX * 0.5, LocationY + OffsetY * 0.5, SizedNext, Rotation + 180, Iteration - 1);

			OffsetX = ((LineSize / IterationSize) * Size) * Math.Cos((Rotation + 90) * (Math.PI / 180));
			OffsetY = ((LineSize / IterationSize) * Size) * Math.Sin((Rotation + 90) * (Math.PI / 180));

			RecursiveLine(Map, LocationX + OffsetX * 0.5, LocationY + OffsetY * 0.5, SizedNext, Rotation + 90, Iteration - 1);

			LocationX += OffsetX;
			LocationY += OffsetY;

			OffsetX = ((LineSize / IterationSize) * Size) * Math.Cos(Rotation * (Math.PI / 180));
			OffsetY = ((LineSize / IterationSize) * Size) * Math.Sin(Rotation * (Math.PI / 180));

			RecursiveLine(Map, LocationX + OffsetX * 0.5, LocationY + OffsetY * 0.5, SizedNext, Rotation, Iteration - 1);
		}

		public static void DrawImage(byte[] Map, double X, double Y, double length, int level, int rotation, uint Width)
        {
			DrawLines(Map, X, Y, length, level, rotation, Width);
        }

		public static void DrawLines(byte[] Map, double X, double Y, double length, int level, int rotation, uint Width)
        {
			double tX = X;
			double tY = Y;

			if ( level == 0)
            {
				//rotation 1 -> right
				//rotation 2 -> up
				//rotation 3 -> down
				
                switch (rotation)
                {
					case 1:
						DrawImageRight(Map, ref tX, ref tY, length, Width);
						break;
					case 2:
						DrawImageUp(Map, ref tX, ref tY, length, Width);
						break;
					case 3:
						DrawImageDown(Map, ref tX, ref tY, length, Width);
						break;
					default:
						break;
                }
            }

			if ( level != 0 )
            {
                DrawLines(Map, tX, tY, length / 4, level - 1, 1, Width);
                tX += length;												// c1 | 1
                DrawLines(Map, tX, tY, length / 4, level - 1, 2, Width);
                tY += length;                                               // c2 | 1
				DrawLines(Map, tX, tY, length / 4, level - 1, 1, Width);
                tX += length;                                               // c1 | 1
				DrawLines(Map, tX, tY, length / 4, level - 1, 3, Width);
                tY -= length;                                               // c2 | 1
				DrawLines(Map, tX, tY, length / 4, level - 1, 3, Width);
                tY -= length;                                               // c2 | 1
				DrawLines(Map, tX, tY, length / 4, level - 1, 1, Width);
                tX += length;                                               // c1 | 1
				DrawLines(Map, tX, tY, length / 4, level - 1, 2, Width);
                tY += length;                                               // c2 | 1
				DrawLines(Map, tX, tY, length / 4, level - 1, 1, Width);
            }
		}


		private static void DrawImageRight(byte[] Map, ref double tX, ref double tY, double length, uint Width)
        {
			DrawRight(Map, ref tX, ref tY, length, Width);
			DrawUp(Map, ref tX, ref tY, length, Width);
			DrawRight(Map, ref tX, ref tY, length, Width);
			DrawDown(Map, ref tX, ref tY, length, Width);
			DrawDown(Map, ref tX, ref tY, length, Width);
			DrawRight(Map, ref tX, ref tY, length, Width);
			DrawUp(Map, ref tX, ref tY, length, Width);
			DrawRight(Map, ref tX, ref tY, length, Width);
		}

		private static void DrawImageUp(byte[] Map, ref double tX, ref double tY, double length, uint Width)
		{
			DrawUp(Map, ref tX, ref tY, length, Width);
			DrawLeft(Map, ref tX, ref tY, length, Width);
			DrawUp(Map, ref tX, ref tY, length, Width);
			DrawRight(Map, ref tX, ref tY, length, Width);
			DrawRight(Map, ref tX, ref tY, length, Width);
			DrawUp(Map, ref tX, ref tY, length, Width);
			DrawLeft(Map, ref tX, ref tY, length, Width);
			DrawUp(Map, ref tX, ref tY, length, Width);

		}

		private static void DrawImageDown(byte[] Map, ref double tX, ref double tY, double length, uint Width)
		{
			DrawDown(Map, ref tX, ref tY, length, Width);
			DrawRight(Map, ref tX, ref tY, length, Width);
			DrawDown(Map, ref tX, ref tY, length, Width);
			DrawLeft(Map, ref tX, ref tY, length, Width);
			DrawLeft(Map, ref tX, ref tY, length, Width);
			DrawDown(Map, ref tX, ref tY, length, Width);
			DrawRight(Map, ref tX, ref tY, length, Width);
			DrawDown(Map, ref tX, ref tY, length, Width);
		}

		private static void DrawRight(byte[] Map, ref double tX, ref double tY, double length, uint Width)
        {
			for (double i = tX; i < tX + length; i++)	// c1 | n + 1
			{
				SetPixel(Map, i, tY, Width);			// SetPixel() | 1
			}
			tX += length;								// c2 | 1
		}

		private static void DrawLeft(byte[] Map, ref double tX, ref double tY, double length, uint Width)
		{
			for (double i = tX; i > tX - length; i--)   // c1 | n + 1
			{
				SetPixel(Map, i, tY, Width);            // SetPixel() | 1
			}
			tX -= length;                               // c2 | 1
		}

		private static void DrawUp(byte[] Map, ref double tX, ref double tY, double length, uint Width)
        {
			for (double i = tY; i < tY + length; i++)   // c1 | n + 1
			{
				SetPixel(Map, tX, i, Width);            // SetPixel() | 1
			}
			tY += length;                               // c2 | 1
		}

		private static void DrawDown(byte[] Map, ref double tX, ref double tY, double length, uint Width)
		{
			for (double i = tY; i > tY - length; i--)   // c1 | n + 1
			{
				SetPixel(Map, tX, i, Width);            // SetPixel() | 1
			}
			tY -= length;                               // c2 | 1
		}

		public static uint GetPixel(double X, double Y, uint Width)
		{
			uint Padding = Width % 32;												// c1 | 1
			if (Padding != 0)														// c2 | 1
				Padding = 32 - Padding;												// c3 | 1

			return (uint)((Math.Round(Y) * (Width + Padding)) + Math.Round(X));		// c4 | 1
		}

		public static void SetPixel(byte[] Map, double X, double Y, uint Width)
		{
			if (X < 0)													// c1 | 1
				return;													// c2 | 1

			if (X > Width)												// c3 | 1
				return;													// c2 | 1

			if (Y < 0)													// c4 | 1
				return;													// c2 | 1
			uint Pixel = GetPixel(X, Y, Width);							// c5 | 1
			Map[Pixel / 8] |= (byte)(1 << (byte)(8 - Pixel % 8 - 1));	// c6 | 1
		}

		public static byte[] CreateMap(uint Width, uint Height)
		{
			uint Padding = Width % 32;							// c1 | 1
			if (Padding != 0)									// c2 | 1
				Padding = 32 - Padding;                         // c3 | 1

			return new byte[Height * (Width + Padding) / 8];	// c4 | 1
		}
	}
}
