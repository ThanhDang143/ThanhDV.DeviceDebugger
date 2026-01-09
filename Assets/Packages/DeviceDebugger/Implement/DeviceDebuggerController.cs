using IngameDebugConsole;
using UnityEngine;
using UnityEngine.UI;

namespace ThanhDV.DeviceDebugger
{
    public class DeviceDebuggerController : MonoBehaviour
    {
        [Space]
        [SerializeField] private bool dontDestroyOnLoad = true;

        [Header("Active Module")]
        [SerializeField] private bool showFPS = true;
        [SerializeField] private bool showRAM = true;
        [SerializeField] private bool showAUDIO = true;
        [SerializeField] private bool showCONSOLE = true;
        [SerializeField] private bool showADVANCED = true;

        [Header("Module")]
        [SerializeField] private RectTransform moduleFPS;
        [SerializeField] private RectTransform moduleRAM;
        [SerializeField] private RectTransform moduleAUDIO;
        [SerializeField] private RectTransform moduleCONSOLE;
        [SerializeField] private RectTransform moduleADVANCED;

        [Header("Other Reference")]
        [SerializeField] private RectTransform sideBar;
        [SerializeField] private DebugLogPopup logPopup;

        private void Awake()
        {
            if (dontDestroyOnLoad) DontDestroyOnLoad(gameObject);
        }

        private void OnEnable()
        {
            Show();
            DebugLogManager.Instance.OnLogWindowShown += OnLogWindowShown;
            DebugLogManager.Instance.OnLogWindowHidden += OnLogWindowHidden;
        }

        private void OnDisable()
        {
            DebugLogManager.Instance.OnLogWindowShown -= OnLogWindowShown;
            DebugLogManager.Instance.OnLogWindowHidden -= OnLogWindowHidden;
        }

        private void ReloadUI()
        {
            LayoutRebuilder.MarkLayoutForRebuild(sideBar);
        }

        private void OnLogWindowShown()
        {
            moduleFPS.localScale = new(1, 0, 1);
            moduleRAM.localScale = new(1, 0, 1);
            moduleAUDIO.localScale = new(1, 0, 1);
            moduleADVANCED.localScale = new(1, 0, 1);
        }

        private void OnLogWindowHidden()
        {
            Show();
        }

        private void Show()
        {
            moduleFPS.localScale = showFPS ? Vector3.one : new(1, 0, 1);
            moduleRAM.localScale = showRAM ? Vector3.one : new(1, 0, 1);
            moduleAUDIO.localScale = showAUDIO ? Vector3.one : new(1, 0, 1);
            moduleCONSOLE.localScale = showCONSOLE ? Vector3.one : new(1, 0, 1);
            moduleADVANCED.localScale = showADVANCED ? Vector3.one : new(1, 0, 1);

            ReloadUI();
        }

        public void OnBtnOpenConsoleClicked()
        {
            logPopup.OpenConsoleWindow();
        }

        private void OnValidate()
        {
            Show();
        }
    }
}
