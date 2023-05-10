using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;

public class BaseCard : MonoBehaviour
{
    private CardMove cardmove;
    // private bool card_Panel_bool = false;

    [SerializeField] private GameObject MonsterPrefab;
    private BaseMonster MonsterOject_BM;

    [SerializeField] private string Monster_string = "Monster_Test"; 

    // Card info
    private int hp,damage,cost,attackRange,attackDirection;
    [SerializeField] private Text [] textlist; // hp,damage,cost,attackRange,attackDirection;
    private string type;

    public int card_number;

    private bool cardClick = false;
    public static Playerinfo Playerinfo_Instance;

    [SerializeField] private Button cardbutton;
    // Start is called before the first frame update
    private void Awake() {
        
        cardmove = gameObject.GetComponent<CardMove>();

        MonsterPrefab = Resources.Load<GameObject>("Prefabs/Monster/"+ Monster_string);
        MonsterOject_BM  = MonsterPrefab.gameObject.GetComponent<BaseMonster>();
        CardInfo_update();

        cardbutton.onClick.AddListener(OnClick_card);

    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick_card(){
        Debug.Log("Card on click");
        // update Selectcard
        if (Playerinfo.Instance.Get_player_SelectCard() != null &&  Playerinfo.Instance.Get_player_SelectCard() == MonsterPrefab ){
            return;
        }else{
            
            Debug.Log("Get MonsterPrefab: " + MonsterPrefab.name);
            Playerinfo.Instance.Set_player_SelectCard(MonsterPrefab);
            
        }
        // if (card_Panel_bool)
        // {
            // cardmove.CardMove_down();
            // card_Panel_bool = false;
            // return;
        // }
        // card_Panel_bool = true;
        // cardmove.CardMove_Up();
	}
    
    private void CardInfo_update(){
        
        //   hp,damage,cost,attackRange,attackDirection;  type
        hp = MonsterOject_BM.Get_Hp();
        damage = MonsterOject_BM.Get_Damage();
        cost = MonsterOject_BM.Get_Cost();
        attackRange = MonsterOject_BM.Get_AttackRange();
        attackDirection = MonsterOject_BM.Get_AttackDirection();
        type = MonsterOject_BM.Get_Type();
        
        
        textlist[0].text = hp.ToString();
        textlist[1].text = cost.ToString();
        textlist[2].text = damage.ToString();
        textlist[3].text = type;
    }

}
