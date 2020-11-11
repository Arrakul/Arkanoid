using System;
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
        Debug.Log("PlatformExtension");
        Platform = GameController.Instance.platform;
        var scale = Platform.transform.localScale;

        if(scale.x.Equals(1))
        {
            scale = new Vector3(2, scale.y, scale.z);
        }
        else
        {
            scale = new Vector3(1, scale.y, scale.z);
        }
        Platform.transform.localScale = scale;
    }
}
