using UnityEngine;
using UnityEngine.Serialization;

namespace Management
{
    public class PauseMenu : MonoBehaviour
    {
        public static bool isGamePaused = false;

        [SerializeField] public GameObject pausePanel;
        private float oldTimeScale = 1;
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (isGamePaused)
                    Resume();
                else
                    Pause();
            }        
        }

        public void Resume()
        {
            isGamePaused = false;
            Time.timeScale = oldTimeScale;
            pausePanel.SetActive(false);
        }

        public void Pause()
        {
            isGamePaused = true;
            oldTimeScale = Time.timeScale;
            Time.timeScale = 0;
            pausePanel.SetActive(true);
        }
    
    }
}
