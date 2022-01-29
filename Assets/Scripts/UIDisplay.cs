using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIDisplay : MonoBehaviour
{
    [SerializeField] Slider healthBar;
    [SerializeField] Slider invulnerabilityBar;
    WJChar player;

    void Start() {
        player = WJChar.Instance;
        healthBar.value = player.CurrentHP;
        invulnerabilityBar.value = WJGame.Instance.InvulGauge;
    }

    void Update()
    {
        healthBar.value = player.CurrentHP;
        invulnerabilityBar.value = WJGame.Instance.InvulGauge;
    }
}