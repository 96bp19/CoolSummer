using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
[RequireComponent(typeof(Image))]
public class TabButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    public TabController tabController;
    [HideInInspector]
    public Image backgroundImage;
    public GameObject MyTabField;

    public void OnPointerClick(PointerEventData eventData)
    {
        tabController.OnTabSelected(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tabController.OnTabHovered(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tabController.OnTabExit(this);
    }

    private void Start()
    {
        backgroundImage = GetComponent<Image>();
        tabController.RegisterTab(this);
    }


}
