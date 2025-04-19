using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash : MonoBehaviour
{
    [SerializeField] private Material material;
    [SerializeField] private float duration = .1f;

    private Material defaultMat;
    private SpriteRenderer spriteRenderer;
    private Enemy enemy;

    private void Awake() {
        enemy = GetComponent<Enemy>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultMat = spriteRenderer.material;
    }

    public float GetRestoreMatTime() {
        return duration;
    }

    public IEnumerator FlashRoutine() {
        spriteRenderer.material = material;
        yield return new WaitForSeconds(duration);
        spriteRenderer.material = defaultMat;
        enemy.CheckForDeath();
    }
}
