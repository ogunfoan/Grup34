using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.SceneManagement;

public class RaycastScript : MonoBehaviour
{

    [SerializeField] private RadioButton playButton;
    public static RaycastScript instance;
    public float rayDistance = 5f; // Ray'in ne kadar uza�a gidece�i
    public Canvas TimerCanvas;
    public LayerMask layerMask, interactableLayer;
    public Canvas DreamCanvas;

    public TMP_Text interactableText;


    // bulunacak nesneler
    bool Emzik, Ayicik, Cingirak;

    public AudioSource Dream1_Radio, Dream2_Radio, Dream3_Radio;
    public GameObject Bed, DoctorRoomSound;
    bool isPlaying;


    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (interactableText != null)
        {
            interactableText.enabled = false;
        }
        else { return; }

        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        // Etkile�imli nesneler i�in raycast
        if (Physics.Raycast(ray, out hit, rayDistance, interactableLayer))
        {
            if (interactableText != null)
            {
                interactableText.enabled = true;
            }
            else { return; }

            // E tu�una bas�ld�ysa etkile�imi �al��t�r
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("e");
                switch (hit.collider.tag)
                {
                    case "Yatak":
                        StartCoroutine(DreamLoad());                        
                        break;

                    case "PlayButton":
                        Debug.Log("butona geldi");
                        PlayButtonClicked();
                        break;

                    default:
                        Debug.Log("Hi�bir i�lem yap�lmad�.");
                        break;
                }
            }
        }
    }

    IEnumerator DreamLoad()
    {
        DreamCanvas.gameObject.SetActive(true);
        yield return new WaitForSeconds(2.49f);
        
        if (PlayerPrefs.GetInt("Dream1WakeUp") == 0)
        {
            SceneManager.LoadScene("DreamNo1");
        }
        else if (PlayerPrefs.GetInt("Dream2WakeUp") == 0)
        {
            SceneManager.LoadScene("Dream2_New");
        }
        else if (PlayerPrefs.GetInt("Dream3WakeUp") == 0)
        {
            SceneManager.LoadScene("DreamNo3");
        }
    }

    public void PlayButtonClicked()
    {
        if (DoctorRoomSound != null)
        {
            DoctorRoomSound.GetComponent<AudioSource>().Stop();
        }

        playButton?.SetPressed(true);

        if (PlayerPrefs.GetInt("Dream1WakeUp") == 0)
        {
            StartCoroutine(PlayAndWait(Dream1_Radio));
        }
        else if (PlayerPrefs.GetInt("Dream2WakeUp") == 0)
        {
            StartCoroutine(PlayAndWait(Dream2_Radio));
        }
        else if (PlayerPrefs.GetInt("Dream3WakeUp") == 0)
        {
            StartCoroutine(PlayAndWait(Dream3_Radio));
        }
        PlayerPrefs.SetInt("RadyoEtkilesim", 0);
        PlayDoctorRoomSound.instance.etkilesimUpdate();
    }
        
    private IEnumerator PlayAndWait(AudioSource audio)
    {
        if (audio == null)
        {
            Debug.LogWarning("AudioSource is null!");
            yield break;
        }

        isPlaying = true;

        audio.Play();

        yield return new WaitWhile(() => audio.isPlaying); // Ses bitene kadar bekle

        isPlaying = false;
        OnAudioFinished(); // Ses bittikten sonra çağrılır
    }

    private void OnAudioFinished()
    {
        PlayerPrefs.SetInt("YatakEtkilesim", 1);
        PlayerPrefs.Save();
        SoundsPrefs.instance.Missions();
        EtkilesimUpdate();
    }

    private void EtkilesimUpdate()
    {
        if (Bed == null) return;

        if (PlayerPrefs.GetInt("YatakEtkilesim") == 1)
        {
            Bed.tag = "Yatak";
            Bed.layer = 10;
        }
    }
}
