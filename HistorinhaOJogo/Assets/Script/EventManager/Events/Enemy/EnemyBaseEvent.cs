using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public abstract class EnemyBaseEvent : Event {
    [field:SerializeField] public Constants.EnemyGroup EnemyGroup { get; private set; } = Constants.EnemyGroup.DEFAULT;

    protected List<Mov_inimigo> GetEnemies()
    {
        return EnemyGroup == Constants.EnemyGroup.DEFAULT ? FindByTag() : FindByGroup();
    }

    private List<Mov_inimigo> FindByGroup()
    {
        var objs = FindObjectsByType<Mov_inimigo>(FindObjectsSortMode.None);
        return objs.Where(x => x.EnemyGroup == EnemyGroup).ToList();
    }

    private static List<Mov_inimigo> FindByTag()
    {
        var gameObjects = GameObject.FindGameObjectsWithTag(Constants.INIMIGO_TAG);
        return gameObjects.Where(x => x.GetComponent<Mov_inimigo>() != null)
                            .Select(x => x.GetComponent<Mov_inimigo>())
                            .ToList();
    }
}