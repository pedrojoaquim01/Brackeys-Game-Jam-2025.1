using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "BiggerEnemiesEvent", menuName = "Events/BiggerEnemiesEvent", order = 0)]
public class BiggerEnemiesEvent : Event
{
    [field:SerializeField] public float ScaleSize { get; private set; } = 2f;
    [field:SerializeField] public float PulsingModfier { get; private set; } = .1f;
    [field:SerializeField] public float TransitionTime { get; private set; } = 1f;
    [field:SerializeField] public float Duration { get; private set; } = 5f;
    [field:SerializeField] public Constants.EnemyGroup EnemyGroup { get; private set; } = Constants.EnemyGroup.DEFAULT;

    public override void TriggerEvent()
    {
        var enemies = EnemyGroup == Constants.EnemyGroup.DEFAULT ? FindByTag() : FindByGroup();
        
        enemies.ForEach(e =>
        {
            var duration = Duration;
            e.StartCoroutine(ScaleEnemy(e, ScaleSize, duration));
        });
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

    private IEnumerator ScaleEnemy(Mov_inimigo e, float baseSize, float duration)
    {
        var maxSize = baseSize + PulsingModfier;
        var minSize = baseSize - PulsingModfier;
        var currentSize = 1f;
        while (duration > 0f)
		{
            duration -= Time.fixedDeltaTime;
			while (currentSize != maxSize)
			{
				currentSize = Mathf.MoveTowards( currentSize, maxSize, TransitionTime);
				e.transform.localScale = Vector3.one * currentSize;
				yield return new WaitForEndOfFrame();
			}

			while (currentSize != minSize)
			{
				currentSize = Mathf.MoveTowards( currentSize, minSize, TransitionTime);
				e.transform.localScale = Vector3.one * currentSize;

				yield return new WaitForEndOfFrame();
			}
		}
    }
}