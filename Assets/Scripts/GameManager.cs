using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    public GameObject canvas;
    public Font fontArial;

    public Entity[] entities;
    public Entity playerEntity;

    public GameMode gameMode;
    public bool isPlayerMoving;
    public bool hasPlayerMoved;

    public GameObject uiExit;
    public GameObject uiCurrLevel;
    public GameObject uiLevel0;
    public GameObject uiLevel1;
    public GameObject uiLevel2;
    public GameObject uiEntity;
    public GameObject uiAction;

    void Start()
    {
        gameMode = GameMode.none;
        isPlayerMoving = false;
        hasPlayerMoved = false;

        {
            uiExit = new GameObject();
            uiExit.transform.SetParent(canvas.transform);
            uiExit.AddComponent<RectTransform>();
            uiExit.GetComponent<RectTransform>().position = new Vector3(200, 800, 0);
            uiExit.GetComponent<RectTransform>().sizeDelta = new Vector2(500, 50);
            uiExit.AddComponent<Text>();
            uiExit.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
            uiExit.GetComponent<Text>().font = fontArial;
            uiExit.GetComponent<Text>().fontSize = 30;
            uiExit.GetComponent<Text>().color = Color.yellow;
            uiExit.GetComponent<Text>().text = "exit game";
            uiExit.AddComponent<Button>();
            uiExit.GetComponent<Button>().onClick.AddListener(() => ExitGame());

            uiCurrLevel = new GameObject();
            uiCurrLevel.transform.SetParent(canvas.transform);
            uiCurrLevel.AddComponent<RectTransform>();
            uiCurrLevel.GetComponent<RectTransform>().position = new Vector3(1400, 800, 0);
            uiCurrLevel.GetComponent<RectTransform>().sizeDelta = new Vector2(200, 50);
            uiCurrLevel.AddComponent<Text>();
            uiCurrLevel.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
            uiCurrLevel.GetComponent<Text>().font = fontArial;
            uiCurrLevel.GetComponent<Text>().fontSize = 30;
            uiCurrLevel.GetComponent<Text>().color = Color.green;
            uiCurrLevel.GetComponent<Text>().text = "";

            uiLevel0 = new GameObject();
            uiLevel0.transform.SetParent(canvas.transform);
            uiLevel0.AddComponent<RectTransform>();
            uiLevel0.GetComponent<RectTransform>().position = new Vector3(1600, 800, 0);
            uiLevel0.GetComponent<RectTransform>().sizeDelta = new Vector2(50, 50);
            uiLevel0.AddComponent<Text>();
            uiLevel0.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
            uiLevel0.GetComponent<Text>().font = fontArial;
            uiLevel0.GetComponent<Text>().fontSize = 30;
            uiLevel0.GetComponent<Text>().color = Color.green;
            uiLevel0.GetComponent<Text>().text = "0";
            uiLevel0.AddComponent<Button>();
            uiLevel0.GetComponent<Button>().onClick.AddListener(() => changeLevel(0));

            uiLevel1 = new GameObject();
            uiLevel1.transform.SetParent(canvas.transform);
            uiLevel1.AddComponent<RectTransform>();
            uiLevel1.GetComponent<RectTransform>().position = new Vector3(1700, 800, 0);
            uiLevel1.GetComponent<RectTransform>().sizeDelta = new Vector2(50, 50);
            uiLevel1.AddComponent<Text>();
            uiLevel1.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
            uiLevel1.GetComponent<Text>().font = fontArial;
            uiLevel1.GetComponent<Text>().fontSize = 30;
            uiLevel1.GetComponent<Text>().color = Color.green;
            uiLevel1.GetComponent<Text>().text = "1";
            uiLevel1.AddComponent<Button>();
            uiLevel1.GetComponent<Button>().onClick.AddListener(() => changeLevel(1));

            uiLevel2 = new GameObject();
            uiLevel2.transform.SetParent(canvas.transform);
            uiLevel2.AddComponent<RectTransform>();
            uiLevel2.GetComponent<RectTransform>().position = new Vector3(1800, 800, 0);
            uiLevel2.GetComponent<RectTransform>().sizeDelta = new Vector2(50, 50);
            uiLevel2.AddComponent<Text>();
            uiLevel2.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
            uiLevel2.GetComponent<Text>().font = fontArial;
            uiLevel2.GetComponent<Text>().fontSize = 30;
            uiLevel2.GetComponent<Text>().color = Color.green;
            uiLevel2.GetComponent<Text>().text = "2";
            uiLevel2.AddComponent<Button>();
            uiLevel2.GetComponent<Button>().onClick.AddListener(() => changeLevel(2));

            uiEntity = new GameObject();
            uiEntity.transform.SetParent(canvas.transform);
            uiEntity.AddComponent<RectTransform>();
            uiEntity.GetComponent<RectTransform>().position = new Vector3(200, 700, 0);
            uiEntity.GetComponent<RectTransform>().sizeDelta = new Vector2(500, 50);
            uiEntity.AddComponent<Text>();
            uiEntity.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
            uiEntity.GetComponent<Text>().font = fontArial;
            uiEntity.GetComponent<Text>().fontSize = 30;
            uiEntity.GetComponent<Text>().color = Color.blue;
            uiEntity.GetComponent<Text>().text = "";

            uiAction = new GameObject();
            uiAction.transform.SetParent(canvas.transform);
            uiAction.AddComponent<RectTransform>();
            uiAction.GetComponent<RectTransform>().position = new Vector3(200, 600, 0);
            uiAction.GetComponent<RectTransform>().sizeDelta = new Vector2(500, 50);
            uiAction.AddComponent<Text>();
            uiAction.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
            uiAction.GetComponent<Text>().font = fontArial;
            uiAction.GetComponent<Text>().fontSize = 30;
            uiAction.GetComponent<Text>().color = Color.blue;
            uiAction.GetComponent<Text>().text = "";
        }
    }

    void Update()
    {
        if(!MapManager.I.isMapLoaded)
        {
            MapManager.I.LoadMap();
            changeLevel(MapManager.I.startPlayerLevel);
            MapManager.I.isMapLoaded = true;
        }

        if(!CameraManager.I.isCameraSet)
        {
            CameraManager.I.camera.transform.position=
                new Vector3(MapManager.I.mapWidth / 2.0f, MapManager.I.mapHeight / -2.0f, -10);
            CameraManager.I.currCameraLevel = MapManager.I.startPlayerLevel;
            CameraManager.I.isCameraSet = true;
        }

        GameLoop();
    }

    public void GameLoop()
    {
        InputManager.I.UpdateInput();

        CameraManager.I.MoveCamera();
        CameraManager.I.AdjustCamera();

        MapManager.I.RefreshEntities();
        TilemapManager.I.ChangeSelection();
    }

    public void changeGameMode(GameMode newGameMode)
    {
        gameMode = newGameMode;
    }

    public void changeLevel(int newLevel)
    {
        CameraManager.I.currCameraLevel = newLevel;
        uiCurrLevel.GetComponent<Text>().text = "level: " + newLevel;
        TilemapManager.I.RefreshTilemaps();
    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
