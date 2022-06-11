using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
public class SaveDataManager : SingletonBase<SaveDataManager>
{

    // --- ���� ������ �����̸� ���� ---
    public string GameDataFileName = "GameData.json";

    private Player thePlayer;

    // "���ϴ� �̸�(����).json"
    public GameData _gameData;
    public GameData gameData
    {
        get
        {
            // ������ ���۵Ǹ� �ڵ����� ����ǵ���
            if (_gameData == null)
            {
                LoadGameData();
                SaveGameData();
            }
            return _gameData;
        }
    }

    private void Start()
    {
        thePlayer = FindObjectOfType<Player>();
        LoadGameData();
        SaveGameData();
    }

    // ����� ���� �ҷ�����
    public void LoadGameData()
    {
        string filePath = Application.persistentDataPath + GameDataFileName;

        // ����� ������ �ִٸ�
        if (File.Exists(filePath))
        {
      
            print("�ҷ����� ����");
            string FromJsonData = File.ReadAllText(filePath);
            _gameData = JsonUtility.FromJson<GameData>(FromJsonData);
            thePlayer.transform.position = gameData.playerPos;
            thePlayer.transform.eulerAngles = gameData.playerRot;
        }

        // ����� ������ ���ٸ�
        else
        {
            print("���ο� ���� ����");
            _gameData = new GameData();
        }
    }

    // ���� �����ϱ�
    public void SaveGameData()
    {

        // �ùٸ��� ����ƴ��� Ȯ�� (�����Ӱ� ����)
        gameData.playerPos = thePlayer.transform.position;
        gameData.playerRot = thePlayer.transform.rotation.eulerAngles;
        string ToJsonData = JsonUtility.ToJson(gameData);
        string filePath = Application.persistentDataPath + GameDataFileName;

        // �̹� ����� ������ �ִٸ� �����
        File.WriteAllText(filePath, ToJsonData);
    }

    // ������ �����ϸ� �ڵ�����ǵ���
    private void OnApplicationQuit()
    {
        //SaveGameData();
    }
}