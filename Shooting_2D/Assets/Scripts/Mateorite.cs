  using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mateorite : MonoBehaviour
{

    [SerializeField]
    private float damage;
    [SerializeField]
    private GameObject explosionPrefab; // 폭발 효과 프리팹

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerHP>().TakeDamage(damage);
            //Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            //Destroy(gameObject);
            OnDie();
        }
    }

    public void OnDie()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
