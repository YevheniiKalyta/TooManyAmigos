using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorUI : MonoBehaviour
{
    [SerializeField] private RectTransform cursorIMG;
    [SerializeField] private float shrinkSpeed = 1f;
    [SerializeField] private float cursorMaxScale = 2f;
    float cursorScaleMultiplier= 1f;

    private void Start()
    {
        Cursor.visible = false;
        PlayerShootProcessor.OnFire += CursorGrow;
    }

    private void CursorGrow()
    {
        cursorScaleMultiplier = Mathf.Clamp(cursorScaleMultiplier + 0.4f, 1, cursorMaxScale);
    }

    void Update()
    {
        cursorIMG.position = InputManager.Instance.GetMousePosition();
        cursorIMG.localScale = Vector3.one * cursorScaleMultiplier;
        if (cursorScaleMultiplier > 1)
        {
            cursorScaleMultiplier = Mathf.Clamp(cursorScaleMultiplier - shrinkSpeed * Time.deltaTime, 1, cursorMaxScale);
        }
    }
}
