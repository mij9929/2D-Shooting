using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public enum BossState {  MoveToApperPoint = 0, Phase01, Phase02, Phase03 }

public class Boss : MonoBehaviour
{
    [SerializeField]
    private StageData stageData;
    [SerializeField]
    private GameObject bossExplosionPrefab;
    [SerializeField]
    private PlayerController playerController;
    [SerializeField]
    private string nextSceneName;
    [SerializeField]
    private float bossAppearPoint = 2.5f;
    private BossState bossState = BossState.MoveToApperPoint;
    private Movement2D movement2D;
    private BossWeapon bossweapon;
    private BossHp bossHP;

    private void Awake()
    {
        movement2D = GetComponent<Movement2D>();
        bossweapon = GetComponent<BossWeapon>();
        bossHP = GetComponent<BossHp>();
    }

    public void ChangeState(BossState newState)
    {
        Debug.Log((int)bossState);
        StopCoroutine(bossState.ToString());

        bossState = newState;

        StartCoroutine(bossState.ToString());
    }

    private IEnumerator MoveToApperPoint()
    {
        movement2D.MoveTo(Vector3.down);

        while(true)
        {
            if(transform.position.y <= bossAppearPoint)
            {
                movement2D.MoveTo(Vector3.zero);

                ChangeState(BossState.Phase01);
            }

            yield return null;
        }
    }

    private IEnumerator Phase01()
    {

        bossweapon.StartFiring(AttackType.CircleFire);

        while(true)
        {
            if(bossHP.CurrentHp <= bossHP.MaxHp * 0.7f)
            {
                // 원 방사 형태의 공격 중지
                bossweapon.StopFiring(AttackType.CircleFire);
                // phase02로 변경
                ChangeState(BossState.Phase02);
            }
             
            yield return null;
        }
    }

    private IEnumerator Phase02()
    {
        bossweapon.StartFiring(AttackType.SingleFireToCenterPosition);

        Vector3 direction = Vector3.right;
        movement2D.MoveTo(direction);

        while (true)
        {
            if (transform.position.x < stageData.LimitMin.x ||
                transform.position.x > stageData.LimitMax.x)
            {
                direction *= -1;
                movement2D.MoveTo(direction);
                // 0.5 딜레이를 줘서 Boss가 맵을 밖으로 빠져나가지 않도록 함
                yield return new WaitForSeconds(0.5f);
            }


            if (bossHP.CurrentHp <= bossHP.MaxHp * 0.3f)
            {
                bossweapon.StopFiring(AttackType.SingleFireToCenterPosition);
                // phase03으로 변경
                ChangeState(BossState.Phase03);
            }

            yield return null;
        }
    }

    private IEnumerator Phase03()
    {
        //  원 방사 형태의 공격 시작
        bossweapon.StartFiring(AttackType.CircleFire);
        bossweapon.StartFiring(AttackType.SingleFireToCenterPosition);

        Vector3 direction = Vector3.right;
        movement2D.MoveTo(direction);


        while(true)
        {
            if (transform.position.x < stageData.LimitMin.x || transform.position.x > stageData.LimitMax.x)
            {
                Debug.Log("Test");
                direction *= -1;
                movement2D.MoveTo(direction);
          
            }

            yield return null;
        }
        
    }

    public void OnDie()
    {
        GameObject clone = Instantiate(bossExplosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
        GameObject.Find("EnemySpanwer").GetComponent<EnemySpawner>().stage++;

        if (GameObject.Find("EnemySpanwer").GetComponent<EnemySpawner>().stage > 2)
        {
            clone.GetComponent<BossExplosion>().SetUp(playerController, nextSceneName);
        }

        else
            GameObject.Find("EnemySpanwer").GetComponent<EnemySpawner>().StartSpawn();
    }
}
