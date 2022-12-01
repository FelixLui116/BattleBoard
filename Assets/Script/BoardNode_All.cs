using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardNode_All : MonoBehaviour
{
    // Start is called before the first frame update
    private int row = 4,  column = 4;  //0 1 2 3

    void Start()
    {
        SetNodePosition();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetNodePosition(){
        /*
        var nodeCount = this.transform.childCount;
        for (int i = 0; i < nodeCount; i++)
        {
            GameObject node = this.transform.GetChild(i).gameObject;
            BoardNode BN = node.GetComponent<BoardNode>();
            /*
            //row >   column v 

            3,3     3.3
            .
            .
            .
            0,0     0,3
            
        }
        */

        int countChil = 0;

        for (int j = 0; j < row; j++)
        {
            for (int k = 0; k < column; k++)
            {
                var nodeCount = this.transform.GetChild(countChil);
                BoardNode BN = nodeCount.gameObject.GetComponent<BoardNode>();
                
                BN.SetNodePosition(j,k);

                countChil++;
            }
        }
    }
}
