using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "BiggerEnemiesEvent", menuName = "Events/BiggerEnemiesEvent", order = 0)]
public class BiggerEnemiesEvent : EnemyBaseEvent
{
    [field:SerializeField] public float ScaleSize { get; private set; } = 2f;
    [field:SerializeField] public float PulsingModfier { get; private set; } = .1f;
    [field:SerializeField] public float TransitionTime { get; private set; } = 1f;
    [field:SerializeField] public float Duration { get; private set; } = 5f;

    public override void TriggerEvent()
    {
        List<Mov_inimigo> enemies = GetEnemies();

        enemies.ForEach(e =>
        {
            var duration = Duration;
            e.StartCoroutine(ScaleEnemy(e, ScaleSize, duration));
        });
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