namespace Contracts;

public struct BadRequestResponse
{
    public string ValidationAttribute { get; set; }
    public ValidationResult ValidationResult { get; set; }
    public object Value { get; set; }
    public string TargetSite { get; set; }
    public string Message { get; set; }
    public Dictionary<string, object> Data { get; set; }
    public Exception InnerException { get; set; }
    public string HelpLink { get; set; }
    public string Source { get; set; }
    public int HResult { get; set; }
    public string StackTrace { get; set; }
}

public struct ValidationResult
{
    public List<string> MemberNames { get; set; }
    public string ErrorMessage { get; set; }
}