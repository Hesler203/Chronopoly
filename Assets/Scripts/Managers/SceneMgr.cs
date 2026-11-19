using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Manager: Singleton to manage scene transitions
/// </summary>
///
public enum eScene { FrontEnd, InGame }
public class SceneMgr : MonoBehaviour
{
	public static SceneMgr Instance;

	void Awake()
	{
		if (Instance != null && Instance != this)
		{
			Debug.Log("Instance already exists, destroying SceneMgr!");
			Destroy(this.gameObject);
		}
		else
		{
			Instance = this;
			//TODO awaiting parent -- DontDestroyOnLoad(this.gameObject);
		}
	}

	void OnEnable()
	{
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	void OnDisable()
	{
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}

	private void OnSceneLoaded(Scene _scene, LoadSceneMode _mode)
	{
		Debug.Log("<color=yellow><b>[Scene] Scene Loaded: " + _scene.name + "</b></color=yellow>");

		switch ((eScene)_scene.buildIndex)
		{
			case eScene.FrontEnd:
				PlayerManager.Instance.CreatePlayers();
				CanvasManager.Instance.ShowCanvasSplash();
				break;
			case eScene.InGame:
				PlayerManager.Instance.CreatePieces();
				CanvasManager.Instance.ShowCanvasHUD();
				CameraManager.SetCurrentCamera(eCameraPosition.main);
				break;
		}
	}

	public void LoadScene(eScene _scene)
	{
		Debug.Log("<color=yellow><b>[Scene] Load Scene: " + _scene + "</b></color>");
		SceneManager.LoadScene((int)_scene);
	}
}