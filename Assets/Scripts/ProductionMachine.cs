using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductionMachine : MonoBehaviour
{
    public bool IsProducing { get; private set; }
    public float Progress { get; private set; }

    private Coroutine productionCoroutine;

    public bool StartProduction(float duration, Action onCompleted)
    {
        if (IsProducing || duration <= 0f)
            return false;
        productionCoroutine = StartCoroutine(Produce(duration, onCompleted));
        return true;
    }
    private IEnumerator Produce(float duration, Action onCompleted)
    {
        IsProducing = true;
        Progress = 0f;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            Progress = Mathf.Clamp01(elapsed / duration);
            yield return null;
        }
        Progress = 1f;
        IsProducing = false;
        productionCoroutine = null;
        onCompleted?.Invoke();
    }
}
