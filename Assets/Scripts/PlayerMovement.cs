using UnityEngine;
//Acess the new Unity Input System Classes
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private PlayerInputsControlls pInput;
    private Rigidbody2D rb;

    private float moveInput = 0f;
    private float jumpInput = 0f;

    public float speed = 7f;
    public float jumpHeight = 10f;

    private bool isRight = true;

    private void Awake() => pInput = new PlayerInputsControlls();

    private void OnEnable() => pInput.Enable();

    private void OnDisable() => pInput.Disable();

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //Getting Input
        moveInput = pInput.PlayerKeys.Horizontal.ReadValue<float>();

        jumpInput = pInput.PlayerKeys.Jump.ReadValue<float>();
    }

    private void FixedUpdate()
    {
        if (isRight && moveInput < 0f) FlipSprite(); //isRight == true && moving left
        if (!isRight && moveInput > 0f) FlipSprite(); //isRight == false && moving right

        //Move the player
        transform.position += new Vector3(moveInput * speed * Time.fixedDeltaTime, 0f);

        if (jumpInput > 0f && Mathf.Abs(rb.velocity.y) < 0.000001f)
        {
            rb.AddForce(new Vector2(0f, jumpHeight), ForceMode2D.Impulse);
        }
    }

    private void FlipSprite()
    {
        transform.localScale = new Vector3(transform.localScale.x * -1f, transform.localScale.y);
        //Positive -> Negative
        //Negative -> Positive

        isRight = !isRight;
    }
}
