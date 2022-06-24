namespace Arex388.NhtsaVpic;

/// <summary>
/// The response status.
/// </summary>
public enum ResponseStatus :
    byte {
    /// <summary>
    /// The request was cancelled.
    /// </summary>
    Cancelled,

    /// <summary>
    /// The request failed.
    /// </summary>
    Failure,

    /// <summary>
    /// The request is invalid.
    /// </summary>
    Invalid,

    /// <summary>
    /// The request succeeded.
    /// </summary>
    Success,

    /// <summary>
    /// The request timed out.
    /// </summary>
    TimeOut
}