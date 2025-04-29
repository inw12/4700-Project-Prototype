using System.Collections;
using UnityEngine;

public class BurstFireRanged : MonoBehaviour, IEnemy
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float projectileSpd = 8f;
    [SerializeField] private int shotsPerBurst = 6;
    [SerializeField] private float burstRateOfFire = 0.25f;
    [SerializeField] private AudioSource audioSource;
    
    private Enemy enemy;

    private void Awake() {
        enemy = GetComponent<Enemy>();        
    }

    public void Attack() {
        if (Player.Instance != null && enemy.isAlive) {
            StartCoroutine(AttackRoutine());
        }
    }

    private IEnumerator AttackRoutine() {
        yield return new WaitForSeconds(Random.value);

        for (int i = 0; i < shotsPerBurst; i++) {
            // stop firing if enemy dies mid-burst
            if (!enemy.isAlive) break;

            Vector2 targetDirection = Player.Instance.transform.position - transform.position;

            // bullet spawns here
            GameObject newBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            newBullet.transform.right = targetDirection;
            
            audioSource.Play();
            
            if (newBullet.TryGetComponent(out EnemyProjectile projectile)) {
                projectile.UpdateProjectileSpeed(projectileSpd);
            }

            yield return new WaitForSeconds(burstRateOfFire);
        }
    }
}
