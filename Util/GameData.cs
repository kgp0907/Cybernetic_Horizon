using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable] // 직렬화

public class GameData
{
    // 플레이어의 체력, 위치
    public bool PlayerHP;
    public Vector3 playerPos;
    public Vector3 playerRot;
}
