using System.Collections;
using TMPro;
using UnityEngine;

public class RadioController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private LayerMask layerMask;
    private Vector2 screenCenter;
    private bool wasPlaying = false;
    public GameObject Bed, DoctorRoomSound;
    public TMP_Text MissionText;

    [Header("Audio")]
    [SerializeField] private AudioSource Dream1_Radio, Dream2_Radio, Dream3_Radio;

    [Header("References")]
    private PlayerInput playerInput;
    private Camera mainCamera;

    // Buton referanslar�
    [SerializeField] private RadioButton playButton;
    [SerializeField] private RadioButton stopButton;

    // Durum kontrol�
    private bool isPlaying = false;


    private void Start()
    {
        EtkilesimUpdate(); // yatak etkile�imi
        Debug.Log("Dream1" + PlayerPrefs.GetInt("Dream1WakeUp"));
        playerInput = PlayerInput.Instance;
        playerInput.OnInteractTriggered += OnClick;

        mainCamera = Camera.main;

        //screenCenter = new Vector3 (Screen.width / 2, Screen.height / 2);

        stopButton?.SetPressed(true);
    }
    private void OnDisable()
    {
        if (playerInput == null) return;

        playerInput.OnInteractTriggered -= OnClick;
    }

    public void OnClick()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (!Physics.Raycast(ray, out RaycastHit hitInfo, 3f, layerMask))
            return;


        Debug.Log(hitInfo.collider.gameObject.name);

        // Radyo butonu mu?
        if (hitInfo.collider.gameObject.TryGetComponent<IClickable>(out IClickable radioButton))
        {
            RadioButtonType radioButtonType = radioButton.OnClicked();

            switch (radioButtonType)
            {
                case RadioButtonType.Play:
                    if (!isPlaying)
                        PlayRadio();
                    break;
                case RadioButtonType.Stop:
                    if (isPlaying)
                        StopRadio();
                    break;
            }
        }
    }
    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawRay(mainCamera.ScreenPointToRay(screenCenter));
    //}

    private void PlayRadio()
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

    private void StopRadio()
    {
        if (!isPlaying) return;

        Dream1_Radio?.Stop();
        Dream2_Radio?.Stop();
        Dream3_Radio?.Stop();

        isPlaying = false;

        playButton?.SetPressed(false);
        stopButton?.SetPressed(true);
    }


    // D��ar�dan ses de�i�tirmek i�in
    public void SetAudioClip(AudioClip clip)
    {

        // Yeni ses geldi�inde e�er oynuyorsa durdur
        if (isPlaying)
        {
            StopRadio();
        }

        PlayRadio();
    }

    private IEnumerator PlayAndWait(AudioSource audio)
    {
        if (audio == null)
        {
            Debug.LogWarning("AudioSource is null!");
            yield break;
        }

        isPlaying = true;
        playButton?.SetPressed(true);
        stopButton?.SetPressed(false);

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

