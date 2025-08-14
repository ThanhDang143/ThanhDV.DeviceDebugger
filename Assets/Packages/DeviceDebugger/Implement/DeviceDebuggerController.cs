using System.Threading.Tasks;
using IngameDebugConsole;
using UnityEngine;

namespace ThanhDV.DeviceDebugger
{
    public class DeviceDebuggerController : MonoBehaviour
    {
        [Header("Active Module")]
        [SerializeField] private bool showFPS = true;
        [SerializeField] private bool showRAM = true;
        [SerializeField] private bool showAUDIO = true;
        [SerializeField] private bool showCONSOLE = true;
        [SerializeField] private bool showADVANCED = true;

        [Header("Module")]
        [SerializeField] private GameObject moduleFPS;
        [SerializeField] private GameObject moduleRAM;
        [SerializeField] private GameObject moduleAUDIO;
        [SerializeField] private GameObject moduleCONSOLE;
        [SerializeField] private GameObject moduleADVANCED;

        [Header("Other Reference")]
        [SerializeField] private DebugLogPopup debugLogPopup;
        [SerializeField] private RectTransform sideBar;

        private void OnEnable()
        {
            Show();
        }

        public void OnBtnLogPopupClicked()
        {
            debugLogPopup.OpenConsoleWindow();
        }

        private async void ReloadUI()
        {
            await Task.Yield();

            Vector2 anchorPos = sideBar.anchoredPosition;
            anchorPos.y = -sideBar.rect.height / 2f - 10f;
            sideBar.anchoredPosition = anchorPos;
        }

        private void Show()
        {
            moduleFPS.SetActive(showFPS);
            moduleRAM.SetActive(showRAM);
            moduleAUDIO.SetActive(showAUDIO);
            moduleCONSOLE.SetActive(showCONSOLE);
            moduleADVANCED.SetActive(showADVANCED);

            ReloadUI();
        }

        private void OnValidate()
        {
            Show();
        }
    }
}
