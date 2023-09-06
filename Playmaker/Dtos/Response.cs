namespace Playmaker.Dtos;

public class Response<T>
{
    public T? Data { get; set; }
    public bool Success { get; set; } = true;
    public string? Errors { get; set; }
}