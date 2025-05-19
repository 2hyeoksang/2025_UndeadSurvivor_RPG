using UnityEngine;
using UnityEngine.UI;

public class SkillUp : MonoBehaviour
{
    RectTransform rect;
    Item[] items;
    Text pointText;
    Button[] itemButtons;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        items = GetComponentsInChildren<Item>(true);
        pointText = GetComponentsInChildren<Text>(true)[16];    // æÍ ∏∏æ‡ø° æ∆¿Ã≈€ √ﬂ∞°µ«∏È ¿Œµ¶Ω∫ πŸ≤„æﬂµ .
        itemButtons = GetComponentsInChildren<Button>(true);
    }

    private void LateUpdate()
    {
        pointText.text = string.Format("Skill Point : {0}", GameManager.instance.skillPoint);    

        if (GameManager.instance.skillPoint <= 0)
        {
            for (int i = 0; i < itemButtons.Length; i++)
            {
                itemButtons[i].interactable = false;
            }
        }

        else
        {
            for (int i = 0; i < itemButtons.Length; i++)
            {
                if (GameManager.instance.level == 0 && 2 <= i && i <= 4)
                    continue;

                itemButtons[i].interactable = true;
            }
        }
    }


    public void Toggle()
    {
        bool isActive = rect.localScale == Vector3.one;

        if (isActive)
            Hide();

        else
            Show();
    }

    public void Show()
    {
        Next();
        rect.localScale = Vector3.one;
    }

    public void Hide()
    {
        rect.localScale = Vector3.zero;
    }

    void Next()
    {
        foreach (Item item in items)
        {
            item.gameObject.SetActive(false);   
        }

        foreach (Item item in items)
        {
            item.gameObject.SetActive(true);
        }
    }
}
