﻿// <copyright file="CreateClickwrap.cs" company="DocuSign">
// Copyright (c) DocuSign. All rights reserved.
// </copyright>

namespace DocuSign.Click.Examples
{
    using System;
    using System.Collections.Generic;
    using DocuSign.Click.Api;
    using DocuSign.Click.Client;
    using DocuSign.Click.Model;

    public class CreateClickwrap
    {
        /// <summary>
        /// Creates a new clickwrap
        /// </summary>
        /// <param name="name">The name of new clickwrap</param>
        /// <param name="basePath">BasePath for API calls (URI)</param>
        /// <param name="accessToken">Access Token for API call (OAuth)</param>
        /// <param name="accountId">The DocuSign Account ID (GUID or short version) for which the APIs call would be made</param>
        /// <returns>The summary response of a newly created clickwrap</returns>
        public static ClickwrapVersionSummaryResponse Create(string name, string basePath, string accessToken, string accountId, string pdfFile)
        {
            var docuSignClient = new DocuSignClient(basePath);
            docuSignClient.Configuration.DefaultHeader.Add("Authorization", "Bearer " + accessToken);
            var clickAccountApi = new AccountsApi(docuSignClient);

            var clickwrapRequest = BuildClickwrapRequest(name, pdfFile);

            return clickAccountApi.CreateClickwrap(accountId, clickwrapRequest);
        }

        public static ClickwrapRequest BuildClickwrapRequest(string name, string pdfFile)
        {
            var clickwrapRequest = new ClickwrapRequest
            {
                DisplaySettings = new DisplaySettings()
                {
                    ConsentButtonText = "I Agree",
                    DisplayName = name,
                    Downloadable = true,
                    Format = "modal",
                    MustRead = true,
                    RequireAccept = true,
                    DocumentDisplay = "document",
                },
                Documents = new List<Document>()
                {
                    new Document()
                    {
                        DocumentBase64 = Convert.ToBase64String(System.IO.File.ReadAllBytes(pdfFile)),
                        DocumentName = "Terms of Service",
                        FileExtension = "pdf",
                        Order = 0,
                    },
                },
                Name = name,
                RequireReacceptance = true,
            };

            return clickwrapRequest;
        }
    }
}
