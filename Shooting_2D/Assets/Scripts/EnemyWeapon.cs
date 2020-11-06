 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    [SerializeField]
    private GameObject projectilePrefab;
    private Movement2D movement2D;
    [SerializeField]
    private float attackRate = 1.5f;
    Transform playerTransform;

    private void Awake()
    {
        movement2D = GetComponent<Movement2D>();
        playerTransform = GameObject.Find("Player").transform;
    }

    public void StartShot()
    {
        StartCoroutine("Shot");
    }

    private IEnumerator Shot()
    {
        while(true)
        {
            GameObject clone = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            Vector3 direction = (playerTransform.position - clone.transform.position).normalized;
            clone.GetComponent<Movement2D>().MoveTo(direction);
            yield return new WaitForSeconds(attackRate);
        }
    }
}
