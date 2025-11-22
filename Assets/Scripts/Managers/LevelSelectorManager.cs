using UnityEngine;
using UnityEngine.UI;

public class LevelSelectorManager : MonoBehaviour
{
    public Button[] buttons;
    public ButtonScale[] animations;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("Level")) PlayerPrefs.SetInt("Level", 1); // Evita bugs;
        
        // Ativa os botões;
        int level = PlayerPrefs.GetInt("Level");
        for(int i = 0; i < level; i++)
        {
            buttons[i].interactable = true;
            animations[i].enabled = true;
        }
    }

    // Libera todas as fases;
    public void Hack()
    {
        foreach(var b in buttons) b.interactable = true;
        foreach (var a in animations) a.enabled = true;
    }
}
