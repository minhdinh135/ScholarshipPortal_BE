﻿namespace Application.Interfaces.IServices;

public interface IBackgroundService
{
    Task ScheduleScholarshipsAfterDeadline();
    Task ScheduleApplicationsNeedExtend();
    Task ScheduleApplicationsRejectedInAward();
}
