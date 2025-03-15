namespace BaseScripts.PlayerMovement
{
    public interface IJumpable
    {
        void Jump();
        void UpdateGroundedState();
    }
}