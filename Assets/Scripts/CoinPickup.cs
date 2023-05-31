using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] private AudioClip coinPickupSfx;
    [SerializeField] private int pointsForCoinPickup = 100;

    private const bool IsCollected = false;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.GetComponent<PlayerMovement>() || IsCollected) return;
        FindObjectOfType<GameSession>().AddToScore(pointsForCoinPickup);
        AudioSource.PlayClipAtPoint(coinPickupSfx, Camera.main.transform.position);
        Destroy(gameObject);
    }
}
