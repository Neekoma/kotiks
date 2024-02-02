namespace Vald
{
    public class GameSceneData
    {
        public byte cansOfTunaToCollect { get; set; } = 0;
        public byte collectedTuna { get; private set; } = 0;


        public void OnTunaCollected()
        {
            collectedTuna++;
        }
    }
}
