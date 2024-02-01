using System.Net;

namespace Gradeo.CoreApplication.Application.Common.DTOs;

public class Error
{
    public int StatusCode { get; set; }
    public string Message { get; set; }
    public string ErrorDetails { get; set; }
}