using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeviceDebugger
{
    public class DDTransporter : MonoBehaviour
    {
        #region Singleton
        private static DDTransporter _instance;
        private static readonly object _lock = new object();

        public static DDTransporter Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new DDTransporter();

                        Debug.Log($"<color=red>{_instance.GetType().Name} instance is null!!! Auto create new instance!!!</color>");
                    }
                    return _instance;
                }
            }
        }

        public static bool IsExist => _instance != null;
        #endregion

        public DeviceDebuggerController DeviceDebuggerController;
    }
}
