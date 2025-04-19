using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] private FloatValue damage;   
    [SerializeField] private float projectileSpd = 20f;
    [SerializeField] private float knockbackThrust;
    [SerializeField] private float knockbackDuration;

    private void Update() {
        MoveProjectile();
    }

    // shoot in target direction
    private void MoveProjectile() {
        transform.Translate(projectileSpd * Time.deltaTime * Vector3.right);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && other.isTrigger)
        {
            Player player = other.GetComponent<Player>();
            if (player != null) {
                player.TakeDamage(damage.value, knockbackThrust, knockbackDuration);
                Destroy(gameObject);
            }
        }
        else if (!other.isTrigger) {
            Destroy(gameObject);
        }
    }
}

