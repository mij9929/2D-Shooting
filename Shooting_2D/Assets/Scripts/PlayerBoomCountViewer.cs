using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBoomCountViewer : MonoBehaviour
{
    [SerializeField]
    private Weapon weapon;
    private TextMeshProUGUI textBoomCount;

    private void Awake()
    {
        textBoomCount = GetComponent<TextMeshProUGUI>();

    }

    private void Update()
    {
        textBoomCount.text = "x " + weapon.BoomCount;
    }
}
