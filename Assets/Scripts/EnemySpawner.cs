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

    void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        while(true)
        {
            yield return new WaitForSeconds(3);

            SpawnFlyingEnemy();

            yield return new WaitForSeconds(3);

            SpawnRangedEnemy();
        }
    }

    void SpawnFlyingEnemy()
    {
        float xPos, zPos;

        if (Random.value > 0.5f)
            xPos = xDist;
        else
            xPos = -xDist;

        zPos = (Random.value - 0.5f) * zDist;

        Instantiate(FlyingEnemy, new Vector3(xPos, 2, zPos), Quaternion.identity);
    }

    void SpawnRangedEnemy()
    {
        float xPos, zPos;

        if (Random.value > 0.5f)
            xPos = xDist;
        else
            xPos = -xDist;

        zPos = (Random.value - 0.5f) * zDist;

        Instantiate(RangedEnemy, new Vector3(xPos, 2, zPos), Quaternion.identity);
    }
}
