
using System.Collections;
using System.Collections.Generic;
using Unity; 
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float damage = 1; // 데미지
    [SerializeField]
    private int scorePoint = 100; // 점수
    [SerializeField]
    private GameObject explosionPrefab; // 폭발 효과
    private PlayerController playerController;
    [SerializeField]
    private GameObject[] itemPrefab;

    private void Awake()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerHP>().TakeDamage(damage);
            OnDie();
        } 
    }

    public void OnDie()
    {
        playerController.Score += scorePoint;
        // 폭발 이펙트 생성
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        SpawnItem();
        Destroy(gameObject);
    }

    private void SpawnItem()
    {
        int spawnItem = Random.Range(0, 100);
        if(spawnItem < 10)
        {
            Instantiate(itemPrefab[0], transform.position, Quaternion.identity);
        }
        else if(spawnItem < 15)
        {
            Instantiate(itemPrefab[1], transform.position, Quaternion.identity);
        }
        else if(spawnItem < 30)
        {
            Instantiate(itemPrefab[2], transform.position, Quaternion.identity);
        }
    }
}
