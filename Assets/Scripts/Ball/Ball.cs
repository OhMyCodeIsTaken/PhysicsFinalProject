using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private int _score;

    public int Score { get => _score; }

    /* This function will be called when a ball is "pocketed" or "deposited" into a hole.
     * What happens next depends on the ball type: a PlayBall will add score and "disapper", where as a
     * WhiteBall will subtract score and respawn at a predetermined spawn point on the table.
     */
    public virtual void PocketBall() { } 


}
