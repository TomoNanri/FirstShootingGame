using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private static ObjectPool _instance;

    // �V���O���g��
    public static ObjectPool instance
    {
        get
        {
            if (_instance == null)
            {
                // �V�[���ォ��擾����
                _instance = FindObjectOfType<ObjectPool>();

                if (_instance == null)
                {
                    // �Q�[���I�u�W�F�N�g���쐬��ObjectPool�R���|�[�l���g��ǉ�����B
                    _instance = new GameObject("ObjectPool").AddComponent<ObjectPool>();
                }
            }
            return _instance;
        }
    }

    // �Q�[���I�u�W�F�N�g�� Dictionary
    private Dictionary<string, Queue<GameObject>> pooledGameObjects = new Dictionary<string, Queue<GameObject>>();

    // �Q�[���I�u�W�F�N�g�� pooledGameObject ����擾����B�K�v�Ȃ琶���B
    public GameObject GetGameObject(GameObject prefab, Vector2 position, Quaternion rotation)
    {
        // �v���t�@�u�̃C���X�^���X���� key �Ƃ���
        string key = prefab.name;

        // Dictionary �� key �����݂��Ȃ���� key �� Queue �̃y�A�𐶐�����
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
            Debug.Log($"{go.name}�ė��p");
        }
        else
        {
            go = (GameObject)Instantiate(prefab, position, rotation);
            go.name = prefab.name;
            Debug.Log($"<color=red>{go.name}�V�K�쐬</color>");

            go.transform.parent = transform;    // ObjectPool �̎q�ɂ���
        }
        return go;
    }

    public void ReleaseGameObject(GameObject go)
    {
        go.SetActive(false);
        string key = go.name;
        Queue<GameObject> gameObjects = pooledGameObjects[key];
        // ��A�N�e�B�u�Q�[���I�u�W�F�N�g�p�L���[�ɓo�^
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
