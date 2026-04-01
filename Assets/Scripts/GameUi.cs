using UnityEngine;
using TMPro;

public class GameUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private TextMeshProUGUI restartText;

    public void UpdateScore(float score)
    {
        scoreText.text = "Time: " + score.ToString("F1");
    }

    public void ShowGameOver()
    {
        gameOverText.gameObject.SetActive(true);
        restartText.gameObject.SetActive(true);
    }

    void Start()
    {
        gameOverText.gameObject.SetActive(false);
        restartText.gameObject.SetActive(false);
    }
}