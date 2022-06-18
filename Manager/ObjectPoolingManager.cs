using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 오브젝트 풀링 매니저
/// 스크립트 삽입후 리스트에 태그, 프리팹, 개수를 입력
/// </summary>
public class ObjectPoolingManager : SingletonBase<ObjectPoolingManager>
{
    GameObject obj;

    [System.Serializable]
    public class ObjectPool
    {
        public string tag;
        public GameObject Prefab;
        public int Size;
    }

    public List<ObjectPool> ObjectPoolList;
    public Dictionary<string, Queue<GameObject>> ObjectPoolDictionary;

    // 게임 시작시 등록 해놓은 모든 오브젝트를 오브젝트풀링매니저에 등록 후 비활성화
    private void Start()
    {
        ObjectPoolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (ObjectPool pool in ObjectPoolList)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.Size; i++)
            {
                obj = Instantiate(pool.Prefab) as GameObject;
                obj.name = pool.Prefab.name;
                obj.transform.SetParent(gameObject.transform);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            ObjectPoolDictionary.Add(pool.tag, objectPool);
        }
    }

    // "태그"와 오브젝트를 소환할 위치를 인자로 받음
    public GameObject GetObject(string tag, GameObject Parent)
    {
        if (!ObjectPoolDictionary.ContainsKey(tag))
            return null;
        GameObject SpawnObject;
        SpawnObject = ObjectPoolDictionary[tag].Dequeue();
        SpawnObject.transform.SetParent(Parent.transform);
        SpawnObject.transform.position = Parent.transform.position;
        SpawnObject.transform.rotation = Parent.transform.rotation;
        SpawnObject.SetActive(true);
        return SpawnObject;
    }
    // "태그"와 오브젝트를 소환할 위치,각도를 인자로 받음
    public GameObject GetObject(string tag, Vector3 Parent, Quaternion Parent2)
    {
        if (!ObjectPoolDictionary.ContainsKey(tag))
            return null;
        GameObject SpawnObject;
        SpawnObject = ObjectPoolDictionary[tag].Dequeue();
        SpawnObject.transform.position = Parent;
        SpawnObject.transform.rotation = Parent2;
        SpawnObject.SetActive(true);
        return SpawnObject;
    }
    // "태그"와 오브젝트를 소환할 위치(오브젝트)를 인자로 받음. 다만 부모를 지정하지 않아 이펙트, 총알등 지정 권장
    public GameObject GetObject_Noparent(string tag, GameObject spawnPos)
    {
        if (!ObjectPoolDictionary.ContainsKey(tag))
            return null;
        GameObject SpawnObject;
        SpawnObject = ObjectPoolDictionary[tag].Dequeue();
        SpawnObject.SetActive(true);
        SpawnObject.transform.position = spawnPos.transform.position;
        SpawnObject.transform.rotation = spawnPos.transform.rotation;

        return SpawnObject;
    }
    // "태그"와 반환할 오브젝트를 인자로 받음.
    public GameObject ReturnObject(string tag, GameObject Object)
    {
        ObjectPoolDictionary[tag].Enqueue(Object);
        Object.transform.SetParent(gameObject.transform);
        Object.SetActive(false);
        return Object;
    }
}
