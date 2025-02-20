using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "FasterEnemiesEvent", menuName = "Events/FasterEnemiesEvent", order = 0)]
public class FasterEnemiesEvent : EnemyBaseEvent
{
    [field:SerializeField] public float SpeedModifier { get; private set; } = 1f;
    public override void TriggerEvent()
    {
        var enemies = GetEnemies();
        enemies.ForEach(x => x.velocidadeMod = SpeedModifier);
    }
}