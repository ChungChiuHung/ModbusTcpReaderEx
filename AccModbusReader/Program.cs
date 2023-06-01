using System;
using System.Net;
using FluentModbus;

namespace AccModbusReader
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var client = new ModbusTcpClient();

            IPEndPoint ipAddress = new IPEndPoint(
                IPAddress.Parse("127.0.0.1"),
                502);

            client.Connect();


            var results = client.ReadHoldingRegisters<Int16>(1, 0, 2).ToArray();
            // little-endian with two 16-bit values
            byte[] bytes1 = BitConverter.GetBytes(results[1]);
            byte[] bytes2 = BitConverter.GetBytes(results[0]);

            byte[] floatBytes = new byte[4];

            Array.Copy(bytes1, floatBytes, 2);
            Array.Copy(bytes2, 0, floatBytes, 2, 2);

            string hexValue = BitConverter.ToString(floatBytes, 0, floatBytes.Length).Replace("-", "");

            uint intValue = uint.Parse(hexValue, System.Globalization.NumberStyles.HexNumber);

            byte[] bytes = BitConverter.GetBytes(intValue);

            float floatValue = BitConverter.ToSingle(bytes, 0);

        }

        
    }
}
