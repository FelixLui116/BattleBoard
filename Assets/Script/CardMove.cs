using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CardMove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CardMove_Up(){
        this.gameObject.transform.DOMoveY(35, 0.1f);
    }
    
    public void CardMove_down(){
        this.gameObject.transform.DOMoveY(30, 0.1f);
    }
}
