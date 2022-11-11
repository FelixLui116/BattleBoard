using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class BoardNode : NetworkBehaviour // NetworkBehaviour MonoBehaviour
{
    private Renderer Node_renderer;

    [SerializeField] private GameObject master_;

    public GameObject TestClone;
    [SerializeField] private GameObject Node;

    private GameObject master_Ghost;

    private bool mouseClick_bool = false;

    public static Playerinfo Playerinfo_Instance;
    private void Awake() {
        Node_renderer = Node.gameObject.GetComponent<Renderer>();
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
            master_ = Playerinfo.Instance.Get_player_SelectCard();
        }else{
            return;
        }
        // if (master_ != null){
        //     return;
        // }

        if (mouseClick_bool){
            return;
        }
        
        Node_renderer.material.color = Color.yellow;
        
        if (master_Ghost != null){
            
            if (master_Ghost.name != master_.name){
                Destroy_Ghost();
                CloneMasterGhost_func(master_); 
            }

            master_Ghost.SetActive(true);
            
            return;
        }

        ////// 
        // if (master_Ghost.name != master_.name)
        // {
        //     Destroy_Ghost();
        // }
        CloneMasterGhost_func(master_); 
    }

    void OnMouseDown()
    {
        if (master_ == null){
            return;
        }

        if (mouseClick_bool == true) // cannot changed the Clicked clone object
        {
            return;
        }

        mouseClick_bool = true;
        
        Node_renderer.material.color = Color.green;
        Debug.Log("Is mouse click");
        CloneMaster(master_);
    }

    private void OnMouseExit() {
        Debug.Log("In Mouse Exit");
        if (master_ == null){
            return;
        }
        
        if (master_Ghost == null){
            return;
        }else{
            master_Ghost.SetActive(false);
        }

        if (mouseClick_bool){
            return;
        }

        Node_renderer.material.color = Color.white;
        
    }

    
    private void CloneMasterGhost_func(GameObject prefab){
        GameObject go;
        go = GameObject.Instantiate(prefab, this.gameObject.transform.position, Quaternion.identity , this.gameObject.transform);
        // go.transform.position = new Vector3(0 ,0 ,0);
        go.name += "_Ghost";
        go.transform.localScale = new Vector3(10 , 10 , 10);
        master_Ghost = go;

        // NEED ADD model 
    }

    private void Destroy_Ghost(){
        //string ghostName = master_Ghost.name;
        // GameObject Object = this.gameObject;
        // for (int i = 0; i < Object.transform.childCount; i++)
        // {
        //     Debug.Log( Object.transform.GetChild(i).gameObject.name );
        //     if (Object.transform.GetChild(i).gameObject.name == master_Ghost.name + "(Clone)_Ghost" )
        //     {
                
        //         Destroy(this.gameObject.transform.GetChild(i).gameObject);
        //     }
        //     // string M_name = master_Ghost.name;
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

    public void CloneMaster(GameObject go){
        GameObject masterclone = Instantiate(go, transform.position, transform.rotation , this.gameObject.transform);
        masterclone.transform.localScale = new Vector3(10 , 10 , 10);

        Destroy_Ghost();
    }
}
