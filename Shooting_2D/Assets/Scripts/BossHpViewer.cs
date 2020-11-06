using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHpViewer : MonoBehaviour
{
    [SerializeField]
    private BossHp bossHp;
    private Slider sliderHp;

    private void Awake()
    {
        sliderHp = GetComponent<Slider>();
    }

    private void Update()
    {
        sliderHp.value = bossHp.CurrentHp / bossHp.MaxHp;
    }
}
