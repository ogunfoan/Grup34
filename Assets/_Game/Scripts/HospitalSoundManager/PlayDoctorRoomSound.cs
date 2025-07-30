using UnityEngine;

public class PlayDoctorRoomSound : MonoBehaviour
{
    public static PlayDoctorRoomSound instance;
    public GameObject CorridorObject, PlayButton;
    public AudioClip doctorRoomClip;
    private AudioSource audioSource;
    private bool hasPlayed = false;

    private void Awake()
    {
        instance = this;
        if (PlayerPrefs.GetInt("RadyoEtkilesim") == 1)
        {
            // radyo butonu ile etkile�imi a��yoruz.
            PlayButton.GetComponent<Collider>().enabled = true;
            MetallicController mc = PlayButton.GetComponent<MetallicController>();
            mc.StartMetallicEffect(2f);
        }
    }

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (!hasPlayed && other.CompareTag("Player") && PlayerPrefs.GetInt("DoctorRoomSound") == 0)
        {
            CorridorObject.GetComponent<AudioSource>().Stop();
            audioSource.PlayOneShot(doctorRoomClip);
            hasPlayed = true;
            PlayerPrefs.SetInt("DoctorRoomSound", 1);
            SoundsPrefs.instance.Missions();
            PlayerPrefs.SetInt("RadyoEtkilesim", 1);
            PlayerPrefs.Save();
            etkilesimUpdate();
        }
    }


    public void etkilesimUpdate()
    {
        if (PlayerPrefs.GetInt("RadyoEtkilesim") == 1)
        {
            // radyo butonu ile etkile�imi a��yoruz.
            PlayButton.GetComponent<Collider>().enabled = true;
            MetallicController mc = PlayButton.GetComponent<MetallicController>();
            mc.StartMetallicEffect(2f);
        }
    }
}
