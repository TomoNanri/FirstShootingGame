using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emitter : MonoBehaviour
{
    // Wave�v���n�u���i�[����
    public GameObject[] waves;

    // ���݂�Wave
    private int currentWave;

    // Start is called before the first frame update
    IEnumerator Start()
    {

        // Wave�����݂��Ȃ���΃R���[�`�����I������
        if (waves.Length == 0)
        {
            yield break;
        }

        while (true)
        {

            // Wave���쐬����
            GameObject wave = (GameObject)Instantiate(waves[currentWave], transform.position, Quaternion.identity);

            // Wave��Emitter�̎q�v�f�ɂ���
            wave.transform.parent = transform;

            // Wave�̎q�v�f��Enemy���S�č폜�����܂őҋ@����
            while (wave.transform.childCount != 0)
            {
                yield return new WaitForEndOfFrame();
            }

            // Wave�̍폜
            Destroy(wave);

            // �i�[����Ă���Wave��S�Ď��s������currentWave��0�ɂ���i�ŏ����� -> ���[�v�j
            if (waves.Length <= ++currentWave)
            {
                currentWave = 0;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
