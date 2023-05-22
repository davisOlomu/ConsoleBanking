using System;

namespace ConsoleBanking
{
    public  class Designs
    {
        // Center with acarriage return
        public static void CenterNewLine(string message)
        {
            int spaces = 50 + (message.Length / 2);
            Console.WriteLine(message.PadLeft(spaces));      
        }

        // Center without carriage return
        public static void CenterSameLine(string message)
        {
            int spaces = 50 + (message.Length / 2);
            Console.Write(message.PadLeft(spaces));
        }

        // Align text
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

        // Draw lines
        public static void DrawLine()
        {
            Console.WriteLine("+--------------------------------------------------------------------------------------------------+");
        }
    }
}
