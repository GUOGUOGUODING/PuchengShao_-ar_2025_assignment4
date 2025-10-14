using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
public class Player_Controller : MonoBehaviour
{
    private InputAction moveAction;
    private InputAction jumpAction;
    private Rigidbody rb;
    public float speed = 1;
    public float jumpForce = 0.0f;
    private int numCollected = 0;
    public int total = 0;

    public GameObject youWinObject;
    public GameObject SpeedUpObject;
    public TextMeshProUGUI counterText;
    private bool isActive;
    private bool isPowerUp;

    public GameObject capsule;
    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move"); // Find the "Move" which has already been defined
        jumpAction = InputSystem.actions.FindAction("Jump");

        rb = GetComponent<Rigidbody>();

        youWinObject.SetActive(false);
        counterText.text = numCollected + "/" + total;
        isActive = false;
        rb.useGravity = false;
        capsule.SetActive(false);
        SpeedUpObject.SetActive(false);
        isPowerUp = false;
    }

    public void StartButton()
    {
        isActive = true;
        rb.useGravity = true;
        Debug.Log("Game Started!");
    }
    private void FixedUpdate()
    {
        if (isActive)
        {
            Vector2 moveValue = moveAction.ReadValue<Vector2>(); // W: [0, 1] S:[0, -1]

            Vector3 movement = new Vector3(moveValue.x, 0.0f, moveValue.y); // 

            rb.AddForce(movement * speed); // speed scale * direction
        }
    }
    // Update is called once per frame
    private void Update()
    {
        if (jumpAction.WasPressedThisFrame())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
        }
        // if (jumpAction.IsPressed())
        // {
        //     rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
        // } 
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectible"))
        {
            other.gameObject.SetActive(false);
            Debug.Log("Collision with" + other.gameObject.name);

            numCollected++;
            counterText.text = numCollected + "/" + total;
            if (numCollected >= total)
            {
                capsule.SetActive(true);
            }
        }

        if (other.CompareTag("Capsule"))
        {
            other.gameObject.SetActive(false);
            Debug.Log("Speed up!!!");
            speed *= 2;
            GetComponent<Renderer>().material.color = Color.blue;
            SpeedUpObject.SetActive(true);
            isPowerUp = true;
        }

        if (other.CompareTag("Goal"))
        {
            other.gameObject.SetActive(false);
            Debug.Log("You win!!!!");                       
            youWinObject.SetActive(true);
        }
    }


    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Breakable"))
        {
            if (isPowerUp)
            {
                BreakableWall_Controller wall = other.gameObject.GetComponent<BreakableWall_Controller>();
                if (wall != null)
                {
                    wall.Break();
                    Debug.Log("Break the wall!");
                }
            }
            else
            {
                Debug.Log("Hit wall, but no power-up — cannot break!");
            }
        }
    }

}
