namespace FFSolution.Models
{
    public enum TranStatusEnum
    {
        Submitted = 1,
        WaitingForOperations = 2,
        WaitingForAccounting = 3,
        Finalized = 4,
        TodayIsDueDate = 5,
        Deleted = 6
    }
}