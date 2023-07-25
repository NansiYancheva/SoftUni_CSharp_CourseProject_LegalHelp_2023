﻿namespace LegalHelpSystem.Services.Data.Interfaces
{
    public interface IUserService
    {
        Task<string> GetFullNameByEmailAsync(string email);

        Task AddDocToUserCollectionOfDocsAsync(string userId, string ticketId);
    }
}
