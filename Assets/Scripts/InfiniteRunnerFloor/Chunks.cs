/*
A chunk object that can be moved around.
*/

public class Chunk
{
    public enum ChunkState
    {
        active,
        disabled
    }

    private ChunkState _chunkState = ChunkState.disabled;

    public void SetState(ChunkState state)
    {
        _chunkState = state;
    }
}