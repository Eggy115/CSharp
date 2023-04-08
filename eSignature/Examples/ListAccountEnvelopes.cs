﻿// <copyright file="ListAccountEnvelopes.cs" company="DocuSign">
// Copyright (c) DocuSign. All rights reserved.
// </copyright>

namespace ESignature.Examples
{
    using System;
    using DocuSign.eSign.Api;
    using DocuSign.eSign.Client;
    using DocuSign.eSign.Model;
    using static DocuSign.eSign.Api.EnvelopesApi;

    public static class ListAccountEnvelopes
    {
        /// <summary>
        /// Returns information about the envelopes in this DocuSign account
        /// </summary>
        /// <param name="accessToken">Access Token for API call (OAuth)</param>
        /// <param name="basePath">BasePath for API calls (URI)</param>
        /// <param name="accountId">The DocuSign Account ID (GUID or short version) for which the APIs call would be made</param>
        /// <returns>Object containing envelopes information</returns>
        public static EnvelopesInformation ListAllEnvelope(string accessToken, string basePath, string accountId)
        {
            // Data for this method
            // accessToken
            // basePath
            // accountId

            // Step 1 start
            var docuSignClient = new DocuSignClient(basePath);
            docuSignClient.Configuration.DefaultHeader.Add("Authorization", "Bearer " + accessToken);
            EnvelopesApi envelopesApi = new EnvelopesApi(docuSignClient);
            // Step 1 end

            // Step 2 start
            ListStatusChangesOptions options = new ListStatusChangesOptions();
            options.fromDate = DateTime.Now.AddDays(-30).ToString("yyyy/MM/dd");

            // Call the API method:
            EnvelopesInformation results = envelopesApi.ListStatusChanges(accountId, options);
            return results;
            // Step 2 end
        }
    }
}
