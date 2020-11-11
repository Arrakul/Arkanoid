using UnityEngine;
using DG.Tweening;

public class AnimationController : MonoBehaviour
{
    public static AnimationController Instance;
    
    [SerializeField] float timeAnimation = 0f;
    [SerializeField] float scaleAnimation = 0f;
    [SerializeField] LoopType loopType;
    [SerializeField] Ease ease;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
        
        DontDestroyOnLoad(gameObject);
    }

    public void ResetAnimation(RectTransform rectTransform)
    {
        rectTransform = GetComponent<RectTransform> ();
        rectTransform.DOScale (new Vector3 (scaleAnimation, scaleAnimation, 0), timeAnimation).SetLoops (-1, loopType).SetEase (ease);
    }

    public void AnimationPulsation(RectTransform rectTransform)
    {
        rectTransform.DOKill ();
        rectTransform.localScale = new Vector3 (1f, 1f, 1f);
        rectTransform.DOScale (new Vector3 (scaleAnimation, scaleAnimation, 0), timeAnimation).SetLoops (-1, loopType).SetEase (ease);
    }
    
    public void AnimationPulsation(Transform rectTransform)
    {
        rectTransform.DOKill ();
        rectTransform.localScale = new Vector3 (1f, 1f, 1f);
        rectTransform.DOScale (new Vector3 (scaleAnimation, scaleAnimation, 0), timeAnimation).SetLoops (-1, loopType).SetEase (ease);
    }
}
