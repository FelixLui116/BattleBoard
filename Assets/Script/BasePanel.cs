using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasePanel : MonoBehaviour
{
    [SerializeField] private Button openPanel;
    [SerializeField] private Button closePanel;

    // public GameObject TestObject;
    
    private void Awake() {
        openPanel.onClick.AddListener(openPanel_func);
        closePanel.onClick.AddListener(closePanel_func);
    }
    // Start is called before the first frame update
    void Start()
    {
        // TestObject = Resources.Load<GameObject>("Prefabs/Monster_Test.prefab"); Prefabs/Monster/Monster_Test.prefab
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void openPanel_func() {
        this.gameObject.SetActive(true);
    }
    public void closePanel_func() {
        this.gameObject.SetActive(false);
    }
    // public void CancelSeleteCard_func (){
        
    // }
}
