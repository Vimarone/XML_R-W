using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefineEnum
{
    public enum TableName
    {
        // Test
        PersonallityTable,
        TribeTable,
        GradeTable,
        MonsterTable,
        StageInfoTable,

        // Dialog
        EpisodeTable,
        CharacterTable,
        MapTable,
        DialogTable,

        max
    }

    public enum RSP
    {
        Rock,
        Scissors,
        Paper,

        max
    }

    public enum eSceneIndex
    {
        Lobby,
        Dialog,
        Game,

        none
    }
}
