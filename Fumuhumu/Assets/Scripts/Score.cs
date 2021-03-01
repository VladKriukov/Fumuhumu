using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    TMP_Text scoreText;
    int score;

    private void Awake()
    {
        scoreText = GetComponent<TMP_Text>();
    }

    void OnEnable()
    {
        Finish.OnCollected += OnCollected;
    }

    private void OnDisable()
    {
        Finish.OnCollected -= OnCollected;
    }

    void OnCollected()
    {
        score++;
        scoreText.text = "" + score;
    }

}
