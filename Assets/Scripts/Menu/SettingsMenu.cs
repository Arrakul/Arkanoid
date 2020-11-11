using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    [Header("UI")] 
    [SerializeField] private Text nameGameText;
    [SerializeField] private Button playButton;
    [SerializeField] private Image LoadingImg;
    [SerializeField] private Image LoadingImg2;
    [SerializeField] private Text progressText;


    [Space] [Header("Space detwwen menu items")] [SerializeField]
    private Vector2 spacing;

    [Space] [Header("Main button rotation")] 
    [SerializeField] private float rotationDuration;
    [SerializeField] private Ease rotationEase;
    
    [Space] [Header("Animation")] 
    [SerializeField] private float expendDuration;
    [SerializeField] private float collapseDuration;
    [SerializeField] private Ease expendEase;
    [SerializeField] private Ease collapseEase;
    
    [Space] [Header("Fading")] 
    [SerializeField] private float expendFadeDuration;
    [SerializeField] private float collapseFadeDuration;

    private Button mainButtom;
    private SettingsMenuItem[] menuItems;
    private bool isExpanded = false;

    private Vector2 mainButtomPosition;
    private int itemsCount;

    private void Start()
    {
        AnimationController.Instance.AnimationPulsation(nameGameText.rectTransform);
        AnimationController.Instance.AnimationPulsation(playButton.GetComponent<RectTransform>());
        
        LoadingImg.gameObject.SetActive(false);

        itemsCount = transform.childCount - 1;
        menuItems = new SettingsMenuItem[itemsCount];

        for (int i = 0; i < itemsCount; i++)
        {
            menuItems[i] = transform.GetChild(i + 1).GetComponent<SettingsMenuItem>();
        }

        mainButtom = transform.GetChild(0).GetComponent<Button>();
        mainButtom.onClick.AddListener(ToggleManu);
        mainButtom.transform.SetAsLastSibling();

        mainButtomPosition = mainButtom.transform.position;
        ResetPositions();
    }

    void ResetPositions()
    {
        for (int i = 0; i < itemsCount; i++)
        {
            menuItems[i].trans.position = mainButtomPosition;
        }
    }

    void ToggleManu()
    {
        isExpanded = !isExpanded;

        if (isExpanded)
        {        
            for (int i = 0; i < itemsCount; i++)
            {
                //menuItems[i].trans.position = mainButtomPosition + spacing * (i + 1);
                menuItems[i].trans.DOMove(mainButtomPosition + spacing * (i + 1), expendDuration).SetEase(expendEase);
                menuItems[i].img.DOFade(1f, expendFadeDuration).From(0f);
            }
        }
        else
        {
            for (int i = 0; i < itemsCount; i++)
            {
                //menuItems[i].trans.position = mainButtomPosition;
                menuItems[i].trans.DOMove(mainButtomPosition, collapseDuration).SetEase(collapseEase);
                menuItems[i].img.DOFade(0f, collapseFadeDuration);
            }
        }

        mainButtom.transform
            .DORotate(Vector3.forward * 180f, rotationDuration)
            .From(Vector3.zero)
            .SetEase(rotationEase);
    }

    private void OnDestroy()
    {
        mainButtom.onClick.RemoveListener(ToggleManu);
    }

    public void ButtonPlay(int sceneID)
    {        
        LoadingImg.gameObject.SetActive(true);
        StartCoroutine(AsyncLoad(sceneID));
    }
    
    IEnumerator AsyncLoad(int sceneID)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync (sceneID);
        while (!operation.isDone) 
        {
            float progress = operation.progress / 0.9f;
            LoadingImg2.fillAmount = progress;
            progressText.text = string.Format("{0:0}%", progress * 100);
            yield return null;
        }
    }

}
