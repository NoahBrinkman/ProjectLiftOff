using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
using TiledMapParser;

public class PlatformMap
{
    public TiledLoader loader;
    public bool loaded = false;

    public PlatformMap(string fileName, GameObject rootObject)
    {
        loader = new TiledLoader(fileName, rootObject);
    }
}
class Level : Scene
{
    private Player player;
    private Pivot objectOwner;
    private float gravity = .3f;
    public int score = 0;


    EasyDraw scoreUI;
    public Level(): base()
    {
    }

    /// <summary>
    ///Set up the level.
    /// </summary>
    protected override void Start()
    {
        isActive = true;
        objectOwner = new Pivot();
        player = new Player("square.png", 4,2,objectOwner,1,1,null);
        player.SetColor(0,255,0);
        player.SetOrigin(player.width / 2 + .1f, player.height / 2 +.1f);
        player.SetScaleXY(.5f,.5f);
        player.x = game.width / 2;
        player.y = game.height - 100;
        Platform starterPlatform = new Platform("Square.png");
        starterPlatform.SetXY(game.width / 2, game.height - 100);
        starterPlatform.SetScaleXY(2,2);
        Platform p1 = new Platform("Square.png"), p2 = new Platform("Square.png"), p3 = new Platform("Square.png");
        p1.SetXY(200, 100);
        p1.SetScaleXY(2);
        p2.SetXY(700, 700);
        p2.SetScaleXY(2);
        p3.SetXY(300, 500);
        p3.SetScaleXY(2);
        objectOwner.AddChild(p1);
        objectOwner.AddChild(p2);
        objectOwner.AddChild(p3);
        
        AddChild(objectOwner);
        objectOwner.AddChild(player);
        PlatformSpawner platformSpawner = new PlatformSpawner(1.2f, objectOwner, 10, 3, 5);
        AddChild(platformSpawner);
        // Score UI 
        scoreUI = new EasyDraw(200, 30, false);
        scoreUI.SetXY(game.width - (scoreUI.width), 30);
        AddChild(scoreUI);
    }

    /// <summary>
    /// Gets called every frame
    /// When all enemies are out of the game. Complete the level
    /// When hte level is considered complete. Start a timer. When this timer is complete go to the next level
    /// When you are out of health. Tell the game to end it.
    /// </summary>
    protected override void Update()
    {
        if (!base.isActive)
        {
            return;
        }
        scoreUI.Text("SCORE: " + score, true);
        //Console.WriteLine(gravity);
        objectOwner.Move(0, gravity);
        gravity += 0.000045f * (1 + Mathf.Pow((float)Time.deltaTime/ 1000, gravity)); 
    }

    public override void UnLoadScene()
    {
        MyGame mGame = (MyGame)game;
        mGame.currentScore = score;
        base.UnLoadScene();
        
    }
}

