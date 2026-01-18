using UnityEngine;

namespace ThanhDV.DeviceDebugger
{
    public static class DebugLog
    {
        private static void Log(string message, string color = "white") => Debug.Log($"<color={color}>[DeviceDebugger] {message}</color>");
        public static void Info(string message) => Log(message, "white");
        public static void Warning(string message) => Log(message, "yellow");
        public static void Error(string message) => Log(message, "red");
        public static void Success(string message) => Log(message, "green");
    }
}