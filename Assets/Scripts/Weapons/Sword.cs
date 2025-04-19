using UnityEngine;

public class Melee : MonoBehaviour, IWeapon
{
    [SerializeField] private GameObject hitboxPrefab;
    [SerializeField] private Transform hitboxSpawnPoint;

    [SerializeField] private Transform target;

    public void Attack() {
        GameObject hitbox = Instantiate(hitboxPrefab, hitboxSpawnPoint.position, Weapon.Instance.transform.rotation);
        hitbox.GetComponent<FollowPlayer>().SetTarget(target);
    }
}
