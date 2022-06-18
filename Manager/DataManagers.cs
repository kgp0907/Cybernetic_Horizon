using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
// 

public class PlayerData
{
    //이름, 레벨, 코인, 착용중 무기
    public string name;
    public int level;
    public int coin;
    public int item;
    public Vector3 playerPos;
    public Vector3 playerRot;
}

public class DataManagers : SingletonBase<DataManagers>
{
    // Start is called before the first frame update
    private Player thePlayer;
    public PlayerData nowPlayer = new PlayerData();   
    public GameData _gameData;
    public string path;
    public int nowSlot;


    private void Awake()
    { 
        
        path = Application.persistentDataPath+"/";
       
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
            SaveData();
    }

    public void SaveData()
    {
        thePlayer = FindObjectOfType<Player>();
        nowPlayer.playerPos = thePlayer.transform.position;
        nowPlayer.playerRot = thePlayer.transform.eulerAngles;
        string data = JsonUtility.ToJson(nowPlayer);
        File.WriteAllText(path +nowSlot.ToString(), data);  
    }

    public void LoadData()
    {
        string data= File.ReadAllText(path+nowSlot.ToString());
        nowPlayer= JsonUtility.FromJson<PlayerData>(data);
    }

    public void DataClear()
    {
        nowSlot = -1;
        nowPlayer = new PlayerData();
    }

   
}
