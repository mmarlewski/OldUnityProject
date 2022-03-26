using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : Singleton<CameraManager>
{
    public new Camera camera;

    public int currCameraLevel;

    public float mapMinY;
    public float mapMaxY;
    public float mapMinX;
    public float mapMaxX;
    public float halfHeight;
    public float halfWidth;

    public bool isCameraSet;
    public float screenSpeed;
    public int screenHeight;
    public int screenWidth;

    void Start()
    {
        halfHeight = 0;
        halfWidth = 0;
        mapMinY = 0;
        mapMaxY = 0;
        mapMinX = 0;
        mapMaxX = 0;

        isCameraSet = false;
        screenSpeed = 7.0f;
        screenHeight = Screen.height;
        screenWidth = Screen.width;

    }

    public void MoveCamera()
    {
        if (Input.GetKey(KeyCode.RightArrow) || Input.mousePosition.x >= screenWidth)
        {
            camera.transform.Translate(new Vector3(screenSpeed * Time.deltaTime, 0, 0));
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.mousePosition.x <= 0)
        {
            camera.transform.Translate(new Vector3(-screenSpeed * Time.deltaTime, 0, 0));
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.mousePosition.y <= 0)
        {
            camera.transform.Translate(new Vector3(0, -screenSpeed * Time.deltaTime, 0));
        }
        if (Input.GetKey(KeyCode.UpArrow) || Input.mousePosition.y >= screenHeight)
        {
            camera.transform.Translate(new Vector3(0, screenSpeed * Time.deltaTime, 0));
        }
    }

    public void AdjustCamera()
    {
        screenHeight = Screen.height;
        screenWidth = Screen.width;

        halfHeight = camera.orthographicSize;
        halfWidth = halfHeight * camera.aspect;
        mapMinY = halfHeight;
        mapMaxY = MapManager.I.mapHeight - halfHeight;
        mapMinX = halfWidth;
        mapMaxX = MapManager.I.mapWidth - halfWidth;

        Vector3 cameraPos = camera.transform.position;

        if (mapMinY > mapMaxY)
        {
            cameraPos.y = MapManager.I.mapHeight / -2.0f + halfHeight;
        }
        else
        {
            cameraPos.y = Mathf.Clamp(cameraPos.y, mapMinY, mapMaxY);
        }
        if (mapMinX > mapMaxX)
        {
            cameraPos.x = MapManager.I.mapWidth / 2.0f;
        }
        else
        {
            cameraPos.x = Mathf.Clamp(cameraPos.x, mapMinX, mapMaxX);
        }

        camera.transform.position = cameraPos;
    }
}
