﻿// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Diagnostics.Tracing;

namespace Microsoft.Practices.EnterpriseLibrary.SemanticLogging.InProc.Tests.TestObjects
{
    public sealed class MockEventSrcForXml : EventSource
    {
        public const int UsingKeywordsEventID = 1;
        public const int LogUsingMessageEventID = 2;
        public const int LogUsingMessageFormatEventID = 3;
        public const int LogMessageEventID = 4;
        public const int LogUsingMessageWithRelatedActivityIdEventID = 5;
        public const string LogMessage = @"Test Message /";

        public static readonly MockEventSrcForXml Logger = new MockEventSrcForXml();

        public class Keywords
        {
            public const EventKeywords Errors = (EventKeywords)0x0001;
            public const EventKeywords Trace = (EventKeywords)0x0002;
        }

        public class Tasks
        {
            public const EventTask Page = (EventTask)1;
            public const EventTask DBQuery = (EventTask)2;
        }

        [Event(UsingKeywordsEventID, Level = EventLevel.Informational, Opcode = EventOpcode.Start, Task = Tasks.DBQuery, Keywords = Keywords.Errors)]
        public void UsingKeywords(string message, long longArg)
        {
            if (this.IsEnabled(EventLevel.Informational, Keywords.Errors))
            {
                this.WriteEvent(UsingKeywordsEventID, message, longArg); 
            }
        }

        [Event(LogUsingMessageEventID, Level = EventLevel.Informational, Opcode = EventOpcode.Start, Task = Tasks.Page, Message = LogMessage)]
        public void LogUsingMessage(string message)
        {
            if (this.IsEnabled(EventLevel.Informational, Keywords.Errors))
            {
                this.WriteEvent(LogUsingMessageEventID, message); 
            }
        }

        [Event(LogUsingMessageFormatEventID, Level = EventLevel.Informational, Opcode = EventOpcode.Stop, Task = Tasks.DBQuery, Message = "{0}")]
        public void LogUsingMessageFormat(string message)
        {
            if (this.IsEnabled(EventLevel.Informational, Keywords.Errors))
            {
                this.WriteEvent(LogUsingMessageFormatEventID, message); 
            }
        }

        [Event(LogUsingMessageWithRelatedActivityIdEventID, Level = EventLevel.Informational, Opcode = EventOpcode.Stop, Task = Tasks.Page, Message = LogMessage)]
        internal void LogUsingMessageWithRelatedActivityId(string message, Guid relatedActivityId)
        {
            if (this.IsEnabled(EventLevel.Informational, Keywords.Errors))
            {
                this.WriteEventWithRelatedActivityId(LogUsingMessageWithRelatedActivityIdEventID, relatedActivityId, message);
            }
        }
    }
}
