using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ������Ʈ Ǯ�� �Ŵ���
/// ��ũ��Ʈ ������ ����Ʈ�� �±�, ������, ������ �Է�
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

    // ���� ���۽� ��� �س��� ��� ������Ʈ�� ������ƮǮ���Ŵ����� ��� �� ��Ȱ��ȭ
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

    // "�±�"�� ������Ʈ�� ��ȯ�� ��ġ�� ���ڷ� ����
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
    // "�±�"�� ������Ʈ�� ��ȯ�� ��ġ,������ ���ڷ� ����
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
    // "�±�"�� ������Ʈ�� ��ȯ�� ��ġ(������Ʈ)�� ���ڷ� ����. �ٸ� �θ� �������� �ʾ� ����Ʈ, �Ѿ˵� ���� ����
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
    // "�±�"�� ��ȯ�� ������Ʈ�� ���ڷ� ����.
    public GameObject ReturnObject(string tag, GameObject Object)
    {
        ObjectPoolDictionary[tag].Enqueue(Object);
        Object.transform.SetParent(gameObject.transform);
        Object.SetActive(false);
        return Object;
    }
}
