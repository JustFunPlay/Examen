using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private TMPro.TextMeshProUGUI scoreText;
    [SerializeField] private TMPro.TextMeshProUGUI livesText;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private RawImage icon;

    public void OnSetup(Character character)
    {
        character.OnChangeHealth = UpdateHealthSlider;
        icon.texture = character.iconRender;
        gameObject.SetActive(true);
    }
    private void Start()
    {
        GameManager.OnChangeLives += UpdateLivesText;
        GameManager.OnChangeScore += UpdateScoreText;
    }
    private void OnDestroy()
    {
        GameManager.OnChangeLives -= UpdateLivesText;
        GameManager.OnChangeScore -= UpdateScoreText;
    }
    private void UpdateHealthSlider(int health)
    {
        healthSlider.value = health;
    }
    private void UpdateScoreText()
    {
        if (player == Player.Player1)
            scoreText.text = GameManager.Player1Score.ToString();
        else
            scoreText.text = GameManager.Player2Score.ToString();
    }

    private void UpdateLivesText()
    {
        if (player == Player.Player1)
            livesText.text = $"X{GameManager.Player1Lives}";
        else
            livesText.text = $"X{GameManager.Player2Lives}";
    }
}
