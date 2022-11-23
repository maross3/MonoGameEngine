using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TestMonogame.Components;
using TestMonogame.Components.@abstract;
using TestMonogame.Entities.@abstract;
namespace TestMonogame.Entities;

// if this stays, expo to new fl 
internal interface IIdleImplemented
{
    public int LastKnowntime { get; set; }
    public void AdvanceTime(float value);
    public void ApplySecondDifference(int secondDifference);
}


//todo dependency on idle implemented
// whut?>,>
internal class IdleEntity : BaseEntity, IIdleImplemented
{
    public override int ID { get; set; }
    public int LastKnowntime { get; set; }
    public List<GameObject> components = new();
    public TimeSpan currentSecondsDelay = TimeSpan.FromSeconds(2);

    protected int secondDifference;

    private bool _harvesting;
    private float _targetWaitMs;

    public override void Initialize()
    {
        _harvesting = false;
        var inventoryView = new InventoryView(Game1.windowTexture, Game1.electroFontOne);
        inventoryView.Initialize();
        inventoryView.chopWoodButton.OnButtonPress += ChopWoodButton_OnButtonPress;
        components.Add(inventoryView);
    }

    private void ChopWoodButton_OnButtonPress(object sender, EventArgs e)
    {
        _harvesting = !_harvesting;
        if (_harvesting) _targetWaitMs = -1;
        else 
            foreach (var component in components)
                if (component is IIdleImplemented idler) idler.AdvanceTime(1);
    }

    public override void Update(GameTime gameTime)
    {
        foreach (var component in components) component.Update(gameTime);
        if(_harvesting) HandleHarvest(gameTime);
    }

    private void HandleHarvest(GameTime gameTime)
    {
        var gameMs = (float)gameTime.TotalGameTime.TotalMilliseconds;
        foreach (var component in components)
            if (component is IIdleImplemented idler)
                idler.AdvanceTime((_targetWaitMs - gameMs) / (float)currentSecondsDelay.TotalMilliseconds);

        //if (!_harvesting || _targetWaitMs >= gameMs) return;
        if (!_harvesting || _targetWaitMs - gameMs > 0) return;

        Debug.WriteLine($"Time Elapsed. Target: {_targetWaitMs} GameTime MS: {gameMs}");

        var lastMs = gameTime.TotalGameTime;
        _targetWaitMs = (float)lastMs.TotalMilliseconds + (float)currentSecondsDelay.TotalMilliseconds;
    }

    public override void Draw(SpriteBatch batch)
    {
        foreach (var component in components) component.Draw(batch);

        batch.Draw(
            Game1.treeOne,
            new Vector2((int)(Game1.GameWindowBounds.Width * 0.55), 60),
            null,
            Color.White,
            0f,
            new Vector2(0, 0),
            new Vector2(0.4f),
            SpriteEffects.None,
            0f
        );
    }

    public void AdvanceTime(float value)
    {
    }

    public void ApplySecondDifference(int seconds) =>
        secondDifference = seconds;
}