using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.Rendering.LookDev;

public class Item : MonoBehaviour
{
    UnitCode unitCode;
    Status status;
    GameManager gameManager;

    long tick;

    public float randomSeed;

    public GameObject[] randomObjects;

    void Awake()
    {
        StatusInit();
        RandomSeed();
    }

    // �������ͽ� �ʱ�ȭ
    void StatusInit()
    {
        // ���� ����
        string itemName = "������ �̸�";
        unitCode = UnitCode.Item;
        status = new Status(unitCode, itemName, gameManager.stageCount);
    }

    // ��� ������ �Լ�
    void RandomStatus()
    {
        // �������� ������
        
    }

    void RandomSeed()
    {
        tick = DateTime.Now.Ticks;
        randomSeed = (int)(tick % int.MaxValue);
        UnityEngine.Random.InitState((int)randomSeed);
    }
}