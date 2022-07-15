using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor.Animations;
using TMPro;

public class MainGameUi : MonoBehaviour
{
    private PlayerController playerScript;
    private Animator playerAnim;
    [SerializeField] private int playerHealth = 0;
    [SerializeField] private GameObject healthBar;
    [SerializeField] private GameObject healthPipPrefab;
    [SerializeField] private GameObject comboGuide;
    [SerializeField] private GameObject comboItemPrefab;
    private ChildAnimatorState[] playerStates;
    private int currentAnim = 0;

    [SerializeField] private Sprite psSquare;
    [SerializeField] private Sprite psTriangle;
    [SerializeField] private Sprite psR1;
    [SerializeField] ButtonDictionary buttonDictionary;

    private void Awake()
    {
        buttonDictionary.Init();
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        playerAnim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();

        playerStates = (playerAnim.runtimeAnimatorController as AnimatorController).layers[0].stateMachine.states;
    }
    private void Update()
    {
        UpdateHealth();
        UpdateComboGuide();
    }

    private void UpdateComboGuide()
    {
        if (currentAnim == 0)
        {
            currentAnim = playerAnim.GetCurrentAnimatorStateInfo(0).shortNameHash;
        } 
        else if(currentAnim != playerAnim.GetCurrentAnimatorStateInfo(0).shortNameHash)
        {
            foreach(Transform child in comboGuide.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
            AnimatorState currentAnimState = null;
            currentAnim = playerAnim.GetCurrentAnimatorStateInfo(0).shortNameHash;
            foreach (ChildAnimatorState i in playerStates)
            {
                if (i.state.nameHash == currentAnim)
                    currentAnimState = i.state;
            }

            if (currentAnimState != null)
            {
                var transitions = currentAnimState.transitions;
                GameObject _copy = comboItemPrefab;
                foreach(AnimatorStateTransition i in transitions)
                {
                    if(!i.destinationState.name.Equals("Idle"))
                    {
                        _copy.GetComponentInChildren<TextMeshProUGUI>().SetText(i.destinationState.name);
                        _copy.GetComponentInChildren<Image>().sprite = buttonDictionary.GetItem(i.destinationState.tag);
                        Instantiate(_copy, comboGuide.transform);
                    }
                }
            }
        }
    }

    private void UpdateHealth()
    {
        if (playerScript.playerHealth > playerHealth)
        {
            while (playerScript.playerHealth != healthBar.transform.childCount)
            {
                Instantiate(healthPipPrefab, healthBar.transform);
            }
            playerHealth = playerScript.playerHealth;
        }
        else if (playerScript.playerHealth < playerHealth)
        {
            for (int i = 0; i < playerHealth - playerScript.playerHealth; i++)
            {
                DestroyImmediate(healthBar.transform.GetChild(0).gameObject);
            }
            playerHealth = playerScript.playerHealth;
        }
    }


}
