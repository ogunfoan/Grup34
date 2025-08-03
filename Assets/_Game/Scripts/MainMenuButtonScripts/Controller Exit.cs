using UnityEngine;

public class ControllerExit : MonoBehaviour
{
    public GameObject myCanvas;
    public void OpenCanvas()
    {
        myCanvas.SetActive(true);
    }
    public void CloseCanvas()
    {
        myCanvas.SetActive(false);
    }
}
