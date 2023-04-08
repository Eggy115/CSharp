﻿// <copyright file="RetrieveDocuSignProfileByUserId.cs" company="DocuSign">
// Copyright (c) DocuSign. All rights reserved.
// </copyright>

namespace DocuSign.Admin.Examples
{
    using System;
    using DocuSign.Admin.Api;
    using DocuSign.Admin.Client;
    using DocuSign.Admin.Model;

    public class RetrieveDocuSignProfileByUserId
    {
        /// <summary>
        /// Get a DocuSign profile by the user ID
        /// </summary>
        /// <param name="basePath">BasePath for API calls (URI)</param>
        /// <param name="accessToken">Access Token for API call (OAuth)</param>
        /// <param name="orgId">DocuSign Organization Id (GUID)</param>
        /// <param name="userId">The DocuSign User Id (GUID)</param>
        /// <returns>DocuSign profile, that has the searched user ID</returns>
        public static UsersDrilldownResponse GetDocuSignProfileByUserId(
            string basePath,
            string accessToken,
            Guid? orgId,
            Guid userId)
        {
            // Step 2 start
            var apiClient = new ApiClient(basePath);
            apiClient.Configuration.DefaultHeader.Add("Authorization", "Bearer " + accessToken);

            // Step 2 end

            // Step 3 start
            var usersApi = new UsersApi(apiClient);
            var recentlyModifiedUsers = usersApi.GetUserDSProfile(orgId, userId);

            // Step 3 end
            return recentlyModifiedUsers;
        }
    }
}
