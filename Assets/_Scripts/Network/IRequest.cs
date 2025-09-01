using Cysharp.Threading.Tasks;

namespace _Scripts.Network
{
    public interface IRequest
    {
        UniTask Execute();
    }
}