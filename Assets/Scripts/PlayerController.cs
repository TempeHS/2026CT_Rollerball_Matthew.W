using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;
    public float boostedSpeed = 50f;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public float boostedDuration = 5f;

    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;
    private bool isBoosted = false;
    private float boostedTimer = 0f;
    private float originalSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count=0;

        originalSpeed = speed;

        SetCountText();
        winTextObject.SetActive(false);
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if(count >= 17)
        {
            winTextObject.SetActive(true);

            Destroy(GameObject.FindGameObjectWithTag("Enemy"));
        }
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3 (movementX, 0.0f, movementY);

        rb.AddForce(movement * speed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);

            winTextObject.gameObject.SetActive(true);
            winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";
        }
        else if (collision.gameObject.CompareTag("Doom"))
        {
            Destroy(gameObject);

            winTextObject.SetActive(true);
            winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";
        }
       
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PickUp"))
       {
            other.gameObject.SetActive(false);
            count = count + 1;

            SetCountText();
       }
       else if(other.gameObject.CompareTag("Speed Up"))
       {
            other.gameObject.SetActive(false);

            speed = boostedSpeed;
            isBoosted = true;
            boostedTimer = boostedDuration;
       }
    }

    void Update()
    {
        if (isBoosted)
        {
            boostedTimer -= Time.deltaTime;

            if (boostedTimer <= 0)
            {
                isBoosted = false;
                speed = originalSpeed;
            }

        }
    } 
}
