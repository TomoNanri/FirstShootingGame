using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Spaceship�R���|�[�l���g
    Spaceship spaceship;
    public bool GodMode;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        // Spaceship�R���|�[�l���g���擾
        spaceship = GetComponent<Spaceship>();

        while (true)
        {
            // �e���v���C���[�Ɠ����ʒu/�p�x�ō쐬
            spaceship.Shot(transform);

            // �V���b�g����炷
            GetComponent<AudioSource>().Play();

            // 0.05�b�҂�
            yield return new WaitForSeconds(0.05f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // �E�E��
        float x = Input.GetAxisRaw("Horizontal");

        // ��E��
        float y = Input.GetAxisRaw("Vertical");

        // �ړ�������������߂�
        Vector2 direction = new Vector2(x, y).normalized;

        // �ړ���������ƃX�s�[�h��������
        //spaceship.Move(direction);

        // �ړ��̐���
        Clamp(direction);
    }

    // �Ԃ������u�ԂɌĂяo�����
    void OnTriggerEnter2D(Collider2D c)
    {
        // ���C���[�����擾
        string layerName = LayerMask.LayerToName(c.gameObject.layer);

        // ���C���[����Bullet (Enemy)�̎��͒e���폜
        if (layerName == "Bullet(Enemy)")
        {
            // �e�̍폜
            //Destroy(c.gameObject);
            ObjectPool.instance.ReleaseGameObject(c.gameObject);
        }

        // ���C���[����Bullet (Enemy)�܂���Enemy�̏ꍇ�͔���
        if ((layerName == "Bullet(Enemy)" || layerName == "Enemy") && GodMode != true)
        {
            // ��������
            spaceship.Explosion();

            // �v���C���[���폜
            Destroy(gameObject);
        }
    }

    void Clamp(Vector2 direction)
    {
        // ��ʍ����̃��[���h���W���r���[�|�[�g����擾
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

        // ��ʉE��̃��[���h���W���r���[�|�[�g����擾
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        // �v���C���[�̍��W���擾
        Vector2 pos = transform.position;

        // �ړ��ʂ�������
        pos += direction * spaceship.speed * Time.deltaTime;

        // �v���C���[�̈ʒu����ʓ��Ɏ��܂�悤�ɐ�����������
        pos.x = Mathf.Clamp(pos.x, min.x, max.x);
        pos.y = Mathf.Clamp(pos.y, min.y, max.y);

        // �������������l���v���C���[�̈ʒu�Ƃ���
        transform.position = pos;
    }
}
