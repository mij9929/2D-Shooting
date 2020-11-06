using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    [SerializeField]
    private float maxHp = 10;
    private float currentHp;
    private SpriteRenderer spriteRenderer;
    private PlayerController playerController;

    public float MaxHp => maxHp; // maxHP 변수에 접근할 수 있는 프로퍼티(Get만 가능)
    //public float CurrentHp => currentHp; // currentHp 변수에 접근할 수 있는 프로퍼티(Get만 가능)
    public float CurrentHp
    {
        set => currentHp = Mathf.Clamp(value, 0, maxHp);
        get => currentHp;
    }
    private void Awake()
    {
        currentHp = maxHp;
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerController = GetComponent<PlayerController>();
    }

    public void TakeDamage(float damage)
    {
        // 현재 체력을 감소
        currentHp -= damage;

        StopCoroutine("HitColorAnimation");
        StartCoroutine("HitColorAnimation");

        if(currentHp <= 0)
        {
            playerController.OnDie();
        }
    }

    private IEnumerator HitColorAnimation()
    {
        // 플레이어 색상을 빨간색으로
        spriteRenderer.color = Color.red;

        yield return new WaitForSeconds(0.1f);

        // 플레이어의 색상을 원래 색상인 하얀색으로
        spriteRenderer.color = Color.white;
    }
}
