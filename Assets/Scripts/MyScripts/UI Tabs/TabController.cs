using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabController : MonoBehaviour
{
    private List<TabButton> TabButtons;
    private List<GameObject> TabFields;

    [SerializeField] private Color SelectedColor;
    [SerializeField] private Color HoveredColor;
    [SerializeField] private Color IdleColor;

    private TabButton selectedTab;

    public void RegisterTab(TabButton button)
    {

        if (TabButtons == null)
        {
            TabButtons = new List<TabButton>();
        }

        if (TabFields == null)
        {
            TabFields = new List<GameObject>();
        }
        TabButtons.Add(button);
        TabFields.Add(button.MyTabField);
        button.backgroundImage.color = IdleColor;
      
    }

    public void OnTabSelected(TabButton button)
    {
        selectedTab = button;
        selectedTab.backgroundImage.color = SelectedColor;
        ResetTabs();
        ShowTabFields();

    }

    public void OnTabHovered(TabButton button)
    {
        ResetTabs();
        if (selectedTab != button)
        {
          
            button.backgroundImage.color = HoveredColor;

        }
    }

    public void OnTabExit(TabButton button)
    {
        ResetTabs();
        if (selectedTab != button)
        {
             
            button.backgroundImage.color = IdleColor;
        }
    }

    void ResetTabs()
    {
        for (int i = 0; i < TabButtons.Count; i++)
        {
            if (selectedTab != null && selectedTab == TabButtons[i])
            {
                continue;
            }
            else
            TabButtons[i].backgroundImage.color = IdleColor;
            
        }  
    }

    void ShowTabFields()
    {
        for (int i = 0; i < TabButtons.Count; i++)
        {
            if (selectedTab != null && selectedTab == TabButtons[i])
            {
                TabFields[i].SetActive(true);
               
               
            }
            else
            TabFields[i].SetActive(false);
                

        }
    }
}
