using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
    public static void StopCoroutines(this MonoBehaviour behaviour, IEnumerable<Coroutine> objects)
    {
        foreach (var c in objects)
            behaviour.StopCoroutine(c);
    }

    public static bool IsPlaying(this Animator animator)
    {
        return animator.GetCurrentAnimatorStateInfo(0).length >
                    animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }
    public static bool IsPlaying(this Animator animator, string name)
    {
        return animator.IsPlaying() && animator.GetCurrentAnimatorStateInfo(0).IsName(name);
    }
}