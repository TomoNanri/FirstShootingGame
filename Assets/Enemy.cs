using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Spaceship�R���|�[�l���g
    Spaceship spaceship;

    // �q�b�g�|�C���g
    public int hp = 1;

    // �X�R�A�̃|�C���g
    public int ScorePoint = 100;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        // Spaceship�R���|�[�l���g���擾
        spaceship = GetComponent<Spaceship>();

        // ���[�J�����W��Y���̃}�C�i�X�����Ɉړ�����
        Move(transform.up * -1);

        // canShot��false�̏ꍇ�A�����ŃR���[�`�����I��������
        if (spaceship.canShot == false)
        {
            yield break;
        }

        while (true)
        {

            // �q�v�f��S�Ď擾����
            for (int i = 0; i < transform.childCount; i++)
            {

                Transform shotPosition = transform.GetChild(i);

                // ShotPosition�̈ʒu/�p�x�Œe������
                spaceship.Shot(shotPosition);
            }

            // shotDelay�b�҂�
            yield return new WaitForSeconds(spaceship.shotDelay);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        // ���C���[�����擾
        string layerName = LayerMask.LayerToName(c.gameObject.layer);

        // ���C���[����Bullet (Player)�ȊO�̎��͉����s��Ȃ�
        if (layerName != "Bullet(Player)") return;

        // PlayerBullet��Transform���擾
        Transform playerBulletTransform = c.transform.parent;

        // Bullet�R���|�[�l���g���擾
        Bullet bullet = playerBulletTransform.GetComponent<Bullet>();

        // �q�b�g�|�C���g�����炷
        hp = hp - bullet.power;


        // �e�̍폜
        Destroy(c.gameObject);

        // �q�b�g�|�C���g��0�ȉ��ł����
        if (hp <= 0)
        {

            // ����
            spaceship.Explosion();

            // �G�l�~�[�̍폜
            Destroy(gameObject);

            // �X�R�A�J�E���g�A�b�v
            if (c.name == "Bullet")
            {
                GameManeger.current.AddPoint(ScorePoint);
            }

        }
        else
        {

            spaceship.GetAnimator().SetTrigger("Damage");

        }

    }

    // �@�̂̈ړ�
    public void Move(Vector2 direction)
    {
        GetComponent<Rigidbody2D>().velocity = direction * spaceship.speed;
    }
}
