﻿using CrmNx.Xrm.Toolkit.Infrastructure;
using System;
using System.Linq;

namespace CrmNx.Xrm.Toolkit
{
    internal static class EntityReferenceExtensions
    {
        /// <summary>
        /// Convert EntityReference to WebApi NavigationUrl
        /// </summary>
        /// <param name="entityReference">EntityReference</param>
        /// <param name="organizationMetadata">Instance metadata store</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">When EntityReference is null</exception>
        public static string ToNavigationLink(this EntityReference entityReference,
            IWebApiMetadataService organizationMetadata)
        {
            if (entityReference is null)
            {
                throw new ArgumentNullException(nameof(entityReference));
            }

            var logicalName = entityReference.LogicalName;
            var collectionName = organizationMetadata.GetEntitySetName(logicalName);

            // If alternate keys not present
            if (entityReference.KeyAttributes.Any() != true)
            {
                return $"{collectionName}({entityReference.Id})";
            }

            // Else If alternate keys present
            var keys = entityReference.KeyAttributes
                .Select(kvp => $"{kvp.Key}='{kvp.Value}'");

            return $"{collectionName}({string.Join("&", keys)})";
        }
    }
}