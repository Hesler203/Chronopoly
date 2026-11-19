using UnityEngine;

/// <summary>
/// Canvas: Manager: Singleton that instantiates the Front End canvas
/// </summary>
public class CanvasManager : MonoBehaviour
{
    public static CanvasManager Instance;
    public HUD HUD { get; private set; }
    public Pause Pause { get; private set; }

    [Header("FrontEnd")]
    private GameObject CanvasSplash;
    private GameObject CanvasFE;
    private GameObject CanvasOptions;
    private GameObject CanvasSetup;
    private GameObject CanvasStats;
    private GameObject CanvasRollHistory;
    private GameObject CanvasHelp;

    [Header("InGame")]
    private GameObject CanvasHUD;
    private GameObject CanvasPause;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.Log("Instance already exists, destroying CanvasManager!");
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void ShowCanvasSplash()
    {
        if (CanvasSplash != null)
        {
            CanvasSplash.SetActive(true);
            return;
        }
        CanvasSplash = Instantiate(Resources.Load<GameObject>("Canvas/" + "CanvasSplash"));
    }

    public void HideCanvasSplash()
    {
        CanvasSplash.SetActive(false);
    }

    public void ShowCanvasFE()
    {
        if (CanvasFE != null)
        {
            CanvasFE.SetActive(true);
            return;
        }
        CanvasFE = Instantiate(Resources.Load<GameObject>("Canvas/" + "CanvasFE"));
    }

    public void HideCanvasFe()
    {
        CanvasFE.SetActive(false);
    }

    public void ShowCanvasOptions()
    {
        if (CanvasOptions != null)
        {
            CanvasOptions.SetActive(true);
            return;
        }
        CanvasOptions = Instantiate(Resources.Load<GameObject>("Canvas/" + "CanvasOptions"));
    }

    public void HideCanvasOptions()
    {
        CanvasOptions.SetActive(false);
    }

    public void ShowCanvasSetup()
    {
        if (CanvasSetup != null)
        {
            CanvasSetup.SetActive(true);
            return;
        }
        CanvasSetup = Instantiate(Resources.Load<GameObject>("Canvas/" + "CanvasSetup"));
        Setup setup = CanvasSetup.GetComponent<Setup>();
        setup.InitCanvas();
    }

    public void HideCanvasSetup()
    {
        CanvasSetup.SetActive(false);
    }

    public void ShowCanvasHUD()
    {
        if (CanvasHUD != null)
        {
            CanvasHUD.SetActive(true);
            return;
        }
        CanvasHUD = Instantiate(Resources.Load<GameObject>("Canvas/" + "CanvasHUD"));
        HUD = CanvasHUD.GetComponent<HUD>();
        HUD.InitCanvas();
    }

    public void HideCanvasHUD()
    {
        CanvasHUD.SetActive(false);
    }

    public void ShowCanvasPause()
    {
        if (CanvasPause != null)
        {
            CanvasPause.SetActive(true);
            return;
        }
        CanvasPause = Instantiate(Resources.Load<GameObject>("Canvas/" + "CanvasPause"));
        Pause = CanvasPause.GetComponent<Pause>();
    }

    public void HideCanvasPause()
    {
        CanvasPause.SetActive(false);
    }

    public void ShowCanvasStats()
    {
        if (CanvasStats != null)
        {
            CanvasStats.SetActive(true);
            return;
        }
        CanvasStats = Instantiate(Resources.Load<GameObject>("Canvas/" + "CanvasStats"));
        Stats stats = CanvasStats.GetComponent<Stats>();
        stats.PopulateStatLabels();
    }

    public void HideCanvasStats()
    {
        CanvasStats.SetActive(false);
    }

    public void ShowCanvasRollHistory()
    {
        if (CanvasRollHistory != null)
        {
            CanvasRollHistory.SetActive(true);
            return;
        }
        CanvasRollHistory = Instantiate(Resources.Load<GameObject>("Canvas/" + "CanvasRollHistory"));
        RollHistory rollHistory = CanvasRollHistory.GetComponent<RollHistory>();
        rollHistory.PopulateRollsGraph();
    }

    public void HideCanvasRollHistory()
    {
        CanvasRollHistory.SetActive(false);
    }

    public void ShowCanvasHelp()
    {
        if (CanvasHelp != null)
        {
            CanvasHelp.SetActive(true);
            return;
        }
        CanvasHelp = Instantiate(Resources.Load<GameObject>("Canvas/" + "CanvasHelp"));
    }

    public void HideCanvasHelp()
    {
        CanvasHelp.SetActive(false);
    }

    public void ShowPurchaseWidget(soSpot _soSpot)
    {
        w_SpotProp widget = Instantiate(Resources.Load<GameObject>("Widgets/w_SpotProp"), HUD.WidgetSpawnPoint).GetComponent<w_SpotProp>();
        widget.InitWidget(_soSpot);
    }
}