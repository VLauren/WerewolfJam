using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject RangedEnemy;
    public GameObject FlyingEnemy;

    [Space()]
    public float xDist;
    public float zDist;

    [Space()]
    public float WaitTime;
    public float WaitTimeReductionSpeed;

    Vector3 Center;

    void Start()
    {
        StartCoroutine(Spawn());
        Center = transform.position;
        Center.y = 0;
    }

    IEnumerator Spawn()
    {
        // Primer spawn
        yield return new WaitForSeconds(1);
        SpawnFlyingEnemy();

        while (true)
        {
            yield return new WaitForSeconds(WaitTime);

            SpawnRangedEnemy();

            yield return new WaitForSeconds(WaitTime);

            SpawnFlyingEnemy();
        }
    }

    void Update()
    {
        if (WaitTime > 1)
            WaitTime -= Time.deltaTime * WaitTimeReductionSpeed;
    }

    void SpawnFlyingEnemy()
    {
        float xPos, zPos;

        if (Random.value > 0.5f)
            xPos = xDist / 2;
        else
            xPos = -xDist / 2;

        zPos = (Random.value - 0.5f) * zDist;

        Instantiate(FlyingEnemy, Center + new Vector3(xPos, 3, zPos), Quaternion.identity);
    }

    void SpawnRangedEnemy()
    {
        float xPos, zPos;

        if (Random.value > 0.5f)
            xPos = xDist / 2;
        else
            xPos = -xDist / 2;

        zPos = (Random.value - 0.5f) * zDist;

        Instantiate(RangedEnemy, Center + new Vector3(xPos, 2, zPos), Quaternion.identity);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(xDist, 2, zDist));
    }
}
