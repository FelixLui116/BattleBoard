using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class BaseMonster : NetworkBehaviour
{
    [SerializeField] private int damage = 0;
    [SerializeField] private int attackRange =1;

    //  1 up 2 down 3 left 4 right   maybe using string????
    [SerializeField] private int attackDirection = 1;
    [SerializeField] private int cost = 0;
    [SerializeField] private int HP = 0;
    [SerializeField] private string type = "Test";
    [SerializeField] private string cardNum;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int Get_Hp(){
        return HP;
    }
    public int Get_Cost(){
        return cost;
    }
    public string Get_Type(){
        return type;
    }
    public int Get_Damage(){
        return damage;
    }
    public int Get_AttackRange(){
        return attackRange;
    }
    public int Get_AttackDirection(){
        return attackDirection;
    }
}
