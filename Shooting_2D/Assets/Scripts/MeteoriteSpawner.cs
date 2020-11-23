using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MeteoriteSpawner : MonoBehaviour
{
    [SerializeField]
    private StageData stageData;
    [SerializeField]
    private GameObject alertLinePrefab;
    [SerializeField]
    private GameObject mateoritePrefab;
    [SerializeField]
    private float minSpawnTime = 1.0f;
    [SerializeField]
    private float maxSpawnTIme = 4.0f;

    private void Awake()
    {
        StartCoroutine("SpawnMeteorite");
    }

    private IEnumerator SpawnMeteorite()
    {
        while(true)
        {
            // x 위치는 스테이지 크기 범위 내에서 임의의 값을 선택
            float positionX = Random.Range(stageData.LimitMin.x, stageData.LimitMax.x);

            // 경고선 오브젝트 생성
            GameObject alertLineClone = Instantiate(alertLinePrefab, new Vector3(positionX, 0, 0), Quaternion.identity);

            // 1초간 대기 
            yield return new WaitForSeconds(1.0f);

            Destroy(alertLineClone);

            //메테오 오브젝트 생성
            Vector3 mateoritePosition = new Vector3(positionX, stageData.LimitMax.y + 1.0f, 0);
            Instantiate(mateoritePrefab, mateoritePosition, Quaternion.identity);

            //대기시간 설정
            float spawnTime = Random.Range(minSpawnTime, maxSpawnTIme);

            // 해당 시간 만큼 대기 후 다음 로직 실행
            yield return new WaitForSeconds(spawnTime);
        }
    }
}
