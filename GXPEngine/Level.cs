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
    private float gravityIncrements;
    public int score = 0;
    public bool levelIsLost = false;
    private float timer = 1.5f;
    EasyDraw scoreUI;
    private SFXHandler sfxHandler;
    private WrappingBackground mainBackGround;
    private WrappingBackground secondaryBackground;
    public Level(float startingGravity, float gravityIncrements, Dictionary<string,Sound> soundLibrary): base()
    {
        this.gravity = startingGravity;
        this.gravityIncrements = gravityIncrements;
        sfxHandler = new SFXHandler(soundLibrary, .4f);
    }

    /// <summary>
    ///Set up the level.
    /// </summary>
    ///
    protected override void Start()
    {
        isActive = true;
        objectOwner = new Pivot();
        player = new Player("charSpriteSheet.png", 3, 2,7, objectOwner,4,3,null);
        player.SetOrigin(player.width / 2, player.height / 2);
        player.SetScaleXY(.5f,.5f);
        player.x = game.width / 2;
        player.y = game.height - 300;
        Platform starterPlatform = new Platform("Meteor.png");
        starterPlatform.SetXY(game.width / 2, game.height - 300);
        starterPlatform.SetScaleXY(0.12f);
        Platform p1 = new Platform("Meteor.png"), p2 = new Platform("Meteor.png"), p3 = new Platform("Meteor.png");
        p1.SetXY(200, 100);
        p1.SetScaleXY(0.12f);
        p2.SetXY(700, 200);
        p2.SetScaleXY(0.12f);
        p3.SetXY(1000, 100);
        p3.SetScaleXY(0.12f);
        objectOwner.AddChild(starterPlatform);
        objectOwner.AddChild(p1);
        objectOwner.AddChild(p2);
        objectOwner.AddChild(p3);
        mainBackGround = new WrappingBackground("background-tile-clean-01.png");
        secondaryBackground = new WrappingBackground("background-tile-planets-01.png");
        secondaryBackground.SetScaleXY(1,1.01f);
        mainBackGround.SetScaleXY(1, 1.01f);
        AddChild(mainBackGround);
        AddChild(secondaryBackground);
        AddChild(objectOwner);
        objectOwner.AddChild(player);
        player.OnJump += PlaySoundEffect;
        PlatformSpawner platformSpawner = new PlatformSpawner(1.3f, objectOwner, 15, 
            3, 10, 20, 5);
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

        mainBackGround.speed = gravity / 4;
        secondaryBackground.speed = gravity / 2;
        scoreUI.Text("SCORE: " + score, true);
        //Console.WriteLine(gravity);
        if (!levelIsLost)
        {
            objectOwner.Move(0, gravity);
                  gravity += gravityIncrements * (1 + Mathf.Pow((float)Time.deltaTime/ 1000, gravity));  
        }
        else
        {
            timer -= (float)Time.deltaTime / 1000;
            player.scale = MathFunctions.Lerp(0, .5f, timer / 1.5f);
        }

        if (timer <= 0)
        {
            MyGame mygame = (MyGame)game;
            mygame.currentScore = score;
            SceneManager.instance.TryLoadNextScene();
        }
        
    }

    public void LostLevel()
    {
        levelIsLost = true;
        gravity = 0.0001f;
        sfxHandler.PlaySound("GameOver");
    }

    private void PlaySoundEffect(string soundEffectName)
    {
        sfxHandler.PlaySound(soundEffectName);
    }
    
    public override void UnLoadScene()
    {
        MyGame mGame = (MyGame)game;
        mGame.currentScore = score;
        base.UnLoadScene();
        
    }
}

