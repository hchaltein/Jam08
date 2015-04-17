using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Singleton object that keeps track of the game state.
/// (We might need to change this to refresh the instance when we load a new scene
/// but for now it will work)
/// </summary>
public class World {
   
    #region Static Interface

    public static Dimensions Dimension { get; private set; }

    public static bool IsColorFuckingBlind { get; set; }

    public static void Register(GameBehaviour behaviour)
    {
        Instance._Register(behaviour);
    }

    public static void ShiftDimension()
    {
        ShiftTo(Dimension == Dimensions.Green ? Dimensions.Red : Dimensions.Green);
    }

    public static void ShiftTo(Dimensions dimension)
    {
        Instance._ShiftTo(dimension);
    }

    public static void SetCheckpoint()
    {
        Instance._SetCheckpoint();
    }

    public static void LoadCheckpoint()
    {
        Instance._LoadCheckpoint();
    }

	public static void ResetWorld()
	{
		_instance = null;
	}

    #endregion

    #region Instance

    private static World _instance;
    private static World Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new World();
            }

            return _instance;
        }
    }

    #endregion

    #region Fields & Properties

    private List<GameBehaviour> behaviours;

    #endregion

    #region Instance Methods

    private World()
    {
        behaviours = new List<GameBehaviour>();
        Dimension = Dimensions.Green;
    }

    private void _Register(GameBehaviour behaviour)
    {
        behaviours.Add(behaviour);
        behaviour.SetCheckpoint();
        behaviour.ShiftTo(Dimension);
    }

    private void _ShiftTo(Dimensions dimension)
    {
        Dimension = dimension;

        foreach (var behaviour in behaviours)
        {
            behaviour.ShiftTo(dimension);
        }
    }

    private void _SetCheckpoint()
    {
        foreach (var behaviour in behaviours)
        {
            behaviour.SetCheckpoint();
        }
    }

    private void _LoadCheckpoint()
    {
        foreach (var behaviour in behaviours)
        {
            behaviour.LoadCheckpoint();
        }
    }

    #endregion
}
