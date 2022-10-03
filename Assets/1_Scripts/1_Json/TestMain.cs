using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DefineEnum;

public class TestMain : MonoBehaviour
{
    // 엔진이나 API의 경우 처음부터 내가 만들지 않는 경우가 많음
    // 그래서 안에서 어떻게 움직이는지 모르는 경우가 많음
    // 그래서 내 의도대로 엔진이나 API가 작동하지 않는 경우가 많음
    // 그에 맞춰서 해야할 필요성이 생김
    // 그래서 코드의 동작이 내 제어 안에 들어와 있는 것을 원함
    // 내 제어를 벗어난 경우 = 오류
    // 따라서 명확하게 생성하는 시기를 만들어놓는 것을 선호
    // 씬에 등록시키는 것 = 누가 먼저 생기고 누가 나중에 생기는지 모름
    // 아예 처음부터 생성시키는 순서까지 정하고 싶을 때 사용
    // 매니저끼리도 상호작용을 하기 때문(로드 순서에 따라 값이 바뀌는 등)
    // 그래서 TSingleton 사용한 것

    void Awake()
    {
        TableManager._instance.LoadAll();
    }

    void Start()
    {
        string val = TableManager._instance.GetTable(TableName.PersonallityTable).ToStr(2, "PersName");
        Debug.LogFormat("[{3}] :: Index : {0}, column : {1}, value : {2}", 2, "PersName", val, TableName.PersonallityTable);

        val = TableManager._instance.GetTable(TableName.StageInfoTable).ToStr(10, "Name");
        Debug.LogFormat("[{3}] :: Index : {0}, column : {1}, value : {2}", 10, "Name", val, TableName.StageInfoTable);

        val = TableManager._instance.GetTable(TableName.MonsterTable).ToStr(5004, "Name");
        Debug.LogFormat("[{3}] :: Index : {0}, column : {1}, value : {2}", 5004, "Name", val, TableName.MonsterTable);

    }
}
