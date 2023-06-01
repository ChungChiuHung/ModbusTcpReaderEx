using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModbusTCPMonitor
{
    internal class Program
    {
        static void Main(string[] args)
        {

            string hexValue = "3EB5C8B4"; // Replace with your 32-bit float Hex value
            float floatValue = HexToFloat(hexValue);

            Console.WriteLine($"Decimal float value: {floatValue}");

            #region Convert Byte To Hex in String

            byte[] bytes = new byte[] {255, 255 };


            hexValue = BitConverter.ToString(bytes, 0, bytes.Length);

            Console.WriteLine(hexValue);

            hexValue = "374C3EC9";
            hexValue = "3EC9374C";
            hexValue = "3EBCED91";
            hexValue = "3EB2B021";
            hexValue = "3E9B22D1";
            floatValue = HexToFloat(hexValue);

            #endregion

            #region Convert the Int16 values to bytes
            //========= Convert Option 1 ==========
            short value1 = 1000;
            short value2 = -2000;
            byte[] bytes1 = BitConverter.GetBytes(value1);
            byte[] bytes2 = BitConverter.GetBytes(value2);

            // Create a new byte array for the 32-bit float
            byte[] floatBytes = new byte[4];

            // Copy the bytes from the Int16 values to the float byte array
            Array.Copy(bytes1, floatBytes, 2);
            Array.Copy(bytes2, 0, floatBytes, 2, 2);

            // Convert the byte array to a float
            floatValue = BitConverter.ToSingle(floatBytes, 0);

            //========Convert Option 2 ==================
            // Convert an array of 'Int16' Values
            short[] values = new short[] { 1234, -5678 };

            // Convert the Int16 values to bytes
            bytes = new byte[values.Length * sizeof(short)];
            // Buffer.BlockBopy(Array, Int32, Array, Int32, Int32)
            // Copies a specified number of bytes from a source array
            // starting at a particular offset to a destination array
            // starting at a particular offset.
            // https://learn.microsoftBitconverter  .tos    to.com/en-us/dotnet/api/system.buffer.blockcopy?view=net-7.0
            Buffer.BlockCopy(values, 0, bytes, 0, bytes.Length);

            // Convert the byte array to a 32-bit float
            floatValue = BitConverter.ToSingle(bytes, 0);


            #endregion

            Console.ReadLine();

        }

        static float HexToFloat(string hexValue)
        {
            // Convert the Hex value to a uint
            uint intValue = uint.Parse(hexValue, System.Globalization.NumberStyles.HexNumber);

            // Interpret the uint as a float
            byte[] bytes = BitConverter.GetBytes(intValue);
            float floatValue = BitConverter.ToSingle(bytes, 0);

            return floatValue;
        }
    }
}
