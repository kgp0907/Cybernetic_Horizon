using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.VFX;
public class Player : MonoBehaviour
{
    #region 플레이어 스테이트 목록
    public enum playerState
    {
        MOVE,
        NORMALATK1,
        NORMALATK2,
        NORMALATK3,
        CHARGEATK,
        DODGE,
        HIT,
        DEAD,
    }
    #endregion
    #region 플레이어 능력치 변수(이펙트 생성위치, 스테이트, 공격력 등)
    public bool useInventory = false;

    public string animation_id;

    public Player_HP player_Hp;
    public float playerDamage = 5f;
    public bool isAttacking = false;
    public bool isLockOn = false;
    public bool isHit;
    public bool isSmashHit;

    [HideInInspector]
    public Vector3 targetPosition;

    private Inventory inventory;
    public Animator playerAnimator;
    public Player_InputManagement inputmanager;
    public CharacterController playerController;
    public GameObject AtkColision;
    public GameObject[] EffectSpawnPos;
    public GameObject inventoryUI;

    public bool AnimationName => playerAnimator.GetCurrentAnimatorStateInfo(0).IsName(animation_id);
    public float AnimationProgress => playerAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime;
    private Fsm_Base<Player> player_StateMachine;
    private Dictionary<playerState, Base_Interface<Player>> player_States = new Dictionary<playerState, Base_Interface<Player>>();
    #endregion
    [SerializeField] private UI_inventory uiInventory;
    private void Awake()
    {
        player_Hp = GetComponent<Player_HP>();
        inputmanager = GetComponent<Player_InputManagement>();
        playerAnimator = GetComponent<Animator>();
        playerController = GetComponent<CharacterController>();
        AtkColision.SetActive(false);
 
        #region 플레이어 스테이트 추가
        player_States.Add(playerState.MOVE, new Player_State_Move());
        player_States.Add(playerState.NORMALATK1, new Player_State_Atk1());
        player_States.Add(playerState.NORMALATK2, new Player_State_Atk2());
        player_States.Add(playerState.NORMALATK3, new Player_State_Atk3());
        player_States.Add(playerState.CHARGEATK, new Player_State_ChargeAtk());
        player_States.Add(playerState.DODGE, new Player_State_Dodge());
        player_States.Add(playerState.DEAD, new Player_State_Dead());
        player_States.Add(playerState.HIT, new Player_State_Hit());
        // 최초 상태를 Move로 설정
        player_StateMachine = new Fsm_Base<Player>(this, player_States[playerState.MOVE]);
        #endregion
     
    }
    private void Start()
    {
         inventory = new Inventory(UseItem);
         uiInventory.SetPlayer(this);
         uiInventory.SetInventory(inventory);
         inventoryUI.SetActive(false);
         spawn();
    }

    public void spawn()
    {
        ItemWorld.SpawnItemWorld(new Vector3(23, -29, 77), new Item { itemType = Item.ItemType.Sword, amount = 1 });
        ItemWorld.SpawnItemWorld(new Vector3(28, -29, 77), new Item { itemType = Item.ItemType.Sword, amount = 1 });
        ItemWorld.SpawnItemWorld(new Vector3(28, -29, 81), new Item { itemType = Item.ItemType.CoolTimePotion, amount = 10 });
        ItemWorld.SpawnItemWorld(new Vector3(23, -29, 81), new Item { itemType = Item.ItemType.HealthPotion, amount = 4 });
    }

    void Update()
    {
        player_StateMachine.OnUpdate();
    }
    private void FixedUpdate()
    {
        player_StateMachine.OnFixedUpdate();
    }

    public void ChangeState(playerState state)
    {
        player_StateMachine.SetState(player_States[state]);
    }
    
    public void OnApplicationFocus(bool focus)
    {
        if (focus)
            Cursor.lockState = CursorLockMode.Locked;
        else
            Cursor.lockState = CursorLockMode.None;
    }

    public void Shskecamera()
    {
        CinemachineImpulse.Instance.CameraShake(2f);
    }

    private void OnTriggerEnter(Collider collider)
    {
        ItemWorld itemWorld = collider.GetComponent<ItemWorld>();
        if (itemWorld != null)
        {
            inventory.AddItem(itemWorld.GetItem());
            itemWorld.DestroySelf();
        }
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    private void UseItem(Item item)
    {
        switch (item.itemType)
        {
            case Item.ItemType.HealthPotion:
                player_Hp.HealingHP();
                inventory.RemoveItem(new Item { itemType = Item.ItemType.HealthPotion, amount = 1 });
                break;
        }
    }
}
