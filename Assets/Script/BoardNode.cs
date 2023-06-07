using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.Networking;

public class BoardNode : NetworkBehaviour // NetworkBehaviour MonoBehaviour
{
    [SerializeField] private Renderer Node_renderer;

    [SerializeField] private GameObject Monster_;

    public GameObject TestClone;
    [SerializeField] private GameObject Node;
    [SerializeField] private Vector2 NodePosition;

    // private GameObject Monster_Ghost;
    [SerializeField] private NetworkObject Monster_Ghost;

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
        // if (IsHost){
        //     ChangeColor_func_ClientRpc(Color.yellow);
        // }else{
        //     ChangeColor_func_ServerRpc(Color.yellow);
        // }
        ChangeColor_func(Color.yellow);
            
        
        if (Monster_Ghost != null){
            
            if (Monster_Ghost.gameObject.name != Monster_.name){
                Destroy_Ghost(Monster_Ghost.gameObject);
                // CloneMonsterGhost_func(Monster_); 
                AddClone_func(Monster_);
            }

            // Monster_Ghost.gameObject.SetActive(true);
            
            return;
        }

        // CloneMonsterGhost_func(Monster_); 
        AddClone_func(Monster_);
    }


    // change Node Color
    private void ChangeColor_func(Color c){
        Debug.Log("In ChangeColor_func A");
        if (IsServer)
        {
            ChangeColor_func_ClientRpc(c);
        }
        else if (IsClient)
        {
            ChangeColor_func_ServerRpc(c);
        }
    }

    [ServerRpc(RequireOwnership = false)] // Allow client edit to server
    private void ChangeColor_func_ServerRpc(Color _c){
        Debug.Log("Launch on Server");
        if (IsServer)
        {
            ChangeColor_func_ClientRpc(_c);
        }
    }
    [ClientRpc]
    private void ChangeColor_func_ClientRpc(Color _c)
    {
        Debug.Log("Launch on ClientRpc");
        Node_renderer.material.color = _c;
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
        if(Playerinfo.Instance.Get_player_SelectCard() ==null){
            return;
        }

        mouseClick_bool = true;

        // Node_renderer.material.color = Color.green;
        ChangeColor_func(Color.green);
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
            Monster_Ghost.gameObject.SetActive(false);
        }

        if (mouseClick_bool){
            return;
        }

        // Node_renderer.material.color = Color.white;
        ChangeColor_func(Color.white);
        
    }


     // Add Clone
    private void AddClone_func(GameObject go){
        NetworkObject n_obj = go.GetComponent<NetworkObject>();
        ulong networkObjectId = n_obj.NetworkObjectId;
        if (IsServer)
        {
            // AddClone_func_ClientRpc(networkObjectId);
            CloneMonsterGhost_func(go);
        }
        else if (IsClient)
        {
            Debug.Log("Launch on AddClone_func");
            AddClone_func_ServerRpc(networkObjectId);
        }
    }

    [ServerRpc(RequireOwnership = false)] // Allow client edit to server
    private void AddClone_func_ServerRpc(ulong networkObjectId)
    {
        // AddClone_func_ClientRpc(networkObjectId);
        if (NetworkManager.Singleton.SpawnManager.SpawnedObjects.TryGetValue(networkObjectId, out NetworkObject clientNetworkObject))
        {
            Debug.Log("==clientNetworkObject.gameObject.name: " + clientNetworkObject.gameObject.name);
            CloneMonsterGhost_func(clientNetworkObject.gameObject);
        }
    }
    // [ClientRpc]
    // private void AddClone_func_ClientRpc(ulong networkObjectId)
    // {
    //     Debug.Log("Launch on ClientRpc");
    //     NetworkObject no = null;
    //     if (NetworkManager.Singleton.SpawnManager.SpawnedObjects.TryGetValue(networkObjectId, out NetworkObject clientNetworkObject))
    //     {
    //         no = clientNetworkObject;
    //     }
    //     GameObject go = no.gameObject;
    //     CloneMonsterGhost_func(go);
    // }

    private void CloneMonsterGhost_func(GameObject prefab){
        // GameObject go;
        // go = GameObject.Instantiate(prefab, this.gameObject.transform.position, Quaternion.identity , this.gameObject.transform);
        
        // go.GetComponent<NetworkObject>().Spawn(true);
        // // go.transform.position = new Vector3(0 ,0 ,0);
        // go.name += "_Ghost";
        // go.transform.localScale = new Vector3(10 , 10 , 10);
        // Monster_Ghost = go;

        GameObject spawnedTestObject_clone = GameObject.Instantiate(prefab, this.gameObject.transform.position, Quaternion.identity , this.gameObject.transform);

        NetworkObject networkObject = spawnedTestObject_clone.GetComponent<NetworkObject>();
        networkObject.transform.localScale = new Vector3(10, 10, 10);
        networkObject.name += "_Ghost";
        // networkObject.Spawn(true);
        networkObject.SpawnWithOwnership(OwnerClientId);
        networkObject.gameObject.SetActive(true);
        
        // networkObject.SpawnAsPlayerObject(NetworkManager.Singleton.LocalClientId);
        ulong networkObjectId = networkObject.NetworkObjectId;
        Debug.Log("Network Object ID: " + networkObjectId);
        // Add the object to the server's ownership
        if (IsClient)
        {
        // NetworkManager.Singleton.SpawnManager.SpawnedObjects.Add(networkObjectId, networkObject);
            networkObject.ChangeOwnership(NetworkManager.Singleton.LocalClientId);
        }


        AddObjectToClient(networkObjectId ); //, Monster_Ghost

        // NEED ADD model 
    }

////////////////////////////////////////////////////////////////////////////
/////////////  add object to client

private void AddObjectToClient(ulong networkObjectId){
        if (IsServer)
        {
            AddObjectClientRpc(networkObjectId);
        }
        else if (IsClient)
        {
            AddObjectServerRpc(networkObjectId);
        }
    }
    [ServerRpc(RequireOwnership = false)] // Allow client to add object to server
    public void AddObjectServerRpc(ulong  networkObjectId)
    {
        // AddObjectClientRpc(tarage_obj);
        // 在客户端中根据网络标识符查找相应的网络对象
        if (NetworkManager.Singleton.SpawnManager.SpawnedObjects.TryGetValue(networkObjectId, out NetworkObject clientNetworkObject))
        {
            Monster_Ghost = clientNetworkObject;
        }
    }
    [ClientRpc]
    public void AddObjectClientRpc(ulong networkObjectId)
    {
        // 在客户端中根据网络标识符查找相应的网络对象
        if (NetworkManager.Singleton.SpawnManager.SpawnedObjects.TryGetValue(networkObjectId, out NetworkObject clientNetworkObject))
        {
            Monster_Ghost = clientNetworkObject;
        }
    }


    private void Destroy_Ghost(GameObject go){
        //////////////////////////////////////////////////
        // Destroy(this.gameObject.transform.GetChild(1).gameObject);
        // 找到要解除生成的 Monster Ghost 的 NetworkObject
        if (go == null){
            return;
        }
        NetworkObject networkObject = go.GetComponent<NetworkObject>();
        if (networkObject != null)
        {
            // 解除生成 NetworkObject
            networkObject.Despawn();
        }
        
    }

    public void CloneMonster(GameObject go){
        // GameObject Monsterclone = Instantiate(go, transform.position, transform.rotation , this.gameObject.transform);
        
        // Monsterclone.GetComponent<NetworkObject>().Spawn(true);
        // Monsterclone.transform.localScale = new Vector3(10 , 10 , 10);
        GameObject spawnedTestObject_clone = GameObject.Instantiate(go, this.gameObject.transform.position, Quaternion.identity , this.gameObject.transform);

        NetworkObject networkObject = spawnedTestObject_clone.GetComponent<NetworkObject>();
        networkObject.transform.localScale = new Vector3(10, 10, 10);
        networkObject.name += "_Ghost";
        networkObject.Spawn(true);
        networkObject.gameObject.SetActive(true);


        Destroy_Ghost(Monster_Ghost.gameObject);
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
