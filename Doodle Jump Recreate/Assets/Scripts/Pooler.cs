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

        initialize();
    }
    #endregion

    void initialize () {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools) {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            GameObject objParent = new GameObject();
            objParent.name = pool.tag + " Set";

            for (int i = 0; i < pool.size; i++) {
                GameObject obj = Instantiate(pool.prefab);
                obj.transform.parent = objParent.transform;
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject spawn (string tag, Vector2 pos) {
        if (!poolDictionary.ContainsKey(tag)) {
            Debug.LogWarning("Tag " + tag + "does not exist in the pool.");
            return null;
        }

        GameObject obj = poolDictionary[tag].Dequeue();

        obj.transform.position = pos;
        obj.transform.rotation = Quaternion.identity;
        obj.SetActive(true);

        poolDictionary[tag].Enqueue(obj);

        return obj;
    }
}
