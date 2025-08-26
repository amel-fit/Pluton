
using UnityEngine;

namespace Management
{
    public class OptionsMenu : MonoBehaviour
    {
        [SerializeField]
        public Canvas optionsCanvas;
        [SerializeField]
        public Canvas mainMenuCanvas;


        private bool isDetectingKey = false;
        public void ChangeScreenMode()
        {
            Screen.fullScreen = !Screen.fullScreen;
        }

        public void ToggleOptionsCanvas()
        {

            mainMenuCanvas.enabled = !mainMenuCanvas.enabled;
            optionsCanvas.enabled = !optionsCanvas.enabled;
            
            
        }

        private void OnGUI()
        {
            if (!isDetectingKey) return;
            KeyCode newCode = GetDetectedKey();
            if (newCode == KeyCode.None) return;
            /*
             * Button.Text = newcode.Tostring()
             * -->> Can't get to the input manager with the Dictionary of keybinds
             * -->> also it's 27.8. 1:07AM and I just can't...  
             */
        }

        public KeyCode GetDetectedKey()
        {
            Event someEvent = Event.current;
            if (someEvent.isKey && someEvent.type == EventType.KeyDown)
            {
                return someEvent.keyCode;
            }
            else
            {
                return KeyCode.None;
            }
        }
    }
}