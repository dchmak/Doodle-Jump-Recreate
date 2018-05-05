using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooler : MonoBehaviour {

    [System.Serializable]
    public class Pool {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    #region Singleton
    public static Pooler Instance;

    private void Awake() {
        Instance = this;
    }
    #endregion

    void Start () {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools) {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++) {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
	}

    public GameObject spawn (string tag, Vector2 pos, Quaternion rota) {
        if (!poolDictionary.ContainsKey(tag)) {
            Debug.LogWarning("Tag " + tag + "does not exist in the pool.");
            return null;
        }

        GameObject obj = poolDictionary[tag].Dequeue();

        obj.transform.position = pos;
        obj.transform.rotation = rota;
        obj.SetActive(true);

        poolDictionary[tag].Enqueue(obj);

        return obj;
    }
}
