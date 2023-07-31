using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManeger : MonoBehaviour
{
    public static GameManeger current;
    public Text ScoreText;
    int score = 1000;

    void Awake()
    {

        if (current == null)
            current = this;
        else if (current != this)
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        ScoreText.text = "SCORE:" + score;
    }

    // Update is called once per frame
    void Update()
    {
        // �X�R�A�E�n�C�X�R�A��\������
        ScoreText.text = score.ToString();
    }

    // �|�C���g�̒ǉ�
    public void AddPoint(int point)
    {
        score = score + point;
    }
}
