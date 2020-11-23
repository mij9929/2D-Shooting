using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyHpViewer : MonoBehaviour
{
    private EnemyHp enemyHP;
    private Slider hpSlider;

    public void SetUp(EnemyHp enemyHP)
    {
        this.enemyHP = enemyHP;
        hpSlider = GetComponent<Slider>();
    }

    private void Update()
    {
        hpSlider.value = enemyHP.CurrentHp / enemyHP.MaxHP;
    }
}
