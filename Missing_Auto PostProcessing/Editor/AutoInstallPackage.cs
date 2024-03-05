using UnityEditor;
using UnityEditor.PackageManager;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

[InitializeOnLoad]
public class AutoInstallPackage
{
    static AutoInstallPackage()
    {
        InstallPackage("com.unity.postprocessing");
    }

    private static void InstallPackage(string packageName)
    {
        var request = Client.List();  
        while (!request.IsCompleted) {}  

        if (request.Status == StatusCode.Success)
        {
            foreach (var package in request.Result)
            {
                if (package.name == packageName)
                {
                    Debug.Log($"{packageName}이(가) 이미 설치되어 있습니다.");
                    return;
                }
            }

            Debug.Log($"{packageName} 설치를 시작합니다...");
            var addRequest = Client.Add(packageName);
            while (!addRequest.IsCompleted) {}  

            if (addRequest.Status == StatusCode.Success)
                Debug.Log($"{packageName}이(가) 성공적으로 설치되었습니다.");
            else
                Debug.LogError($"{packageName} 설치에 실패했습니다: {addRequest.Error.message}");
        }
        else if (request.Status >= StatusCode.Failure)
            Debug.LogError($"패키지 목록을 불러오는 데 실패했습니다: {request.Error.message}");
    }
}