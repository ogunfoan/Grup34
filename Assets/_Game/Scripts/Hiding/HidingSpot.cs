using UnityEngine;

public class HidingSpot : MonoBehaviour
{
    public Transform hidingPosition; // Oyuncunun içine geçeceği nokta
    private bool playerNearby = false;
    private GameObject player;

    void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.E))
        {
            player.GetComponent<PlayerHiding>().ToggleHiding(hidingPosition);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.gameObject;
            playerNearby = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
        }
    }
}
