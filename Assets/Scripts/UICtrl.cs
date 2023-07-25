using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class UICtrl : MonoBehaviour
{
    public Transform spiderPool;

    public GameObject spiderPrefab;

    public List<SpiderData> data;

    public Button spawn20, spawn50, spawn100;

    public TextMeshProUGUI fpsText;

    float deltaTime = 0.0f;

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    private void Start()
    {
        this.spawn20.onClick.AddListener(this.Spawn20Spiders);
        
        this.spawn50.onClick.AddListener(this.Spawn50Spiders);
        
        this.spawn100.onClick.AddListener(this.Spawn100Spiders);
    }

    private void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        UpdateFPS();
    }

    private void Spawn20Spiders()
    {
        ClearSpiders();
        
        int typeAmount = this.data.Count;

        for (int i = 0; i < 20; i++)
        {

            var temp = Random.Range(0,10);

            Vector3 pos = Vector3.zero;
            
            switch (temp)
            {
                case 0:
                    pos = new Vector3(-9.092f,-4.9f,0);
                    break;
                case 1:
                    pos = new Vector3(-8.32f,-4.9f,0);
                    break;
                case 2:
                    pos = new Vector3(-7.58f,-4.9f,0);
                    break;
                case 3:
                    pos = new Vector3(-6.84f,-4.9f,0);
                    break;
                case 4:
                    pos = new Vector3(-6.07f,-4.9f,0);
                    break;
                case 5:
                    pos = new Vector3(-5.32f,-4.9f,0);
                    break;
                case 6:
                    pos = new Vector3(-4.58f,-4.9f,0);
                    break;
                case 7:
                    pos = new Vector3(-3.83f,-4.9f,0);
                    break;
                case 8:
                    pos = new Vector3(-3.06f,-4.9f,0);
                    break;
                case 9:
                    pos = new Vector3(-2.2f,-4.9f,0);
                    break;
            }
            
            GameObject spider = Instantiate(this.spiderPrefab, pos, new Quaternion(0,0,0,0), this.spiderPool);
            
            spider.GetComponent<Spider>().SetData(data[Random.Range(0,typeAmount-1)]);  
        }
    }
    
    private void Spawn50Spiders()
    {
        ClearSpiders();
        
        int typeAmount = this.data.Count;

        for (int i = 0; i < 50; i++)
        {
            var temp = Random.Range(0,10);

            Vector3 pos = Vector3.zero;
            
            switch (temp)
            {
                case 0:
                    pos = new Vector3(-9.092f,-4.9f,0);
                    break;
                case 1:
                    pos = new Vector3(-8.32f,-4.9f,0);
                    break;
                case 2:
                    pos = new Vector3(-7.58f,-4.9f,0);
                    break;
                case 3:
                    pos = new Vector3(-6.84f,-4.9f,0);
                    break;
                case 4:
                    pos = new Vector3(-6.07f,-4.9f,0);
                    break;
                case 5:
                    pos = new Vector3(-5.32f,-4.9f,0);
                    break;
                case 6:
                    pos = new Vector3(-4.58f,-4.9f,0);
                    break;
                case 7:
                    pos = new Vector3(-3.83f,-4.9f,0);
                    break;
                case 8:
                    pos = new Vector3(-3.06f,-4.9f,0);
                    break;
                case 9:
                    pos = new Vector3(-2.2f,-4.9f,0);
                    break;
            }
            
            GameObject spider = Instantiate(this.spiderPrefab, pos, new Quaternion(0,0,0,0), this.spiderPool);
            
            spider.GetComponent<Spider>().SetData(data[Random.Range(0,typeAmount-1)]);  
        }
    }
    
    private void Spawn100Spiders()
    {
        ClearSpiders();
        
        int typeAmount = this.data.Count;

        for (int i = 0; i < 100; i++)
        {
            var temp = Random.Range(0,10);

            Vector3 pos = Vector3.zero;
            
            switch (temp)
            {
                case 0:
                    pos = new Vector3(-9.092f,-4.9f,0);
                    break;
                case 1:
                    pos = new Vector3(-8.32f,-4.9f,0);
                    break;
                case 2:
                    pos = new Vector3(-7.58f,-4.9f,0);
                    break;
                case 3:
                    pos = new Vector3(-6.84f,-4.9f,0);
                    break;
                case 4:
                    pos = new Vector3(-6.07f,-4.9f,0);
                    break;
                case 5:
                    pos = new Vector3(-5.32f,-4.9f,0);
                    break;
                case 6:
                    pos = new Vector3(-4.58f,-4.9f,0);
                    break;
                case 7:
                    pos = new Vector3(-3.83f,-4.9f,0);
                    break;
                case 8:
                    pos = new Vector3(-3.06f,-4.9f,0);
                    break;
                case 9:
                    pos = new Vector3(-2.2f,-4.9f,0);
                    break;
            }
            
            GameObject spider = Instantiate(this.spiderPrefab, pos, new Quaternion(0,0,0,0), this.spiderPool);
        
            spider.GetComponent<Spider>().SetData(data[Random.Range(0,typeAmount-1)]);  
        }
    }

    private void ClearSpiders()
    {
        Destroy(this.spiderPool.gameObject);

        GameObject newPool = new GameObject();

        newPool.name = "Pool";
        
        this.spiderPool = newPool.transform;
    }
    
    void UpdateFPS()
    {
        float  msec = deltaTime * 1000.0f;
        float  fps  = 1.0f / deltaTime;
        string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
        fpsText.text = text;
    }
}
