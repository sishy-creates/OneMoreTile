using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MovePlayer : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private float gravity = -20f;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip loseClip;
    [Header("Arena Limits")]
    [SerializeField] private float minX = -4f;
    [SerializeField] private float maxX = 4f;
    [SerializeField] private float minZ = -4f;
    [SerializeField] private float maxZ = 4f;

    [Header("Game")]
    [SerializeField] private float fallLimit = -5f;
    [SerializeField] private GameUI gameUI;

    private CharacterController controller;
    private Vector3 velocity;
    private Vector2 moveInput;

    private bool isGameOver = false;
    private float score = 0f;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (isGameOver)
        {
            if (Keyboard.current.rKey.wasPressedThisFrame)
            {
                RestartGame();
            }
            return;
        }

        HandleMovement();
        ApplyGravity();
        ClampInsideArena();
        CheckFall();
        UpdateScore();
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void HandleMovement()
    {
        Vector3 move = new Vector3(moveInput.x, 0f, moveInput.y).normalized;
        controller.Move(move * moveSpeed * Time.deltaTime);
    }

    void ApplyGravity()
    {
        if (controller.isGrounded && velocity.y < 0f)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void ClampInsideArena()
    {
        Vector3 pos = transform.position;

        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.z = Mathf.Clamp(pos.z, minZ, maxZ);

        transform.position = pos;
    }

    void CheckFall()
    {
        if (transform.position.y < fallLimit)
        {
            GameOver();
        }
    }

    void GameOver()
    {

                if (audioSource != null && loseClip != null)
        {
    audioSource.PlayOneShot(loseClip);
    }
        isGameOver = true;
        Time.timeScale = 0f;

        if (gameUI != null)
        {
            gameUI.ShowGameOver();
        }

        Debug.Log("Final Score: " + score.ToString("F1"));

    }

    void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    void UpdateScore()
    {
        score += Time.deltaTime;

        if (gameUI != null)
        {
            gameUI.UpdateScore(score);
        }
    }
}