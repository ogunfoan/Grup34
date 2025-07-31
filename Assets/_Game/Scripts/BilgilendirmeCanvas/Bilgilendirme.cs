using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bilgilendirme : MonoBehaviour
{
    public GameObject bilgiCanvas;
    public Button NextButton, BackButton;
    public Image gosterilecekYer;
    public static Bilgilendirme Instance;
    public List<Sprite> bilgilendirmeSprites;
    private int currentIndex = 0;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Sprite sprite = Resources.Load<Sprite>("bilgilendirme/Kapak");
        bilgilendirmeSprites.Add(sprite);
        gosterilecekYer.sprite = bilgilendirmeSprites[currentIndex];

        if (PlayerPrefs.GetInt("Dream1WakeUp") == 1)
        {
            Sprite Dream1Sprite = Resources.Load<Sprite>("bilgilendirme/Dream1_WakeUp");
            bilgilendirmeSprites.Add(Dream1Sprite);
        }
        if (PlayerPrefs.GetInt("Dream2WakeUp") == 1)
        {
            Sprite Dream2Sprite = Resources.Load<Sprite>("bilgilendirme/Dream1_WakeUp");
            bilgilendirmeSprites.Add(Dream2Sprite);
        }
        if (PlayerPrefs.GetInt("Dream3WakeUp") == 1)
        {
            Sprite Dream3Sprite = Resources.Load<Sprite>("bilgilendirme/Dream1_WakeUp");
            bilgilendirmeSprites.Add(Dream3Sprite);
        }
        UpdateImage();
        UpdateButtonStates();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            bilgiCanvas.SetActive(!bilgiCanvas.activeSelf);
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            if (currentIndex < bilgilendirmeSprites.Count - 1)
            {
                currentIndex++;
                UpdateImage();
                UpdateButtonStates();
            }
            else
            {
                Debug.Log("fazla ileriledin.");
            }
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            if (currentIndex > 0)
            {
                currentIndex--;
                UpdateImage();
                UpdateButtonStates();
            }
            else
            {
                Debug.Log("daha fazla d��emezsin.");
            }
        }
    }
    private void UpdateImage()
    {
        gosterilecekYer.sprite = bilgilendirmeSprites[currentIndex];
    }

    private void UpdateButtonStates()
    {
        BackButton.interactable = currentIndex > 0;
        NextButton.interactable = currentIndex < bilgilendirmeSprites.Count - 1;
    }
}
