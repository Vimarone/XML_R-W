using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DefineEnum;

public class ScreenManager : MonoBehaviour
{
    static ScreenManager _uniqueInstance;

    eSceneIndex _prevScene;
    eSceneIndex _currScene;        //Current Scene

    public static ScreenManager _instance
    {
        get { return _uniqueInstance; }
    }

    void Awake()
    {
        _uniqueInstance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        // 임시
        
        //=====
    }

    void Update()
    {

    }

    public void StartDialScene()
    {
        _prevScene = _currScene;
        _currScene = eSceneIndex.Dialog;
        StartCoroutine(LoadingScene(eSceneIndex.Dialog.ToString()));
    }
    public void StartGameScene()
    {
        _prevScene = _currScene;
        _currScene = eSceneIndex.Game;
        StartCoroutine(LoadingScene(eSceneIndex.Game.ToString()));
    }

    IEnumerator LoadingScene(string sceneName)
    {
        _prevScene = _currScene;
        _currScene = eSceneIndex.none;

        AsyncOperation aOper = SceneManager.LoadSceneAsync(sceneName);
        while (!aOper.isDone)
            yield return null;
        yield return new WaitForSeconds(2);
        _currScene = _prevScene;

        // 씬 시작처리.....
        if (_currScene == eSceneIndex.Game)
        {
            // 스테이지 정보 불러오기
            //int no = UserInfoManager._instance._nowStageNumber;
        }
    }
    public DefineEnum.eSceneIndex NowScene()
    {
        return _currScene;
    }
}
