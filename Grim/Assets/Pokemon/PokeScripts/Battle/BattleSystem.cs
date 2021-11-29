using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { Start, ActionSelection, MoveSelection, PerformMove, Busy }

public class BattleSystem : MonoBehaviour
{
    /* unit references */
    [SerializeField] BattleUnit playerUnit;
    [SerializeField] BattleUnit enemyUnit;
    [SerializeField] BattleHud playerHud;
    [SerializeField] BattleHud enemyHud;
    [SerializeField] BattleDialogBox dialogBox;

    [SerializeField] Image playerImage;
    [SerializeField] Image enemyImage;

    BattleState state;
    int currentAction;  // 0 for fight, 1 for run
    int currentMove;    // 0, 1, 2, 3 respectively for each move

    bool firstTime = true;  // play trainer animation during first play through

    /* Arrow keys for navigation, Return to choose selection */

    private void Start()
    {
        StartCoroutine(SetupBattle()); 
    }

    public IEnumerator PlayTrainerAnimation()
    {
        yield return dialogBox.TypeDialog("Anna wants to battle!");
        enemyImage.enabled = false;
        enemyUnit.gameObject.GetComponent<Image>().enabled = true;
        enemyHud.gameObject.SetActive(true);
        enemyUnit.Setup();
        enemyHud.SetData(enemyUnit.Unit);
        yield return dialogBox.TypeDialog($"Anna sends out {enemyUnit.Unit.Base.Name}.");

        yield return dialogBox.TypeDialog("Surely I'll beat her!");
        playerImage.enabled = false;
        playerUnit.gameObject.GetComponent<Image>().enabled = true;
        playerHud.gameObject.SetActive(true);
        playerUnit.Setup();
        playerHud.SetData(playerUnit.Unit);
        yield return dialogBox.TypeDialog($"Go {playerUnit.Unit.Base.Name}!");

        yield return new WaitForSeconds(1f);
    }

    public IEnumerator SetupBattle()
    {
        if (firstTime)
        {
            enemyUnit.gameObject.GetComponent<Image>().enabled = false;
            playerUnit.gameObject.GetComponent<Image>().enabled = false;
            
            enemyHud.gameObject.SetActive(false);
            playerHud.gameObject.SetActive(false);

            yield return PlayTrainerAnimation();
            firstTime = false;
        }
        else
        {
            playerUnit.Setup();
            enemyUnit.Setup();

            playerHud.SetData(playerUnit.Unit);
            enemyHud.SetData(enemyUnit.Unit);
        }        

        dialogBox.SetMoveNames(playerUnit.Unit.Moves);

        ActionSelection();
    }

    void ActionSelection()
    {
        state = BattleState.ActionSelection;

        StartCoroutine(dialogBox.TypeDialog("Choose an action"));
        dialogBox.EnableActionSelector(true);
    }

    void MoveSelection()
    {
        state = BattleState.MoveSelection;

        dialogBox.EnableActionSelector(false);
        dialogBox.EnableDialogText(false);
        dialogBox.EnableMoveSelector(true);
    }

    IEnumerator PlayerMove()
    {
        state = BattleState.PerformMove;

        var move = playerUnit.Unit.Moves[currentMove];
        yield return RunMove(playerUnit, enemyUnit, move);

        if (enemyUnit.Unit.HP <= 0)
        {
            yield return dialogBox.TypeDialog($"{enemyUnit.Unit.Base.Name} Fainted");
            enemyUnit.PlayFaintAnimation();
            BattleOver();

            // Placeholder for victory scene, triggered here
            yield return dialogBox.TypeDialog("Nice! We beat this level!");
        }
        else
        {
            StartCoroutine(EnemyMove());
        }
    }

    IEnumerator EnemyMove()
    {
        state = BattleState.PerformMove;

        var move = enemyUnit.Unit.GetRandomMove();
        yield return RunMove(enemyUnit, playerUnit, move);

        if (playerUnit.Unit.HP <= 0)
        {
            yield return dialogBox.TypeDialog($"{playerUnit.Unit.Base.Name} Fainted");
            playerUnit.PlayFaintAnimation();
            BattleOver();

            // Placeholder for defeat scene, which will the trigger Start again
            yield return dialogBox.TypeDialog("Dang it! Gotta start over the battle now!");
            yield return new WaitForSeconds(1f);
            Start();
        }
        else
        {
            ActionSelection();
        }
    }

    void BattleOver()
    {
        playerUnit.Unit.OnBattleOver();
        enemyUnit.Unit.OnBattleOver();
    }

    IEnumerator RunMove(BattleUnit sourceUnit, BattleUnit targetUnit, Move move)
    {
        move.PP--;
        yield return dialogBox.TypeDialog($"{sourceUnit.Unit.Base.Name} used {move.Base.Name}");

        sourceUnit.PlayAttackAnimation();
        yield return new WaitForSeconds(1f);

        targetUnit.PlayHitAnimation();

        if (move.Base.Category == MoveCategory.Status)
        {
            yield return RunMoveEffects(move, sourceUnit.Unit, targetUnit.Unit);
        }
        else 
        {
            bool isFainted = targetUnit.Unit.TakeDamage(move, sourceUnit.Unit);
            
            if (sourceUnit.IsPlayerUnit)
                yield return enemyHud.UpdateHP();
            else
                yield return playerHud.UpdateHP();
        }
    }

    IEnumerator RunMoveEffects(Move move, Unit source, Unit target)
    {
        var effects = move.Base.Effects;
        if (effects.Boosts != null)
        {
            if (move.Base.Target == MoveTarget.Self)
                source.ApplyBoosts(effects.Boosts);
            else
                target.ApplyBoosts(effects.Boosts);
        }

        yield return ShowStatusChanges(source);
        yield return ShowStatusChanges(target);
    }
    
    IEnumerator ShowStatusChanges(Unit unit)
    {
        while (unit.StatusChanges.Count > 0)
        {
            var message = unit.StatusChanges.Dequeue();
            yield return dialogBox.TypeDialog(message);
        }
    }

    private void Update()
    {
        if (state == BattleState.ActionSelection) 
        {
            HandleActionSelection();
        }
        else if (state == BattleState.MoveSelection)
        {
            HandleMoveSelection();
        }
    }

    void HandleActionSelection()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow)) 
        {
            if (currentAction < 1)
                ++currentAction;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow)) 
        {
            if (currentAction > 0)
                --currentAction;
        }

        dialogBox.UpdateActionSelection(currentAction);

        if (Input.GetKeyDown(KeyCode.Return)) {
            if (currentAction == 0)
            {
                // Fight
                MoveSelection();
            }
            else if (currentAction == 1)
            {
                // Run: play dialog that Liv would never run
                StartCoroutine(RunDialog());
            }
        }
    }

    IEnumerator RunDialog()
    {
        state = BattleState.Busy;

        yield return dialogBox.TypeDialog("Heck no! I'm no quitter!");
        ActionSelection();
    }

    void HandleMoveSelection()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow)) 
        {
            if (currentMove < playerUnit.Unit.Moves.Count - 1)
                ++currentMove;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow)) 
        {
            if (currentMove > 0)
                --currentMove;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow)) 
        {
            if (currentMove < playerUnit.Unit.Moves.Count - 2)
                currentMove += 2;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow)) 
        {
            if (currentMove > 1)
                currentMove -= 2;
        }

        dialogBox.UpdateMoveSelection(currentMove, playerUnit.Unit.Moves[currentMove]);

        if (Input.GetKeyDown(KeyCode.Return))
        {
            dialogBox.EnableMoveSelector(false);
            dialogBox.EnableDialogText(true);
            StartCoroutine(PlayerMove());
        }
    }
}
