using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static Enemy Instance { get; private set; }

    [SerializeField] private FloatValue HP;
    [SerializeField] private float moveSpeed = 2f;

    private Rigidbody2D enemyRB;
    private SpriteRenderer mySpriteRenderer;
    private Vector2 movementDirection;
    private Knockback knockback;
    private Flash flash;
    
    private void Awake() {
        Instance = this;

        enemyRB = GetComponent<Rigidbody2D>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();

        knockback = GetComponent<Knockback>();
        flash = GetComponent<Flash>();
    }

    private void FixedUpdate() {
        if (knockback.GettingKnockedBack) return;       

        enemyRB.MovePosition(enemyRB.position + movementDirection * (moveSpeed * Time.fixedDeltaTime));
    }

    public void MoveTo(Vector2 targetPosition) {
        movementDirection = targetPosition;
    }

    public void TakeDamage(float receivedDamage, float knockbackThrust, float knockbackDuration)
    {
        // reduce HP
        HP.runtimeValue -= receivedDamage;
        // apply knockback
        knockback.TriggerKnockback(Player.Instance.transform, knockbackThrust, knockbackDuration);
        // damage flash effect
        StartCoroutine(flash.FlashRoutine());
        // check for death
        CheckForDeath();
    }

    public void CheckForDeath() {
        if (HP.runtimeValue <= 0) {
            Destroy(gameObject);
        }
    }

}
