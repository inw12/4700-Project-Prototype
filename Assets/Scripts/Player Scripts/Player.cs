using System.Collections;
using UnityEngine;

public enum PlayerState
{
    idle,
    moving,
    stagger
}

public class Player : MonoBehaviour
{
    public PlayerState currentState;
    public static Player Instance { get; private set; }
    public bool FacingLeft { get { return isFacingLeft; } set { isFacingLeft = value; } }

    [SerializeField] private FloatValue HP;
    [SerializeField] private Signal playerHealthSignal;
    [SerializeField] private FloatValue moveSpeed;
    [SerializeField] private FloatValue dashSpeed;
    [SerializeField] private TrailRenderer trailRenderer;
    [SerializeField] private Collider2D hurtbox;

    private PlayerControls playerControls;
    private Vector2 movement;
    private Rigidbody2D myRigidBody;
    private Animator myAnimator;
    private SpriteRenderer mySpriteRenderer;
    private Knockback knockback;
    private Flash flash;
    private bool isFacingLeft = false;
    private bool isDashing = false;

    private void Awake()
    {
        Instance = this;
        playerControls = new PlayerControls();
        currentState = PlayerState.idle;
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        knockback = GetComponent<Knockback>();
        flash = GetComponent<Flash>();
    }

    private void OnEnable() {
        playerControls.Enable();
    }

    private void Start() {
        playerControls.Movement.Dash.performed += _ => Dash();
    }

    private void Update() {
        PlayerInput();
    }

    private void FixedUpdate() {
        if (knockback.GettingKnockedBack) return;    
        AdjustPlayerFacingDirection();
        Move();
    }

    private void PlayerInput()
    {
        movement = playerControls.Movement.Move.ReadValue<Vector2>();
        if (movement != Vector2.zero)
        {
            myAnimator.SetFloat("moveX", movement.x);
            myAnimator.SetFloat("moveY", movement.y);
            myAnimator.SetBool("isMoving", true);
        }
       else {
            myAnimator.SetBool("isMoving", false);
        }
        
    }

    private void Move() {
        myRigidBody.MovePosition(myRigidBody.position + movement * (moveSpeed.runtimeValue * Time.fixedDeltaTime));
    }

    private void Dash()
    {
        if (!isDashing) {
            isDashing = true;
            moveSpeed.runtimeValue = dashSpeed.value;
            trailRenderer.emitting = true;
            StartCoroutine(DashRoutine());
        }
    }

    private void AdjustPlayerFacingDirection()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);

        if (mousePos.x < playerScreenPoint.x) {
            mySpriteRenderer.flipX = true;
            FacingLeft = true;
        } else {
            mySpriteRenderer.flipX = false;
            FacingLeft = false;
        }
    }

    public void TakeDamage(float damageReceived, float knockbackThrust, float knockbackDuration)
    {
        HP.runtimeValue -= damageReceived;
        playerHealthSignal.Raise();
        knockback.TriggerKnockback(Enemy.Instance.transform, knockbackThrust, knockbackDuration);
        StartCoroutine(flash.FlashRoutine());
        CheckForDeath();
    }

    public void CheckForDeath() {
        if (HP.runtimeValue <= 0) {
            Destroy(gameObject);
        }
    }

    private IEnumerator DashRoutine()
    {
        // Initiate Dash
        float dashTime = .08f;
        float dashCD = .24f;
        hurtbox.enabled = false;
        yield return new WaitForSeconds(dashTime);
        // End Dash
        moveSpeed.runtimeValue = moveSpeed.value;
        trailRenderer.emitting = false;
        hurtbox.enabled = true;
        yield return new WaitForSeconds(dashCD);
        isDashing = false;
    }
}
