using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIDisplay : MonoBehaviour
{
    [SerializeField] Slider healthBar;
    [SerializeField] Slider invulnerabilityBar;
    [SerializeField] TextMeshProUGUI scoreText;
    WJChar player;
    WJScoreKeeper scoreKeeper;

    void Start() {
        player = WJChar.Instance;
        scoreKeeper = WJScoreKeeper.Instance;
        healthBar.value = player.CurrentHP;
        invulnerabilityBar.value = WJGame.Instance.InvulGauge;
        scoreText.text = "Puntos: " + scoreKeeper.GetScore().ToString("000000000");
    }

    void Update()
    {
        healthBar.value = player.CurrentHP;
        invulnerabilityBar.value = WJGame.Instance.InvulGauge;
        scoreText.text = "Puntos: " + scoreKeeper.GetScore().ToString("000000000");
    }
}