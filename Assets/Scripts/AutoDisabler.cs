using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class AutoDisabler : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(DisableAfterSec(4f));
    }

    private IEnumerator DisableAfterSec(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject?.SetActive(false);
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
