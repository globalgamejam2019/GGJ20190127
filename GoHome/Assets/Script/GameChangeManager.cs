using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneName
{
    Part0 = 0,
    Part1 = 1,
    Part2 = 2
}

public class GameChangeManager : MonoBehaviour
{
    private Dictionary<SceneName, string> SceneStringDict;
    void Awake()
    {
        SceneStringDict = new Dictionary<SceneName, string>();
        SceneStringDict.Add(SceneName.Part0, "Part0");
        SceneStringDict.Add(SceneName.Part1, "Part1");
        SceneStringDict.Add(SceneName.Part2, "Part2");

        DontDestroyOnLoad(this);
    }
    public void ChangeScene(int i)
    {
        print(SceneStringDict);
        SceneManager.LoadScene(SceneStringDict[(SceneName)i]);
    }

    public void Quit()
    {
        Application.Quit();
    }

}
