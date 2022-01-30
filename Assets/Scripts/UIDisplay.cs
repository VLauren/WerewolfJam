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
    [SerializeField] Image kidImage;
    [SerializeField] Image wolfImage;
    [SerializeField] Sprite kidMaskSprite;
    [SerializeField] Sprite wolfMaskSprite;
    [SerializeField] Image maskImage;
    WJChar player;
    WJScoreKeeper scoreKeeper;

    void Start() {
        player = WJChar.Instance;
        scoreKeeper = WJScoreKeeper.Instance;
        healthBar.value = player.CurrentHP;

        invulnerabilityBar.value = WJGame.Instance.InvulGauge * 0.58f;

        scoreText.text = "Score: " + scoreKeeper.GetScore().ToString("000");
        kidImage.gameObject.SetActive(true);
        wolfImage.gameObject.SetActive(false);
    }

    void Update()
    {
        healthBar.value = player.CurrentHP;
        invulnerabilityBar.value = WJGame.Instance.InvulGauge;
        scoreText.text = "Score: " + scoreKeeper.GetScore().ToString("000");
        if (WJUtil.IsOnDaySide(WJChar.Instance.transform.position)) {
            kidImage.gameObject.SetActive(true);
            wolfImage.gameObject.SetActive(false);
            maskImage.sprite = kidMaskSprite;
        } else {
            kidImage.gameObject.SetActive(false);
            wolfImage.gameObject.SetActive(true);
            maskImage.sprite = wolfMaskSprite;
        }
    }
}