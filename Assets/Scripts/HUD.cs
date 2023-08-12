using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI lifeLeftText;

    public TextMeshProUGUI GetLifeLeftText() => lifeLeftText;
}
