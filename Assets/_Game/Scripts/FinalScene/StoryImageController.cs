using DG.Tweening;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class StoryImageController : MonoBehaviour
{
    public Image[] ImageHolders; // 0,1,2 þeklinde sýrayla ekle
    public Image[] Images; // 0,1,2 þeklinde sýrayla ekle
    public float fadeDuration = 1f;
    public float showDuration = 2f; // gösterimde kalma süresi

    private int currentIndex = 0;
    private bool isWaitingForInput = false; // Týklama bekliyor mu?


    public Image inputImage;
    public GameObject lastText;

    void Start()
    {
        foreach (var p in Images)
        {
            p.color = new Color(1, 1, 1, 0); // Tümünü görünmez baþlat
        }

        ShowPanel(currentIndex);
    }

    void Update()
    {

        if (isWaitingForInput && Input.GetMouseButtonDown(0))
        {
            NextPanel();
        }
    }

    void ShowPanel(int index)
    {
        Images[index].DOFade(1f, fadeDuration)
            .OnComplete(() =>
            {
                ImageHolders[index].rectTransform.DOScale(1.02f, showDuration)
                    .SetLoops(-1, LoopType.Yoyo)
                    .SetEase(Ease.InOutSine);

                isWaitingForInput = true;
                inputImage.enabled = isWaitingForInput;

                //Invoke(nameof(NextPanel), showDuration + fadeDuration);
            });
    }

    void NextPanel()
    {
        isWaitingForInput = false;
        inputImage.enabled = isWaitingForInput;

        Images[currentIndex].DOFade(0f, fadeDuration);

        currentIndex++;
        if (currentIndex < Images.Length)
        {
            ShowPanel(currentIndex);
        }
        else
        {
            Debug.Log("Hikaye Bitti!");
            lastText.SetActive(true);
        }
    }
}
