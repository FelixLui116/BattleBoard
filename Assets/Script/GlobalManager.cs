using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GlobalManager Instance { get; private set; }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void Awake() {

        if (Instance == null){
            Instance = this;
        }
    } 

}
