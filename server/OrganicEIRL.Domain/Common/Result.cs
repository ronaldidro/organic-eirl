namespace OrganicEIRL.Domain.Common;

public class Result<T>
{
  public bool IsSuccess { get; set; }
  public T Data { get; set; } = default!;
  public List<string> Errors { get; set; } = new List<string>();

  public static Result<T> Success(T data) => new() { IsSuccess = true, Data = data };

  public static Result<T> Failure(string error) => new()
  {
    IsSuccess = false,
    Errors = new List<string> { error }
  };

  public static Result<T> Failure(List<string> errors) => new()
  {
    IsSuccess = false,
    Errors = errors
  };
}