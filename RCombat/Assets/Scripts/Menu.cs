// Written J Nguyen
// 03/25/22

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    // Text fields
    public Text levelText, treasureText, upgradeCostText, xpText;

    // Character sprite and weapon sprite
    private int currentCharacterSelection = 0;
    public Image characterSelectionSprite;
    public Image weaponSprite;
    public RectTransform xpBar; // Using local scale for XP bar

    // Character selection
    public void OnArrowClick(bool right) // Both buttons call same function 
    {
        // If right button is clicked 
        if (right)
        {
            currentCharacterSelection++; // Incrementing by 1 to switch

            // If clicking too far from character selection length
            if (currentCharacterSelection == GameManager.instance.playerSprites.Count)
            {
                currentCharacterSelection = 0;
            }
            OnSelectionChange();
        }
        else
        {
            currentCharacterSelection--; // Decrementing by 1 to switch

            // If clicking too far from character selection length
            if (currentCharacterSelection < 0) // -1 is invalid in array 
            {
                currentCharacterSelection = GameManager.instance.playerSprites.Count -1;
            }
            OnSelectionChange();
        }
        
    }
    private void OnSelectionChange()
    {
        characterSelectionSprite.sprite = GameManager.instance.playerSprites[currentCharacterSelection];
        // Switch characters
        GameManager.instance.player.SwapSprite(currentCharacterSelection); 
    }

    // Character weapon upgrade
    public void OnUpgradeClick()
    {
        // Reference - Change with GameManager
        if (GameManager.instance.TryUpgradeWeapon()) // Update menu, if appropriate
        {
            UpdateMenu(); // If upgrade insufficient, do not update menu 
        }
    }

    // Character text information
    public void UpdateMenu()
    {
        // Weapon
        weaponSprite.sprite = GameManager.instance.weaponSprites[GameManager.instance.weapon.weaponLevel];
        if (GameManager.instance.weapon.weaponLevel == GameManager.instance.weaponPrices.Count)
        {
            upgradeCostText.text = "MAX";
        }
        else
        {
            upgradeCostText.text = GameManager.instance.weaponPrices[GameManager.instance.weapon.weaponLevel].ToString(); 
        }

        // Meta - Level 
        levelText.text = GameManager.instance.GetCurrentLevel().ToString();
        treasureText.text = GameManager.instance.treasure.ToString();

        // XP 
        int currentLevel = GameManager.instance.GetCurrentLevel();
        // Check max level and to display current total XP or is in progress to leveling up 
        if (currentLevel == GameManager.instance.xpTable.Count)
        {
            xpText.text = GameManager.instance.experience.ToString() + " total XP points"; // Display total XP if maxed level
            xpBar.localScale = Vector3.one; // Fills the XP bar 
        }
        else // If not maxed level, find the ratio into level - Scales correctly 
        {
            int previousLevelXP = GameManager.instance.GetXPToLevel(currentLevel - 1); // List previous level (completed)
            int currentLevelXP = GameManager.instance.GetXPToLevel(currentLevel); // List current level (incomplete)

            // Ratio of what must be completed
            int difference = currentLevelXP - previousLevelXP;
            // List XP in level
            int currentXPIntoLevel = GameManager.instance.experience - previousLevelXP;

            float completionRatio = (float)currentXPIntoLevel / (float)difference;
            xpBar.localScale = new Vector3(completionRatio, 1, 1); // Only need to change x - axis
            xpText.text = currentXPIntoLevel.ToString() + " / " + difference; // Text of XP 
        }
    }

}
