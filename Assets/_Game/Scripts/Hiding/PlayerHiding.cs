using UnityEngine;

public class PlayerHiding : MonoBehaviour
{
    private bool isHiding = false;
    private CharacterController controller;
    private PlayerMovement movementScript; // kendi hareket script'in

    void Start()
    {
        controller = GetComponent<CharacterController>();
        movementScript = GetComponent<PlayerMovement>(); // varsa
    }

    public void ToggleHiding(Transform hidingSpot)
    {
        if (!isHiding)
        {
            // Saklan
            movementScript.enabled = false;
            controller.enabled = false;
            transform.position = hidingSpot.position;
            isHiding = true;
        }
        else
        {
            // Çık
            movementScript.enabled = true;
            controller.enabled = true;
            isHiding = false;
        }
    }
}
