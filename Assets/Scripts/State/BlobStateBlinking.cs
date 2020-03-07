using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobStateBlinking : BlobState
{
    private float exitTimer;
    private float blinkTimer;
    
    public BlobStateBlinking(Blob theBlob) : base(theBlob)
    {

    }

    public override void Run()
    {
        blinkTimer -= Time.deltaTime;
        if (blinkTimer <= 0) // blink every 5 seconds
        {
            blob.Blink();
            blinkTimer = .5f;
        }
        
        exitTimer -= Time.deltaTime;
        if (exitTimer <= 0)
        {
            blob.ChangeState(new BlobStateMoving(blob)); // enter moving when 2-5 seconds is up
        }
    }

    public override void Enter()
    {
        exitTimer = Random.Range(2,5);
        blinkTimer = .5f;
    }

    public override void Leave()
    {
        blob.AddOne();
    }
}
