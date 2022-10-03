using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DefineEnum;

public class DialogMain : MonoBehaviour
{
    [SerializeField] Text _stageName;
    [SerializeField] GameObject _fadeBlack;

    [SerializeField] Text _mapTxt;
    [SerializeField] Text _nameTxt;
    [SerializeField] Text _dialTxt;

    [SerializeField] Toggle _autoTog;

    [SerializeField] Image _charImg;
    [SerializeField] Image _mapImg;

    [SerializeField] float _typingSpd = 0.25f;
    [SerializeField] float _autoSkipTime = 0.25f;

    int _index = 0;
    int _convSt = 0;
    int _convEn = 0;
    int _map = 0;
    bool _isTyping = false;
    string _dial = string.Empty;
    float _timer = 0;
    int _dialCnt = 0;
    float _autoTimer = 0;

    public bool _isAuto
    {
        get { return _autoTog.isOn; }
    }

    void Awake()
    {
        TableManager._instance.LoadDialogue();
    }

    void Start()
    {
        _index = TableManager._instance.GetTable(TableName.EpisodeTable).ToInt32(1, "Index");
        StartCoroutine(Play(_index));
    }

    void Update()
    {
        if (_isTyping)
        {
            if (_dialCnt < _dial.Length)
            {
                _timer += Time.deltaTime;
                _dialTxt.text = _dial.Substring(0, _dialCnt + 1);
                if (_timer > _typingSpd)
                {
                    _timer = 0;
                    _dialCnt++;
                }
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    _dialCnt = _dial.Length;
                    _dialTxt.text = _dial;
                }
            }
            else if (_dialCnt == _dial.Length)
            {
                if (_isAuto)
                {
                    _autoTimer += Time.deltaTime;
                    if (_autoTimer > _autoSkipTime)
                    {
                        _autoTimer = 0;
                        GoNextEpisode();
                    }
                }
                else
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                        GoNextEpisode();
                }
            }
        }
    }

    void SetParam(int idx)
    {
        _convSt = TableManager._instance.GetTable(TableName.EpisodeTable).ToInt32(idx, "ConvStart");
        _convEn = TableManager._instance.GetTable(TableName.EpisodeTable).ToInt32(idx, "ConvEnd");

        _map = TableManager._instance.GetTable(TableName.EpisodeTable).ToInt32(idx, "Map");
        _mapImg.sprite = ResourcePoolManager._instance.GetMapImage(_map);
        _mapTxt.text = TableManager._instance.GetTable(TableName.MapTable).ToStr(_map, "Map");

        SetDialog(_convSt);
    }
    void SetDialUI(int convNum)
    {
        int nameNum = TableManager._instance.GetTable(TableName.DialogTable).ToInt32(convNum, "Character");
        string name = TableManager._instance.GetTable(TableName.CharacterTable).ToStr(nameNum, "Name");
        _charImg.sprite = ResourcePoolManager._instance.GetCharImage(nameNum);
        _nameTxt.text = name;

        SetDialog(convNum);
    }
    void SetDialog(int convNum)
    {
        _dial = TableManager._instance.GetTable(TableName.DialogTable).ToStr(convNum, "Dialog");
        _dialCnt = 0;
    }

    void GoNextEpisode()
    {
        if (_convSt < _convEn)
            SetDialUI(++_convSt);
        else
            StartCoroutine(Play(_index));
    }

    IEnumerator Play(int idx)
    {
        _isTyping = false;
        yield return StartCoroutine(WaitForFading(idx));
    }
    IEnumerator WaitForFading(int idx)
    {
        _stageName.text = TableManager._instance.GetTable(TableName.EpisodeTable).ToStr(idx, "StageName");
        _fadeBlack.SetActive(true);
        _fadeBlack.GetComponent<Image>().CrossFadeAlpha(1, 1, true);
        _stageName.GetComponent<Text>().CrossFadeAlpha(1, 1, true);
        yield return new WaitForSeconds(1);
        SetParam(_index++);
        SetDialUI(_convSt);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));

        _fadeBlack.GetComponent<Image>().CrossFadeAlpha(0, 1, true);
        _stageName.GetComponent<Text>().CrossFadeAlpha(0, 1, true);
        yield return new WaitForSeconds(0.25f);
        _fadeBlack.SetActive(false);
        _isTyping = true;
    }
}
