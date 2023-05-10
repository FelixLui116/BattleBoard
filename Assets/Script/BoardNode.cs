using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class BoardNode : NetworkBehaviour // NetworkBehaviour MonoBehaviour
{
    private Renderer Node_renderer;

    [SerializeField] private GameObject Monster_;

    public GameObject TestClone;
    [SerializeField] private GameObject Node;
    [SerializeField] private Vector2 NodePosition;

    private GameObject Monster_Ghost;

    private bool mouseClick_bool = false;

    public static Playerinfo Playerinfo_Instance;

    private NetworkVariable<Vector3> NodeColor = new NetworkVariable<Vector3>();

    private void Awake() {
        Node_renderer = Node.gameObject.GetComponent<Renderer>();
    }

    public void SetNodePosition(int x, int y){
        NodePosition = new Vector2(x ,y);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // private void OnMouse
    private void OnMouseEnter() {
        Debug.Log("In MouseEnter");

        // Destroy_Ghost();

        if(Playerinfo.Instance.Get_player_SelectCard() !=null){
            Monster_ = Playerinfo.Instance.Get_player_SelectCard();
        }else{
            return;
        }
        // if (Monster_ != null){
        //     return;
        // }

        if (mouseClick_bool){
            return;
        }
        
        // Node_renderer.material.color = Color.yellow;
        if (IsHost){
            ColorServerToClient_ClientRpc(Color.yellow);
        }else{
            ChangeColorFunc_Green_ServerRpc(Color.yellow);
        }
            
        
        if (Monster_Ghost != null){
            
            if (Monster_Ghost.name != Monster_.name){
                Destroy_Ghost();
                CloneMonsterGhost_func(Monster_); 
            }

            Monster_Ghost.SetActive(true);
            
            return;
        }

        ////// 
        // if (Monster_Ghost.name != Monster_.name)
        // {
        //     Destroy_Ghost();
        // }
        CloneMonsterGhost_func(Monster_); 
    }

    [ServerRpc]
    private void ChangeColorFunc_Green_ServerRpc(Color _c){
        Debug.Log("Launch on Server");
        Node_renderer.material.color = _c;

        // if(!IsHost){
        //     ColorServerToClient_ClientRpc(_c);
        // }
    }

    [ClientRpc]
    private void ColorServerToClient_ClientRpc(Color _c)
    {
        Debug.Log("Launch on ClientRpc");
        Node_renderer.material.color = Color.yellow;
    }




    void OnMouseDown()
    {
        if (Monster_ == null){
            return;
        }

        if (mouseClick_bool == true) // cannot changed the Clicked clone object
        {
            return;
        }

        mouseClick_bool = true;
        
        Node_renderer.material.color = Color.green;
        Debug.Log("Is mouse click");
        Playerinfo.Instance.Clean_SeletcCard();
        CloneMonster(Monster_);
    }

    private void OnMouseExit() {
        Debug.Log("In Mouse Exit");
        if (Monster_ == null){
            return;
        }
        
        if (Monster_Ghost == null){
            return;
        }else{
            Monster_Ghost.SetActive(false);
        }

        if (mouseClick_bool){
            return;
        }

        Node_renderer.material.color = Color.white;
        
    }

    
    private void CloneMonsterGhost_func(GameObject prefab){
        GameObject go;
        go = GameObject.Instantiate(prefab, this.gameObject.transform.position, Quaternion.identity , this.gameObject.transform);
        
        go.GetComponent<NetworkObject>().Spawn(true);
        // go.transform.position = new Vector3(0 ,0 ,0);
        go.name += "_Ghost";
        go.transform.localScale = new Vector3(10 , 10 , 10);
        Monster_Ghost = go;

        // NEED ADD model 
    }

    private void Destroy_Ghost(){
        //string ghostName = Monster_Ghost.name;
        // GameObject Object = this.gameObject;
        // for (int i = 0; i < Object.transform.childCount; i++)
        // {
        //     Debug.Log( Object.transform.GetChild(i).gameObject.name );
        //     if (Object.transform.GetChild(i).gameObject.name == Monster_Ghost.name + "(Clone)_Ghost" )
        //     {
                
        //         Destroy(this.gameObject.transform.GetChild(i).gameObject);
        //     }
        //     // string M_name = Monster_Ghost.name;
        // }


        // GameObject go = this.gameObject.transform.GetChild(0).gameObject;
        // if(go.name.contains("_Ghost"))  tag?

        // if (this.gameObject.transform.childCount == 1)
        // {
        //     return;
        // }
        // Debug.Log(this.gameObject.transform.childCount);
        Destroy(this.gameObject.transform.GetChild(1).gameObject);
    }

    public void CloneMonster(GameObject go){
        GameObject Monsterclone = Instantiate(go, transform.position, transform.rotation , this.gameObject.transform);
        
        Monsterclone.GetComponent<NetworkObject>().Spawn(true);
        Monsterclone.transform.localScale = new Vector3(10 , 10 , 10);

        Destroy_Ghost();
    }

    private void OnSomeValueChanged(int previous, int current)
    {
        Debug.Log($"Detected NetworkVariable Change: Previous: {previous} | Current: {current}");
    }

    [ServerRpc()]
    private void TestServerRpc(){
        Debug.Log("-- TestServerRpc "+ OwnerClientId);
    }
    // private void ChangeColorClientRpc(Renderer randerer, Color _color){
    //     // randerer.material.SetColor(_color);
    // }

    [ClientRpc]
    private void TestingClientRpc( )
    {
        Debug.Log("-- is TestingClientRpc");
    }
}
