using UnityEngine;

public class Status : MonoBehaviour
{
    public RectTransform isSkillUp;

    private void Awake()
    {
        isSkillUp = GetComponentsInChildren<RectTransform>()[2];
    }

    private void LateUpdate()
    {
        if (GameManager.instance.skillPoint != 0)
        {
            isSkillUp.gameObject.SetActive(true);
        }

        else
        {
            isSkillUp.gameObject.SetActive(false);
        }
    }
}
