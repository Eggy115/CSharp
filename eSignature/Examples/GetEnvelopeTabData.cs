﻿// <copyright file="GetEnvelopeTabData.cs" company="DocuSign">
// Copyright (c) DocuSign. All rights reserved.
// </copyright>

namespace ESignature.Examples
{
    using DocuSign.eSign.Api;
    using DocuSign.eSign.Client;
    using DocuSign.eSign.Model;

    public static class GetEnvelopeTabData
    {
        /// <summary>
        /// Get all tab data (form data) from envelope
        /// </summary>
        /// <param name="accessToken">Access Token for API call (OAuth)</param>
        /// <param name="basePath">BasePath for API calls (URI)</param>
        /// <param name="accountId">The DocuSign Account ID (GUID or short version) for which the APIs call would be made</param>
        /// <param name="envelopeId">The required envelopeId</param>
        /// <returns>Object containing all the tab data (form data)</returns>
        public static EnvelopeFormData GetEnvelopeFormData(string accessToken, string basePath, string accountId, string envelopeId)
        {
            // Construct your API headers
            var docuSignClient = new DocuSignClient(basePath);
            docuSignClient.Configuration.DefaultHeader.Add("Authorization", "Bearer " + accessToken);

            // Call the eSignature REST API
            EnvelopesApi envelopesApi = new EnvelopesApi(docuSignClient);
            return envelopesApi.GetFormData(accountId, envelopeId);
        }
    }
}
