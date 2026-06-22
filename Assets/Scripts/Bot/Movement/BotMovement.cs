using System;
using System.Collections.Generic;
using UnityEngine;

public class BotMovement : MonoBehaviour
{
    public event Action OnDestinationReached;

    private MovementControllerAnimation movement;

    private bool moving;

    private List<Node> currentPath;
    private int currentIndex;

    //Indices de lectura
    public List<Node> CurrentPath => currentPath;
    public int CurrentIndex => currentIndex;
    public bool IsMoving => moving;

    private Vector2 CurrentTarget
    {
        get
        {
            return currentPath[currentIndex]
                .worldPosition;
        }
    }

    private void Awake()
    {
        movement =
            GetComponent<MovementControllerAnimation>();
    }

    public void FollowPath(
        List<Node> path)
    {
        if (path == null || path.Count == 0)
            return;

        currentPath = path;

        currentIndex = 0;

        moving = true;
    }

    private void Update()
    {
        if (!moving)
            return;

        Vector2 direction =
            CurrentTarget -
            (Vector2)transform.position;

        float distance =
            direction.magnitude;

        if (distance <= 0.15f)
        {
            currentIndex++;

            if (currentIndex >= currentPath.Count)
            {
                moving = false;

                movement.SetMovement(
                    Vector2.zero
                );

                OnDestinationReached?.Invoke();

                return;
            }

            return;
        }

        movement.SetMovement(
            direction.normalized
        );
    }
}