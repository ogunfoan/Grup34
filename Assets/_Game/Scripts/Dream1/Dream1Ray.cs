using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Dream1Ray : MonoBehaviour
{

    [SerializeField] private Image Dream1;
    [SerializeField] private Image Dream2;
    [SerializeField] private Image Dream3;


    private int rayDistance = 5;
    bool Pacifier, Bear, Hammer, Paper, MusicBox, Shoes, Clothes, Vaccine, ToyCar;
    public TMP_Text CollectedText;
    public AudioSource PickUpSound;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerPrefs.SetInt("RadyoEtkilesim", 0);
        PlayerPrefs.SetInt("Collected", 0);
        CollectedText.text = PlayerPrefs.GetInt("Collected").ToString() + "/3";
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                switch (hit.collider.tag)
                {
                    case "Pacifier":
                        Pacifier = true;
                        PickUpSound.Play();
                        Animator PacifierAnimator = hit.collider.gameObject.GetComponent<Animator>();
                        hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                        PlayerPrefs.SetInt("Collected", PlayerPrefs.GetInt("Collected") + 1);
                        PacifierAnimator.Play("PacifierAnim");
                        Dream1Control();
                        break;

                    case "Bear":
                        Bear = true;
                        PickUpSound.Play();
                        Animator BearAnimator = hit.collider.gameObject.GetComponent<Animator>();
                        hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                        PlayerPrefs.SetInt("Collected", PlayerPrefs.GetInt("Collected") + 1);
                        BearAnimator.Play("BearAnim");
                        Dream1Control();
                        break;

                    case "Hammer":
                        Hammer = true;
                        PickUpSound.Play();
                        Animator HammerAnimator = hit.collider.gameObject.GetComponent<Animator>();
                        hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                        PlayerPrefs.SetInt("Collected", PlayerPrefs.GetInt("Collected") + 1);
                        HammerAnimator.Play("HammerAnim");
                        Dream1Control();
                        break;

                    case "Paper":
                        Paper = true;
                        PickUpSound.Play();
                        Animator PaperAnimator = hit.collider.gameObject.GetComponent<Animator>();
                        hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                        PlayerPrefs.SetInt("Collected", PlayerPrefs.GetInt("Collected") + 1);
                        PaperAnimator.Play("PaperAnim");
                        Dream2Control();
                        break;

                    case "MusicBox":
                        MusicBox = true;
                        PickUpSound.Play();
                        Animator MusicBoxAnimator = hit.collider.gameObject.GetComponent<Animator>();
                        hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                        PlayerPrefs.SetInt("Collected", PlayerPrefs.GetInt("Collected") + 1);
                        MusicBoxAnimator.Play("MusicBoxAnim");
                        Dream2Control();
                        break;

                    case "Shoes":
                        Shoes = true;
                        PickUpSound.Play();
                        Animator ShoesAnimator = hit.collider.gameObject.GetComponent<Animator>();
                        hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                        PlayerPrefs.SetInt("Collected", PlayerPrefs.GetInt("Collected") + 1);
                        ShoesAnimator.Play("ShoesAnim");
                        Dream2Control();
                        break;

                    case "Clothes":
                        Clothes = true;
                        PickUpSound.Play();
                        Animator ClothesAnimator = hit.collider.gameObject.GetComponent<Animator>();
                        hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                        PlayerPrefs.SetInt("Collected", PlayerPrefs.GetInt("Collected") + 1);
                        ClothesAnimator.Play("ClothesAnim");
                        Dream3Control();
                        break;

                    case "Vaccine":
                        Vaccine = true;
                        PickUpSound.Play();
                        Animator VaccineAnimator = hit.collider.gameObject.GetComponent<Animator>();
                        hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                        PlayerPrefs.SetInt("Collected", PlayerPrefs.GetInt("Collected") + 1);
                        VaccineAnimator.Play("VaccineAnim");
                        Dream3Control();
                        break;

                    case "ToyCar":
                        ToyCar = true;
                        PickUpSound.Play();
                        Animator ToyCarAnimator = hit.collider.gameObject.GetComponent<Animator>();
                        hit.collider.gameObject.GetComponent<Collider>().enabled = false;   
                        PlayerPrefs.SetInt("Collected", PlayerPrefs.GetInt("Collected") + 1);
                        ToyCarAnimator.Play("ToyCarAnim");
                        Dream3Control();
                        break;

                    default:
                        Debug.Log("Hicbir islem yapilmadi.");
                        Debug.Log("tag: " + hit.collider.tag);
                        Debug.Log("name: " + hit.collider.name);
                        break;
                }
            }
        }
    }


    private void Dream1Control()
    {
        CollectedText.text = PlayerPrefs.GetInt("Collected").ToString()+"/3";
        if (Pacifier == true && Bear == true && Hammer == true)
        {
            Sprite sprite = Resources.Load<Sprite>("bilgilendirme/Dream1_WakeUp");
            Bilgilendirme.Instance.bilgilendirmeSprites.Add(sprite);
            PlayerPrefs.SetInt("Dream1WakeUp", 1);
            PlayerPrefs.SetInt("YatakEtkilesim", 0);
            SceneManager.LoadScene("HospitalRoomMap");
            Debug.Log("1 Bolum Kazanildi.");
        }
        else { return; }
    }

    private void Dream2Control()
    {
        CollectedText.text = PlayerPrefs.GetInt("Collected").ToString() + "/3";
        if (Paper == true && MusicBox == true && Shoes == true)
        {
            Sprite sprite = Resources.Load<Sprite>("bilgilendirme/Dream2_WakeUp");
            Bilgilendirme.Instance.bilgilendirmeSprites.Add(sprite);
            PlayerPrefs.SetInt("Dream2WakeUp", 1);
            PlayerPrefs.SetInt("YatakEtkilesim", 0);
            SceneManager.LoadScene("HospitalRoomMap");
            Debug.Log("2 Bolum Kazanildi.");
        }
        else { return; }
    }

    public bool collectedAllItem = false;
    private void Dream3Control()
    {
        CollectedText.text = PlayerPrefs.GetInt("Collected").ToString() + "/3";
        if (Clothes == true && Vaccine == true && ToyCar == true)
        {
            collectedAllItem = true;
            Sprite sprite = Resources.Load<Sprite>("bilgilendirme/Dream3_WakeUp");
            Bilgilendirme.Instance.bilgilendirmeSprites?.Add(sprite);
            PlayerPrefs.SetInt("Dream3WakeUp", 1);
            PlayerPrefs.SetInt("YatakEtkilesim", 0);
            //SceneManager.LoadScene("HospitalRoomMap");
            Debug.Log("3 Bolum Kazanildi.");
        }
        else { return; }
    }


}
