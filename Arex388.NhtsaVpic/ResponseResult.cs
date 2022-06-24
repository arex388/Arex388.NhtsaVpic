namespace Arex388.NhtsaVpic;

internal readonly struct ResponseResult {
    public string Json { get; }
    public bool Success { get; }

    public ResponseResult(
        string json,
        bool success) {
        Json = json;
        Success = success;
    }
}