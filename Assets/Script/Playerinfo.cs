using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerinfo : MonoBehaviour
{
    // Start is called before the first frame update
    public static Playerinfo Instance { get; private set; }

    [SerializeField] private GameObject player_SelectCard;


    public GameObject Get_player_SelectCard(){
        return player_SelectCard;
    }

    public void Set_player_SelectCard(GameObject go){
        player_SelectCard = go;
    }

    private void Awake() {
        
        if (Instance == null){
            Instance = this;
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
