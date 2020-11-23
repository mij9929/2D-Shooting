using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerBoom : MonoBehaviour
{
    [SerializeField]
    private AnimationCurve curve;
    [SerializeField]
    private AudioClip boomAudio;
    private float boomDelay = 0.5f;
    private Animator animator;
    private AudioSource audioSource;
    [SerializeField]
    private int damage = 100;
    

    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        StartCoroutine("MoveToCenter");
    }

    private IEnumerator MoveToCenter()
    {
        Vector3 startPosition = transform.position;
        Vector3 endPosition = Vector3.zero;
        float current = 0;
        float percent = 0;

        while(percent < 1 )
        {
            current += Time.deltaTime;
            percent = current / boomDelay;
            transform.position = Vector3.Lerp(startPosition, endPosition, curve.Evaluate(percent));

            yield return null;
        }

        animator.SetTrigger("onBoom");
        // 사운드 change

        audioSource.clip = boomAudio;
        audioSource.Play();

    }

    public void OnBoom()
    {
        GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] meteorites = GameObject.FindGameObjectsWithTag("Meteorite");
        GameObject[] projectiles = GameObject.FindGameObjectsWithTag("EnemyProjectile");
        GameObject[] bosses = new GameObject[] { GameObject.Find("Boss"), GameObject.Find("Boss1"), GameObject.Find("Boss2") };

        for (int i =0; i<enemys.Length; i++)
        {
            enemys[i].GetComponent<Enemy>().OnDie();
        }

        for(int i =0; i < meteorites.Length; i++)
        {
            meteorites[i].GetComponent<Mateorite>().OnDie();
        }

        for (int i  = 0; i < projectiles.Length; i++)
        {
            projectiles[i].GetComponent<EnemyProejctile>().OnDie();
        }

        Debug.Log(bosses.Length);
        bosses[GameObject.Find("EnemySpanwer").GetComponent<EnemySpawner>().stage].GetComponent<BossHp>().TakeDamage(damage);
 

        Destroy(gameObject);
    }
}
    