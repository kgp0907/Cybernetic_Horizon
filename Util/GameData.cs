using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable] // 직렬화

public class GameData
{
    // 각 챕터의 잠금여부
    public bool PlayerHP;
    public Vector3 playerPos;
    public Vector3 playerRot;
}