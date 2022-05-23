using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.VFX;
public class Player : MonoBehaviour
{
    #region 플레이어 스테이트 목록
    public enum eState
    {
        MOVE,
        BUFF,
        SPATK,
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
    private StateMachine<Player> p_sm;
    private Dictionary<eState, IState<Player>> p_states = new Dictionary<eState, IState<Player>>();
    public bool AnimationName => PlayerAnimator.GetCurrentAnimatorStateInfo(0).IsName(animation_id);
    public float AnimationProgress => PlayerAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime;
    public Player_HP player_Hp;
    [HideInInspector]
    public Vector3 targetPosition;
    private Inventory inventory;
    public Animator PlayerAnimator;
    public Player_InputManagement inputmanager;
    public CharacterController playerController;
    public Transform target;
    public GameObject AtkColision;
    public GameObject[] EffectSpawnPos;
    public GameObject Character;
    public bool isAttacking = false;
    public bool isLockOn=false;
    public bool Hit;
    public bool SmashHit;
    public float playerDamage = 5f;
    public GameObject inventoryUI;
    #endregion
    public List<GameObject> MonsterList = new List<GameObject>();
    [SerializeField] private UI_inventory uiInventory;
    private void Awake()
    {
        player_Hp = GetComponent<Player_HP>();
        inputmanager = GetComponent<Player_InputManagement>();
        PlayerAnimator = GetComponent<Animator>();
        playerController = GetComponent<CharacterController>();
        AtkColision.SetActive(false);
 
        #region 플레이어 스테이트 추가
        p_states.Add(eState.MOVE, new StateMove());
        p_states.Add(eState.SPATK, new State_SpAtk());
        p_states.Add(eState.NORMALATK1, new State_NormalAtk1());
        p_states.Add(eState.NORMALATK2, new State_NormalAtk2());
        p_states.Add(eState.NORMALATK3, new State_NormalAtk3());
        p_states.Add(eState.CHARGEATK, new State_ChargeAtk());
        p_states.Add(eState.DODGE, new StateDodge());
        p_states.Add(eState.DEAD, new StateDead());
        p_states.Add(eState.HIT, new StateHit());
        p_sm = new StateMachine<Player>(this, p_states[eState.MOVE]);

        // RagDoll.SetActive(false);
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
        ItemWorld.SpawnItemWorld(new Vector3(-23, -29, 151), new Item { itemType = Item.ItemType.Sword, amount = 1 });
        ItemWorld.SpawnItemWorld(new Vector3(-13, -29, 151), new Item { itemType = Item.ItemType.HealthPotion, amount = 1 });
        ItemWorld.SpawnItemWorld(new Vector3(0, -29, 151), new Item { itemType = Item.ItemType.CoolTimePotion, amount = 1 });
    }

    void Update()
    {
        p_sm.OnUpdate();
    }
    private void FixedUpdate()
    {
        p_sm.OnFixedUpdate();
    }
    public void ChangeState(eState state)
    {
        p_sm.SetState(p_states[state]);
    }
    
    public void LockOn()
    {
        if (isLockOn)
        {
            targetPosition = new Vector3(target.transform.position.x, transform.position.y, target.position.z);
            transform.LookAt(targetPosition);
        }
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
                player_Hp.PlayerHeal();
                inventory.RemoveItem(new Item { itemType = Item.ItemType.HealthPotion, amount = 1 });
                break;
            case Item.ItemType.CoolTimePotion:
                Debug.Log("쿨타임 줄음");
                inventory.RemoveItem(new Item { itemType = Item.ItemType.CoolTimePotion, amount = 1 });
                break;
        }
    }
}
