using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }
    public int activeSlotIdx = 0;
    public PlayerController playerController;
    public GameObject player;
    public GameObject focalPoint;
    public Camera mainCamera;
    public int slotIdx;
    public bool isSlotEmpty;

    [Header("PLAYER STATE")]
    public string playerName;
    public float maxHealth;
    public float maxMana;
    public float currentHealth;
    public float currentMana;
    public float status;

    [Header("PLAYER POSITION")]
    public int sceneIdx;
    public string sceneName;
    public Vector3 playerPosition;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        activeSlotIdx = -1;
        player = GameObject.FindGameObjectWithTag("Player");
        mainCamera = Camera.main;
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        focalPoint = GameObject.Find("Focal Point");
    }

    [System.Serializable]
    public class SaveData
    {
        public int slotIdx;
        public bool isSlotEmpty;

        [Header("PLAYER STATE")]
        public string playerName;
        public float maxHealth;
        public float maxMana;
        public float currentHealth;
        public float currentMana;
        public float status;

        [Header("PLAYER POSITION")]
        public int sceneIdx;
        public string sceneName;
        public Vector3 playerPosition;

        public SaveData()
        {
            slotIdx = 0;
            isSlotEmpty = true;

            maxHealth = 0;
            maxMana = 0;
            currentHealth = 0;
            currentMana = 0;
            status = 0;

            sceneIdx = 0;
            sceneName = "";
            playerPosition = Vector3.zero;
        }
    }

    public void SaveGameData()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        player = GameObject.FindGameObjectWithTag("Player");
        mainCamera = Camera.main;
        SaveData saveData = new SaveData();
        saveData.playerName = playerController.playerName;
        saveData.maxHealth = playerController.maxHealth;
        saveData.maxMana = playerController.maxMana;
        saveData.currentHealth = playerController.currentHealth;
        saveData.currentMana = playerController.currentMana;
        saveData.status = playerController.intStatus;
        saveData.sceneIdx = SceneManager.GetActiveScene().buildIndex;
        saveData.sceneName = SceneManager.GetActiveScene().name;
        saveData.playerPosition.x = player.transform.position.x;
        saveData.playerPosition.y = player.transform.position.y;
        saveData.playerPosition.z = player.transform.position.z;


        string json = JsonUtility.ToJson(saveData);

        File.WriteAllText(Application.persistentDataPath + $"/{playerName} savefile.json", json);

    }
    public void ReadGameData()
    {
        string path = Application.persistentDataPath + $"/{playerName} savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData saveData = JsonUtility.FromJson<SaveData>(json);
            
            playerName = saveData.playerName;
            maxHealth = saveData.maxHealth;
            maxMana = saveData.maxMana;
            status = saveData.status;
            SceneManager.GetSceneByName(saveData.sceneName);
        }
    }

    public void LoadGameData()
    {
        string path = Application.persistentDataPath + $"/{playerName} savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData saveData = JsonUtility.FromJson<SaveData>(json);

            playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            player = GameObject.FindGameObjectWithTag("Player");
            focalPoint = GameObject.Find("Focal Point");
            mainCamera = Camera.main;
            
            playerController.playerName = saveData.playerName;
            playerController.maxHealth = saveData.maxHealth;
            playerController.maxMana = saveData.maxMana;
            playerController.currentHealth = saveData.currentHealth;
            playerController.currentMana = saveData.currentMana;
            playerController.intStatus= saveData.status;
            SceneManager.LoadScene(saveData.sceneName, LoadSceneMode.Single);
            Vector3 loadPlayerPosition;
            loadPlayerPosition.x = saveData.playerPosition.x;
            loadPlayerPosition.y = saveData.playerPosition.y;
            loadPlayerPosition.z = saveData.playerPosition.z;
            player.transform.position = loadPlayerPosition;
        }
    }
}
