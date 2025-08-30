using System.Collections.Generic;
using _Scripts;
using Cysharp.Threading.Tasks;

public class NetworkRequestQueue
{
    private Queue<IRequest> _requestQueue = new ();
    private bool _isProcessing;

    public void AddRequest(IRequest request)
    {
        _requestQueue.Enqueue(request);
        if (!_isProcessing)
        {
            ProcessQueue().Forget();
        }
    }

    private async UniTask ProcessQueue()
    {
        _isProcessing = true;
        while (_requestQueue.Count > 0)
        {
            var request = _requestQueue.Dequeue();
            await request.Execute();
        }
        _isProcessing = false;
    }
    
    public void CancelAndRemoveRequest<T>() where T : IRequest
    {
        var tempQueue = new Queue<IRequest>();
        while(_requestQueue.Count > 0)
        {
            var request = _requestQueue.Dequeue();
            if (!(request is T))
            {
                tempQueue.Enqueue(request);
            }
        }
        _requestQueue = tempQueue;
    }
}