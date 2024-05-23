

namespace Tadi.Data.State
{
    public enum GameState
    {
        MainMenu,
        Loading,
        Pause,
        Exploration,
        Cutscene,
        Dialogue,
        Menu,
        Inventory,
        Quest,
        Battle,
        Victory,
        Defeat,
        GameOver
    }

    public enum PlayerState
    {
        FreeRoam,
        ShowDialogue,
        Battle
    }

    public static class StateData
    {
        public static GameState GameState { get; set; }
        public static PlayerState PlayerState { get; set; }
    }
}
