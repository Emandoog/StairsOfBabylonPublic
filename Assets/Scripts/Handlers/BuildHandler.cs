using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class BuildHandler : MonoBehaviour
{
    public static BuildHandler instance;
    public GameObject SelectedBuildGrid;
    [SerializeField] private GameObject BuildMenu;
    [SerializeField] private GameObject ActiveBuildGrid;
    [SerializeField] private Button UpgradeButton;
    [SerializeField] private Button SellButton;
    [SerializeField] private Button AddHeightButton;
    [SerializeField] private Button RemoveHeightButton;
    [SerializeField] private Button RotateButton;
    [SerializeField] private Button Tower1Button;
    [SerializeField] private Button Tower2Button;
    [SerializeField] private Button Tower3Button;
    [SerializeField] private Button Tower4Button;
    [SerializeField] private TMP_Text BPTotalText;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        instance = this;

    }


    private void Start()
    {
        BuildMenu.SetActive(false);

        Tower1Button.onClick.AddListener(delegate { BuildTower(1);});
        Tower2Button.onClick.AddListener(delegate { BuildTower(2);});
        Tower3Button.onClick.AddListener(delegate { BuildTower(3);});
       //Tower4Button.onClick.AddListener(delegate { BuildTower(1);});

        SellButton.onClick.AddListener(SellTower);
        UpgradeButton.onClick.AddListener(UpgradeTower);
        RotateButton.onClick.AddListener(Rotate);
        AddHeightButton.onClick.AddListener(AddHeight);
        RemoveHeightButton.onClick.AddListener(RemoveHeight);
        UpdateUI();
    }
    

    /// <summary>
    /// Sets grid point to be selected
    /// </summary>
    /// <param name="context"></param>
    public void SelectGridPoint(InputAction.CallbackContext context)
    {
        if (SelectedBuildGrid != null)
        {
            if (context.started)
            {
                if (ActiveBuildGrid != null)
                {
                    ActiveBuildGrid.GetComponent<BuildableGround>().ActiveSelection = false;
                    ActiveBuildGrid.GetComponent<BuildableGround>().Highlight();
                    BuildMenu.SetActive(false);
                }
                ActiveBuildGrid = SelectedBuildGrid;

                
                ActiveBuildGrid.GetComponent<BuildableGround>().ActiveSelection = true;
                ActiveBuildGrid.GetComponent<BuildableGround>().Highlight();
                BuildMenu.SetActive(true);
            }
        }
        else if (SelectedBuildGrid == null && EventSystem.current.IsPointerOverGameObject() == false) 
        {
            if (ActiveBuildGrid != null)
            {
                ActiveBuildGrid.GetComponent<BuildableGround>().ActiveSelection = false;
                ActiveBuildGrid.GetComponent<BuildableGround>().Highlight();
            }
           
            BuildMenu.SetActive(false);
        }
        //UpdateUI();

    }

    /// <summary>
    /// Attempt to build tower when on active grid on button press
    /// </summary>
    /// <param name="TowerId"> Tower Id to build</param>
    private void BuildTower(int TowerId = 1)
    {
        if (ActiveBuildGrid.GetComponent<BuildableGround>().empty == true)
        {
            ActiveBuildGrid.GetComponent<BuildableGround>().BuildTower(TowerId);
        }
        else 
        {
            Debug.Log("Cannot Build");
        }
       
        UpdateUI();
    }
    /// <summary>
    /// Attempt to sell tower on active grid on button press
    /// </summary>
    private void SellTower() 
    {
        if (ActiveBuildGrid.GetComponent<BuildableGround>().empty == false)
        {
            ActiveBuildGrid.GetComponent<BuildableGround>().SellTower();
        }
        else
        {
            Debug.Log("Cannot sell");
        }
        UpdateUI();
    }
    /// <summary>
    /// Attempt to add height to tower on button press
    /// </summary>
    private void AddHeight() 
    {
        ActiveBuildGrid.GetComponent<BuildableGround>().AddHeight();
    }
    /// <summary>
    /// Attempt to remove height to tower on button press
    /// </summary>
    private void RemoveHeight()
    {
        ActiveBuildGrid.GetComponent<BuildableGround>().RemoveHeight();
    }
    /// <summary>
    /// Attempt to rotate  tower on button press
    /// </summary>
    private void Rotate() 
    {
        ActiveBuildGrid.GetComponent<BuildableGround>().Rotate();
    }
    /// <summary>
    /// Attempt to upgreade  tower on button press
    /// </summary>
    private void UpgradeTower() 
    {
        ActiveBuildGrid.GetComponent<BuildableGround>().UpgradeTower();
        UpdateUI();
    }

    /// <summary>
    /// Update build UI
    /// </summary>
    public void UpdateUI()
    {
        BPTotalText.text = "Current BP: " + PointHandler.instance.buildPoints;
    }
}
