using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private float movementX;
    private float movementY;
    private Rigidbody rb;
    public float speed = 15f;

    public TextMeshProUGUI scoreText;
    private int score;

    public TextMeshProUGUI resultText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        scoreText.text = "Score: 0";
        resultText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0, movementY);
        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Collectable"))
        {
            other.gameObject.SetActive(false);
            updateScore();
        }
    }

    void updateScore()
    {
        score++;
        scoreText.text = "Score: " + score;
        if (score >= 12)
        {
            resultText.gameObject.SetActive(true);
            resultText.text = "You Win!";
            Destroy(GameObject.FindGameObjectWithTag("Enemy"));
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            resultText.gameObject.SetActive(true);
            resultText.text = "You Lose!";
            resultText.color = Color.red;
        }
    }
}
