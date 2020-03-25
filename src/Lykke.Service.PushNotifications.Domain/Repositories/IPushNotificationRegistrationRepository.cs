﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lykke.Service.PushNotifications.Domain.Enums;

namespace Lykke.Service.PushNotifications.Domain.Repositories
{
    public interface IPushNotificationRegistrationRepository
    {
        Task<PushTokenInsertionResult> CreateAsync(PushNotificationRegistration data);
        Task<List<PushNotificationRegistration>> GetAllAsync();
        Task<List<PushNotificationRegistration>> GetAllForCustomerAsync(string customerId);
        Task<bool> DeleteAsync(Guid pushRegistrationId);
        Task<bool> DeleteByTokenAsync(string token);
        Task<bool> DeleteAllForCustomerAsync(string customerId);
    }
}
