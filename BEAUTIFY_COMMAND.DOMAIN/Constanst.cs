namespace BEAUTIFY_COMMAND.DOMAIN;
public static class ErrorMessages
{
    public static class ShiftConfig
    {
        public const string ShiftConfigNotFound = "Không tìm thấy ca làm việc.";
        public const string ShiftConfigAlreadyExists = "Ca làm việc đã tồn tại.";
        public const string ShiftConfigNotActive = "Ca làm việc không hoạt động.";

        public const string ShiftConfigStartTimeMustBeEarlierThanEndTime =
            "Thời gian bắt đầu ca làm việc phải trước thời gian kết thúc ca làm việc";
    }

    public static class Clinic
    {
        public const string BankAccountNotFound = "Không tìm thấy tài khoản ngân hàng.";
        public const string ClinicNotFound = "Không tìm thấy phòng khám.";
        public const string ClinicIsNotABranch = "Phòng khám không phải là chi nhánh.";
        public const string ParentClinicNotFound = "Không tìm thấy phòng khám chi nhánh chính";
        public const string AmountMustBeGreaterThan2000 = "Số tiền phải lớn hơn 2000";
        public const string InvalidTimeFormat = "Thời gian không hợp lệ. Định dạng thời gian phải là HH:mm";
        public const string InsufficientFunds = "Số dư không đủ";
        public const string ClinicDoNotHaveWorkingHours = "Phòng khám không có giờ làm việc";

        public const string ClinicWorkingScheduleCapacityMustBeGreaterThanZero =
            "Số lượng bác sĩ trong ca làm việc phải lớn hơn 0";

        public const string ClinicAlreadyExists = "Clinic already exists.";
        public const string ClinicBranchNotFound = "Clinic branch not found.";
        public const string ClinicBranchAlreadyExists = "Clinic branch already exists.";
        public const string ClinicBranchNotActive = "Clinic branch is not active.";

        public static string ClinicStartTimeMustBeEarlierThanEndTime(TimeSpan startTime, TimeSpan endTime) =>
            $"Thời gian bắt đầu ca làm việc {startTime} phải trước thời gian kết thúc ca làm việc {endTime}";

        public static string OutsideWorkingHours(TimeSpan startTime, TimeSpan endTime, TimeSpan?
            workingHoursStart, TimeSpan? workingHoursEnd) =>
            $"Thời gian làm việc {startTime} {endTime} không nằm trong giờ làm việc của phòng khám {workingHoursStart} {workingHoursEnd}";
    }

    public static class Wallet
    {
        public const string WalletNotFound = "Không tìm thấy ví.";
        public const string InsufficientBalance = "Insufficient balance.";
        public const string InvalidTransactionType = "Invalid transaction type.";
        public const string InvalidTransactionStatus = "Trạng thái giao dịch không hợp lệ.";
        public const string TransactionNotFound = "Transaction not found.";
        public const string TransactionAlreadyExists = "Transaction already exists.";
        public const string TransactionFailed = "Transaction failed.";
    }
}