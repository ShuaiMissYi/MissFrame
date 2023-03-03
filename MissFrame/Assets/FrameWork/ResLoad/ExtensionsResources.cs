using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;

public static class ExtensionsResources
{
    public static ResourceRequestAwaiter GetAwaiter(this ResourceRequest request) => new ResourceRequestAwaiter(request);

    public static async Task<T> LoadResourcesAsync<T>(string path) where T : UnityEngine.Object
    {
        ResourceRequest request = Resources.LoadAsync(path);
        await request;
        return request.asset as T;
    }

}
public class ResourceRequestAwaiter : INotifyCompletion
{
    public Action Continuation;
    public ResourceRequest resourceRequest;
    public bool IsCompleted => resourceRequest.isDone;
    public ResourceRequestAwaiter(ResourceRequest resourceRequest)
    {
        this.resourceRequest = resourceRequest;

        //ע�����ʱ�Ļص�
        this.resourceRequest.completed += Accomplish;
    }

    //awati ����Ĵ����װ�� continuation �����������з������ʱ����
    public void OnCompleted(Action continuation) => this.Continuation = continuation;

    public void Accomplish(AsyncOperation asyncOperation) => Continuation?.Invoke();

    public void GetResult() { }
}