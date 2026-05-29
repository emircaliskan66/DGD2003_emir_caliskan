using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class SaveData
{
    public float playerPosX;
    public float playerPosY;
    public float playerPosZ;

    public float playerRotX;
    public float playerRotY;
    public float playerRotZ;

    public float timerTime;
    public int itemsFound;

    public List<string> collectedItems = new List<string>();
}

public class SaveManager : MonoBehaviour
{
    private bool skipSaveOnQuit = false;
    [SerializeField] private Transform playerTransform;

    private string savePath;

    private void Awake()
    {
        savePath = Path.Combine(Application.persistentDataPath, "saveData.json");
    }

    private void Start()
    {
        StartCoroutine(LoadAfterSceneReady());
    }

    private IEnumerator LoadAfterSceneReady()
    {
        yield return null;

        LoadGame();
    }

    private void OnApplicationQuit()
    {
        if (skipSaveOnQuit) return;

        SaveGame();
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus && !skipSaveOnQuit)
        {
            SaveGame();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
        {
            SaveGame();
        }

        if (Input.GetKeyDown(KeyCode.F9))
        {
            LoadGame();
        }

        if (Input.GetKeyDown(KeyCode.F12))
        {
            DeleteSave();
        }
    }

    public void SaveGame()
    {
        if (skipSaveOnQuit)
        {
            Debug.Log("Save atlandı çünkü save silindi.");
            return;
        }

        if (playerTransform == null)
        {
            Debug.LogWarning("Player Transform atanmadı!");
            return;
        }

        SaveData data = new SaveData();

        data.playerPosX = playerTransform.position.x;
        data.playerPosY = playerTransform.position.y;
        data.playerPosZ = playerTransform.position.z;

        data.playerRotX = playerTransform.eulerAngles.x;
        data.playerRotY = playerTransform.eulerAngles.y;
        data.playerRotZ = playerTransform.eulerAngles.z;

        if (TimerManager.Instance != null)
        {
            data.timerTime = TimerManager.Instance.GetCurrentTime();
        }

        if (ScavengerManager.Instance != null)
        {
            data.itemsFound = ScavengerManager.Instance.GetItemsFound();
            data.collectedItems = ScavengerManager.Instance.GetCollectedItemNames();
        }

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath, json);

        Debug.Log("Game saved: " + savePath);
    }

    public void LoadGame()
    {
        if (!File.Exists(savePath))
        {
            Debug.Log("Save file yok. Yeni oyun başladı.");
            return;
        }

        if (playerTransform == null)
        {
            Debug.LogWarning("Player Transform atanmadı!");
            return;
        }

        string json = File.ReadAllText(savePath);
        SaveData data = JsonUtility.FromJson<SaveData>(json);

        CharacterController controller = playerTransform.GetComponent<CharacterController>();

        if (controller != null)
        {
            controller.enabled = false;
        }

        Vector3 loadedPosition = new Vector3(data.playerPosX, data.playerPosY, data.playerPosZ);
        playerTransform.position = loadedPosition + Vector3.up * 0.3f;

        playerTransform.eulerAngles = new Vector3(data.playerRotX, data.playerRotY, data.playerRotZ);

        if (controller != null)
        {
            controller.enabled = true;
        }

        FPSController fpsController = playerTransform.GetComponent<FPSController>();

        if (fpsController != null)
        {
            fpsController.ResetVelocity();
        }

        if (TimerManager.Instance != null)
        {
            TimerManager.Instance.LoadTime(data.timerTime);
        }

        if (ScavengerManager.Instance != null)
        {
            ScavengerManager.Instance.LoadCollectedItems(data.collectedItems);
        }

        Debug.Log("Game loaded.");
    }

    public void DeleteSave()
    {
        if (File.Exists(savePath))
        {
            File.Delete(savePath);
            Debug.Log("Save silindi.");
        }
        else
        {
            Debug.Log("Silinecek save bulunamadı.");
        }

        skipSaveOnQuit = true;
    }
}