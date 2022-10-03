using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResolutionTest : MonoBehaviour
{

    #region 내 코드
    [SerializeField] GameObject SettingsUI;
    [SerializeField] Dropdown _ddRes;
    int _idx = 0;

    // 모니터에서 지원되는 전체 해상도 리스트
    List<Resolution> _resolInfoList = new List<Resolution>();

    // 해상도 변환용 리스트(중복된 해상도 제외한 리스트)
    List<Resolution> _resolDataList = new List<Resolution>();

    // 드랍다운 옵션용 리스트
    List<string> m_DropOptions = new List<string>();

    void Awake()
    {
        // 전체 해상도 리스트에 지원되는 해상도 값을 모두 삽입
        Resolution[] resols = Screen.resolutions;
        for(int n = 0; n < resols.Length; n++)
            _resolInfoList.Add(resols[n]);
        
        // 전체 해상도 리스트의 첫 번째 값을 해상도 변환용 리스트와 드랍다운 옵션용 리스트에 삽입(비교대상 없음)
        m_DropOptions.Add(_resolInfoList[0].ToString() + ", Window Mode");
        _resolDataList.Add(_resolInfoList[0]);

        // 풀스크린 모드는 해상도 변환용 리스트에 값을 삽입하지 않음(윈도우 모드와 해상도 동일)
        m_DropOptions.Add(_resolInfoList[0].ToString() + ", FullScreen Mode");

        // 전체 해상도 리스트의 두 번째 값부터 그 앞 값과 비교하는 반복문
        for (int n = 1; n < _resolInfoList.Count; n++)
        {
            // 값이 같으면 패스
            if (_resolInfoList[n - 1].width == _resolInfoList[n].width && _resolInfoList[n - 1].height == _resolInfoList[n].height)
            //if(_resolInfoList[n].refreshRate < 60)
                continue;
            else
            {
                // 값이 다르면 해상도 변환용 & 드랍다운 옵션용 리스트에 삽입
                m_DropOptions.Add(_resolInfoList[n].ToString() + ", Window Mode");
                _resolDataList.Add(_resolInfoList[n]);
                // 풀스크린 모드는 따로 해상도 변환용 리스트에 값을 삽입하지 않음(윈도우 모드와 해상도 동일)
                m_DropOptions.Add(_resolInfoList[n].ToString() + ", FullScreen Mode");
            }
        }
        // 기존에 있던 드랍다운 옵션들(더미값)을 모두 지우고 드랍다운 옵션용 리스트를 새로 추가
        _ddRes.ClearOptions();
        _ddRes.AddOptions(m_DropOptions);
    }
    void Start()
    {
        // 해상도 변환용 리스트의 첫 번째 해상도값으로 해상도 변경
        Screen.SetResolution(_resolDataList[0].width, _resolDataList[0].height, false);

        // 드랍다운 이벤트 리스너 추가
        _ddRes.onValueChanged.AddListener(delegate { ChangeResolution(_ddRes); });
    }

    void OnGUI()
    {
        //int startX = Screen.width / 2 - 200 / 2;
        //for (int n = 0; n < _resolInfoList.Count; n++)
        //    GUI.Box(new Rect(startX, 20 * n, 200, 20), _resolInfoList[n].ToString());

        //string s = string.Empty;
        //for(int n = 0; n < _resols.Length; n++)
        //{
        //    s += "W : " + _resols[n].width + ", H : " + _resols[n].height + ", R : " + _resols[n].refreshRate + "\n";
        //}
        //Rect screen = new Rect(0, 0, Screen.width, Screen.height);
        //GUI.Box(screen, s);

        Rect rtScreenX = new Rect(0, 0, 200, 50);
        Rect rtScreenY = new Rect(210, 0, 200, 50);
        Rect rtChange1 = new Rect(0, Screen.height - 50, 200, 50);
        Rect rtChange2 = new Rect(Screen.width - 200, Screen.height - 50, 200, 50);
        GUI.Box(rtScreenX, Screen.width.ToString());
        GUI.Box(rtScreenY, Screen.height.ToString());

        //if(GUI.Button(rtChange1, "1920 x 1080"))
        //    Screen.SetResolution(1920, 1080, true);     //풀스크린일 때 true
        //if (GUI.Button(rtChange2, "800 x 600"))
        //    Screen.SetResolution(800, 600, true);
    }

    // 드랍다운 이벤트 발생 시 실행되는 함수
    void ChangeResolution(Dropdown change)
    {
        // 선택된 드랍다운의 인덱스 저장
        _idx = change.value;
    }

    public void ButtonDown()
    {
        // 버튼이 눌리면 저장된 인덱스로 해상도 변환용 리스트에서 값을 찾아 해상도 변경
        if (_idx % 2 == 0)  // 윈도우 모드
            // 인덱스를 2로 나눈 이유는 풀스크린 모드 해상도를 저장하지 않았기 때문
            Screen.SetResolution(_resolDataList[_idx/2].width, _resolDataList[_idx/2].height, false);
        else // 풀스크린 모드
            Screen.SetResolution(_resolDataList[_idx/2].width, _resolDataList[_idx/2].height, true);
    }

    public void SettingsBtnDown()
    {
        SettingsUI.SetActive(true);
    }

    public void ExitButtonDown()
    {
        SettingsUI.SetActive(false);
    }
    
    #endregion
}
