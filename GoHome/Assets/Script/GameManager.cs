using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Canvas _UICanvas;

    [SerializeField]
    private Slider _bloodSlider;

    [SerializeField]
    private Camera _mainCamera;
    void Awake()
    {
        DontDestroyOnLoad(this);
        DontDestroyOnLoad(_mainCamera);
        DontDestroyOnLoad(_UICanvas);
        DontDestroyOnLoad(_bloodSlider);
    }

    /// <summary>
    /// 刷新血条UI
    /// </summary>
    /// <param name="maxBlood">满血</param>
    /// <param name="currentBlood">当前血</param>
    public void  UpdataBloodSlider(int maxBlood, int currentBlood)
    {
        _bloodSlider.maxValue = (float)maxBlood;
        _bloodSlider.value = (float)currentBlood;
    }
}
