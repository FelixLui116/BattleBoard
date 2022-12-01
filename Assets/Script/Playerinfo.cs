using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerinfo : MonoBehaviour
{
    // Start is called before the first frame update
    public static Playerinfo Instance { get; private set; }

    [SerializeField] private GameObject player_SelectCard;
    
    [SerializeField] private int totalCard = 10;
    private int startingCard = 3;
    [SerializeField] private int currentCard;
    [SerializeField] private int [] cardDeck;

    public int playcost = 0;
    
    public bool isPlayTrun; 

    [SerializeField] private CardPanel cardPanel; 

    public GameObject Get_player_SelectCard(){
        return player_SelectCard;
    }

    public void Set_player_SelectCard(GameObject go){
        player_SelectCard = go;
    }

    public void Clean_SeletcCard(){
        player_SelectCard = null;

    }

    private void Awake() {
        
        if (Instance == null){
            Instance = this;
        }
    }
    void Start()
    {
        givecardDeck_player();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void givecardDeck_player (){
        cardDeck = new int [totalCard];
        for (int i = 0; i < totalCard; i++)
        {
            var rn = Random.Range(0,2);
            cardDeck[i] = rn;
            // Debug.Log(rn);
        }
    }

    private void givePlayerCard (int amount){
        for (int i = 0; i < amount; i++)
        {
            // cardPanel.cardHold;
        }

    }
}
