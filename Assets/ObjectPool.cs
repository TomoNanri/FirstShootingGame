using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private static ObjectPool _instance;

    // シングルトン
    public static ObjectPool instance
    {
        get
        {
            if (_instance == null)
            {
                // シーン上から取得する
                _instance = FindObjectOfType<ObjectPool>();

                if (_instance == null)
                {
                    // ゲームオブジェクトを作成しObjectPoolコンポーネントを追加する。
                    _instance = new GameObject("ObjectPool").AddComponent<ObjectPool>();
                }
            }
            return _instance;
        }
    }

    // ゲームオブジェクトの Dictionary
    private Dictionary<string, Queue<GameObject>> pooledGameObjects = new Dictionary<string, Queue<GameObject>>();

    // ゲームオブジェクトを pooledGameObject から取得する。必要なら生成。
    public GameObject GetGameObject(GameObject prefab, Vector2 position, Quaternion rotation)
    {
        // プレファブのインスタンス名を key とする
        string key = prefab.name;

        // Dictionary に key が存在しなければ key と Queue のペアを生成する
        if (pooledGameObjects.ContainsKey(key) == false)
        {
            pooledGameObjects.Add(key, new Queue<GameObject>());
        }

        Queue<GameObject> gameObjects = pooledGameObjects[key];
        GameObject go = null;

        if (gameObjects.Count > 0)
        {
            go = gameObjects.Dequeue();
            go.transform.position = position;
            go.transform.rotation = rotation;
            go.SetActive(true);
            Debug.Log($"{go.name}再利用");
        }
        else
        {
            go = (GameObject)Instantiate(prefab, position, rotation);
            go.name = prefab.name;
            Debug.Log($"<color=red>{go.name}新規作成</color>");

            go.transform.parent = transform;    // ObjectPool の子にする
        }
        return go;
    }

    public void ReleaseGameObject(GameObject go)
    {
        go.SetActive(false);
        string key = go.name;
        Queue<GameObject> gameObjects = pooledGameObjects[key];
        // 非アクティブゲームオブジェクト用キューに登録
        gameObjects.Enqueue(go);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
