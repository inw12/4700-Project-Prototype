using UnityEngine;

public class TestDummy : Enemy
{
    [SerializeField] private float baseAttack = 1f;
    [SerializeField] private float knockbackThrust;
    [SerializeField] private float knockbackDuration;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Player>()) {
            Player player = other.GetComponent<Player>();
            if (player != null) {
                player.TakeDamage(baseAttack, knockbackThrust, knockbackDuration);
            }
            else {
                Debug.LogWarning("Player component not found on the object with tag 'Player'.");
            }
        }
    }
}
