using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyArea : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // ���C���[�����擾
        string layerName = LayerMask.LayerToName(collision.gameObject.layer);
        // �G�̓f�X�g���C����
        if(layerName == "Enemy")
            Destroy(collision.gameObject);
    }
}
