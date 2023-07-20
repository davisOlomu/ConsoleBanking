using System;

namespace ConsoleBanking
{
    /// <summary>
    /// 
    /// </summary>
    public static class Designs
    {
        /// <summary>
        /// Center with acarriage return
        /// </summary>
        /// <param name="message"></param>
        public static void CenterTextNewLine(string message)
        {
            int spaces = 50 + (message.Length / 2);
            Console.WriteLine(message.PadLeft(spaces));      
        }

        /// <summary>
        /// Center without carriage return
        /// </summary>
        /// <param name="message"></param>
        public static void CenterTextSameLine(string message)
        {
            int spaces = 50 + (message.Length / 2);
            Console.Write(message.PadLeft(spaces));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SpacesToAdd"></param>
        /// <param name="Msg"></param>
        /// <param name="Alignment"></param>
        /// <returns></returns>
        public static string AlignText(int SpacesToAdd, string Msg, string Alignment = "R")
        {
            if (Alignment == "L")
                Msg = Msg.PadLeft(SpacesToAdd + Msg.Length);
            else
            {
                Msg = Msg.PadLeft(SpacesToAdd + Msg.Length);
                Msg = Msg.PadRight((98 - Msg.Length) + Msg.Length);
            }
            return Msg;
        }

        /// <summary>
        /// Fancy Lines.
        /// </summary>
        public static void DrawLine()
        {
            Console.WriteLine("+--------------------------------------------------------------------------------------------------+");
        }
    }
}
