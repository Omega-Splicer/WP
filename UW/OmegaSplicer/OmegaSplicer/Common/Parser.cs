using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmegaSplicer.Common
{
    public static class Parser
    {
        public static string SERVICE_UUID = "58409710-D5E2-4A7D-B439-10CF9C59E89F";
        public static string READ_UUID = "58409711-D5E2-4A7D-B439-10CF9C59E89F";
        public static string WRITE_UUID = "58409712-D5E2-4A7D-B439-10CF9C59E89F";

        public enum Value
        {
            SIGNAL,
            BATTERY,
            MOTORS
        }

        public static byte[] WriteMessage<T>(Value valuesType, List<T> values)
        {
            string ret = "";
            if (valuesType == Value.MOTORS && values.Count > 1)
                ret = "m1:" + values[0].ToString() + "/m2:" + values[1].ToString();
            return GetBytes(ret);
        }

        static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        static string GetString(byte[] bytes)
        {
            char[] chars = new char[bytes.Length / sizeof(char)];
            System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }
    }
}
