using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private float movementX;
    private float movementY;
    private Rigidbody rb;
    public float speed = 15f;

    public TextMeshProUGUI scoreText;
    private int score;

    public TextMeshProUGUI resultText;
    public GameObject resultPanel;

    private bool? isWin;

    public Button retryButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        scoreText.text = "Score: 0";
        resultText.gameObject.SetActive(false);
        resultPanel.SetActive(false);
        retryButton.onClick.AddListener(() =>
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        });
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMove(InputValue movementValue)
    {
        if (isWin == true) return;
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    private void FixedUpdate()
    {
        if (isWin == true)
        {
            rb.AddForce(Vector3.zero);
            // remove acceleration
            rb.linearVelocity = Vector3.zero;
            return;
        }

        if (transform.position.y < -10)
        {
            gameObject.SetActive(false);
            showResult(false);
            return;
        }

        Vector3 movement = new(movementX, 0, movementY);
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
            showResult(true);
            GameObject.FindGameObjectWithTag("Enemy").SetActive(false);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            gameObject.SetActive(false);
            showResult(false);
        }
    }

    void showResult(bool isWin)
    {
        resultText.gameObject.SetActive(true);
        resultText.text = isWin ? "You Win!" : "You Lose!";
        resultPanel.SetActive(true);
        this.isWin = isWin;
    }
}
