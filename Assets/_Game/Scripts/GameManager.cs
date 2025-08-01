using TMPro;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject DreamBilgilendirmeCanvas;
    public AudioSource Dream1WakeUpSound, Dream2WakeUpSound;
    private bool isGameStopped = false;
    public Volume menuBlur;
    //public Texture[] menuUi;
    //public UnityEngine.UIElements.Image image;
    public UnityEngine.UI.Image imageUI;
    public GameObject adsPanel;

    public TextMeshProUGUI textUI;
     
    private void Awake()
    {
        #region PlayerPrefs kayýtlarýný sýfýrlamak için burayý yorum satýrýndan çýkarýn
        /* 
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        */
        // bunu yorum satýrýndan çýkarýp, WakeUpSound scriptine gidin.
        #endregion
    }

    private void OnEnable()
    {
        GameInput.Instance.OnEscapeTriggered += ShowMenu;
    }

    private void OnDisable()
    {
        GameInput.Instance.OnEscapeTriggered -= ShowMenu;
    }

    void Start()
    {
        PlayerPrefs.SetInt("RadyoEtkilesim", 0);
        Cursor.lockState = CursorLockMode.Locked;

        if (PlayerPrefs.GetInt("Dream2WakeUp") == 1)
        {
            StartCoroutine(PlayWakeUpAndContinue(Dream2WakeUpSound));
        }
        else if (PlayerPrefs.GetInt("Dream1WakeUp") == 1)
        {
            StartCoroutine(PlayWakeUpAndContinue(Dream1WakeUpSound));
        }
        else
        {
            return;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (DreamBilgilendirmeCanvas != null)
            {
                DreamBilgilendirmeCanvas.SetActive(!DreamBilgilendirmeCanvas.activeSelf);
            }
        }
    }

    private void ShowMenu()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0) return;

        if (!isGameStopped)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            adsPanel?.SetActive(true);
            //image.image = menuUi[0];
            imageUI.enabled = true;
            textUI.enabled = true;
            menuBlur.enabled = true;
            AudioListener.pause = true;
            isGameStopped = true;
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;

            adsPanel?.SetActive(false);

            //image.image = null;
            imageUI.enabled = false;
            textUI.enabled = false;
            menuBlur.enabled = false;
            AudioListener.pause = false;
            isGameStopped = false;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = false;

            StartCoroutine(ReLockCursorNextFrame());

        }
    }
    private IEnumerator ReLockCursorNextFrame()
    {
        yield return null;
        Cursor.lockState = CursorLockMode.Locked;
    }


    private IEnumerator PlayWakeUpAndContinue(AudioSource audio)
    {
        if (audio == null)
        {
            Debug.LogWarning("WakeUp AudioSource is null!");
            yield break;
        }

        audio.Play();
        yield return new WaitWhile(() => audio.isPlaying);

        OnWakeUpFinished(); // Ses bittiðinde yapýlacaklar
    }

    private void OnWakeUpFinished()
    {
        Debug.Log("SES BÝTTÝ");
        PlayerPrefs.SetInt("RadyoEtkilesim", 1);
        PlayerPrefs.Save();
        PlayDoctorRoomSound.instance.etkilesimUpdate();
    }
}
