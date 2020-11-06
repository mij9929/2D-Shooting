using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionAutoDestoryer : MonoBehaviour
{
    [SerializeField]
    private StageData stageData;
    private float destoyWeight = 2.0f;

    private void LateUpdate()
    {
        if (transform.position.y < stageData.LimitMin.y - destoyWeight || 
            transform.position.y > stageData.LimitMax.y + destoyWeight ||
            transform.position.x < stageData.LimitMin.x - destoyWeight ||
            transform.position.x > stageData.LimitMax.x + destoyWeight)
        {
            Destroy(gameObject);
        }
    }    
}
