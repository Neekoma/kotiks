using UnityEngine;
using Zenject;


namespace Vald
{
    /**
     * <summary>Use fields of this class for setup scene</summary>
     */
    public class GameplaySceneEntryPoint : MonoBehaviour
    {
        private GameSceneData _data;

        [SerializeField]
        private byte _cansOfTunaNeedToCollect;

        [Inject]
        private void Construct(GameSceneData sceneData)
        {
            _data = sceneData;
        }

        private void Awake()
        {
            Entry();
        }

        private void Entry()
        {
            _data.cansOfTunaToCollect = _cansOfTunaNeedToCollect;
        }
    }
}