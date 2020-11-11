using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformNarrowing : MonoBehaviour
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
        scale = new Vector3(0.5f, scale.y, scale.z);
        Platform.transform.localScale = scale;
    }
}
