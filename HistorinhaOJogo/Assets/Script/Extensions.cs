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
}