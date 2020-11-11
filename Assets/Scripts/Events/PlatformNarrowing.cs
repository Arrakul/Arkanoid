using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformNarrowing : MonoBehaviour
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
        Debug.Log("PlatformNarrowing");
        _platform = GameController.Instance.platform;
        var scale = _platform.transform.localScale;
        
        if(scale.x.Equals(1))
        {
            scale = new Vector3(0.5f, scale.y, scale.z);
        }
        else
        {
            scale = new Vector3(1, scale.y, scale.z);
        }
        _platform.transform.localScale = scale;
    }
}
