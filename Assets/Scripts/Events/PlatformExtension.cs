using System;
using UnityEngine;

public class PlatformExtension : MonoBehaviour
{
    private Platform _platform;

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
        _platform = GameController.Instance.platform;
        var scale = _platform.transform.localScale;

        if(scale.x.Equals(1))
        {
            scale = new Vector3(2, scale.y, scale.z);
            _platform.leftBorder += 1f;
            _platform.rightBorder -= 1f;
        }
        else
        {
            scale = new Vector3(1, scale.y, scale.z);
            _platform.leftBorder = -1.7f;
            _platform.rightBorder = 1.7f;
        }
        _platform.transform.localScale = scale;
    }
}
