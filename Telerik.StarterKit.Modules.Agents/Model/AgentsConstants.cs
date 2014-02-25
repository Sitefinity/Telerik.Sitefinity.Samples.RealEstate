using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Telerik.StarterKit.Modules.Agents.Model
{
    /// <summary>
    /// Various constants related to Agents
    /// </summary>
    public static class AgentsConstants
    {
        /// <summary>
        /// Security-Related constants related to Agents
        /// </summary>
        public static class Security
        {
            /// <summary>
            /// The main permission set name related to Agents
            /// </summary>
            public const string PermissionSetName = "Agents";

            /// <summary>
            /// View action name
            /// </summary>
            public const string View = "ViewAgents";

            /// <summary>
            /// Modify Agents action name
            /// </summary>
            public const string Modify = "ModifyAgents";

            /// <summary>
            /// Create Agents action name
            /// </summary>
            public const string Create = "CreateAgents";

            /// <summary>
            /// Delete Agents action name
            /// </summary>
            public const string Delete = "DeleteAgents";

            /// <summary>
            /// ChangeOwner Agents action name
            /// </summary>
            public const string ChangeOwner = "ChangeAgentsOwner";

            /// <summary>
            /// ChangePermissions Agents action name
            /// </summary>
            public const string ChangePermissions = "ChangeAgentsPermissions";
        }
    }
}
