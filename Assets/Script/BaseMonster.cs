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

    private readonly NetworkVariable<Vector3> _newPos = new(writePerm: NetworkVariableWritePermission.Owner);
    private readonly NetworkVariable<Quaternion> _newRot = new(writePerm: NetworkVariableWritePermission.Owner);
    // Start is called before the first frame update
    void Start()
    {
        Position_update();
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

    public void Position_update(){
        if (IsOwner){       // update to server position
            _newPos.Value = transform.position;
            _newRot.Value = transform.rotation;

        }
        else{
            transform.position =  _newPos.Value;
            transform.rotation = _newRot.Value;
        }
    }
}
