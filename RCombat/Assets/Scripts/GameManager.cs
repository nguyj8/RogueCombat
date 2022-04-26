// Written J Nguyen
// 03/19/22 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        if (GameManager.instance != null) // If GameManager does not equal null
        {
            // Destroy following objects 
            Destroy(gameObject);
            Destroy(player.gameObject);
            Destroy(floatingTextManager.gameObject);
            Destroy(hud);
            Destroy(menu);

            return;
        }
        instance = this;
        SceneManager.sceneLoaded += LoadState;
        SceneManager.sceneLoaded += OnSceneLoaded; 
    }

    // Resources
    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;
    public List<int> xpTable;

    // References
    public Player player;
    // Weapon
    public Weapon weapon;
    // Floating Text Manager 
    public FloatingTextManager floatingTextManager;
    // Health
    public RectTransform healthBar;
    // HUD
    public GameObject hud;
    // Menu
    public GameObject menu; 
    // Treasure
    public int treasure;
    // Experience 
    public int experience;

    // Have only one reference from anywhere to use the showing text - floating text 
    public void ShowText(string message, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        floatingTextManager.Show(message, fontSize, color, position, motion, duration);
    }

    // Upgrade weapon
    public bool TryUpgradeWeapon()
    {
        // Max leveled weapon - cannot upgrade further than set max 
        if (weaponPrices.Count <= weapon.weaponLevel)
        {
            return false; 
        }
        // Upgrade 
        if (treasure >= weaponPrices[weapon.weaponLevel])
        {
            treasure -= weaponPrices[weapon.weaponLevel];
            weapon.UpgradeWeapon();
            return true;
        }
        // If none are called, return false 
        return false;
    }

    // Health Bar
    public void OnHealthChange()
    {
        float ratio = (float)player.hitPoint / (float)player.maxHitPoint;
        healthBar.localScale = new Vector3(ratio, 1, 1); 
    }

    // Experience system - Both functions calculates experience and or level 
    public int GetCurrentLevel() // States current level 
    {
        int x = 0; 
        int add = 0;

        while (experience >= add)
        {
            add += xpTable[x]; 
            x++; // Level up

            // Check max level
            // Exempts any out of range exceptions of level array 
            if (x == xpTable.Count)
            {
                return x; 
            }
        }
        return x;
    }
    public int GetXPToLevel(int level) // States next level XP points 
    {
        int x = 0;
        int xp = 0;

        while (x < level)
        {
            xp += xpTable[x];
            x++;
        }
        return xp; 
    }
    public void GrantXP(int xp) // Check XP level up 
    {
        int currentLevel = GetCurrentLevel();
        experience += xp;
        if (currentLevel < GetCurrentLevel()) // If current level is smaller than current after retrieving XP, level up 
        {
            LevelUp();
        } 
    }
    public void LevelUp()
    {
        Debug.Log("Level up!");
        player.LevelUp();
        OnHealthChange();
    }

    // Scene Loaded
    public void OnSceneLoaded(Scene s, LoadSceneMode mode)
    {
        player.transform.position = GameObject.Find("SpawnPoint").transform.position; 
    }

    // Save state
    public void SaveState()
    {
        string s = "";
        s += "0" + "|";
        s += treasure.ToString() + "|";
        s += experience.ToString() + "|";
        s += weapon.weaponLevel.ToString(); 

        PlayerPrefs.SetString("SaveState", s);
    }
    public void LoadState(Scene s, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= LoadState;

        if (!PlayerPrefs.HasKey("SaveState"))
        {
            return; 
        }

        // ex. 0|10|15|2 - 10 treasure, 15 xp, weapon lvl 2 all split 
        string[] data = PlayerPrefs.GetString("SaveState").Split('|');

        // Change player skin

        // Change amount of tresure
        treasure = int.Parse(data[1]);

        // Change amount of experience
        experience = int.Parse(data[2]);
        if (GetCurrentLevel() != 1) // Does not exceed set XP points from XP progress
        {
            player.SetLevel(GetCurrentLevel());
        }

        // Change weapon level
        weapon.SetWeaponLevel(int.Parse(data[3]));

        // Player loaded into set spawn point of each scene
    }
}
