using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject Enemy;
    [SerializeField] float StartDelay = 2f;
    [SerializeField] float Delay = 0.5f;

    void Start()
    {
        Enemy.transform.localScale = transform.localScale;
        InvokeRepeating("Spawn", StartDelay, Delay);
    }

    void Spawn()
    {
        Instantiate(Enemy, transform.position, transform.rotation);
    }
}
