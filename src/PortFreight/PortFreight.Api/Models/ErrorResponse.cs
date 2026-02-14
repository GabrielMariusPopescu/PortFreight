namespace PortFreight.Api.Models;

/// <summary>
/// Represents a standardized error response returned by an API, containing information about the error that occurred.
/// </summary>
/// <remarks>
/// This class is typically used to convey error details in HTTP responses, including a human-readable
/// message, optional additional details, the HTTP status code, and the time the error was generated. It enables clients
/// to programmatically interpret and display error information returned from the API.
/// </remarks>
public class ErrorResponse
{
    /// <summary>
    /// Gets or sets the message associated with the current instance.
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the detailed information associated with the object.
    /// </summary>
    /// <remarks>
    /// This property can hold additional context or information that may be relevant for
    /// understanding the state or behavior of the object. It may be null if no detail is provided.
    /// </remarks>
    public string? Detail { get; set; }
    
    /// <summary>
    /// Gets or sets the HTTP status code associated with the response.
    /// </summary>
    /// <remarks>
    /// This property allows the user to specify the status code returned by the server, which can
    /// indicate the result of the request processing. Common status codes include 200 for success, 404 for not found,
    /// and 500 for server error.
    /// </remarks>
    public int StatusCode { get; set; }
    
    /// <summary>
    /// Gets or sets the date and time when the object was created or last modified.
    /// </summary>
    /// <remarks>
    /// The timestamp is represented in Coordinated Universal Time (UTC). This property is
    /// automatically initialized to the current UTC time when a new instance is created.
    /// </remarks>
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}
