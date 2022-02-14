using System;
using System.Collections.Generic; // System contains a lot of default C# libraries 
using GXPEngine;                                // GXPEngine contains the engine
using System.Drawing;							// System.Drawing contains drawing tools such as Color definitions

public class MyGame : Game
{
	public float gravity = .3f;
	public int currentScore;

	private Dictionary<string, Sound> soundLibrary = new Dictionary<string, Sound>()
	{
		{"Jump", new Sound("Jump.wav")},
		{"GameOver", new Sound("GameOver.wav")}
	};
	public MyGame() : base(1366, 768, true)	
	{
		Sprite backGround = new Sprite("background-01.png", false, false);
		AddChild(backGround);
		SetUpScenes();
	}

	public void SetUpScenes()
	{
		MainMenuScene menuScene = new MainMenuScene();
		SceneManager.instance.AddScene(menuScene);
		Level level = new Level(.3f,0.000045f,soundLibrary);
		SceneManager.instance.AddScene(level);
		GameOverScene gameOverScene = new GameOverScene();
		SceneManager.instance.AddScene(gameOverScene);
		SceneManager.instance.LoadScene(0);
	}
	// For every game object, Update is called every frame, by the engine:
	void Update()
	{
		//Console.WriteLine(gravity);
	}

	static void Main()							// Main() is the first method that's called when the program is run
	{ 
		new MyGame().Start();					// Create a "MyGame" and start it
	}
}