using UnityEngine;

public class PlatformExtension : MonoBehaviour, EventGame
{
    private Platform Platform;

    public void Play()
    {
        Debug.Log("Change!");
        Platform = GameController.Instance.platform;
        var scale = Platform.transform.localScale;
        scale = new Vector3(scale.x * 2, scale.y, scale.z);
        Platform.transform.localScale = scale;
    }
}
