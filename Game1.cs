﻿using System;
using GoOutGame.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GoOutGame;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private State _currentState;
    private State _nextState;

    public static Random random;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        random = new Random();
        _graphics.PreferredBackBufferWidth = 1440;
        _graphics.PreferredBackBufferHeight = 900;
        _graphics.ApplyChanges();
        // TODO: Add your initialization logic here

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _currentState = new MenuState(this, GraphicsDevice, Content);
        _currentState.LoadContent();
        _nextState = null;

        // TODO: use this.Content to load your game content here
    }
    protected override void UnloadContent()
    {

    }
    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
        if (_nextState != null)
        {
            _currentState = _nextState;
            _currentState.LoadContent();

            _nextState = null;
        }
        _currentState.Update(gameTime);
        _currentState.PostUpdate(gameTime);

        // TODO: Add your update logic here

        base.Update(gameTime);
    }

    public void ChangeState(State state)
    {
        _nextState = state;
    }
    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        _currentState.Draw(gameTime, _spriteBatch);
        // TODO: Add your drawing code here

        base.Draw(gameTime);
    }
}