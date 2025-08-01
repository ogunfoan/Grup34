using System.Collections;
using UnityEngine;

public class PlayBUttonCOntroller : MonoBehaviour
{
    public AudioSource Dream1_Radio, Dream2_Radio, Dream3_Radio;
    public GameObject Bed;
    public LayerMask interactableLayer;
    bool isPlaying;


    private void Update()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 5f, interactableLayer))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Yeni kod e");
                switch (hit.collider.tag)
                {
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
    public void PlayButtonClicked()
    {
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
