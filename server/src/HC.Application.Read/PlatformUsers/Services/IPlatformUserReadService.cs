﻿using HC.Application.Read.Users.ReadModels;
using HC.Domain.Notifications;

namespace HC.Application.Read.Users.Services;

public interface IPlatformUserReadService
{
    Task<PlatformUserReadModel?> GetUserById(UserAccountId userId);
    Task<LibraryReadModel?> GetLibraryInformation(UserAccountId requesterId, LibraryId? libraryId = null);
}