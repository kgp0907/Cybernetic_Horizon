using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
  //  public Text name;
    private Player thePlayer;


    private void Awake()
    {
        thePlayer = FindObjectOfType<Player>();
    }
    // Start is called before the first frame update
    void Start()
    {
      //  name.text += DataManagers.Instance.nowPlayer.name;
        thePlayer.transform.position = DataManagers.Instance.nowPlayer.playerPos;
        thePlayer.transform.eulerAngles = DataManagers.Instance.nowPlayer.playerRot;
    }



}
