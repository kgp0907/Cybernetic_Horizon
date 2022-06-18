using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
public class Select : MonoBehaviour
{
    public GameObject creat;
    public Text[] slotText;
    public Text newPlayername;

    bool[] savefile=new bool[3];

    // Start is called before the first frame update
    void Start()
    { // ���Ժ��� ����� �����Ͱ� �����ϴ��� �Ǵ�.
        for (int i = 0; i < 3; i++)
        {
            if (File.Exists(DataManagers.Instance.path + $"{i}"))
            {
                savefile[i] = true;
                DataManagers.Instance.nowSlot = i;
                DataManagers.Instance.LoadData();
                slotText[i].text = DataManagers.Instance.nowPlayer.name;
            }
            else
            {
                slotText[i].text = "�������";
            }
        }
        DataManagers.Instance.DataClear();
    }



    public void Slot(int number)
    {
        DataManagers.Instance.nowSlot = number;

        // 1 ����� �����Ͱ� ������
        if (savefile[number])
        {
            // 2.����� �����Ͱ� ������
            DataManagers.Instance.LoadData();
            GoGame();
        }
        else
        {
            Creat();
        }
    }


    public void Creat()
    {
        creat.gameObject.SetActive(true);
    }

    public void GoGame()
    {
        if (!savefile[DataManagers.Instance.nowSlot])
        {
            DataManagers.Instance.nowPlayer.name = newPlayername.text;
            DataManagers.Instance.SaveData();
        }     
        LoadSceneManager.Instance.LoadScene("Main");
    }
}
