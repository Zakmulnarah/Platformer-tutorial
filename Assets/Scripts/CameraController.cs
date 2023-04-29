using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //follow player
    public Transform target;
    //make background stay, second move slower
    public Transform farBackground, middleBackground;

    public float minHeight, maxHeight;

  // Turned to lastPos
  //private float lastXPos, lastYPos;
    private Vector2 lastPos;
    // Start is called before the first frame update
    void Start()
    {
        lastPos = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
       /* Code for clamping camera to not go higher,lower than set, before putting it all into one code
        * transform.position= new Vector3(target.position.x, target.position.y, transform.position.z);

        float clampedY = Mathf.Clamp(transform.position.y, minHeight, maxHeight);
        transform.position=new Vector3(transform.position.x,clampedY, transform.position.z);
       */
        transform.position = new Vector3(target.position.x,Mathf.Clamp(target.position.y, minHeight, maxHeight),transform.position.z);
        
        /*for slowly moving the background
        float amountToMoveX = transform.position.x - lastXPos;
        float amountToMoveY = transform.position.y - lastYPos;
        */
        Vector2 amountToMove = new Vector2(transform.position.x - lastPos.x, transform.position.y - lastPos.y);
      
        farBackground.position += new Vector3(amountToMove.x,amountToMove.y,0f);
        middleBackground.position += new Vector3(amountToMove.x, amountToMove.y, 0f) * .5f;
        lastPos=transform.position;

    }
}
