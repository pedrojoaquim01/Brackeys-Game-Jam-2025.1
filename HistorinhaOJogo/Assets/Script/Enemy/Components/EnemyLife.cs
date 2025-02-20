using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLife : MonoBehaviour
{
    [field:SerializeField] public int Health { get; private set; } = 1;
    private int currentHealth;

    void Awake()
    {
        currentHealth = Health;
    }
}
