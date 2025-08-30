using Cysharp.Threading.Tasks;

namespace _Scripts
{
    public interface IRequest
    {
        UniTask Execute();
    }
}