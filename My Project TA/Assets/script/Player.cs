using UnityEngine;
using UnityEngine.UI;

public class PlayerControllerMobile : MonoBehaviour
{
    [Header("Movement & Jump")]
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    public int maxJump = 2;

    [Header("UI Control")]
    public Button jumpButton;
    public Button interactButton;
    public InteractionUI uiManager;

    private Rigidbody2D rb;
    private Animator animator;
    private bool isGrounded;
    private int jumpCount = 0;
    private int moveDirection = 0;
    private bool jumpRequested = false;

    private InteractableInfo currentInfo;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // Setup buttons
        if (jumpButton != null)
            jumpButton.onClick.AddListener(() => jumpRequested = true);

        if (interactButton != null)
        {
            interactButton.gameObject.SetActive(true);
            interactButton.onClick.AddListener(Interact);
            interactButton.interactable = false;
        }
    }

    void Update()
    {
        // Ground check
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        if (isGrounded) jumpCount = 0;

        // Handle jump
        if (jumpRequested && jumpCount < maxJump)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            jumpCount++;
            jumpRequested = false;
        }

        // Animation
        animator.SetBool("run", moveDirection != 0 && isGrounded);
        animator.SetBool("jump", !isGrounded);
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveDirection * moveSpeed, rb.linearVelocity.y);
    }

    public void SetMoveDirection(int dir)
    {
        moveDirection = dir;
        if (dir != 0)
            transform.localScale = new Vector3(Mathf.Sign(dir), 1, 1);
    }

    void Interact()
    {
        if (currentInfo != null && uiManager != null)
        {
            uiManager.ShowInfo(currentInfo.imageToShow, currentInfo.title, currentInfo.textToShow);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<InteractableInfo>(out var info))
        {
            currentInfo = info;
            if (interactButton != null)
                interactButton.interactable = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent<InteractableInfo>(out var info) && info == currentInfo)
        {
            currentInfo = null;
            if (interactButton != null)
                interactButton.interactable = false;

            if (uiManager != null)
                uiManager.HideInfo();
        }
    }
}
