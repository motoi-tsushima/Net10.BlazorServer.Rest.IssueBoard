namespace Shared.Rest.IssueBoard.Dtos;

public enum IssueStatus
{
    NotStarted = 0,
    InProgress = 1,
    Failed = 2,
    CannotConfirm = 3,
    Resolved = 4
}

public static class IssueStatusExtensions
{
    public static string ToDisplayName(this IssueStatus status) => status switch
    {
        IssueStatus.NotStarted => "未着手",
        IssueStatus.InProgress => "着手中",
        IssueStatus.Failed => "解決失敗",
        IssueStatus.CannotConfirm => "課題確認不能",
        IssueStatus.Resolved => "解決済み",
        _ => status.ToString()
    };
}
