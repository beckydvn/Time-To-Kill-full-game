using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameObject player;
    private Vector2 pixelPos;
    private Vector3 newPos;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");        
    }
    // Update is called once per frame
    void Update()
    {
        Debug.Log(transform.position.z);
        pixelPos = new Vector2(player.transform.position.x, player.transform.position.y);
        pixelPos = PixelPerfectClamp(pixelPos, 18);
        newPos.x = pixelPos.x;
        newPos.y = pixelPos.y;
        newPos.z = -10;
        transform.position = newPos;
    }
    //taken from https://www.youtube.com/watch?v=OBulUgXe7rA
    private Vector2 PixelPerfectClamp(Vector3 moveVector, float pixelsPerUnit)
    {
        Vector2 vectorInPixels = new Vector2(
            Mathf.RoundToInt(moveVector.x * pixelsPerUnit),
            Mathf.RoundToInt(moveVector.y * pixelsPerUnit));
        return vectorInPixels / pixelsPerUnit;
    }

}
