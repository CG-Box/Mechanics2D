using System.Collections.Generic;

[System.Serializable]
public class GameData
{
    public Globals globals;
    public SceneData scene;
    public DialogueData dialogueData;
    public SerializableDictionary<string, SceneData> sceneList;
    public GameData() 
    {
        globals = new Globals();
        scene = new SceneData();
        dialogueData = new DialogueData();
        sceneList = new SerializableDictionary<string, SceneData>
        {
            { scene.name, scene }
        };
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


        dataCopy.globals.playerCharisma = dataOriginal.globals.playerCharisma;
        dataCopy.globals.playerManipulation = dataOriginal.globals.playerManipulation;
        dataCopy.globals.playerAppearance = dataOriginal.globals.playerAppearance;
        dataCopy.globals.playerPerception = dataOriginal.globals.playerPerception;
        dataCopy.globals.playerIntelligence = dataOriginal.globals.playerIntelligence;


        //NEED TESTING
        //dataCopy.scene = dataOriginal.scene;
        //dataCopy.sceneList = dataOriginal.sceneList;
        dataCopy.dialogueData.jsonState = dataOriginal.dialogueData.jsonState;
        return dataCopy;
    }

    [System.Serializable]
    public class Globals
    {
        public long lastUpdated;
        public int playerHealth;

        public int playerCharisma;
        public int playerManipulation;
        public int playerAppearance;
        public int playerPerception;
        public int playerIntelligence;

        [Mechanics2D.SceneName]
        public string lastSceneName;
        public Mechanics2D.SceneTransitionDestination.DestinationTag destinationTag;

        public List<ItemData> itemList;
        public Globals()
        {
            //First scene to load after the new game starts
            lastSceneName = Constants.NewGameScene;
            destinationTag = default;

            playerHealth = 100;
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
    public class DialogueData
    {
        public string jsonState;
        public DialogueData()
        {
            jsonState = null;
        }
    }


    public int GetPercentageComplete() 
    {
        return globals.playerHealth;
    }
}

