﻿// ----------------------------------------------------------------------------------
//
// Copyright Microsoft Corporation
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// ----------------------------------------------------------------------------------


using System;
using System.Management.Automation;
using Microsoft.Azure.Commands.Websites.Models.WebApp;
using Microsoft.Azure.Management.WebSites.Models;
using System.Linq;

namespace Microsoft.Azure.Commands.WebApps.Cmdlets.WebApps
{
    /// <summary>
    /// this commandlet will let you get Azure servce plan metrics
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "AzureRMAppServicePlanMetrics"), OutputType(typeof(PSResourceMetric))]
    [CliCommandAlias("appservice;plan;metrics;ls")]
    public class GetAzureAppServicePlanMetricsCmdlet : AppServicePlanBaseCmdlet
    {
        [Parameter(Position = 2, Mandatory = true, HelpMessage = "Names of web app metrics")]
        [ValidateNotNullOrEmpty]
        public string[] Metrics { get; set; }

        [Parameter(Position = 3, Mandatory = true, HelpMessage = "Metrics start time")]
        [ValidateNotNullOrEmpty]
        public DateTime StartTime { get; set; }

        [Parameter(Position = 4, Mandatory = false, HelpMessage = "Metrics end time")]
        [ValidateNotNullOrEmpty]
        public DateTime? EndTime { get; set; }

        [Parameter(Position = 5, Mandatory = true, HelpMessage = "Metric granularity. Allowed values: [PT1M|PT1H|P1D]")]
        [ValidateSet("PT1M", "PT1H", "P1D", IgnoreCase = true)]
        public string Granularity { get; set; }

        [Parameter(Position = 6, Mandatory = false, HelpMessage = "Whether or not to include instance details")]
        [ValidateNotNullOrEmpty]
        public SwitchParameter InstanceDetails { get; set; }

        protected override void ProcessRecord()
        {
            base.ProcessRecord();
            WriteObject(WebsitesClient.GetAppServicePlanHistoricalUsageMetrics(ResourceGroupName, Name, Metrics, StartTime, EndTime, Granularity, InstanceDetails.IsPresent).Select(m => (PSResourceMetric)m), true);
        }
    }
}