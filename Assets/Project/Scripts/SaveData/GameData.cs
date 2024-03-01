using System.Collections.Generic;

[System.Serializable]
public class GameData
{
    public Globals globals;
    public SceneData scene;
    public Wrap<string> dialogueData;
    public SerializableDictionary<string, SceneData> sceneList;
    //public SerializableDictionary<string, Mechanics2D.Data> persistData;
    public GameData() 
    {
        globals = new Globals();
        scene = new SceneData();
        dialogueData = new Wrap<string>();
        sceneList = new SerializableDictionary<string, SceneData>
        {
            { scene.name, scene }
        };
        //persistData = new SerializableDictionary<string, Mechanics2D.Data>();
    }

    public SceneData GetSceneData(string sceneName)
    {
        bool haveSceneData = sceneList.ContainsKey(sceneName);
        return haveSceneData ? sceneList[sceneName] : null;
    }

    public void SetSceneData(SceneData info)
    {
        sceneList[info.name] = info;
        globals.lastSceneName = info.name;
    }
 
    public void CreateNewSceneData()
    {
        scene = new SceneData();
    }

    public GameData Copy()
    {
        GameData dataOriginal = this;
        GameData dataCopy = new GameData();
        dataCopy.globals.lastUpdated = dataOriginal.globals.lastUpdated;
        dataCopy.globals.playerHealth = dataOriginal.globals.playerHealth;
        dataCopy.globals.lastSceneName = dataOriginal.globals.lastSceneName;
        dataCopy.globals.destinationTag = dataOriginal.globals.destinationTag;

        //NEED TESTING
        //dataCopy.scene = dataOriginal.scene;
        //dataCopy.sceneList = dataOriginal.sceneList;
        //dataCopy.persistData = dataOriginal.persistData;
        //dataCopy.globals.stats = dataOriginal.globals.stats;
        dataCopy.dialogueData.value = dataOriginal.dialogueData.value; //dataCopy.dialogueData.jsonState = dataOriginal.dialogueData.jsonState;
        return dataCopy;
    }

    [System.Serializable]
    public class Globals
    {
        public long lastUpdated;
        public int playerHealth;

        [Mechanics2D.SceneName]
        public string lastSceneName;
        public Mechanics2D.SceneTransitionDestination.DestinationTag destinationTag;

        public Wrap<int> money;

        public Stats stats;

        public List<ItemData> itemList;
        public Globals()
        {
            //First scene to load after the new game starts
            lastSceneName = Constants.NewGameScene;
            destinationTag = default;

            playerHealth = 100;

            money = new Wrap<int>(77);
            
            SerializableDictionary<StatType, int> statsDictionary = new SerializableDictionary<StatType, int>();
            //statsDictionary[StatType.Agility] = 1;
            //statsDictionary[StatType.Strength] = 2;
            statsDictionary[StatType.Intelligence] = 3;

            statsDictionary[StatType.Appearance] = 1;
            statsDictionary[StatType.Manipulation] = 1;
            statsDictionary[StatType.Perception] = 1;
            statsDictionary[StatType.Charisma] = 1;

            stats = new Stats(10, statsDictionary);

            itemList = new List<ItemData>();
        }
    }

    [System.Serializable]
    public class SceneData
    {
        public string name;
        public SceneData()
        {
            name = "SCENE_NAME";
        }
    }

    [System.Serializable]
    public class Stats
    {
        public int points;
        public SerializableDictionary<StatType, int> dict;
        public Stats(int points, SerializableDictionary<StatType, int> dictionary)
        {
            this.points = points;
            this.dict = dictionary;
        }
    }

    public int GetPercentageComplete() 
    {
        return globals.playerHealth;
    }
}

[System.Serializable]
public class Wrap<T>
{
    public T value;
    public Wrap()
    {
        this.value = default;
    }
    public Wrap(T value)
    {
        this.value = value;
    }
}