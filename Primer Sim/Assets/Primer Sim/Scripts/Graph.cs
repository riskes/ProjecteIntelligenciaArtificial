//using System.Reflection.Metadata;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class Graph : MonoBehaviour
{
    [SerializeField] public Sprite circleSprite;
    public RectTransform graphContainer;
    private RectTransform labelTemplateX;
    private RectTransform labelTemplateY;
    Dictionary<string, List<GameObject>> cercles = new Dictionary<string, List<GameObject>>();
    Dictionary<string, List<RectTransform>> labelnums = new Dictionary<string, List<RectTransform>>();
    //private List<GameObject> cercles = new List<GameObject>();
    //private List<RectTransform> labelnums = new List<RectTransform>();
    float yMaximum = 50f;
    float xSize = 3f; 
    private String graphName = "d";

    public void Awake(){

        //List<int> valueList = new List<int>(){ 5, 98, 56, 45, 30, 22, 17, 15, 13, 17, 25, 37, 40, 36, 33};
        //ShowGraph(valueList);
    }
    public void CreateCircle(Vector2 graphPosition, float barWidth){
        
        GameObject gameObject = new GameObject("bar", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(graphPosition.x, 0f);;
        rectTransform.sizeDelta = new Vector2(barWidth, graphPosition.y);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        rectTransform.pivot = new Vector2(.5f, 0f);
        cercles[graphName].Add(gameObject);
    } 
    
    public void ShowLabelY(float graphHeight){
        int separatorCount = 10;
        for(int i = 0; i < separatorCount; i++){
            RectTransform labely = Instantiate(labelTemplateY);
            labely.SetParent(graphContainer);
            labely.gameObject.SetActive(true);
            float normalizedValue = i * 1f/ separatorCount;
            labely.anchoredPosition3D = new Vector3(-1.2f, normalizedValue*graphHeight, -0.12f);
            labely.GetComponent<Text>().text = Mathf.RoundToInt(normalizedValue*yMaximum).ToString();
            labelnums[graphName].Add(labely);
        }
    }
    public void ShowGraph(String nomGraph, int[] valueList){
        graphName=nomGraph;
        graphContainer = transform.Find(graphName).GetComponent<RectTransform>();
        labelTemplateX = graphContainer.Find("labelTemplateX").GetComponent<RectTransform>();
        labelTemplateY = graphContainer.Find("labelTemplateY").GetComponent<RectTransform>();
        float graphHeight = graphContainer.sizeDelta.y; 
        //cercles[graphName].Clear();.
        if(!cercles.ContainsKey(graphName)){
            cercles[graphName] = new List<GameObject>();
            labelnums[graphName] = new List<RectTransform>();
        }
        foreach(GameObject cercle in cercles[graphName]){
            Destroy(cercle);
        }
        foreach(RectTransform num in labelnums[graphName]){
            if(num != null){
                Destroy(num.gameObject);
            }
        }
        for (int i= 0; i < valueList.Length; i++){
            float xPosition =  (i * xSize)+1.5f;
            float yPosition = (valueList[i]/yMaximum) * graphHeight;
            CreateCircle(new Vector2(xPosition, yPosition), xSize-1);
            RectTransform labelx = Instantiate(labelTemplateX);
            labelx.SetParent(graphContainer);
            labelx.gameObject.SetActive(true);
            labelx.anchoredPosition3D = new Vector3(xPosition, -1.87f, -0.12f);
            labelx.GetComponent<Text>().text = i.ToString();
            labelnums[graphName].Add(labelx);
        }
        ShowLabelY(graphHeight);
    }
    
}
