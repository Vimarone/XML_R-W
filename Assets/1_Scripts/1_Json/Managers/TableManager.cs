using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DefineEnum;

public class TableManager : TSingleton<TableManager>
{
    Dictionary<TableName, TableBase> _dicTable = new Dictionary<TableName, TableBase>();

    protected override void Init()
    {
        base.Init();
    }

    public void LoadAll()
    {
        Load<PersonallityTable>(TableName.PersonallityTable);
        Load<MonsterTable>(TableName.MonsterTable);
        Load<GradeTable>(TableName.GradeTable);
        Load<StageInfoTable>(TableName.StageInfoTable);
        Load<TribeTable>(TableName.TribeTable);
    }

    public void LoadDialogue()
    {
        Load<EpisodeTable>(TableName.EpisodeTable);
        Load<CharacterTable>(TableName.CharacterTable);
        Load<MapTable>(TableName.MapTable);
        Load<DialogTable>(TableName.DialogTable);
    }

    public TableBase GetTable(TableName tName)
    {
        // 해당 테이블이 있는지 없는지부터 검사
        if (_dicTable.ContainsKey(tName))
            return _dicTable[tName];

        return null;
    }

    TableBase Load<T>(TableName tName) where T : TableBase, new()
    {
        TextAsset datas;
        // 같은 이름의 객체가 있는가 ? 있으면 패스
        if (_dicTable.ContainsKey(tName))
            return _dicTable[tName];

        string path = "Tables/Json/" + tName;
        if(Resources.Load<TextAsset>(path))
            datas = Resources.Load<TextAsset>(path);
        else
        {
            path = "Tables/Dialog/" + tName;
            datas = Resources.Load<TextAsset>(path);
        }

        if (datas != null)
        {
            T t = new T();
            t.Load(datas.text);
            _dicTable.Add(tName, t);
        }
        else
            return null;

        return _dicTable[tName];
    }

    #region [1안]
    /*
    Dictionary<string, Dictionary<string, string>> _tableData = new Dictionary<string, Dictionary<string, string>>();

    void Awake()
    {
        //SetData();
        //DebugData();
    }

    void Update()
    {
    }

    void SetData()
    {
        {
            // 실질적인 데이터는 대괄호 이후의 데이터
            // 필드 데이터는 쉼표에 의해 구분
            // 콜론을 기준으로 앞에 있는 것은 키, 뒤에 있는 것은 값
            // 대괄호를 기점으로 중괄호가 열리면 필드, 콤마가 나오기 전까지의 데이터 중에 콜론을 기준으로 앞에 있는 것은 키, 뒤에 있는 것은 값 반복
            // 단계별로 구분(대괄호 > 중괄호 > 콤마 > 콜론)
            // 중괄호가 닫히는 순간 다음 필드로 넘어가는 것
            // 대괄호가 닫히면 모든 데이터가 다 들어온 것
            // 각각의 테이블 데이터 저장용 클래스를 만들 것 : 원본 파일을 읽어와서 딕셔너리로 저장하고 넘겨주는 역할 + column 종류(잘못된 컬럼명을 찾을 경우 에러 표시용)
            // 그 클래스들을 매니저에서 가지고 있어야 함
            // 찾는게 귀찮아짐 : 넘버링, 클래스마다 고유한 이름을 부여해주고 메인 키 서브 키 넣어줘서 값을 찾아올것임
            // 그 값이 string
            // 어떨 때는 int, 어떨 때는 float, string, bool, enum 등등 다양한 타입으로 가져와야 함
            // 이걸 수월하게 하기 위해 상속(LowBase Class - Main Key, Sub Key를 이용한 Get함수)을 사용할 것임
            // Dictionary<string, Dictionary<string, strint>>
            // Load(JsonData)
            // int MaxCount()
            // string ToS(key, subKKey)
            // int Tol(key, subkey)
            // 데이터만 테이블 클래스에서 긁어오는 것
            // EnumHelper 사용
            // 추상화
            // enum 가짓수만큼 온로드 = 모두 로드 가능
            // 메모 = enum
            // 스피드 = float
            // 이외 = int 혹은 string
            // DataManager
            // enum LowDataType{
            // 시트이름1,
            // }
            // Dictionary<LowData, >
            // 구조화하는 것에 대해 생각해볼 것
        }
        string path = "Tables/Json/MonsterTable";

        TextAsset datas = Resources.Load<TextAsset>(path);
        string[] realDatas = datas.text.Split('[');
        string[] fields = realDatas[1].Split('{');
        for (int i = 1; i < fields.Length; i++)
        {
            Dictionary<string, string> fieldData = new Dictionary<string, string>();
            string[] field = fields[i].Split(',');
            string tableKey = field[0].Split(':')[1].Split('"')[1];
            int len = i == fields.Length - 1 ? field.Length : field.Length - 1;
            for (int j = 0; j < len; j++)
            {
                string key = field[j].Split(':')[0].Split('"')[1];
                string data = field[j].Split(':')[1].Split('"')[1];
                string value = string.Empty;
                int n = 0;
                while (n < data.Length)
                {
                    switch (data[n])
                    {
                        case '\\':
                            n++;
                            switch (data[n])
                            {
                                case 'u':
                                    string s = data.Substring(n + 1, 4);
                                    value += (char)int.Parse(s, System.Globalization.NumberStyles.AllowHexSpecifier);
                                    n += 4;
                                    break;
                            }
                            break;
                        default:
                            value += data[n];
                            break;
                    }n++;
                }
                fieldData.Add(key, value);
            }
            _tableData.Add(tableKey, fieldData);
        }
    }

    void DebugData()
    {
        if (_tableData != null)
        {
            foreach (string tKey in _tableData.Keys)
            {
                //Debug.Log("Index : " + tKey);
                Dictionary<string, string> fieldData = _tableData[tKey];
                foreach (string fKey in fieldData.Keys)
                {
                    Debug.Log("[index : " + tKey + ", [key<" + fKey + ">, Value<" + fieldData[fKey] + ">]]");
                }
            }
        }
    }
    */
    #endregion
}