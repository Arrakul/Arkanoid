﻿using System;
using UnityEngine;

public class PlatformExtension : MonoBehaviour
{
    private Platform Platform;

    private void Start()
    {
        gameObject.GetComponent<EventGame>().onEvent += Play;
    }

    private void OnDestroy()
    {
        gameObject.GetComponent<EventGame>().onEvent -= Play;
    }

    public void Play()
    {
        Platform = GameController.Instance.platform;
        var scale = Platform.transform.localScale;
        scale = new Vector3(2, scale.y, scale.z);
        Platform.transform.localScale = scale;
    }
}
