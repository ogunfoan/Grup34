using UnityEngine;

public class Dream3PlayedSc : MonoBehaviour
{
    void Start()
    {
        PlayerPrefs.SetInt("Dream3Played", 1);
    }
}
