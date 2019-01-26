using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Singleton<T>
    where T : new()
{
    private static T m_Instance = default(T);

    public static T Instance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = new T();
            }

            return m_Instance;
        }
    }
}

public enum MonFace
{
    VeryAngry = 0,
    Angry = 1,      
    Sad = 2,
    Happy = 3
}

public enum MySelfAwareness
{
    red = 0,
    black = 1
}

public enum BackgroundSceneStatus
{
    StartScene1 = 0,
    EndScene1 = 1,
    StartScene2 = 2,
    EndScene2 = 3
}
public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Canvas _UICanvas;
    
    [SerializeField]
    private Camera _mainCamera;

    #region UICache
    [SerializeField]
    private Image _bloodImage;
    private float _bloodImageMaxWidth;
    [SerializeField]
    private Image _monFace;
    [SerializeField]
    private Image _selfAwarenessImage;
    [SerializeField]
    private Image _backGroundImage;
    [SerializeField]
    private Text _backGroundText;
    #endregion


    #region UISprite
    private List<Sprite> _monFaceSprite;
    private List<Sprite> _selfAwarenessSprite;
    #endregion

    #region Temporary DataCache
    [System.Serializable]
    public struct BackGroundTextList
    {
        public BackgroundSceneStatus sceneStatus;
        public List<string> sceneTextList;
    }
    [SerializeField]
    public List<BackGroundTextList> backGroundTextList;
    private Dictionary<BackgroundSceneStatus, List<string>> backGroundTextDict;
    #endregion
    void Awake()
    {
        DontDestroyOnLoad(this);
        DontDestroyOnLoad(_mainCamera);
        DontDestroyOnLoad(_UICanvas);
        DontDestroyOnLoad(_bloodImage);

        LoadUIResource();
        InitData();
    }


    private void LoadUIResource()
    {
        //Mon Face Sprite
        _monFaceSprite = new List<Sprite>();
        for (int i = 1; i < 5; i++)
            _monFaceSprite.Add(Resources.Load<Sprite>("Sprite/UI/monface" + i));
        //自我觉醒
        _selfAwarenessSprite = new List<Sprite>();
        for (int i = 0; i < 2; i++)
            _selfAwarenessSprite.Add(Resources.Load<Sprite>("Sprite/UI/selfAwareness" + i));
        return;
    }

    private void InitData()
    {
        _bloodImageMaxWidth = _bloodImage.rectTransform.offsetMax.x;

        backGroundTextDict = new Dictionary<BackgroundSceneStatus, List<string>>();
        for (int i = 0; i < backGroundTextList.Count; i++)
            if (!backGroundTextDict.ContainsKey(backGroundTextList[i].sceneStatus))
                backGroundTextDict.Add(backGroundTextList[i].sceneStatus, backGroundTextList[i].sceneTextList);
    }



    #region UIManager
    /// <summary>
    /// 刷新血条UI
    /// </summary>
    /// <param name="maxBlood">满血</param>
    /// <param name="currentBlood">当前血</param>
    public void  UpdateBloodSlider(int maxBlood, int currentBlood)
    {
        _bloodImage.rectTransform.offsetMax = new Vector2(
            -(_bloodImage.rectTransform.offsetMax.x + (989.5f - 989.5f * (currentBlood * 1.0f / maxBlood))),
            _bloodImage.rectTransform.offsetMax.y
        );
    }
    /// <summary>
    /// 显示母亲表情 （只需要在对应的条件下调用此函数即可）
    /// </summary>
    /// <param name="face">母亲的表情</param>
    public void SetUpMonFace(MonFace face)
    {
        _monFace.gameObject.SetActive(true);
        _monFace.sprite = _monFaceSprite[(int)face];
        var MonFaceIEnumertor = SetUpFuntionForSeconds(HideMonFace, 3);
        StartCoroutine(MonFaceIEnumertor);
        return;
    }
    public void HideMonFace()
    {
        StartCoroutine(HideImageForSeconds(_monFace,0.5f));
    }

    /// <summary>
    /// 自我觉醒UI 
    /// </summary>
    /// <param name="mySelfAwareness">红色为觉醒，黑色为普通状态</param>
    public void SetupSelfAwarenessImage(MySelfAwareness mySelfAwareness)
    {
        _selfAwarenessImage.gameObject.SetActive(true);
        _selfAwarenessImage.sprite = _selfAwarenessSprite[(int) mySelfAwareness];
    }
    #endregion

    #region BlackGround Text 
    private bool isCanChange = false;
    /// <summary>
    /// 场景黑幕 显示文字
    /// </summary>
    /// <param name="backgroundSceneStatus"></param>
    public void SetupBackgroundText(BackgroundSceneStatus backgroundSceneStatus)
    {
        StartCoroutine(ChangingBackGroundIE(backGroundTextDict[backgroundSceneStatus]));
    }

    private void ChangeSetupText()
    {
        isCanChange = true;
    }

    IEnumerator ChangingBackGroundIE(List<string> sceneList)
    {
        int strIndex = -1;
        isCanChange = true;
        _backGroundImage.gameObject.SetActive(true);
        while (true)
        {
            if (isCanChange)
            {
                isCanChange = false;
                ++strIndex;
                if (strIndex >= sceneList.Count) break;
                _backGroundText.text = sceneList[strIndex];
                StartCoroutine(SetUpFuntionForSeconds(ChangeSetupText, 3.0f));
            }
            yield return null;
        }
        _backGroundImage.gameObject.SetActive(false);
    }


    #endregion

    #region Common Methon
    IEnumerator SetUpFuntionForSeconds(UnityAction callBackAction, float timeSecond)
    {
        float currentSecond = 0.0f;
        while (currentSecond <= timeSecond)
        {
            currentSecond += Time.deltaTime;
            yield return null;
        }
        callBackAction();
        yield break;
    }
    IEnumerator HideImageForSeconds(Image image, float second)
    {
        float currentSecond = 0;
        float alphaSize = 1;
        CanvasGroup imageCanvasGroup = image.GetComponent<CanvasGroup>();
        while (currentSecond <= second)
        {
            currentSecond += Time.deltaTime;
            alphaSize = 1 - (currentSecond * 1.0f / second);
            if (alphaSize >= 0)
            {
                imageCanvasGroup.alpha *= alphaSize;
            }
            else
            {
                image.gameObject.SetActive(false);
                imageCanvasGroup.alpha = 0;
            }
            yield return null;
        }
        yield break;
    }
    #endregion
}
