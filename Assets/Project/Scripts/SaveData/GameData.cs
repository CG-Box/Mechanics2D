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

    [System.Serializable]
    public class Globals
    {
        public long lastUpdated;
        public int playerHealth;

        public string lastSceneName;

        //public List<ItemBase> itemList;
        public Globals()
        {
            playerHealth = 100;
            //itemList = new List<ItemBase>();
            //Add items for new game
            //itemList.Add(new ItemBase(ItemType.Ammo,null,true,50));
            lastSceneName = "SampleScene";
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
        return globals.playerHealth/1000;
    }
}

