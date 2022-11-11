using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;

public class BaseCard : MonoBehaviour
{
    private CardMove cardmove;
    // private bool card_Panel_bool = false;

    [SerializeField] private GameObject masterPrefab;
    private BaseMaster masterOject_BM;

    [SerializeField] private string master_string = "Master_Test"; 

    // Card info
    private int hp,damage,cost,attackRange,attackDirection;
    [SerializeField] private Text [] textlist; // hp,damage,cost,attackRange,attackDirection;
    private string type;


    private bool cardClick = false;
    public static Playerinfo Playerinfo_Instance;

    [SerializeField] private Button cardbutton;
    // Start is called before the first frame update
    private void Awake() {
        
        cardmove = gameObject.GetComponent<CardMove>();

        masterPrefab = Resources.Load<GameObject>("Prefabs/Master/"+ master_string);
        masterOject_BM  = masterPrefab.gameObject.GetComponent<BaseMaster>();
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
        if (Playerinfo.Instance.Get_player_SelectCard() != null &&  Playerinfo.Instance.Get_player_SelectCard() == masterPrefab ){
            return;
        }else{
            Playerinfo.Instance.Set_player_SelectCard(masterPrefab);
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
        hp = masterOject_BM.Get_Hp();
        damage = masterOject_BM.Get_Damage();
        cost = masterOject_BM.Get_Cost();
        attackRange = masterOject_BM.Get_AttackRange();
        attackDirection = masterOject_BM.Get_AttackDirection();
        type = masterOject_BM.Get_Type();
        
        
        textlist[0].text = hp.ToString();
        textlist[1].text = cost.ToString();
        textlist[2].text = damage.ToString();
        textlist[3].text = type;
    }

}
