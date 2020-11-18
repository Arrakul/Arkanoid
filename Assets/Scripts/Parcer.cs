using UnityEngine;
using SimpleJSON;

public class Parcer
{
	public class BlockObject 
	{
		public int x;
		public int y;
		public string type;
	}

	public class Size
	{
		public int x;
		public int y;
	}

	public class LevelInfo 
	{
		public int level_id;
	}

	public class ModelJson 
	{
		public Size size;
		public LevelInfo levelInfo;
		public BlockObject[] blocks;
	}

	static Parcer _instance = null;
	public static Parcer Instance()
	{
		return _instance ?? (_instance = new Parcer());
	}

	/*ModelJson _modelJson = null;
	public ModelJson modelJson
	{
		get => _modelJson ?? (_modelJson = LoadModels(GameController.Instance.numberLevel));
		set => _modelJson = value;
	}*/

	public const int CountLevels = 10;

	public ModelJson LoadModels(int level)
	{
		if (level > CountLevels)
		{
			level = 1;
		}
		
		var path = "Levels/Level_" + level;
		var asset = Resources.Load<TextAsset>(path);
		if(asset == null)
			return null;
		var json = asset.text;
		var newJson = JSON.Parse(json);
		var model = new ModelJson
		{
			size = JsonUtility.FromJson<Size>(newJson["size"].ToString()),
			levelInfo = JsonUtility.FromJson<LevelInfo>(newJson["level_info"].ToString()),
		};

		model.blocks = new BlockObject[newJson["board"].Count];
		for(var i=0; i<newJson["board"].Count; i++) 
		{
			model.blocks[i] = JsonUtility.FromJson<BlockObject>(newJson["board"][i].ToString());
		}
			
		return model;
	}
}
