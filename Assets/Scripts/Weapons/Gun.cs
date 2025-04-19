using UnityEngine;

public class Gun : MonoBehaviour, IWeapon
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform projectileSpawnPoint;

    public void Attack() {
        Instantiate(projectilePrefab, projectileSpawnPoint.position, Weapon.Instance.transform.rotation);
    }
}
