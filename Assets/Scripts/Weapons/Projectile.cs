using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private FloatValue damage;  
    [SerializeField] private float projectileSpd = 22f;
    [SerializeField] private float knockbackThrust;
    [SerializeField] private float knockbackDuration;

    private void Update() {
        MoveProjectile();
    }

    private void MoveProjectile() {
        transform.Translate(projectileSpd * Time.deltaTime * Vector3.right);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Enemy>())
        {
            Enemy enemy = other.GetComponent<Enemy>();
            enemy.TakeDamage(damage.value, knockbackThrust, knockbackDuration);
            Destroy(gameObject);
        }
        else if (!other.isTrigger) {
            Destroy(gameObject);
        }
    }
}

