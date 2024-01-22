using System.Collections.Generic;

[System.Serializable]
public class GameData
{
    public Globals globals;
    public SceneData scene;
    public SerializableDictionary<string, SceneData> sceneList;
    public GameData() 
    {
        globals = new Globals();
        scene = new SceneData();
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
        //NEED TESTING
        //dataCopy.scene = dataOriginal.scene;
        //dataCopy.sceneList = dataOriginal.sceneList;
        return dataCopy;
    }

    [System.Serializable]
    public class Globals
    {
        public long lastUpdated;
        public int playerHealth;

        [Mechanics2D.SceneName]
        public string lastSceneName;

        public List<ItemData> itemList;
        public Globals()
        {
            //First scene to load after the new game starts
            lastSceneName = "L00";

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


    public int GetPercentageComplete() 
    {
        return globals.playerHealth;
    }
}

